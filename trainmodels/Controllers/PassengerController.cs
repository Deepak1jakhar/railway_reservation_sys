using Microsoft.AspNetCore.Mvc;
using System.Reflection;
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
            try
            {
                var p = new Passenger
                {
                    PassengerId = pDto.PassengerId,
                    LastName = pDto.LastName,
                    PhoneNumber = pDto.PhoneNumber,
                    FirstName = pDto.FirstName,
                    Gender = pDto.Gender,
                    Age = pDto.Age

                };

                bool added = _pr.AddPassenger(p);
                if (added)
                {
                    return Json(p);
                }
                return BadRequest("Not added either user is already available");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("GetPassengersByBooking/{id}")]
        public async Task<IActionResult> GetPassengersByBookingId([FromRoute] int bookingId)
        {
            try
            {
                var passengers = _pr.GetPassengersByBookingId(bookingId);
                return Ok(passengers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  
            }
        }

        [HttpPut]
        [Route("UpdatePassenger/{id}")]
        public async Task<IActionResult> UpdatePassenger(int id, PassengerDTO pDto)
        {
            try
            {
                var p = _pr.GetPassengerById(id);
                if(p== null)
                {
                    return BadRequest("Passenger not found");
                }
                p.PassengerId = pDto.PassengerId;
                p.LastName = pDto.LastName;
                p.PhoneNumber = pDto.PhoneNumber;
                p.FirstName = pDto.FirstName;
                p.Gender = pDto.Gender;
                p.Age = pDto.Age;
                _pr.UpdatePassenger(p);
                return Ok(pDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            } 
        }

        [HttpDelete]
        [Route("DeletePassenger")]
        public async Task<IActionResult> DeletePassenger(int id)
        {
            try
            {
                bool deleted = _pr.DeletePassenger(id);
                if (deleted == false)
                {
                    return BadRequest("Passenger not found to delete");
                }
                return Ok("Deleted");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
