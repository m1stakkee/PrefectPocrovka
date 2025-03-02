using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2.Models
{
    internal class ConnectDb
    {
        public static prefect_pocrovskoe_streshnegoDataSet Connect { get; set; } = new prefect_pocrovskoe_streshnegoDataSet();
    }
}
