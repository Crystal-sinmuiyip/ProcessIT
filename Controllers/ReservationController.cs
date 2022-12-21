using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Restaurant.Data;
using Restaurant.Models.Reservation;
using Microsoft.AspNetCore.Identity;
using Restaurant.Areas.Admin.Models;

namespace Restaurant.Controllers
{
    public class ReservationController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;


        //public ReservationController(ApplicationDbContext context)
        public ReservationController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;

            _context = context;
        }

        public async Task<IActionResult> ThankYou()
        {

          return View();
        }
   
        public async Task<IActionResult> Booking()
        {

            var now = DateTime.Now;
          
         //   var user = User
          var user = await _userManager.GetUserAsync(User);
            var usersReservations = _context.Reservations
                            .Include(s => s.SittingType )
                            .Include(s => s.Status)
                            .Where (s => s.Email == user.Email)
                            .OrderByDescending(s => s.StartTime)
                            .ToArray();
           // where (p=> p.AspeNetUserId == user.Id)

            return View(usersReservations);
        }
        // GET: Reservation/Select a sitting then then make a reservation for
        public async Task<IActionResult> SelectSitting()
        {
            var now = DateTime.Now;
            var end = DateTime.Now.Date.AddDays(55);

            var sittings =  _context.Sittings
                            .Include(s => s.SittingType)
                            .Where(s => s.DateAvailable > now && s.DateAvailable < end)
                            .ToArray();
            var myList = new List<Models.Reservation.SelectSittingVM>();
            foreach (var item in sittings)
            {
                // do not allow public to book in special reservation types ie private events
                if (item.PublicCanMakeReservation)
                {
                    var myOne = new Models.Reservation.SelectSittingVM();
                    myOne.SittingId = item.Id;
                    myOne.Name = item.Name;
                    myOne.SittingTypeName = item.SittingType.Name;
                    myOne.SittingTypeId = item.SittingTypeId;
                    myOne.DateAvailable = item.DateAvailable;
                    myOne.StartTime = item.StartTime;
                    myOne.EndTime = item.EndTime;
                    myOne.Capacity = item.Capacity;
                    myOne.CurrentAvailableCapacity = item.Capacity * 200;
                    myList.Add(myOne);
                }
            }
           
            return View(myList);
        }
        // GET: Reservation/Create
        [HttpGet]
        public async Task<IActionResult>  Create(int? id)
        {
            if (id == null || _context.Reservations == null)
            {
                return NotFound();
            }
            var sitting = await _context.Sittings.FirstOrDefaultAsync(s => s.Id == id);
            var times = new List<DateTime>();
            var index = sitting.StartTime;
            while (index < sitting.EndTime)
            {
                times.Add(index);
                index = index.AddMinutes(15);
            }
            var m = new CreateVM
            {
                SittingId = sitting.Id,
                SittingTypeId = sitting.SittingTypeId,
                SittingDescription=$"{sitting.Name}",
                Times = new SelectList(times),
                Time = sitting.StartTime,
                RequestedDate = sitting.DateAvailable
            };
            return View(m);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateVM res)
        {
    
                var sitting = await _context.Sittings.FirstOrDefaultAsync(s => s.Id == res.SittingId);
               
                var SelectedDateTime = res.StartTime;

                var newRes = new Reservation
                {
                    Name = res.Name,
                    Phone = res.Phone,
                    Email = res.Email,
                    RequestedDate = DateTime.Now,
                    StartTime = SelectedDateTime,
                    EndTime = SelectedDateTime.AddMinutes(sitting.DurationReservation),
                    NumberOfPeople = res.NumberOfPeople,
                    Birthday = res.Birthday,
                    Anniversary = res.Anniversary,
                    Pram =res.Pram,
                    HighChair = res.HighChair,
                    DisabledAccess = res.DisabledAccess,
                    Allergy = res.Allergy,
                    Notes = res.Notes,
                    Comments = res.Comments,
                    SittingId = res.SittingId,
                    SittingTypeId = sitting.SittingTypeId,
                    StatusId= 1  //pending
                   
                };

                _context.Add(newRes);
                await _context.SaveChangesAsync();
                return View(nameof(ThankYou));
                          
        }

            
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Reservations == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Reservations'  is null.");
            }
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
          return _context.Reservations.Any(e => e.Id == id);
        }
      
          
       

        public IActionResult FindAllBookings()
           
        {
            // Not complex enough - customer can book old sittings
            //    var events = _context.Sittings.Select(e => new
            //    {
            //        id = e.Id,
            //        title = e.Name,
            //        start = e.StartTime,
            //        end = e.EndTime

            //    }).ToList();

            //    return new JsonResult(events);
            var now = DateTime.Now;
            var end = DateTime.Now.Date.AddDays(55);

            var sittings = _context.Sittings
                            .Include(s => s.SittingType)
                            .Where(s => s.DateAvailable > now && s.DateAvailable < end &&
                            s.PublicCanMakeReservation)
                            .Select(e => new
                            {
                                id = e.Id,
                                title = e.Name,
                                start = e.StartTime,
                                end = e.EndTime

                            }).ToList();
            return new JsonResult(sittings);

        }
    }
}

