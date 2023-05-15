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
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
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


        [HttpGet]
        [Route("GetBookingsbyUserId/{userid}")]
        public async Task<IActionResult> GetBookingByUserIdAsync(int userid)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           var getb =  _bookingRepository.GetBookingsByUserId(userid);
           if(getb == null)
            {
                return NotFound("No Bookings were done!");
            }
           var bookingslist = new List<BookingDTO>();
            foreach(var b in getb)
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

        [HttpDelete]
        [Route("Cancebooking/{id}")]
        public async Task<IActionResult> CancelBookingAsync(int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var getb = _bookingRepository.GetBookingById(id);
            if(getb == null)
            {
                return NotFound("Not booking done");
            }
            _bookingRepository.CancelBooking(id);
            return Ok();
        }


        [HttpPut]
        [Route("Updatebookingdetails/{id}")]
        public async Task<IActionResult> UpdateBookingAsync(int id, BookingDTO bookingDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var b = new Booking
            {
                BookingId = id,
                Destination = bookingDto.Destination,
                DepartureTime = bookingDto.DepartureTime,
                Source = bookingDto.Source,
                UserId = bookingDto.UserId,
                TrainId = bookingDto.TrainId,
                Status= bookingDto.Status,
                NumberOfTickets = bookingDto.NumberOfTickets,
                TotalFare = bookingDto.TotalFare

            };
            _bookingRepository.UpdateBooking(b);
            return Ok(b);
        }
        
    }
}
