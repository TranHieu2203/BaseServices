using System.Collections.Generic;

namespace Base.DataContractCore.Base
{
    public class RequestBaseList : BaseRequest
    {
        public string KEYWORD { set; get; }
        public int CURRENT_PAGE { set; get; }
        public int PAGE_SIZE { set; get; }
        public int? ACTFLG { set; get; }
        public IList<BaseFilter>? FILTER { get; set; }
        public RequestBaseList()
        {
            KEYWORD = "";
            CURRENT_PAGE = 1;
            PAGE_SIZE = 20;
        }
    }
}
