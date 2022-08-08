using Base.DataContractCore.Base;
using System.Collections.Generic;

namespace Base.DataContractCore.Constants
{
    public class BaseTable
    {
        public const string SchemaName = "SE.";

        public static readonly TableObject SE_USER = new TableObject(SchemaName + "SE_USER", new List<string>() { "ID" }, "ID");
        public static readonly TableObject SE_GROUP = new TableObject(SchemaName + "SE_GROUP", new List<string>() { "ID" }, "ID");
        public static readonly TableObject SE_USER_LOGIN = new TableObject(SchemaName + "SE_USER_LOGIN", new List<string>() { "ID" }, "ID");
        public static readonly TableObject SE_ACTION_LOG = new TableObject(SchemaName + "SE_ACTION_LOG", new List<string>() { "ID" }, "ID");
        public static readonly TableObject SE_ACCESS_LOG = new TableObject(SchemaName + "SE_ACCESS_LOG", new List<string>() { "ID" }, "ID");
        public static readonly TableObject SE_GROUP_PERMISSION = new TableObject(SchemaName + "SE_GROUP_PERMISSION", new List<string>() { "ID" }, "ID");

        public static readonly TableObject SE_LANGUAGE = new TableObject(SchemaName + "SE_LANGUAGE", new List<string>() { "ID" }, "ID");
        public static readonly TableObject SE_LDAP = new TableObject(SchemaName + "SE_LDAP", new List<string>() { "ID" }, "ID");
        public static readonly TableObject SE_GRP_SE_USR = new TableObject(SchemaName + "SE_GRP_SE_USR", new List<string>() { "ID" }, "ID");
        public static readonly TableObject SE_CONFIG = new TableObject(SchemaName + "SE_CONFIG", new List<string>() { "ID" }, "ID");

    }

}
