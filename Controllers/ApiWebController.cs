using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Areas.Admin.Models.Reservation;
using Restaurant.Data;
using Restaurant.MongoModel;
using Restaurant.MongoModel.Services;
using System;
using System.Collections.Generic;


namespace Restaurant.Controllers
{
    [Route("api/[controller]")]

    public class ApiWebController : ApiBaseController
    {
        private readonly ApplicationDbContext _context;

        public ApiWebController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("AddSingleTable")]
        //public async JsonResult AddSingleTable(AddSelectedTableVM selectedTables)
        public JsonResult AddSingleTable(DragAndDropTable singleDragAndDropTable)
        {
            Console.WriteLine("in the api addsingletable");
            Console.WriteLine(singleDragAndDropTable);
            // Select reservation and get start and end time
            var res = _context.Reservations
                     .FirstOrDefault(r => r.Id == singleDragAndDropTable.ReservationId);

            // check that the user is not moving things about within the drop area.
            //var resTable = _context.ReservationTables
            //         .FirstOrDefault(r => r.ReservationId == singleDragAndDropTable.ReservationId 
            //         && r.TableForSittingId == singleDragAndDropTable.TableForSittingId);

            //if (resTable != null)
            //{
                var newTableForRes = new ReservationTable
                {
                    ReservationId = singleDragAndDropTable.ReservationId,
                    TableForSittingId = singleDragAndDropTable.TableForSittingId,
                    //  TableReferenceId = selectedTable.SelectedTableId,
                    StartDateTime = res.StartTime,
                    EndDateTime = res.EndTime
                };
                _context.ReservationTables.Add(newTableForRes);

                _context.SaveChanges();
            //};

            Response.StatusCode = StatusCodes.Status200OK;
            return new JsonResult("OK");

        }



    }
}
