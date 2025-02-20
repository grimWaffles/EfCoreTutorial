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
    [Index(nameof(Name), IsUnique = true)]
    public class Role
    {
        public Role()
        {
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(40)]
        public UserRole Name { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [Required]
        public int CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedBy { get; set; }

        public bool IsDeleted { get; set; } = false;

        //Foreign Keys
        [ForeignKey(nameof(CreatedBy))]
        [DeleteBehavior(DeleteBehavior.ClientNoAction)]
        public virtual User CreatedByUser { get; set; }

        [ForeignKey(nameof(ModifiedBy))]
        [DeleteBehavior(DeleteBehavior.ClientNoAction)]
        public virtual User ModifiedByUser { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
