using trainmodels.Models;

namespace trainmodels.Repository
{
    public interface IBooking
    {
        Booking GetBookingById(int id);
        List<Booking> GetBookingsByUserId(int userId);
        void AddBooking(Booking booking);
        void UpdateBooking(Booking booking);
        void CancelBooking(int id);
    }
}
