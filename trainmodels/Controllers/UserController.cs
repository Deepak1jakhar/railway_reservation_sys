using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Text.RegularExpressions;
using System.Text;
using trainmodels.Data;
using trainmodels.Models;
using trainmodels.Models.DTO;
using trainmodels.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using trainmodels.Helper;

namespace trainmodels.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUser _u;
        private readonly RailContext _railcontext;
        public UserController(IUser u,RailContext railContext)
        {
            _u = u;
            _railcontext = railContext;
        }

        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest userobj)
        {
            if (userobj == null)
            {
                return BadRequest();
            }
            var user = await _railcontext.users.FirstOrDefaultAsync(u => u.UserName == userobj.UserName);
            if (user == null)
            {
                return NotFound();
            }
            if (!PasswordHasher.VerifyPassword(userobj.Password, user.Password))
            {
                return BadRequest(new { Message = "Password is Incorrect" });
            }
            user.Token = CreateJwt(user);
            _railcontext.SaveChangesAsync();
        
            return Ok(
                new
                {
                    Token = user.Token,
                    Message = "Login Succes!"
                });
        }


        [HttpGet]
        [Route("GetUserById")]
        public async Task<IActionResult> GetUserByID(int userid)
        {
            try
            {
                var user = _u.GetUserById(userid);
                if (user != null)
                {
                    return Ok(user);
                }
                return BadRequest("User not found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("CreateUser")]
        public async Task<IActionResult> CreateUser(UserDTO userobj)
        {
            try
            {
                if (userobj == null)
                {
                    return BadRequest();
                }
                //check username
                if (await CheckUserNameExistAsync(userobj.UserName))
                {
                    return BadRequest(new { Message = "UserName Alredy Exist!" });
                }
                if (await CheckEmailExistAsync(userobj.Email))
                {
                    return BadRequest(new { Message = "Email Already Exist!" });
                }

                var pass = CheckPasswordStrength(userobj.Password);
                if (!string.IsNullOrEmpty(pass))
                {
                    return BadRequest(new { Message = pass });
                }
                var role = "User";

                //check email
                var user = new User
                {
                    FirstName = userobj.FirstName,
                    LastName = userobj.LastName,
                    Age = userobj.Age,
                    Token = "",
                    UserName= userobj.UserName,
                    Email = userobj.Email,
                    Password = PasswordHasher.HashPassword(userobj.Password),
                    Role = role
                };
            
                _u.AddUser(user);
                return Ok(new { Message = "User Registered" });
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            

        }

        [HttpDelete]
        [Route("DeleteUserAccount")]
        public async Task<IActionResult> DeleteUserAccount(int userid)
        {
            try
            {
                bool deleted = _u.DeleteUser(userid);
                if (deleted)
                {
                    return Ok(new { Message = "User Deleted Successfully" });
                }
                return BadRequest("User Not Found to delete");
            }
            catch(Exception ex)
            {
                    return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(int id, User userobj)
        {
            try
            {
                var user = _u.GetUserById(id);
                if (user==null)
                {
                    return BadRequest("User not found");
                }
                user.FirstName = userobj.FirstName;
                user.LastName = userobj.LastName;
                user.Age = userobj.Age;
                user.UserName  = userobj.UserName;
                user.Email = userobj.Email;
                _u.UpdateUser(user);
                return Ok(new {Message="User Updated"});
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        private async Task<bool> CheckUserNameExistAsync(string username)
        {
            return await _railcontext.users.AnyAsync(u => u.UserName == username);
        }

        private async Task<bool> CheckEmailExistAsync(string email)
        {
            return await _railcontext.users.AnyAsync(u => u.Email == email);
        }

        private string CheckPasswordStrength(string password)
        {
            StringBuilder sb = new StringBuilder();
            if (password.Length < 8)
            {
                sb.Append("Minimum password length should be 8" + Environment.NewLine);
            }
            if (!(Regex.IsMatch(password, "[a-z]") && Regex.IsMatch(password, "[A-Z]") && Regex.IsMatch(password, "[0-9]")))
            {
                sb.Append("Password should be Alphanumeric" + Environment.NewLine);
            }
            if (!Regex.IsMatch(password, "[<,>,@,!,#,$,%,^,&,*,(,),_,-,+,=,\\],\\[,{,},:,;,',|,/,~]"))
            {
                sb.Append("Password should contain special chars!");
            }
            return sb.ToString();
        }

        private string CreateJwt(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("veryverysecret.....");
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.Name,$"{user.FirstName}{user.LastName}")
            });

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }


    }




}