using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EfCoreTutorial.Entity.ECommerceModels
{
    [Index(nameof(MobileNo), nameof(Email), IsUnique = true)]
    public class Seller
    {
        public Seller()
        {
            Products = new List<Product>();
            CompanyName = "";
            Address = "";
            MobileNo = "";
            Email = "";
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(100)]
        public string CompanyName { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }

        [MaxLength(11)]
        public string MobileNo { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [Precision(18, 2)]
        public decimal Rating { get; set; }

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

        public virtual ICollection<Product> Products { get; set; }

    }
}
