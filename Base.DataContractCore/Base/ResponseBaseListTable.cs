using System.Data;

namespace Base.DataContractCore.Base
{
    public class ResponseBaseListTable
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

        public DataTable TABLE_DATA { set; get; }
    }
}
