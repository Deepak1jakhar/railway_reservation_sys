﻿using System.ComponentModel.DataAnnotations.Schema;

namespace trainmodels.Models.DTO
{
    public class PassengerDTO
    {
        public int PassengerId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public string Gender { get; set; }
        public string PhoneNumber { get; set; }

    }
}