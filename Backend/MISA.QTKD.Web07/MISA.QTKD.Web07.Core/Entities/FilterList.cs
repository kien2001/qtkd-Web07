using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class FilterList
    {
        public List<FilterObject>? ListFilterObject { get; set; }

        public FilterList(List<FilterObject>? listFilterObject)
        {
            ListFilterObject = listFilterObject;
        }
    }
}
