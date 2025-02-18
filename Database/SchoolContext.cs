using EfCoreTutorial.Entity.SchoolModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCoreTutorial.Database
{
    public class SchoolContext : DbContext
    {
        //DbSets for Entity Framework
        public DbSet<Student> Students { get; set; }
        public DbSet<Grade> Grades { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-5DP7KR2\\MSSQLSERVERWAZI;Database=EFCoreTutorial;Trusted_Connection=True;TrustServerCertificate=True");

        }
    }
}
