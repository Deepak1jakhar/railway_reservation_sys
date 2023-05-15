using Microsoft.AspNetCore.Mvc;
using trainmodels.Models;
using trainmodels.Models.DTO;
using trainmodels.Repository;

namespace trainmodels.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PassengerController : Controller
    {
        private readonly IPassenger _pr;
        public PassengerController(IPassenger pr)
        {
            _pr= pr;
        }

        [HttpPost]
        [Route("AddPassenger")]
        public async Task<IActionResult> AddPassengerAsync(PassengerDTO pDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var p = new Passenger
            {
                PassengerId = pDto.PassengerId,
                LastName = pDto.LastName,
                PhoneNumber = pDto.PhoneNumber,
                FirstName = pDto.FirstName,
                Gender = pDto.Gender,
                Age = pDto.Age,
                UserId = pDto.UserId

            };
            
            bool added = _pr.AddPassenger(p);
            if(added)
            {
                return Json(p);
            }
            return BadRequest("Not added either user is already available");
        }

        [HttpGet]
        [Route("GetPassengersbyUserId")]
        public async Task<IActionResult> GetPassengers(int userid)
        {
            var passe = _pr.GetPassengersByUserId(userid);
            if(passe!=null)
            {
                return Ok(passe);
            }
            return BadRequest("No Passenger found");
        }

        [HttpPut]
        [Route("UpdatePassenger/{id}")]
        public async Task<IActionResult> UpdatePassenger(int id, PassengerDTO pDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var p = new Passenger
            {
                PassengerId = pDto.PassengerId,
                LastName = pDto.LastName,
                PhoneNumber = pDto.PhoneNumber,
                FirstName = pDto.FirstName,
                Gender = pDto.Gender,
                Age = pDto.Age,
                UserId = pDto.UserId
            };
            _pr.UpdatePassenger(p);
            return Ok(p);
        }

        [HttpDelete]
        [Route("DeletePassenger")]
        public async Task<IActionResult> DeletePassenger(int id)
        {
            bool deleted = _pr.DeletePassenger(id);
            if(deleted==false)
            {
                return BadRequest("Passenger not found to delete");
            }
            return Ok("Deleted");
        }
    }
}
