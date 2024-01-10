using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyappModels
{
    public class StateListModel
    {
        public int SId { get; set; }
        public string StateName { get; set; }
        public string StateCname { get; set; }

        public virtual CountryListModel CountryList { get; set; }
    }
}
