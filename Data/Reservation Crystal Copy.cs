//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace ProcessITRestaurantResverationSystem.Data
//{
//    public class Reservation
//    {
//        public int Id { get; set; }
//        [StringLength(50)]
//        public string Name { get; set; }
//        [StringLength(50)]
//        public string Email { get; set; }
//        [StringLength(20)]
//        public string Phone { get; set; }
//        public DateTime BookingDateTime { get; set; }
//        [StringLength(50)]
//        public string BookingDate => BookingDateTime.ToString("dd/MM/yyyy");
//        public string BookingTime => BookingDateTime.ToString("hh:mm tt");        
//        public int NumberOfPeople { get; set; }
//        [StringLength(10)]

//        public Boolean Birthday { get; set; }
//        public Boolean Anniversity { get; set; }
//        public Boolean Pram { get; set; }
//        public Boolean HighChair { get; set; }
//        public Boolean DisabledAccess { get; set; }
//        public Boolean Allergy  { get; set; }
//        public string? Notes { get; set; }
//        public Boolean Condition { get; set; }


//    }
//}
