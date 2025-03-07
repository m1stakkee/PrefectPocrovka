using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2.Models
{
    public class Roles
    {
        public int Roleid { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Users> Users { get; set; } = new List<Users>();
    }
}
