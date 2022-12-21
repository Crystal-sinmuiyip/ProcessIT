using System.ComponentModel.DataAnnotations;

namespace Restaurant.Data
{
    public class Reservation
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Phone{ get; set; }
        public string? Email { get; set; }

        [Display(Name = "Requested Date")]
        [DisplayFormat(DataFormatString = "{0: d MMM h:mm tt}")]
        public DateTime? RequestedDate { get; set; }

        [Display(Name = "Start Time")]
        [DisplayFormat(DataFormatString = "{0: d MMM h:mm tt}")]
        public DateTime? StartTime { get; set; }

        [Display(Name = "End Time")]
        [DisplayFormat(DataFormatString = "{0: d MMM h:mm tt}")]
        public DateTime? EndTime { get; set; }
        public int? NumberOfPeople { get; set; }
        public Boolean? Birthday { get; set; }
        public Boolean? Anniversary { get; set; }
        public Boolean? Pram { get; set; }
        public Boolean? HighChair { get; set; }
        public Boolean? DisabledAccess { get; set; }
        public Boolean? Allergy { get; set; }
        public string? Notes { get; set; }
        public Boolean? Condition { get; set; }
        public string? Comments { get; set; }
        ////foreign key for Sitting table
        public int SittingId { get; set; }
        //public Sitting? Sitting { get; set; }
        ////foreign key for SittingType lookup table
        public int SittingTypeId { get; set; }
        public SittingType? SittingType { get; set; }
      
        //foreign key for Status 
        public int StatusId { get; set; }
        public Status? Status { get; set; }

        public List<ReservationTable>? ReservationTables { get; set; }
    }
}
