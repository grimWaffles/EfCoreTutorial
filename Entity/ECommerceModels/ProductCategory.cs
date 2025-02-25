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
    [Index(nameof(CategoryName), IsUnique = true)]
    public class ProductCategory
    {
        public ProductCategory()
        {
            Products = new List<Product>();
            CategoryName = "";
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(100)]
        public string CategoryName { get; set; }

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
