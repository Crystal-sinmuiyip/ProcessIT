using Microsoft.EntityFrameworkCore;
namespace Restaurant.Data
{
    public static class SeedData
    {
        public static void Seed(this ModelBuilder model)
       
        {
            var status = new List<Status>
            {
                new Status { Id = 1, Name = "Pending" },
                new Status { Id = 2, Name = "Confirmed" },
                new Status { Id = 3, Name = "Seated" },
                new Status { Id = 4, Name = "Completed" },
                new Status { Id = 5, Name = "Cancelled"}

            };
                model.Entity<Status>().HasData(status);

                var sittingTypes = new List<SittingType>
            {
                new SittingType { Id = 1, Name = "Breakfast" },
                new SittingType { Id = 2, Name = "Lunch" },
                new SittingType { Id = 3, Name = "Dinner" },
                new SittingType { Id = 4, Name = "Special" }
            };
                model.Entity<SittingType>().HasData(sittingTypes);

            var area = new List<Area>
            {
                new Area { Id = 1, Name = "Main" },
                new Area { Id = 2, Name = "Outside" },
                new Area { Id = 3, Name = "Balcony" }

            };
                model.Entity<Area>().HasData(area);

            var TableNames = new List<TableReference>
            {
                new TableReference { Id = 1, Name = "M1", SeatingCapacity = 10 ,AreaId=1},
                new TableReference { Id = 2, Name = "M2", SeatingCapacity = 10 ,AreaId=1},

                new TableReference { Id = 3, Name = "M3", SeatingCapacity = 10 ,AreaId=1},
                new TableReference { Id = 4, Name = "M4",  SeatingCapacity = 10,AreaId=1 },
                new TableReference { Id = 5, Name = "M5",  SeatingCapacity = 10,AreaId=1 },
                new TableReference { Id = 6, Name = "M6",  SeatingCapacity = 10 ,AreaId=1},
                new TableReference { Id = 7, Name = "M7",  SeatingCapacity = 10 , AreaId = 1},
                new TableReference { Id = 8, Name = "M8",  SeatingCapacity = 10 , AreaId = 1},
                new TableReference { Id = 9, Name = "M9",  SeatingCapacity = 10 , AreaId = 1},
                new TableReference { Id = 10, Name = "M10",  SeatingCapacity = 10 , AreaId = 1},

                new TableReference { Id = 11, Name = "O1", SeatingCapacity = 4 ,AreaId=2 },
                new TableReference { Id = 12, Name = "O2",  SeatingCapacity = 4 ,AreaId=2},
                new TableReference { Id = 13, Name = "O3",  SeatingCapacity = 4 ,AreaId=2},
                new TableReference { Id = 14, Name = "O4",  SeatingCapacity = 4 ,AreaId=2},
                new TableReference { Id = 15, Name = "O5",  SeatingCapacity = 4 ,AreaId=2 },

                new TableReference { Id = 16, Name = "O6", SeatingCapacity = 6 ,AreaId=2},
                new TableReference { Id = 17, Name = "O7",  SeatingCapacity = 6 ,AreaId=2},
                new TableReference { Id = 18, Name = "O8", SeatingCapacity = 6 ,AreaId=2},
                new TableReference { Id = 19, Name = "O9",  SeatingCapacity = 6,AreaId=2 },
                new TableReference { Id = 20, Name = "O10",  SeatingCapacity = 6 ,AreaId=2},

                new TableReference { Id = 21, Name = "B1",  SeatingCapacity = 2 ,AreaId=3},
                new TableReference { Id = 22, Name = "B2", SeatingCapacity = 2,AreaId=3 },
                new TableReference { Id = 23, Name = "B3", SeatingCapacity = 2 ,AreaId=3},
                new TableReference { Id = 24, Name = "B4", SeatingCapacity = 2,AreaId=3 },
                new TableReference { Id = 25, Name = "B5",  SeatingCapacity = 2,AreaId=3 },

                new TableReference { Id = 26, Name = "B6", SeatingCapacity = 4 ,AreaId=3},
                new TableReference { Id = 27, Name = "B7",  SeatingCapacity = 4 ,AreaId=3},
                new TableReference { Id = 28, Name = "B8",  SeatingCapacity = 4,AreaId=3},

                new TableReference { Id = 29, Name = "B9", SeatingCapacity = 6 ,AreaId=3},
                new TableReference { Id = 30, Name = "B10",  SeatingCapacity = 6 ,AreaId=3},

            };
                model.Entity<TableReference>().HasData(TableNames);
                
        }
    }
}

