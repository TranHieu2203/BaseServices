using System.Collections.Generic;

namespace Base.DataContractCore.Base
{
    public class ActiveRequest
    {
		public IList<int> IDS { get; set; }
        public int? ACTFLG { get; set; }
        public int STATUS { get; set; }
    }
}
