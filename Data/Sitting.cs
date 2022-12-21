using System.ComponentModel.DataAnnotations;

namespace Restaurant.Data
{
    public class Sitting
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Sitting Description")]
        [StringLength(30)]
        public string? Name { get; set; }

        [Range(1, 150)]
        [Display(Name = "Seating capacity of the restaurant")]
        public int Capacity { get; set; }
        [Range(1, 120)]
        [Display(Name = "Duration of reservation in minutes")]
        public int DurationReservation { get; set; }
        [Range(1, 180)]
        [Display(Name = "Duration of Sitting in minutes")]
        public int DurationSitting { get; set; }

        [Display(Name = "Start Time")]
        [DisplayFormat(DataFormatString = "{0: hh:mm tt}")]
        [DataType(DataType.Time)]
        public DateTime StartTime { get; set; }

        [Display(Name = "End Time")]
        [DisplayFormat(DataFormatString = "{0: hh:mm tt}")]
        [DataType(DataType.Time)]
        public DateTime EndTime { get; set; }

        [Display(Name = "Pulic Can make Reservations on website")]
        

        public bool PublicCanMakeReservation { get; set; }

        [Display(Name = "Start Date of this Sitting")]
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime DateAvailable { get; set; } = DateTime.Now;
        [Display(Name = "Sitting Type")]
        public string SittingTypeName { get; set; }

        //foreign key for SittingType lookup table
        public int SittingTypeId { get; set; }
        public SittingType SittingType { get; set; }
        public List<Reservation>? Reservations { get; set; }
       
    }
}
