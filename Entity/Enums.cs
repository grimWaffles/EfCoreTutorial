using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCoreTutorial.Entity
{
    public class Enums
    {
        public enum OrderStatus
        {
            PENDING,
            SHIPPING,
            COMPLETED,
            FAILED,
            CANCELLED
        }
        public enum OrderItemStatus
        {
            AVAILABLE,
            STOCKOUT
        }
    }
}
