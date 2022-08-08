using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DataContractCore.Base
{
    public class EntityBase
    {
        public DateTime? CREATED_DATE { get; set; }
        public string CREATED_BY { get; set; }
        public string CREATED_LOG { get; set; }
        public DateTime? MODIFIED_DATE { get; set; }
        public string MODIFIED_BY { get; set; }
        public string MODIFIED_LOG { get; set; }
    }
}
