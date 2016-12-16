using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Transactions;
using LitJson;


namespace BLL
{



    /// <summary>
    /// 优做交易公式门面类
    /// </summary>
    public class FormulaBLL
    {






        /// <summary>
        /// 取得交货时间
        /// </summary>
        /// <param name="OrderQuantity">订单总量</param>
        /// <param name="OrderExpectDay1">第一天产量</param>
        /// <param name="OrderExpectDay2">第二天产量</param>
        /// <param name="OrderExpectDay3">第三天产量</param>
        /// <param name="Places">总名额</param>
        /// <param name="ReceivedTime">领取裁片时间</param>
        /// <returns></returns>
        public static JsonData PlanningTime(decimal OrderQuantity, decimal OrderExpectDay1, decimal OrderExpectDay2, decimal OrderExpectDay3, int Places, DateTime ReceivedTime)
        {

            decimal d = 2 + (OrderQuantity - ((OrderExpectDay1 + OrderExpectDay2) * Places)) / (OrderExpectDay3 * Places) + 1;

            JsonData j = new JsonData();

            j["PlanningDay2"] = d.ToString();


            int PlanningDay = int.Parse(Math.Ceiling(Convert.ToDecimal(d)).ToString());

            j["PlanningDay"] = PlanningDay;  //向上取整

            DateTime PlanningTime = ReceivedTime.AddDays(PlanningDay);

            j["PlanningTime"] = PlanningTime.ToString("yyyy-MM-dd HH:mm:ss");

            return j;
        }


        /// <summary>
        /// 取得交货时间
        /// </summary>
        /// <param name="PlanningDay"></param>
        /// <param name="ReceivedTime"></param>
        /// <returns></returns>
        public static DateTime PlanningTime(int PlanningDay, DateTime ReceivedTime)
        {


            DateTime PlanningTime = ReceivedTime.AddDays(PlanningDay);


            return PlanningTime;



        }


    



        /// <summary>
        /// 计算每人领取裁片数量
        /// </summary>
        /// <param name="OrderQuantity"></param>
        /// <param name="Places"></param>
        /// <returns></returns>
        public static decimal MinQuantity(decimal OrderQuantity, int Places)
        {

            if (Places == 0)
            {
                return 0;
            }


            return transmitNum(OrderQuantity / Places);


        }




        /// <summary>
        /// 小五取5,大5进1
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static decimal transmitNum(decimal d)
        {

            decimal 整除 = Math.Floor(d / 5);

            decimal 余数 = d % 5;


            if (余数 > 0)
            {
                return 整除 * 5 + 5;
            }
            else
            {
                return 整除 * 5;
            }



        }



    }


}