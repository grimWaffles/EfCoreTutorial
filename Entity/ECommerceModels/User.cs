﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace EfCoreTutorial.Entity.ECommerceModels
{
    [Table("Users")]
    [Index(nameof(Username), IsUnique = true)]
    [Index(nameof(MobileNo), IsUnique = true)]
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        public User()
        {
            Username = ""; Password = ""; Email = ""; MobileNo = "";
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(40)")]
        public string Username { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Password { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Email { get; set; }

        [MaxLength(11)]
        public string MobileNo { get; set; }

        public int? RoleId { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

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

        public virtual Role Role { get; set; }
    }
}
