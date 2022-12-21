using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant.Data;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Areas.Admin.Models.Reservation
{
    public class AllocateReservationToTableVM
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public string? Email { get; internal set; }
        public string? Phone { get; internal set; }
        public int? NumberOfPeople { get; set; }
        public string? ReservationName { get; set; }

        //[DisplayFormat(DataFormatString = "{0: d MMM h:mm tt}")]
        public DateTime? ReservationStartTime { get; set; }
        public string? ReservationComments { get; set; }
        public List<ReservationTable>? ReservationTables { get; set; }
        public List<TableForSitting>? SittingTables { get; set; }
        public List<TableForSitting>? AlreadyAllocatedToThisResSittingTables { get; set; }
        public string? ReservationPhone { get; internal set; }
        public string? ReservationEmail { get; internal set; }
        public int? ReservationNumberOfPeople { get; internal set; }
        public int[]? SelectedTableIds { get; set; }
        public MultiSelectList? TableForSittings { get; set; }

        public int StatusId { get; set; }

        [BindProperty]
        [Display(Name = "Has the customer confirmed the reservation ? ")]
        public Boolean StatusChange { get; set; } 
        public void OnGet()
        {
            this.TableForSittings = new SelectList(this.TableForSittings, "Id", "TableReferenceId");

        }
    }
}
