using Microsoft.EntityFrameworkCore;
using trainmodels.Data;
using trainmodels.Models;
using trainmodels.Repository;

namespace trainmodels.Services.Repository
{
    public class BookingRepository : IBooking
    {
        private readonly RailContext _rc;


        public BookingRepository(RailContext rc)
        {
            _rc = rc;
        }
        public void AddBooking(Booking booking)
        {
            _rc.bookings.Add(booking);
            _rc.SaveChanges();
            
        }

        public void CancelBooking(int id)
        {
            var book = _rc.bookings.FirstOrDefault(x=>x.BookingId == id);
            _rc.bookings.Remove(book);
            _rc.SaveChanges();
        }

        public Booking GetBookingById(int id)
        {
            var book = _rc.bookings.FirstOrDefault(x=>x.BookingId==id);
            return book;
        }

        public List<Booking> GetBookingsByUserId(int userId)
        {
            var book = _rc.bookings.Where(b => b.UserId == userId).ToList();
            return book;
        }

        public void  UpdateBooking(Booking booking)
        {
            _rc.bookings.Update(booking);
            _rc.SaveChanges();
        }
    }
}
