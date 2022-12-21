using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant.Data;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Areas.Admin.Models.Reservation
{
    public class Test
    {
       
            public int Id { get; set; }
            public int ReservationId { get; set; }
            public string? Email { get; internal set; }
            public string? Phone { get; internal set; }
            public int? NumberOfPeople { get; set; }
            public string? ReservationName { get; set; }

            [Display(Name = "Has the customer confirmed the reservation ? ")]
            public bool? StatusChange { get; set; }
         
        
    }

}
