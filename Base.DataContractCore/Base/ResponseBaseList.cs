using System.Collections.Generic;

namespace Base.DataContractCore.Base
{
    public class ResponseBaseList<T>
    {
        private int total_row = 0;
        public object TOTAL_ROW
        {
            set
            {
                try { total_row = int.Parse(value.ToString()); } catch { total_row = 0; }
            }
            get { return total_row; }
        }

        public List<T> LIST_DATA { set; get; }

    }
}
