using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EfCoreTutorial.Entity.Enums;

namespace EfCoreTutorial.Dtos
{
    public class OrderInformationDto
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int OrderCounter { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public decimal NetAmount { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }
}
