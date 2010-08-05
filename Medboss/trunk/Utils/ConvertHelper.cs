namespace Nammedia.Medboss.Utils
{
    using Nammedia.Medboss;
    using Nammedia.Medboss.Log;
    using System;
    using System.Globalization;

    public class ConvertHelper
    {
        public static string formatNumber(double a)
        {
            return a.ToString("#,#.###");
        }

        public static string formatNumber(int a)
        {
            return a.ToString("#,0");
        }

        public static bool getBool(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj is bool)
            {
                return (bool) obj;
            }
            if (obj is string)
            {
                bool result = false;
                Boolean.TryParse((string)obj, out result);
                return result;
            }
            return Convert.ToBoolean(obj);
        }

        public static DateTime getDateTime(object obj)
        {
            Type type = obj.GetType();
            if ((type.Name != "DBNull") && (type.Name == "DateTime"))
            {
                return (DateTime) obj;
            }
            return DateTime.MinValue;
        }

        public static double getDouble(object obj)
        {
            try
            {
                if (obj == null)
                {
                    return -1.0;
                }
                Type type = obj.GetType();
                if (type.Name == "String")
                {
                    return double.Parse((string) obj);
                }
                if (type.Name == "DBNull")
                {
                    return -1.0;
                }
                if (type.Name == "Int32")
                {
                    return Convert.ToDouble(obj);
                }
                return (double) obj;
            }
            catch (FormatException exc)
            {
                if (((string) obj) != "")
                {
                    throw new InvalidException((string) obj);
                }
                LogManager.LogException(exc);
                return -1.0;
            }
        }

        public static int getInt(object obj)
        {
            try
            {
                if (obj == null)
                {
                    return -1;
                }
                if (obj is string)
                {
                    CultureInfo provider = new CultureInfo("en-US");
                    return int.Parse((string) obj, NumberStyles.AllowThousands, provider);
                }
                if (obj is DBNull)
                {
                    return -1;
                }
                if (obj is int)
                {
                    return (int) obj;
                }
                if (obj is int)
                {
                    return (int) obj;
                }
                if (obj is double)
                {
                    return Convert.ToInt32((double) obj);
                }
                return Convert.ToInt32(obj);
            }
            catch (FormatException)
            {
                return -1;
            }
        }

        public static string getString(object obj)
        {
            if (obj == null)
            {
                return "";
            }
            if (obj.GetType().Name == "DBNull")
            {
                return "";
            }
            return obj.ToString();
        }

        public static DateTime getTimeFormat(string formatedTime)
        {
            DateTime time2;
            if (formatedTime.Split(new char[] { '/' }).Length == 2)
            {
                formatedTime = "01/" + formatedTime;
            }
            DateTimeFormatInfo provider = new CultureInfo("vi-VN", false).DateTimeFormat;
            provider.YearMonthPattern = "MM/yy";
            try
            {
                time2 = DateTime.Parse(formatedTime, provider);
            }
            catch (FormatException)
            {
                throw new InvalidException(formatedTime);
            }
            return time2;
        }

        public static string getTimeFormatString(DateTime datetime)
        {
            DateTimeFormatInfo provider = new CultureInfo("vi-VN", false).DateTimeFormat;
            return datetime.ToString("dd/MM/yy", provider);
        }
    }
}
