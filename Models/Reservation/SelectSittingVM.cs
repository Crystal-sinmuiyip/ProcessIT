using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant.Data;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models.Reservation
{
    public class SelectSittingVM
   {
        public int Id { get; set; }

        public int SittingId { get; set; }
        public string? Name { get; set; }
        public string SittingTypeName { get; set; }
        public int Capacity { get; set; }
        
        public int CurrentAvailableCapacity { get; set; }
        [DisplayFormat(DataFormatString = "{0: dd MMM hh:mm tt}")]
       // [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        

        public bool PublicCanMakeReservation { get; set; }
       
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime DateAvailable { get; set; }

       


        //foreign key for SittingType lookup table
        public int SittingTypeId { get; set; }
        //public Restaurant.Data.SittingType SittingType { get; set; }
        public SittingType SittingType { get; set; }


        public List<Restaurant.Data.Reservation>? Reservations { get; set; }
        //public List<Reservations>? Reservations { get; set; }

    }
}
