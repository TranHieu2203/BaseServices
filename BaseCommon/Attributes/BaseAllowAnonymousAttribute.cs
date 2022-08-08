using System;

namespace Base.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class BaseAllowAnonymousAttribute : Attribute
    { }
}
