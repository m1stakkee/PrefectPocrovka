using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp2.Models.prefect_pocrovskoe_streshnegoEntitiesTableAdapters;

namespace WpfApp2.Models
{
    internal class Users
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public virtual  Roles Roles { get; set; }
    }
}
