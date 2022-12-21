namespace Restaurant.Data
{
    public class TableReference
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int SeatingCapacity { get; set; }
        public int? AreaId { get; set; }
    }
}
