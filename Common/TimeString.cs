using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Common
{
    public class TimeString
    {

        /// <summary>
        /// 获取两个时间相差的天数
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public static int dateDiff(DateTime dateStart, DateTime dateEnd)
        {
            DateTime start = Convert.ToDateTime(dateStart.ToShortDateString());
            DateTime end = Convert.ToDateTime(dateEnd.ToShortDateString());

            TimeSpan sp = end.Subtract(start);

            return sp.Days;
        }

        public static string GetNow_ff()
        {
            Random ro = new Random(5);
            return DateTime.Now.ToString("yyMMddhhmmssfff") + Common.StringPlus.GetRandomNext(5);
        }
        /// <summary>
        /// 获取不会重复的编码
        /// </summary>
        /// <returns></returns>
        public static string GetNowDifString()
        {
            string s = GetString(DateTime.Now);
            return s + "" + StringPlus.GetRandomNext(5);
        }
        public static string GetString(DateTime dtm)
        {

            return dtm.ToString("yyMMddhhmmssff");

        }

        public static string GetNowDateStr()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }
        public static string Get30DayAgoDateStr()
        {
            DateTime dtm = DateTime.Now.AddMonths(-1);
            return dtm.ToString("yyyy-MM-dd");
        }
        public static string Get7DayAgoDateStr()
        {
            DateTime dtm = DateTime.Now.AddDays(-7);
            return dtm.ToString("yyyy-MM-dd");
        }
        /// <summary>
        /// 生成类似/2010/11/这种的路径，前后无需加“/”
        /// </summary>
        /// <returns></returns>
        public static string GetYM()
        {
            return "/" + DateTime.Now.ToString("yyyy") + "/" + DateTime.Now.ToString("MM") + "/";
        }
        public static string ShuChuString()
        {
            DateTime dt = DateTime.Now.ToUniversalTime();
            string tm = string.Empty;
            tm = dt.Year + "年" + dt.Month + "月" + dt.Day + "日";

            return tm;
        }

        /// <summary>
        /// 一天中的第一天
        /// </summary>
        /// <param name="year"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        public static DateTime GetFristDayInYear(string year)
        {


            DateTime now = DateTime.Parse(year + "-01-01");
            return now;
        }
        public static DateTime GetFristDayInYear()
        {

            string year = DateTime.Now.Year.ToString();
            return GetFristDayInYear(year);
        }

        /// <summary>
        /// 一年中最后一天
        /// </summary>
        /// <param name="year"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        public static DateTime GetLastDayInYear(string year)
        {

            DateTime now = DateTime.Parse(year + "-12-31 23:59:59");
            return now;
        }


        public static DateTime GetLastDayInYear()
        {
            string year = DateTime.Now.Year.ToString();
            return GetLastDayInYear(year);
        }


        /// <summary>
        /// 获取一个月的第一天
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public static DateTime GetFristDayInMonth(string year, string m)
        {
            if (m.Length < 2)
            {
                m = "0" + m;
            }

            DateTime now = DateTime.Parse(year + "-" + m + "-01");
            return now;
        }

        /// <summary>
        /// 获取一个月的最后一天
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public static DateTime GetLastDayInMonth(string year, string m)
        {
            if (m.Length < 2)
            {
                m = "0" + m;
            }
            DateTime now = DateTime.Parse(year + "-" + m + "-01");
            DateTime d2 = now.AddMonths(1).AddDays(-1);
            return d2;
        }


        public static string ConvertDateTime(DateTime date)
        {

            return ConvertDateTime(date, DateTime.Now, "MM-dd");
        }

        /// <summary>
        /// 把两个时间差，三天内的时间用今天，昨天，前天表示，后跟时间，无日期
        /// </summary>
        /// <param name=\"date\">被比较的时间</param>
        /// <param name=\"currentDateTime\">目标时间</param>
        /// <param name=\"timeFormat\">呈现的时间格式</param>
        /// <returns></returns>
        public static string ConvertDateTime(DateTime date, DateTime currentDateTime, string timeFormat)
        {
            string result = string.Empty;
            if (currentDateTime.Year == date.Year && currentDateTime.Month == date.Month)//如果date和当前时间年份或者月份不一致，则直接返回\"yyyy-MM-dd HH:mm\"格式日期
            {
                if (DateDiff(TimeEnums.HOUR, date, currentDateTime) <= 10)//如果date和当前时间间隔在10小时内(曾经是3小时)
                {
                    if (DateDiff(TimeEnums.HOUR, date, currentDateTime) > 0)
                        return DateDiff(TimeEnums.HOUR, date, currentDateTime) + "小时前";

                    if (DateDiff(TimeEnums.MINUTE, date, currentDateTime) > 0)
                        return DateDiff(TimeEnums.MINUTE, date, currentDateTime) + "分钟前";

                    if (DateDiff(TimeEnums.SECOND, date, currentDateTime) >= 0)
                        return DateDiff(TimeEnums.SECOND, date, currentDateTime) + "秒前";
                    else
                        return "刚才";
                }
                else
                {
                    switch (currentDateTime.Day - date.Day)
                    {
                        case 0:
                            //result = "今天 " + date.ToString("HH") + ":" + date.ToString("mm");
                            result = "今天";
                            break;
                        case 1:
                            // result = "昨天" + date.ToString("HH") + ":" + date.ToString("mm");
                            result = "昨天";
                            break;
                        case 2:
                            // result = "前天" + date.ToString("HH") + ":" + date.ToString("mm");
                            result = "前天";
                            break;
                        default:
                            result = date.ToString(timeFormat);
                            break;
                    }
                }
            }
            else
                result = date.ToString(timeFormat);
            return result;
        }
        public enum TimeEnums { HOUR, MINUTE, SECOND, DAY, WEEK, MONTH, QUARTER, YEAR };
        /// <summary>
        /// 两个时间的差值，可以为秒，小时，天，分钟
        /// </summary>
        /// <param name=\"Interval\">需要得到的时差方式</param>
        /// <param name=\"StartDate\">起始时间</param>
        /// <param name=\"EndDate\">结束时间</param>
        /// <returns></returns>
        public static long DateDiff(TimeEnums Interval, DateTime StartDate, DateTime EndDate)
        {
            long lngDateDiffValue = 0;
            System.TimeSpan TS = new System.TimeSpan(EndDate.Ticks - StartDate.Ticks);
            switch (Interval)
            {
                case TimeEnums.SECOND:
                    lngDateDiffValue = (long)TS.TotalSeconds;
                    break;
                case TimeEnums.MINUTE:
                    lngDateDiffValue = (long)TS.TotalMinutes;
                    break;
                case TimeEnums.HOUR:
                    lngDateDiffValue = (long)TS.TotalHours;
                    break;
                case TimeEnums.DAY:
                    lngDateDiffValue = (long)TS.Days;
                    break;
                case TimeEnums.WEEK:
                    lngDateDiffValue = (long)(TS.Days / 7);
                    break;
                case TimeEnums.MONTH:
                    lngDateDiffValue = (long)(TS.Days / 30);
                    break;
                case TimeEnums.QUARTER:
                    lngDateDiffValue = (long)((TS.Days / 30) / 3);
                    break;
                case TimeEnums.YEAR:
                    lngDateDiffValue = (long)(TS.Days / 365);
                    break;
            }
            return (lngDateDiffValue);



        }

        public static string XingQi(DateTime dtm)
        {

            return System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(dtm.DayOfWeek);
        }




        /// <summary>
        /// 小时取整, 如果分钟大于i,则小时进制一位
        /// </summary>
        /// <param name="dtm"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public static DateTime Rounding_hh(DateTime dtm, int i)
        {

            if (dtm.Minute > i)
            {
                dtm.AddHours(1);

            }

            string str = dtm.ToString("yyyy-MM-dd");
            str = str + " " + dtm.Hour + ":00:00";
            return DateTime.Parse(str);

        }
        //该片段来自于http://www.codesnippet.cn/detail/08112012159.html

    }
}
