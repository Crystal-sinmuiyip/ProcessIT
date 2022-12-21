using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant.Data;
namespace Restaurant.Areas.Admin.Models.Reservation
{
    public class AddSelectedTableVM
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public int SelectedTableId { get; set; }
        
    }
}
