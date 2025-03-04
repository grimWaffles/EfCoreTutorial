using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EfCoreTutorial.Entity.Enums;

namespace EfCoreTutorial.Entity.ECommerceModels
{
    [Index(nameof(OrderDate), nameof(OrderCounter))]
    public class Order
    {
        public Order()
        {
            Items = new List<OrderItem>();
        }

        [Key]
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int OrderCounter { get; set; }
        [Required]
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public OrderStatus Status { get; set; }
        [Precision(18, 4)]
        public decimal NetAmount { get; set; }

        [Required]
        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedBy { get; set; }

        public bool IsDeleted { get; set; } = false;

        [ForeignKey(nameof(CreatedBy))]
        [DeleteBehavior(DeleteBehavior.ClientNoAction)]
        public virtual User CreatedByUser { get; set; }

        [ForeignKey(nameof(ModifiedBy))]
        [DeleteBehavior(DeleteBehavior.ClientNoAction)]
        public virtual User ModifiedByUser { get; set; }

        public virtual User User { get; set; }

        [NotMapped]
        public List<OrderItem> Items { get; set; }
    }
}
