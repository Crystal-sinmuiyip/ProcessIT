namespace Restaurant.Data
{
    public class ReservationTable
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public Reservation? Reservation { get; set; }
        public int? TableForSittingId { get; set; }
        public TableForSitting? TableForSitting { get; set; }
        public int? TableReferenceId { get; set; }
        public TableReference? TableReference { get; set; }

        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
    }
}
