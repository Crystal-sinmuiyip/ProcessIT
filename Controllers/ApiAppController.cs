using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.MongoModel;
using Restaurant.MongoModel.Services;
using Restaurant.Models.Reservation;
using System;
using System.Collections.Generic;
//using System.Web.Http.IHttpActionResult;

namespace Restaurant.Controllers
{
    [Route("api/[controller]")]

    public class ApiAppController : ApiBaseController

    {
        //public record Response
        //{
        //    public string Status { get; init; }
        //    public string Message { get; init; }
        //    public object Details { get; init; }
        //}

        //public static class Responses
        //{
        //    public static Response SuccessResponse => new() { Status = "Success", Message = "Request carried out successfully." };
        //    public static Response ErrorResponse => new() { Status = "Error", Message = "Something went wrong." };
        //}

        //  private readonly UserManager<IdentityUser> _userManager;
        private readonly MenuItemsService _menuItemsService;
        private readonly TableOrdersService _tableOrdersService;

        //   public ApiAppController(MenuItemsService menuItemsService, TableOrdersService tableOrderService, UserManager<IdentityUser> userManager)
        public ApiAppController(MenuItemsService menuItemsService, TableOrdersService tableOrderService)
        {
            _menuItemsService = menuItemsService;
            _tableOrdersService = tableOrderService;
            // _userManager = userManager;
        }
        [Route("GetTableOrders")]
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<List<TableOrder>> GetTableOrders()
        {

            var result = await _tableOrdersService.GetAsync();
            return result;

        }

       // [AllowAnonymous]
        [Route("CurrentOrderForTable/{id}")]
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<TableOrder>> CurrentOrderForTable(string id)
      
        {
           var tableOrderSingle = new TableOrder();
        
           tableOrderSingle = await _tableOrdersService.CurrentOrderForTableWithTableOrderId(id);
            if (tableOrderSingle == null)
            {
                return NotFound();
            }
            return Ok(tableOrderSingle);
        }

        //public async Task<ActionResult<List<DocumentsResponseDto>>> SaveDocumentDetails([FromBody] List<DocumentDetailsRequestDto> documentDetailsRequestDto)
        //{

        //    var response = await _documentService.SaveDocumentDetails(documentDetailsRequestDto);

        //    return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status207MultiStatus, response);
        //}


        [Route("GetCurrentTableOrderByTableId/{id}")]
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<TableOrder> GetCurrentTableOrderByTableId(string id)
        {
            var tableOrderSingle = new TableOrder();
            tableOrderSingle = await _tableOrdersService.CurrentOrderForTable(id);

            return tableOrderSingle;
        }
        [Route("PostTableOrder")]
       
        [HttpPost]
        
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Post(TableOrder newTableOrder)
        {
            newTableOrder.Completed = null;
            await _tableOrdersService.CreateAsync(newTableOrder);

            return CreatedAtAction(nameof(Get), new { id = newTableOrder.Id }, newTableOrder);
        }

        [Route("UpdateTableOrderHeader/{id}")]
        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        // Only supply / give the main part of the table order
        // This will fetch the orderitems, and then apply the update to the main / header area only.
        public async Task<IActionResult> UpdateTableOrderHeader(string id, TableOrder updatedTableOrder)
        {
            var originalTableOrder = await _tableOrdersService.GetIdAsync(id);

            if (originalTableOrder is null)
            {
                return NotFound();
            }

            updatedTableOrder.OrderItemList = originalTableOrder.OrderItemList;

            await _tableOrdersService.UpdateAsync(id, updatedTableOrder);

            return NoContent();
        }
        // tthis used to be OrderItem with id and put
        [Route("UpdateTableOrderItem/{id}")]
        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateTableOrderItem(string id, OrderItem updatedOrderItem)
        {
            var originalTableOrder = await _tableOrdersService.GetIdAsync(id);

            if (originalTableOrder is null)
            {
                return NotFound();
            }

            //  code to compare changes and update values - particulary quantity
            //  or insert a new item if if does not exist

            var updatedTableOrder = originalTableOrder; // as TableOrder;

            if (originalTableOrder.OrderItemList is null)
            {
                // this is the first time the table is ordering food
                var workOrderItemList = new List<OrderItem>();
                workOrderItemList.Add(updatedOrderItem);

                updatedTableOrder.OrderItemList = workOrderItemList;
            }
            else
            {
                var originalOrderItems = originalTableOrder.OrderItemList.ToList();

                // loop through the original list of orders and see if customer has ordered this before
                // if so update quantity otherwise add a new order item.
                var foundMenuItem = false;
                var i = 0;

                while (!foundMenuItem && i < originalOrderItems.Count)
                {
                    if (originalOrderItems[i].MenuItemName == updatedOrderItem.MenuItemName)
                    {
                        foundMenuItem = true;
                        originalOrderItems[i].Quantity = updatedOrderItem.Quantity;
                        originalOrderItems[i].Notes = updatedOrderItem.Notes;
                        break;
                    }
                    i++;
                }
                if (!foundMenuItem) originalOrderItems.Add(updatedOrderItem);

                // now assign the worked list to the class
                updatedTableOrder.OrderItemList = originalOrderItems;
            }
            await _tableOrdersService.UpdateAsync(id, updatedTableOrder);

            return NoContent();
        }

        [Route("GetTablesStatus/{id}")]
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        // supply the area id and get the status of tables for coloured table main page
        //public async Task<List<TableStatusCurrent>> GetTablesStatus(string id)
        public async Task<List<TableStatusCurrent>> GetTablesStatus(string id)
        {
            // Call the service for TableOrder
            //  filter for today and status
            // AREA filter for service actually not used - maybe remove maybe need ?
            var workAreaMain = "Main";

            var currenttableOrderList = await _tableOrdersService.GetCurrentTableOrdersByAreaAsync(workAreaMain);

            //Now return all tables for that area and the status

            var workTablesStatusList = new List<TableStatusCurrent>();

            var foundInTableOrder = false;
            // get a swagger error if you call a method here
            // var (atMain, atOutside, atBalcony) = MakeData();
            var workATMain = new AreaTable
            {
                Area = workAreaMain
            };

            string[] workMainTable = { "M1", "M2", "M3", "M4", "M5", "M6", "M7", "M8", "M9", "M10" };
            workATMain.Table = workMainTable;

            var workATOutside = new AreaTable
            {
                Area = "Outside"
            };

            string[] workOutsideTable = { "O1", "O2", "O3", "O4", "O5", "O6", "O7", "O8", "O9", "O10" };
            workATOutside.Table = workOutsideTable;

            var workATBalcony = new AreaTable
            {
                Area = "Balcony"
            };

            string[] workBalconyTable = { "B1", "B2", "B3", "B4", "B5", "B6", "B7", "B8", "B9", "B10" };
            workATBalcony.Table = workBalconyTable;

            var allAreaTableList = new List<AreaTable>();
            switch (id)
            {
                case "Main":
                    allAreaTableList.Add(workATMain);
                    break;
                case "Outside":
                    allAreaTableList.Add(workATOutside);
                    break;
                case "Balcony":
                    allAreaTableList.Add(workATBalcony);
                    break;
                case "All":
                    {
                        allAreaTableList.Add(workATMain);
                        allAreaTableList.Add(workATOutside);
                        allAreaTableList.Add(workATBalcony);
                        break;
                    }
            }
            foreach (var workArea in allAreaTableList)
            {
                foreach (var workTable in workArea.Table)
                {
                    // Console.WriteLine($" {workArea}");
                    if (currenttableOrderList is not null) // have customers arrived yet
                    {
                        // Now loop through the tableOrders to check if they are occupied
                        var j = 0;
                        foundInTableOrder = false;
                        while (!foundInTableOrder && j < currenttableOrderList.Count)
                        {
                            if (workArea.Area == currenttableOrderList[j].Area &&
                                workTable == currenttableOrderList[j].Table)
                            {
                                var workTabStatus = new TableStatusCurrent
                                {
                                    Id = currenttableOrderList[j].Id,
                                    Area = currenttableOrderList[j].Area,
                                    Table = currenttableOrderList[j].Table
                                };

                                var orderStatus = currenttableOrderList[j].OrderStatus;
                                // workTabStatus.TableStatus = (TableStatus)(int)orderStatus;
                                workTabStatus.TableStatus = orderStatus;
                                workTabStatus.Created = DateTime.UtcNow;
                                workTablesStatusList.Add(workTabStatus);
                                foundInTableOrder = true;
                                break;
                            }
                            j++;
                        }
                    }
                    //  if we have not found it occupied or if no customers have arrived
                    if (!foundInTableOrder || currenttableOrderList is null)
                    {
                        var workTabStatus = new TableStatusCurrent
                        {
                            Id = workTable.ToString(),
                            Area = workArea.Area,
                            Table = workTable,
                            TableStatus = "Available", //  status
                            Created = DateTime.UtcNow
                        };
                        workTablesStatusList.Add(workTabStatus);
                    }
                    else { foundInTableOrder = false; }
                }
            }
            return workTablesStatusList.ToList();
        }

        [Route("GetCombinedMenuItemsAndTableOrderItems/{id}")]
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<List<CombinedMenuItemTableOrder>> GetCombinedMenuItemsAndTableOrderItems(string id)
        {
            var workMenuItemsList = new List<MenuItem>();
            workMenuItemsList = await _menuItemsService.GetAsync();

            var tableOrderSingle = new TableOrder();
            tableOrderSingle = await _tableOrdersService.CurrentOrderForTableWithTableOrderId(id);

            var workOutputList = new List<CombinedMenuItemTableOrder>();
            var foundInTableOrderItem = false;
            foreach (var item in workMenuItemsList)
            {
                // Now loop through the tableOrder items to check if they
                // match the current MenuItem

                foundInTableOrderItem = false;
                // while (!foundInTableOrder && j < tableOrderSingle.OrderItemList)
                if (tableOrderSingle.OrderItemList is not null)
                {
                    foreach (var tabOrderItem in tableOrderSingle.OrderItemList)
                    {
                        if (tabOrderItem.MenuItemName == item.Name)
                        {
                            var workItem = new CombinedMenuItemTableOrder
                            {
                                Id = item.Id,
                                Name = item.Name,
                                Price = item.Price,
                                Category = item.Category,
                                AvailableToday = item.AvailableToday,
                                Vegan = item.Vegan,
                                GlutenFree = item.GlutenFree,
                                Quantity = tabOrderItem.Quantity,
                                Notes = tabOrderItem.Notes
                            };
                            workOutputList.Add(workItem);
                            foundInTableOrderItem = true;
                            break;
                        }
                    }
                }
                if (!foundInTableOrderItem)
                {
                    var workItem = new CombinedMenuItemTableOrder
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Price = item.Price,
                        Category = item.Category,
                        AvailableToday = item.AvailableToday,
                        Vegan = item.Vegan,
                        GlutenFree = item.GlutenFree,
                        Quantity = 0,
                        Notes = null
                    };
                    workOutputList.Add(workItem);
                }
            }
            return workOutputList.ToList();
        }


        [Route("GetTableOrderSummary/{id}")]
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<TableOrderSummary> GetTableOrderSummary(string id)
        {
            var tableOrderSingle = await _tableOrdersService.CurrentOrderForTableWithTableOrderId(id);
            int totalQuantity = 0;
            decimal totalPrice = 0;

            if (tableOrderSingle.OrderItemList is not null)
            {
                foreach (var tabOrderItem in tableOrderSingle.OrderItemList)
                {
                    totalQuantity = totalQuantity + tabOrderItem.Quantity;
                    totalPrice = totalPrice + (Convert.ToDecimal(tabOrderItem.Quantity) * tabOrderItem.Price);
                };
            };

            var tableOrderSummary = new TableOrderSummary
            {
                Id = tableOrderSingle.Id,
                Area = tableOrderSingle.Area,
                Table = tableOrderSingle.Table,
                OrderStatus = tableOrderSingle.OrderStatus,
                WaitStaff = tableOrderSingle.WaitStaff,
                Created = tableOrderSingle.Created,
                Completed = tableOrderSingle.Completed,
                TotalQuantity = totalQuantity,
                TotalPrice = totalPrice
            };
            return tableOrderSummary;
        }
        [Route("DeleteTableOrder/{id}")]
        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteTableOrder(string id)
        {
            var tableOrder = await _tableOrdersService.GetBasicAsync(id);

            if (tableOrder is null)
            {
                return NotFound();
            }

            await _tableOrdersService.RemoveAsync(id);

            return NoContent();
        }
        // use this to clear up all the rubbish examples we tested with - will have to change the date range here
        [Route("DeleteAllTableOrders")]
        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteAllTableOrders()
        {
            await _tableOrdersService.DeleteManyAsync();

            return NoContent();
        }


        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX


        [Route("GetMenuItems")]
        [HttpGet]
        public async Task<List<MenuItem>> GetMenuItems()
        {

            return await _menuItemsService.GetAsync();
        }

        [Route("GetMenuItems/{id}")]
        [HttpGet]
        public async Task<ActionResult<MenuItem>> Get(string id)
        {
            var menu = await _menuItemsService.GetAsync(id);

            if (menu is null)
            {
                return NotFound();
            }

            return menu;
        }
        [Route("PostMenuItems")]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post(MenuItem newMenuItem)
        {
           // var tempUser = User;

            //IdentityUser? user = await _userManager.GetUserAsync(User);


            //// Not signed in
            //if (user == null)
            //    return NotFound();

            ////return new UserData
            ////{
            ////    Authorized = true,
            ////    Username = user.UserName
            ////};

            if (newMenuItem.CategorySort is null || newMenuItem.CategorySort == 0)
            {
                switch (newMenuItem.Category)
                {
                    case "Entree":
                        newMenuItem.CategorySort = 0;
                        break;
                    case "Mains":
                        newMenuItem.CategorySort = 1;
                        break;
                    case "Dessert":
                        newMenuItem.CategorySort = 2;
                        break;
                    case "Drinks":
                        newMenuItem.CategorySort = 3;
                        break;
                    case "Specials":
                        newMenuItem.CategorySort = 4;
                        break;
                    default:
                        newMenuItem.CategorySort = 4;
                        break;
                }
            }
            await _menuItemsService.CreateAsync(newMenuItem);

            return CreatedAtAction(nameof(Get), new { id = newMenuItem.Id }, newMenuItem);
        }
        [Route("PutMenuItems/{id}")]
        [HttpPut]
        public async Task<IActionResult> Update(string id, MenuItem updatedMenu)
        {
            var menu = await _menuItemsService.GetAsync(id);

            if (menu is null)
            {
                return NotFound();
            }

            updatedMenu.Id = menu.Id;

            await _menuItemsService.UpdateAsync(id, updatedMenu);

            return NoContent();
        }
        [Route("DeleteMenuItems/{id}")]
        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteMenuItems(string id)
        {
            var menu = await _menuItemsService.GetAsync(id);

            if (menu is null)
            {
                return NotFound();
            }

            await _menuItemsService.RemoveAsync(id);

            return NoContent();
        }
    }
}
