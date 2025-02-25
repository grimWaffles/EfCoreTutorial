using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EfCoreTutorial.Entity.Enums;

namespace EfCoreTutorial.Dtos
{
    public class OrderItemDto
    {
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public decimal GrossAmount { get; set; }
        public OrderItemStatus ProductStatus { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
    }
}
