using Microsoft.AspNetCore.Mvc;
using trainmodels.Models;
using trainmodels.Models.DTO;
using trainmodels.Repository;

namespace trainmodels.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : Controller
    {
        private readonly IBooking _bookingRepository;
        
        public BookingController(IBooking bookingrepository)
        {
            _bookingRepository = bookingrepository;
        }

        [HttpPost]
        [Route("Create-Booking")]
        public async Task<IActionResult> CreateBookingAsync(BookingDTO bookingDto)
        {
            try
            {
                var b = new Booking
                {
                    BookingId = bookingDto.BookingId,
                    Destination = bookingDto.Destination,
                    DepartureTime = bookingDto.DepartureTime,
                    Source = bookingDto.Source,
                    UserId = bookingDto.UserId,
                    TrainId = bookingDto.TrainId,
                    Status = bookingDto.Status,
                    NumberOfTickets = bookingDto.NumberOfTickets,
                    TotalFare = bookingDto.TotalFare
                };

                _bookingRepository.AddBooking(b);
                return View(b);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the booking.");
            }
        }



        [HttpGet]
        [Route("GetBookingsbyUserId/{userid}")]
        public async Task<IActionResult> GetBookingByUserIdAsync(int userid)
        {
            try
            {
                var getb = _bookingRepository.GetBookingsByUserId(userid);
                var bookingslist = new List<BookingDTO>();
                foreach (var b in getb)
                {
                    var bobj = new BookingDTO
                    {
                        BookingId = b.BookingId,
                        Destination = b.Destination,
                        DepartureTime = b.DepartureTime,
                        Source = b.Source,
                        UserId = b.UserId,
                        TrainId = b.TrainId,
                        Status = b.Status,
                        NumberOfTickets = b.NumberOfTickets,
                        TotalFare = b.TotalFare
                    };
                    bookingslist.Add(bobj);
                }
                return Ok(bookingslist);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("Cancebooking/{id}")]
        public async Task<IActionResult> CancelBookingAsync(int id)
        {
            try
            {
                var getb = _bookingRepository.GetBookingById(id);
                if (getb == null)
                {
                    return NotFound("Not booking done");
                }
                _bookingRepository.CancelBooking(id);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }


        [HttpPut]
        [Route("Updatebookingdetails/{id}")]
        public async Task<IActionResult> UpdateBookingAsync(int id, BookingDTO bookingDto)
        {
            try
            {
                var b = _bookingRepository.GetBookingById(id);
                if (b == null)
                {
                    return NotFound("Booking not found for update");
                }

                b.BookingId = id;
                b.Destination = bookingDto.Destination;
                b.DepartureTime = bookingDto.DepartureTime;
                b.Source = bookingDto.Source;
                b.UserId = bookingDto.UserId;
                b.TrainId = bookingDto.TrainId;
                b.Status = bookingDto.Status;
                b.NumberOfTickets = bookingDto.NumberOfTickets;
                b.TotalFare = bookingDto.TotalFare;
                _bookingRepository.UpdateBooking(b);
                return Ok(bookingDto);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        
    }
}
