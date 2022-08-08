using System.Collections.Generic;

namespace Base.DataContractCore.Base
{
    public class TableObject
    {
        public string TABLE_NAME { set; get; }
        public string AUTONUMBER_COLUMN { set; get; }
        public bool IS_AUTONUMBER_BY_MAX { set; get; }
        public List<string> KEYCOLUMNS { set; get; }

        public TableObject(string tbName, List<string> keycols, string autoColumn = "", bool useAutoByMax = true)
        {
            TABLE_NAME = tbName;
            KEYCOLUMNS = keycols;
            AUTONUMBER_COLUMN = autoColumn;
            IS_AUTONUMBER_BY_MAX = useAutoByMax;
        }
    }
}
