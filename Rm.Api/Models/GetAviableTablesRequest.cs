using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Security.Principal;

namespace Rm.Api.Models
{
    public class GetAviableTablesRequest
    {
        public DateTime ReservationDateTime { get; set; }
        public int Size { get; set; }
    }
}
