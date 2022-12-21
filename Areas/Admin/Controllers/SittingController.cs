using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.Data;

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace Restaurant.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SittingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SittingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Sitting
        public async Task<IActionResult> Index()
        {
            var TodaysDate = DateTime.Now;
            var applicationDbContext = _context.Sittings
                .Include(s => s.SittingType)
                .Where(s => s.DateAvailable > TodaysDate);

            return View(await applicationDbContext.ToListAsync());
            //return View();
        }

        // GET: Admin/Sitting/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
          
            if (id == null || _context.Sittings == null)
            {
                return NotFound();
            }

            var sitting = await _context.Sittings
                .Include(s => s.SittingType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sitting == null)
            {
                return NotFound();
            }

            return View(sitting);

        }

        // GET: Admin/Sitting/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            //  ViewData["SittingTypeId"] = new SelectList(_context.SittingTypes, "Id", "Id");
            //ViewData["SittingTypeId"] = new SelectList(_context.SittingTypes, "Id", "Name");
            //return View();
            var p = new Restaurant.Areas.Admin.Models.Sitting.CreateVM();
            p.SittingTypes = new SelectList(_context.SittingTypes.ToArray(), nameof(SittingType.Id), nameof(SittingType.Name));
            return View(p);
        }

        // POST: Admin/Sitting/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Restaurant.Areas.Admin.Models.Sitting.CreateVM s)
        {
            
            // if (ModelState.IsValid)
            {
                var startDate = s.StartTime;
                var numDays = s.DurationNumberOfDays;
                var endDate = startDate.AddDays(numDays);
                var workDate = startDate;

                while (workDate <= endDate)
                {
                    var newSitting = new Sitting();
                    if (workDate.ToString("ddd") == "Mon" && s.ScheduleOnMonday)
                    {
                        newSitting = await MakeSitting(s, workDate);
                        _context.Sittings.Add(newSitting);
                        await _context.SaveChangesAsync();
                        int newId = newSitting.Id;
                        MakeTableForSitting(s, newSitting, newId);
                        await _context.SaveChangesAsync();
                    }
                    if (workDate.ToString("ddd") == "Tue" && s.ScheduleOnTuesday)
                    {
                        newSitting = await MakeSitting(s, workDate);
                        _context.Sittings.Add(newSitting);
                        await _context.SaveChangesAsync();
                        int newId = newSitting.Id;
                        MakeTableForSitting(s, newSitting, newId);
                        await _context.SaveChangesAsync();
                    }
                    if (workDate.ToString("ddd") == "Wed" && s.ScheduleOnWednesday)
                    {
                        newSitting = await MakeSitting(s, workDate);
                        _context.Sittings.Add(newSitting);
                        await _context.SaveChangesAsync();
                        int newId = newSitting.Id;
                        MakeTableForSitting(s, newSitting, newId);
                        await _context.SaveChangesAsync();
                    }
                    if (workDate.ToString("ddd") == "Thu" && s.ScheduleOnThursday)
                    {
                        newSitting = await MakeSitting(s, workDate);
                        _context.Sittings.Add(newSitting);
                        await _context.SaveChangesAsync();
                        int newId = newSitting.Id;
                        MakeTableForSitting(s, newSitting, newId);
                        await _context.SaveChangesAsync();
                    }
                    if (workDate.ToString("ddd") == "Fri" && s.ScheduleOnFriday)
                    {
                        newSitting = await MakeSitting(s, workDate);
                        _context.Sittings.Add(newSitting);
                        await _context.SaveChangesAsync();
                        int newId = newSitting.Id;
                        MakeTableForSitting(s, newSitting, newId);
                        await _context.SaveChangesAsync();
                    }
                    if (workDate.ToString("ddd") == "Sat" && s.ScheduleOnSaturday)
                    {
                        newSitting = await MakeSitting(s, workDate);
                        _context.Sittings.Add(newSitting);
                        await _context.SaveChangesAsync();
                        int newId = newSitting.Id;
                        MakeTableForSitting(s, newSitting, newId);
                        await _context.SaveChangesAsync();

                    }
                    if (workDate.ToString("ddd") == "Sun" && s.ScheduleOnSunday)
                    {
                        newSitting = await MakeSitting(s, workDate);
                        _context.Sittings.Add(newSitting);
                        await _context.SaveChangesAsync();
                        int newId = newSitting.Id;
                        MakeTableForSitting(s, newSitting, newId);
                        await _context.SaveChangesAsync();
                    }
                    workDate = workDate.AddDays(1);

                }
               
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

        }
    
        private async Task<Sitting> MakeSitting(Restaurant.Areas.Admin.Models.Sitting.CreateVM s, DateTime workDate)
        {
            
            var sittingType = await _context.SittingTypes
                            .FirstOrDefaultAsync(st => st.Id == s.SittingTypeId);
            string sittingTypeName = sittingType.Name;

            var workStartTime = workDate.Date;
            var workTimeHour = s.StartTime.Hour;
            var workTimeMinute = s.StartTime.Minute;
            workStartTime = workStartTime.AddHours(workTimeHour);
            workStartTime = workStartTime.AddMinutes(workTimeMinute);


            var newSitting = new Sitting
            {
                Name = s.Name,
                Capacity = s.Capacity,
                DurationReservation = s.DurationReservation,
                DurationSitting = s.DurationSitting,
                StartTime = workStartTime,
                EndTime = workStartTime.AddMinutes(s.DurationSitting),
                DateAvailable = workDate,
                SittingTypeId = s.SittingTypeId,
                SittingTypeName = sittingTypeName,
                PublicCanMakeReservation = s.PublicCanMakeReservation
            };

            return (newSitting);
        }

        private void MakeTableForSitting(Restaurant.Areas.Admin.Models.Sitting.CreateVM s, Sitting newParentSitting, int newSittingId)
        {
            if (s.AreaOutside)
            {
                var tableReferenceOutside = _context.TableReferences
                        .Where(tref => tref.AreaId == 2)
                        .ToList();
                foreach (var item in tableReferenceOutside)
                {
         
                    var newTableforSitting = new TableForSitting
           
                    {
                        SittingId = newSittingId,
                        Sitting = newParentSitting,
                        TableReferenceId = item.Id,
                        TableName = item.Name,
                        TableReference = item
                    };
               
                    _context.TableForSittings.Add(newTableforSitting);
                                  
                }
            }

            if (s.AreaMain)
            {
                var tableReferenceMain = _context.TableReferences
                        .Where(tref => tref.AreaId == 1)
                        .ToList();
                foreach (var item in tableReferenceMain)
                {
                    var newTableforSitting = new TableForSitting
                    {
                        SittingId = newSittingId,
                        Sitting = newParentSitting,
                        TableReferenceId = item.Id,
                        TableName = item.Name,
                        TableReference = item
                    };
                    _context.TableForSittings.Add(newTableforSitting);
                }
            };
            if (s.AreaBalcony)
            {
                var tableReferenceBalcony = _context.TableReferences
                        .Where(tref => tref.AreaId == 3)
                        .ToList();

                foreach (var item in tableReferenceBalcony)
                {
                    var newTableforSitting = new TableForSitting
                    {
                        SittingId = newSittingId,
                        Sitting = newParentSitting,
                        TableReferenceId = item.Id,
                        TableName = item.Name,
                        TableReference = item
                    };
                    _context.TableForSittings.Add(newTableforSitting);
                }
            };

            return;
        }

        // GET: Admin/Sitting/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sittings == null)
            {
                return NotFound();
            }

            var sitting = await _context.Sittings.FindAsync(id);
            if (sitting == null)
            {
                return NotFound();
            }
            ViewData["SittingTypeId"] = new SelectList(_context.SittingTypes, "Id", "Id", sitting.SittingTypeId);
            return View(sitting);
        }

        // POST: Admin/Sitting/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Capacity,Duration,StartTime,EndTime,PublicCanMakeReservation,DateAvailable,ScheduleOnSaturday,ScheduleOnSunday,ScheduleOnMonday,ScheduleOnTuesday,ScheduleOnWednesday,ScheduleOnThursday,ScheduleOnFriday,SittingTypeId")] Sitting sitting)
        {
            if (id != sitting.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sitting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SittingExists(sitting.Id))
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
            ViewData["SittingTypeId"] = new SelectList(_context.SittingTypes, "Id", "Id", sitting.SittingTypeId);
            return View(sitting);
        }

        // GET: Admin/Sitting/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sittings == null)
            {
                return NotFound();
            }

            var sitting = await _context.Sittings
                .Include(s => s.SittingType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sitting == null)
            {
                return NotFound();
            }

            return View(sitting);
        }

        // POST: Admin/Sitting/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sittings == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Sittings'  is null.");
            }
            var sitting = await _context.Sittings.FindAsync(id);
            if (sitting != null)
            {
                _context.Sittings.Remove(sitting);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SittingExists(int id)
        {
            return _context.Sittings.Any(e => e.Id == id);
        }

        public IActionResult FindAllEvents()
        {
            // Not complex enough - Allows customers to book old sittings.
            //var events = _context.Sittings.Select(e => new
            //{
            //    id = e.Id,
            //    title = e.Name,
            //    start = e.StartTime,
            //    end = e.EndTime,

            //}).ToList();
            //return new JsonResult(events);

            var now = DateTime.Now;
            var end = DateTime.Now.Date.AddDays(55);
            var sittings = _context.Sittings
                           //.Include(s => s.SittingType)
                           .Where(s => s.DateAvailable > now && s.DateAvailable < end)
                           .Select(s => new
                           {
                               id = s.Id,
                               title = s.Name,
                               start = s.StartTime,
                               end = s.EndTime,

                           }).ToList();

            return new JsonResult(sittings);
            
          
        }
    }
}

