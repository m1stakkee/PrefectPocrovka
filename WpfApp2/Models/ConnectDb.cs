
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2.Models
{
    public class ConnectDb
    {
        public static prefect_pocrovskoe_streshnegoEntities Connect {  get; } = new prefect_pocrovskoe_streshnegoEntities();

        
    }

}
