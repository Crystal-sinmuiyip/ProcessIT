using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant.Data;
using System.ComponentModel.DataAnnotations;


namespace Restaurant.Models.Reservation
{
    public class UserData
    {
        public bool Authorized { get; set; }
        public string? Username { get; set; }
    }
}