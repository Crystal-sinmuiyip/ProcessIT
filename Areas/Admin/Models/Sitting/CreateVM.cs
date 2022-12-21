using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant.Data;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Areas.Admin.Models.Sitting
    {
        public class CreateVM
        {

            public int Id { get; set; }
            [Required]
            [Display(Name = "Sitting Description")]
            [StringLength(30)]
            public string? Name { get; set; }

            [Range(1, 150)]
            [Display(Name = "Seating capacity of the restaurant")]
            public int Capacity { get; set; }
        
            //public string? Description { get; set; }
            [Range(1, 120)]
            [Display(Name = "Duration of each customers reservation in minutes")]
            public int DurationReservation { get; set; }

            [Range(1, 180)]
            [Display(Name = "Duration of this sitting in minutes")]
            public int DurationSitting { get; set; }

        [Display(Name = "Start Time")]
        //[DisplayFormat(DataFormatString = "{0: h:mm tt}")]
        // [DisplayFormat(ApplyFormatInEditMode= true, DataFormatString = "{0: h:mm tt}")]
        [DataType(DataType.Time)]
        //NO _ CANNOT USE THIS public TimeOnly StartTime { get; set; } = new TimeOnly(6, 00, 00);
            public DateTime StartTime { get; set; } = DateTime.Today.AddHours(6);
            // public DateTime StartTime { get; set; } = DateTime.Now.Date + TimeSpan.FromDays(1);

        [Range(1, 180)]
            [Display(Name = "How long in days will this type of sitting be available from the start date")]
            public int DurationNumberOfDays { get; set; }

            [Display(Name = "Will this sitting be available in the main area ?")]
            public bool AreaMain { get; set; }

            [Display(Name = "Will this sitting be available in the outside area ?")]
            public bool AreaOutside { get; set; }

            [Display(Name = "Will this sitting be available in the balcony area ?")]
            public bool AreaBalcony { get; set; }

            //[Range(typeof(DateTime), "1/2/2004", "3/4/2004",
            //ErrorMessage = "Value for {0} must be between {1} and {2}")]
            //[DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
            // need this in reservation view DateTime: @Model.dateTime.ToString("MM/dd/yyyy hh:mm tt")
            //  need this for reservation  [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
            //[Range(1, 24)]
            //   public string? DateRange { get; set; }

            [Display(Name = "Public can make Reservations on website")]
            public bool PublicCanMakeReservation { get; set; }

            // [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
            [Display(Name = "Start date of this Sitting")]
            [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}")]
            [DataType(DataType.Date)]
            public DateTime DateAvailable { get; set; } = DateTime.Now;

            [Display(Name = "Sitting to be scheduled on Saturday? ")]
            public bool ScheduleOnSaturday { get; set; }

            [Display(Name = "Sitting to be scheduled on Sunday? ")]
            public bool ScheduleOnSunday { get; set; }

            [Display(Name = "Sitting to be scheduled on Monday? ")]
            public bool ScheduleOnMonday { get; set; }

            [Display(Name = "Sitting to be scheduled on Tuesday? ")]
            public bool ScheduleOnTuesday { get; set; }

            [Display(Name = "Sitting to be scheduled on Wednesday? ")]
            public bool ScheduleOnWednesday { get; set; }

            [Display(Name = "Sitting to be scheduled on Thursday? ")]
            public bool ScheduleOnThursday { get; set; }

            [Display(Name = "Sitting to be scheduled on Friday? ")]
            public bool ScheduleOnFriday { get; set; }

            [Display(Name = "Sitting Type")]
            //foreign key for SittingType lookup table
            public int SittingTypeId { get; set; }
            public SelectList? SittingTypes { get; set; }


        }
    }


