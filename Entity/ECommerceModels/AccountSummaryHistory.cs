using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCoreTutorial.Entity.ECommerceModels
{
    [Index(nameof(TransactionDate), nameof(UserId))]
    public class AccountSummaryHistory
    {
        public DateTime TransactionDate { get; set; }

        [Required]
        public int UserId { get; set; }

        [Precision(18, 4)]
        public decimal CashAmount { get; set; }

        [Precision(18, 4)]
        public decimal TotalDepositAmount { get; set; }


        [Precision(18, 4)]
        public decimal TotalPurchaseAmount { get; set; }

        [Precision(18, 4)]
        public decimal TotalWithdrawAmount { get; set; }

        [Required]
        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedBy { get; set; }

        public bool IsDeleted { get; set; } = false;

        //Foreign keys
        [ForeignKey(nameof(CreatedBy))]
        [DeleteBehavior(DeleteBehavior.ClientNoAction)]
        public virtual User CreatedByUser { get; set; }

        [ForeignKey(nameof(ModifiedBy))]
        [DeleteBehavior(DeleteBehavior.ClientNoAction)]
        public virtual User ModifiedByUser { get; set; }

        public virtual User User { get; set; }
    }
}
