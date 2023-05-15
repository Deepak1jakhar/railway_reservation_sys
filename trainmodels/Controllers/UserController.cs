using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using trainmodels.Models;
using trainmodels.Models.DTO;
using trainmodels.Repository;

namespace trainmodels.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUser _u;
        public UserController(IUser u)
        {
            _u = u;
        }

        [HttpPost]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword(string email, string oldPassword, string newPassword)
        {
            var user = _u.GetUserByEmail(email);
            if (user == null)
            {
                return NotFound();
            }
            if (!Strings.Equals(user.Password, oldPassword))
            {
                return BadRequest("Current Password id incorrect!");
            }
            user.Password = newPassword;
            _u.UpdateUser(user);
            return Ok("Password changed successfully");

        }

        [HttpGet]
        [Route("GetUserById")]
        public async Task<IActionResult> GetUserByID(int userid)
        {
            var user = _u.GetUserById(userid);
            if(user!=null)
            {
                return Ok(user);
            }
            return BadRequest("User not found");
        }

        [HttpPost]
        [Route("CreateUser")]
        public async Task<IActionResult> CreateUser(UserDTO userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var usr = _u.GetUserByEmail(userDto.Email);
            if (usr!=null)
            {
                return BadRequest("User with this email already exist.");
            }
            var user = new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Age = userDto.Age,
                Email = userDto.Email,
                Password = userDto.Password
            };
            _u.AddUser(user);
            return Ok("User Created Successfully");

        }

        [HttpDelete]
        [Route("DeleteUserAccount")]
        public async Task<IActionResult> DeleteUserAccount(int userid)
        {
            bool deleted = _u.DeleteUser(userid);
            if(deleted)
            {
                return Ok("User Deleted Successfully");
            }
            return BadRequest("User Not Found to delete");
        }

        [HttpPut]
        [Route("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserDTO usurDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = new User
            {
                FirstName = usurDto.FirstName,
                LastName = usurDto.LastName,
                Age = usurDto.Age,
                Email = usurDto.Email,
                Password = usurDto.Password
            };
            _u.AddUser(user);
            return Ok(user);
        }

    }
}