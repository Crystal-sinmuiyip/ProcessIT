using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant.Data;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models.Reservation
{
    public class CreateVM
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public DateTime RequestedDate { get; set; }

        public SelectList? Times { get; set; }
        public DateTime? Time { get; set; }

        [DisplayFormat(DataFormatString = "{0: hh:mm tt}")]
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? NumberOfPeople { get; set; }
        public Boolean Birthday { get; set; }
        public Boolean Anniversary { get; set; }
        public Boolean Pram { get; set; }
        public Boolean HighChair { get; set; }
        public Boolean DisabledAccess { get; set; }
        public Boolean Allergy { get; set; }
        public string? Notes { get; set; }
        public Boolean Condition { get; set; }
        public string? Comments { get; set; }
        ////foreign key for Sitting table
        public int SittingId { get; set; }
        //public Sitting? Sitting { get; set; }
        //No need to display Sitting Description - just handy to have in controller
        public string? SittingDescription { get; set; }
        ////foreign key for SittingType lookup table
        public int SittingTypeId { get; set; }
        //public SittingType? SittingType { get; set; }

        //foreign key for Status 
        public int StatusId { get; set; }

        public List<ReservationTable>? ReservationTables { get; set; }
    }
}

