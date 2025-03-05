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
            PROCESSING,
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
        public enum UserRole
        {
            ADMIN,
            CUSTOMER,
            SELLER
        }

        public enum TransactionType
        {
            DEPOSIT,
            WITHDRAW,
            PURCHASE,
            SELL
        }
    }
}
