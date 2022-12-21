namespace Restaurant.Data
{
    public class TableForSitting
    {
        public int Id { get; set; }
        public int SittingId { get; set; }
        public Sitting Sitting { get; set; }
        public int TableReferenceId { get; set; }
        public List<ReservationTable>? ListReservationTable { get; set; }
        public string TableName { get; set; }
        public TableReference? TableReference { get; set; }

        // public List<TableReference>? TableReferenceList { get; set; }
    }
}
