using System;
using System.Globalization;
using System.Linq;

namespace Base.Common.Helpers
{
    public static class VarHelper
    {
        /// <summary>
        /// Convert to int
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int? ToInt(this string str, int? defaultValue = null)
        {
            try
            {
                var kq = int.TryParse(str, out int returnValue);
                if (kq)
                {
                    return returnValue;
                }
                else
                {
                    return defaultValue;
                }
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Return double value
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue">Default value = null</param>
        /// <returns></returns>
        public static double? ToDouble(this string str, double? defaultValue = null)
        {
            try
            {
                var kq = double.TryParse(str, out double returnValue);
                if (kq)
                {
                    return returnValue;
                }
                else
                {
                    return defaultValue;
                }
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Convert string to Single
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static float? ToSingle(this string str, float? defaultValue = null)
        {
            try
            {
                var kq = float.TryParse(str, out float returnValue);
                if (kq)
                {
                    return returnValue;
                }
                else
                {
                    return defaultValue;
                }
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }
        
        /// <summary>
        /// Convert string to long
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static long? ToLong(this string str, long? defaultValue = null)
        {
            try
            {
                var kq = long.TryParse(str, out long returnValue);
                if (kq)
                {
                    return returnValue;
                }
                else
                {
                    return defaultValue;
                }
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }
        
        /// <summary>
        /// Convert string to short
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static short? ToShort(this string str, short? defaultValue = null)
        {
            try
            {
                var kq = short.TryParse(str, out short returnValue);
                if (kq)
                {
                    return returnValue;
                }
                else
                {
                    return defaultValue;
                }
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }
       
        /// <summary>
        /// Format first char to Upper case
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string FirstCharToUpper(this string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException("ARGH!");
            return input.First().ToString().ToUpper() + input.Substring(1);
        }

        /// <summary>
        /// Custom format string with ${index} and param array
        /// </summary>
        /// <param name="value"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string FormatCustom(this string value, params string[] param)
        {
            try
            {
                if (param == null || !param.Any())
                {
                    return value;
                }
                for (int i = 1; i <= param.Length; i++)
                {
                    value = value.Replace($"${i}", param[i - 1]);
                }
                return value;
            }
            catch
            {
                return value;
            }
        }

        /// <summary>
        /// Convert date from format date
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue">Default value = null</param>
        /// <param name="formatDate">Default format ""</param>
        /// <returns></returns>
        public static DateTime? ToDate(this string str, DateTime? defaultValue = null, string formatDate = "dd/MM/yyyy")
        {
            try
            {
                if (string.IsNullOrEmpty(str))
                {
                    return defaultValue;
                }

                if (string.IsNullOrEmpty(formatDate))
                {
                    return DateTime.Parse(str.ToString());
                }
                else
                {
                    CultureInfo provider = CultureInfo.InvariantCulture;
                    DateTime date = DateTime.Now;
                    var kq = DateTime.TryParseExact(str.ToString(), formatDate, provider, DateTimeStyles.None, out date);
                    if (kq)
                    {
                        return date;
                    }
                    else
                    {
                        return defaultValue;
                    }
                }
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Return decimal value
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue">Default value</param>
        /// <returns></returns>
        public static decimal? ToDecimal(this string str, decimal? defaultValue = null)
        {
            try
            {
                var kq = decimal.TryParse(str, out decimal returnValue);
                if (kq)
                {
                    return returnValue;
                }
                else
                {
                    return defaultValue;
                }
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

    }
}
