using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCoreTutorial.Dtos
{
    public class CustomerTotalExpenseReport
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public int TotalItemsPurchased { get; set; }
        public decimal TotalExpense { get; set; }
    }
}
