using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrefectPocrovka;
using WpfApp2.Modules;

namespace  PrefectPocrovka
{
    internal class ConnectDb
    {
        public static prefect_pocrovskoe_streshnegoEntities Connect { get; set; } = new prefect_pocrovskoe_streshnegoEntities();
    }
}
