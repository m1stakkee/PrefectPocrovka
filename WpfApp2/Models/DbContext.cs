using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp2.Models.prefect_pocrovskoe_streshnegoDataSetTableAdapters;

namespace WpfApp2.Models
{
    public class YourDbContext : DbContext
    {
        public YourDbContext() : base("prefect_pocrovskoe_streshnego") { } // Строка подключения в конструкторе
           
        public DbSet<UsersTableAdapter> Users { get; set; } 
        public DbSet<RolesTableAdapter> Roles { get; set; }
        public DbSet<DepartmentsTableAdapter> Departments { get; set; }
        public DbSet<AppointmentsTableAdapter> Appointments { get; set; } 
        public DbSet<CancellationReasonsTableAdapter> CancellationReasons { get; set; }
        public DbSet<PrefectsTableAdapter> Prefects { get; set; }
        public DbSet<StatusesTableAdapter> Statuses { get; set; }

      
    }
    public class Users
    {
        public int UserId { get; set; }
        public int RoleId { get; set; } = 2;
        public string FurstName { get; set; }
        public string Login {  get; set; }
        public string Password { get; set; }
    }

    public class Roles
    {
        public int RoleId { get; set; }
        public string FurstName { get; set; }
        
    }

}
