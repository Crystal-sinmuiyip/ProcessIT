//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Restaurant.MongoModel;
//using Restaurant.MongoModel.Services;
//using Restaurant.Models.Reservation;
//using System;
//using System.Collections.Generic;
//using Restaurant.Data;
//using Microsoft.EntityFrameworkCore;

//namespace Restaurant.Controllers
//{
//    [Route("api/[controller]")]

//    public class ApiReservationController : ApiBaseController
//    {
//        private readonly ApplicationDbContext _context;
//        //   public ApiAppController(MenuItemsService menuItemsService, TableOrdersService tableOrderService, UserManager<IdentityUser> userManager)
//        public ApiReservationController(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        [HttpPost]
//        public async Task<IActionResult> Get(CreateVM res)
//        {
//            var sitting = await _context.Sittings.FirstOrDefaultAsync(s => s.Id == res.SittingId);

//            var SelectedDateTime = res.StartTime;

//            var newRes = new Reservation
//            {
//                Name = res.Name,
//                Phone = res.Phone,
//                Email = res.Email,
//                RequestedDate = DateTime.Now,
//                StartTime = SelectedDateTime,
//                EndTime = SelectedDateTime.AddMinutes(sitting.DurationReservation),
//                NumberOfPeople = res.NumberOfPeople,
//                Birthday = res.Birthday,
//                Anniversary = res.Anniversary,
//                Pram = res.Pram,
//                HighChair = res.HighChair,
//                DisabledAccess = res.DisabledAccess,
//                Allergy = res.Allergy,
//                Notes = res.Notes,
//                Comments = res.Comments,
//                SittingId = res.SittingId,
//                SittingTypeId = res.SittingTypeId,
//                StatusId = 1  //pending

//            };

//            _context.Add(newRes);
//            await _context.SaveChangesAsync();
//            return Ok();
//        }
//    }
//}
