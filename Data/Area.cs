namespace Restaurant.Data
{
    public class Area
    {
            public int Id { get; set; }
            public string Name { get; set; }

            public List<TableReference>? TableReferences { get; set; }
        
    }
}

