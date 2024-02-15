﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagementApp.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Can You Please Provide the Title of Your Book")]
        public string Title { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; } = "";
        [ForeignKey("UserId")]

        public string Location { get; set; }
        public int maxAttendees { get; set; }
        public DateTime Date { get; set; }
        public int registrationFee { get; set; }
        
      
    }
}

