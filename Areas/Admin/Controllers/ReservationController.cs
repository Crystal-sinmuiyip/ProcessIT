using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Restaurant.Areas.Admin.Models.Reservation;
using Restaurant.Data;
using Restaurant.Areas.Admin.Models;

using Restaurant.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Authorization;

namespace Restaurant.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, Staff")]
    public class ReservationController : AdministrationAreaController
    {
        public ReservationController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) : base(context, userManager, roleManager)
        {
        }
       
        public async Task<IActionResult> ListReservations(int? id)
        {
            
            if (_context.Reservations == null)
            {
                return NotFound();
            }
           // var now = DateTime.Now.AddHours(-48);
            var now = DateTime.Now;
            var res = await _context.Reservations
                   .Where(r => r.StartTime >= now)
                   .OrderBy(r => r.StartTime)
            //         .OrderBy(r => r.RequestedDate)
                   .ToListAsync();
            if (res == null)
            {
                return NotFound();
            }

            return View(res);
        }
        public async Task<IActionResult> GiveReservationTable(int? id)
        {

            if (_context.Reservations == null)
            {
                return NotFound();
            }

            var res = _context.Reservations
                   .FirstOrDefault(r => r.Id == id);
            if (res == null)
            {
                return NotFound();
            }

            //BASIC - list of tables available for the sitting
            var availableTablesList = _context.TableForSittings
              // .Include(t => t.TableReference)
              .Where(t => t.SittingId == res.SittingId)
              .ToList();

            var availableTablesList1 = _context.TableForSittings
               .Where(t => t.SittingId == res.SittingId)
               .Select(t => t.Id)
               .ToList();

            var tablesAlreadyAllocatedToReservation = await _context.ReservationTables
                //      .Where (r=>res.StartTime > r.EndDateTime || res.EndTime < r.StartDateTime)
                //.Where(r => r.StartDateTime = res.StartTime )
                .Where(r => availableTablesList1.Contains(r.Id))
                .ToListAsync();

            var existingResTablesForSitting = availableTablesList
             .AsEnumerable()
             .GroupJoin(_context.ReservationTables,
                         ts => ts.Id,
                         rt => rt.TableForSittingId,
                         (ts, rt) => new { ts, rt })
             .ToList();

            // this is the list that is then attached to the VM for cshtml
            List<TableForSitting> complexAvailableTables = new();
            List<TableForSitting> AlreadyAllocatedToResTables = new();

            var workResStartDateTime = res.StartTime;
            var workResEndDateTime = res.EndTime;
            int i = 0;
            while (i < existingResTablesForSitting.Count)
            {
                //var thisTableIsAvailable = true;

                var loopList = existingResTablesForSitting[i].rt;
                //   bool isEmpty = loopList.Any();
                if (loopList.Any())
                {
                   // var thisTimeSlotIsAvailable = true;
                    // Now check the start datetime 
                    var workRT = loopList;
                    var workListRT = workRT.ToList();
                    int j = 0;
                    while (j < workListRT.Count)
                    {
                        var workStartDateTime = workListRT[j].StartDateTime;
                        var workEndDateTime = workListRT[j].EndDateTime;
                        if ( //(workResStartDateTime < workStartDateTime &&
                            (workResEndDateTime <= workStartDateTime) |
                            (workResStartDateTime >= workEndDateTime))
                        //so the time slot for this reservation DOES NOT match this existing one
                        {
                           // thisTimeSlotIsAvailable = true;
                            // make an item to add to VM
                            var item = new TableForSitting
                            {
                                SittingId = existingResTablesForSitting[i].ts.Id,
                                Sitting = existingResTablesForSitting[i].ts.Sitting,
                                TableReferenceId = existingResTablesForSitting[i].ts.TableReferenceId,
                                TableName = existingResTablesForSitting[i].ts.TableName
                            };
                            complexAvailableTables.Add(item);
                        }
                        else // so the time slot for this reservation matches this existing one
                        // Does the reservation id match ... Has this table been assigned to this res already
                        // If so add it to this different list so that it can display in drag and drop.
                        {
                            if (id == workListRT[j].ReservationId)
                            {
                              //  thisTimeSlotIsAvailable = false;
                                var item = new TableForSitting
                                {
                                    SittingId = existingResTablesForSitting[i].ts.Id,
                                    Sitting = existingResTablesForSitting[i].ts.Sitting,
                                    TableReferenceId = existingResTablesForSitting[i].ts.TableReferenceId,
                                    TableName = existingResTablesForSitting[i].ts.TableName
                                };
                                AlreadyAllocatedToResTables.Add(item);
                            }
                        }
                        j++;
                    }
                }
                else
                {
                    //OK - this slot can be selected - nothing in this rt
                    // make an item to add to VM
                    var item = new TableForSitting
                    {
                        Id = existingResTablesForSitting[i].ts.Id,
                        SittingId = existingResTablesForSitting[i].ts.Id,
                        Sitting = existingResTablesForSitting[i].ts.Sitting,
                        TableReferenceId = existingResTablesForSitting[i].ts.TableReferenceId,
                        TableName = existingResTablesForSitting[i].ts.TableName
                    };
                    complexAvailableTables.Add(item);
                }
                i++;
            }

            var newVM = new Models.Reservation.AllocateReservationToTableVM()
            {
                ReservationId = res.Id,
                ReservationName = res.Name,
                ReservationPhone = res.Phone,
                Phone = res.Phone,
                ReservationEmail = res.Email,
                ReservationStartTime = res.StartTime,
                ReservationNumberOfPeople = res.NumberOfPeople,
                ReservationComments = res.Notes,
                AlreadyAllocatedToThisResSittingTables = AlreadyAllocatedToResTables,
                SittingTables = complexAvailableTables,
                StatusId = res.StatusId

            };

            return View(newVM);
        }
        [HttpPost]
      public async Task<IActionResult> OnPostUpdateReservationStatus(AllocateReservationToTableVM info)
        {
            if (info.StatusChange)
            {
                var reservation = await _context.Reservations
                                .Where(r => r.Id == info.Id)
                                .FirstOrDefaultAsync();
                if (reservation == null)
                {
                    return NotFound();
                }
                reservation.StatusId = 2;
                _context.Update(reservation);
               
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(GiveReservationTable), new { id = info.Id });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddSingleTable(AddSelectedTableVM selectedTables)
        {
           
            var newTableForRes = new ReservationTable
            {
                ReservationId = selectedTables.ReservationId,
                TableForSittingId = selectedTables.SelectedTableId

            };
            _context.ReservationTables.Add(newTableForRes);
               
            _context.SaveChanges();


            return new JsonResult(Ok());
        }

       
        public async Task<IActionResult> EmailConfirmationSent()
        {
            return View();
        }
        // GET: Admin/Home
        public async Task<IActionResult> Index()
        {
            return View(await _context.Reservations.ToListAsync());
        }

        // GET: Admin/Home/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Reservations == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Admin/Home/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Home/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PatronEmail,RequestedDate,StartTime,EndTime,SittingId")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reservation);
        }

        // GET: Admin/Home/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Reservations == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            return View(reservation);
        }

        // POST: Admin/Home/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PatronEmail,RequestedDate,StartTime,EndTime,SittingId")] Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(reservation);
        }

        // GET: Admin/Home/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Reservations == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Admin/Home/Delete/5
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
    }
}


   

