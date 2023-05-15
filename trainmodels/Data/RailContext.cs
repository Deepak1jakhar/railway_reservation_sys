using Microsoft.EntityFrameworkCore;
using trainmodels.Models;

namespace trainmodels.Data
{
    public class RailContext : DbContext
    {
        public RailContext(DbContextOptions<RailContext> options) : base(options) 
        {

        }

        public DbSet<Train> trains { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Passenger> passengers { get; set; }
        public DbSet<Booking> bookings { get; set; }
        public DbSet<Admin> admins { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Train>().HasData(
                new Train {TrainId = 1, TrainName = "Express", Source = "Mumbai", Destination = "Pune", DepartureTime = new DateTime(2023, 5, 1, 10, 0, 0), ArrivalTime = new DateTime(2023, 5, 1, 14, 0, 0), TotalSeats = 100, AvailableSeats = 80, Fare = 340 },
                new Train {TrainId = 2, TrainName = "Local", Source = "Delhi", Destination = "Jalandhar", DepartureTime = new DateTime(2023, 5, 1, 12, 0, 0), ArrivalTime = new DateTime(2023, 5, 1, 17, 0, 0), TotalSeats = 150, AvailableSeats = 100,Fare= 450 },
                new Train {TrainId = 3, TrainName = "Express", Source = "Bangaluru", Destination = "Kolkata", DepartureTime = new DateTime(2023, 5, 1, 8, 0, 0), ArrivalTime = new DateTime(2023, 5, 1, 18, 0, 0), TotalSeats = 200, AvailableSeats = 150, Fare = 400 }
                );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId= 1,
                    FirstName = "Deepak",
                    LastName = "Jakhar",
                    Age = 22,
                    Email = "Deepak@gmail.com",
                    Password = "Deepak@123"
                }
                ) ;
            modelBuilder.Entity<Admin>().HasData(
                new Admin
                {
                    Id = 1,
                    Name = "admin",
                    Age = 25,
                    Gender = "Male",
                    Email = "Admin@gmail.com",
                    Password = "Admin@123"
                }) ;
        }
        

        
    }
}
