using System.Collections.Generic;

namespace Base.DataContractCore.Base
{
    public class Treestructure
    {
        public string id { set; get; }
        public string key { set; get; }
        public string title { set; get; }
        public bool expanded { set; get; }
        public List<Treestructure> children { set; get; }
        public Treestructure() { }
        public Treestructure(string _id, string _text, string _tille,  bool _expanded, List<Treestructure> _items)
        {
            id = _id;
            key = _text;
            title = _tille;
            expanded = _expanded;
            children = _items;
        }
    }
}
