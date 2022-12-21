//using Microsoft.AspNetCore.Mvc.Rendering;
//using Restaurant.Data;
//namespace Restaurant.Areas.Admin.Models.Reservation
//{
//    public class AllocateReservationToTableVM
//    {
//        public int Id { get; set; }
//        public int ReservationId { get; set; }
//        public string? Email { get; set; }
//        public string? Phone { get; set; }
//        public int? NumberOfPeople { get; set; }
//        public string? ReservationName { get; set; }
//        public DateTime? ReservationStartTime { get; set; }
//        public string? ReservationComments { get; set; }
//        public List<ReservationTable>? ReservationTables { get; set; }
//        public string? ReservationPhone { get; internal set; }
//        public string? ReservationEmail { get; internal set; }
//        public int? ReservationNumberOfPeople { get; internal set; }
//        public int[]? SelectedTableIds { get; set; }
//        public MultiSelectList? TableForSittings { get; set; }
//        public void OnGet()
//        {
//            this.TableForSittings = new SelectList(this.TableForSittings, "Id", "TableReferenceId");

//        }
//    }
//}
