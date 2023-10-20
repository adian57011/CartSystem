using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int CustomerName { get; set; }
    }
}