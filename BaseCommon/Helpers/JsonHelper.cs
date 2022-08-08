using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Base.Common.Helpers
{
    public static class JsonHelper
    {
        /// <summary>
        /// Convert  to XML string
        /// </summary>
        /// <param name="Record"></param>
        /// <param name="RecordType"></param>
        /// <returns></returns>
        public static string XML(this object Record, Type RecordType)
        {
            using var sw = new StringWriter();
            using XmlWriter xw = XmlWriter.Create(sw, new XmlWriterSettings() { OmitXmlDeclaration = true });
            var x = new XmlSerializer(RecordType);
            x.Serialize(xw, Record);
            return sw.ToString();
        }

        /// <summary>
        /// Convert DataTable to JSON string
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string JSON(this DataTable dt)
        {

            if (dt == null)
                return "";
            var l = new List<Dictionary<string, object>>();
            foreach (DataRow dr in dt.Rows)
            {
                var dic = new Dictionary<string, object>();
                foreach (DataColumn dc in dt.Columns)
                    dic.Add(dc.ColumnName, dr[dc.ColumnName].ToString());
                l.Add(dic);
            }

            //var js = new JavaScriptSerializer();
            return JsonConvert.SerializeObject(l);//  js.Serialize(l);

        }

        /// <summary>
        /// Return jSon string from first table in dataset
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static string JSON(this DataSet ds)
        {
            if (ds == null) return "";
            if (ds.Tables.Count == 0) return "";
            return JSON(ds.Tables[0]);

        }

        /// <summary>
        /// Convert Array datarow to JSON string
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string JSON(this DataRow[] dt)
        {
            if (dt == null)
                return "";
            var l = new List<Dictionary<string, object>>();
            foreach (DataRow dr in dt)
            {
                var dic = new Dictionary<string, object>();
                foreach (DataColumn dc in dr.Table.Columns)
                    dic.Add(dc.ColumnName, dr[dc.ColumnName].ToString());
                l.Add(dic);
            }

            //var js = new JavaScriptSerializer();
            return JsonConvert.SerializeObject(l);//js.Serialize(l);
        }

        public static string JSON(this DataRow dt)
        {
            if (dt == null)
                return "";
            var l = new List<Dictionary<string, object>>();
            var dic = new Dictionary<string, object>();
            foreach (DataColumn dc in dt.Table.Columns)
                dic.Add(dc.ColumnName, dt[dc.ColumnName].ToString());
            l.Add(dic);
            //var js = new JavaScriptSerializer();
            return JsonConvert.SerializeObject(l);// js.Serialize(l);

        }

        public static string JSON(this List<DataRow> dt)
        {
            if (dt == null)
                return "";
            var l = new List<Dictionary<string, object>>();
            foreach (DataRow dr in dt)
            {
                var dic = new Dictionary<string, object>();
                foreach (DataColumn dc in dr.Table.Columns)
                    dic.Add(dc.ColumnName, dr[dc.ColumnName].ToString());
                l.Add(dic);
            }

            //var js = new JavaScriptSerializer();
            return JsonConvert.SerializeObject(l);// js.Serialize(l);
        }
    }
}
