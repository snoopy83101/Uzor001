using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using Common;
using Model;
using System.Transactions;
namespace BPage
{
    public class Search : Common.BPageSetting2
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string para = ReStr("para");
                switch (para)
                {


                    case "GetMyExtCount":
                        GetMyExtCount();
                        break;

                    case "GetPaiSongCount":
                        GetPaiSongCount();
                        break;

                    case "GetPinPai":
                        GetPinPai();
                        break;

                    case "GetProClass":
                        GetProClass();
                        break;

                    case "GetPro":
                        GetPro();
                        break;

                    case "GetJiFen":
                        GetJiFen();  //统计积分数据
                        break;


                    case "GetDingDan":
                        GetDingDan(); //统计订单数据
                        break;

                    case "GetXiaoShou":
                        GetXiaoShou();      //统计销售数据
                        break;

                }
            }
            catch (Exception ex)
            {

                BLL.StaticBLL.ReThrow(ex);
            }
            context.Response.End();
        }

        private void GetMyExtCount()
        {
            decimal MemberId = ReDecimal("MemberId", 0);




            var SetpNum = 3;



            StringBuilder s = new StringBuilder();

            for (int i = SetpNum - 1; i >= 0; i--)
            {
                DateTime d = DateTime.Now.AddMonths(-i);
                DateTime d2 = DateTime.Parse("" + d.Year + "-" + d.Month + "-01 00:00:00");
                DateTime d3 = d2.AddMonths(1);

                s.Append(" SELECT  Isnull( SUM(JiFenChangeNum),0) AS SumJiFenChangeNum," + d2.Month + " AS MM ");
                s.Append(" FROM    dbo.JiFenChange ");
                s.Append(" where MemberId=" + MemberId + " ");
                s.Append(" and  CreateTime >='" + d2 + "' and CreateTime<='" + d3 + "' ");
                s.Append(" and  JifenChangeTypeId=50 ");



                if (i != 0)
                {
                    s.Append(" UNION ALL ");

                }




            }

            //  s.Append(" ORDER BY MONTH(CreateTime) ");



            DataSet ds = DAL.DalComm.BackData(s.ToString());


            DataTable dt = ds.Tables[0];

            decimal Max = 0;
            decimal Step = 0;
            foreach (DataRow dr in dt.Rows)
            {

                decimal SumJiFenChangeNum = decimal.Parse(dr["SumJiFenChangeNum"].ToString());

                if (Max == 0)
                {
                    Max = SumJiFenChangeNum;
                }
                else
                {
                    if (Max < SumJiFenChangeNum)
                    {
                        Max = SumJiFenChangeNum;
                    }

                }



            }

            if (Max < 3)
            {
                Step = 1;
            }

            else
            {

                Step = Math.Ceiling(Max / 3);
            }

            Max = Max + Step;

            ReDict.Add("list", JsonHelper.ToJson(dt));

            ReDict2.Add("Max", Max.ToString());
            ReDict2.Add("Step", Step.ToString());
            ReTrue();
        }

        private void GetPaiSongCount()
        {
            StringBuilder s = new StringBuilder();
            string BranchId = ReStr("BranchId", "");

            DateTime dtm1 = ReTime("dtm1");

            DateTime dtm2 = ReTime("dtm2");
            s.Append("    SELECT PaiSongUserId,       u.RealName, SUM(Amount) AS Amount ,COUNT(0) AS DingDanNum FROM dbo.DingDanView  dd");

            s.Append("  LEFT JOIN dbo.UserView u ON dd.PaiSongUserId=u.UserId ");
            s.Append("   WHERE Status>=110 AND BranchId='" + BranchId + "' and dd.CreateTime BETWEEN '"+dtm1+"' and '"+dtm2+"'  GROUP BY PaiSongUserId ,u.RealName ");

            DataSet ds = DAL.DalComm.BackData(s.ToString());
            DataTable dt = ds.Tables[0];


            ReDict.Add("list", JsonHelper.ToJson(dt));
            ReTrue();
        }

        private void GetPinPai()
        {

            decimal MerId = ReDecimal("MerId", 0);
            DateTime dtm1 = ReTime("dtm1");
            DateTime dtm2 = ReTime("dtm2");
            int CurrentPage = ReInt("CurrentPage", 1);
            string Order = ReStr("Order", "  销售金额 DESC ");
            int PageSize = ReInt("PageSize", 20);
            string BranchId = ReStr("BranchId", "");

            if (BranchId == "")
            {
                throw new Exception("BranchId不能为空!");
            }
            StringBuilder s = new StringBuilder();
            s.Append(@"(SELECT  ISNULL(PinPaiId, 0) AS PinPaiId ,
        ISNULL(PinPaiName, '无品牌商品') AS PinPaiName ,
        ISNULL(SUM(Price * Quantity), 0) AS 销售金额,
        COUNT(0) AS 销售次数 ,
        SUM(Quantity) AS 销售数量
FROM    dbo.DingDanDetailView with(nolock)");

            s.Append(" WHERE   MerchantId = 1646 ");
            s.Append(" AND EnTime BETWEEN '" + dtm1 + "' AND '" + dtm2 + "' ");
            s.Append(" AND DingDanDetailTypeId = 1 ");
            s.Append(" and BranchId='" + BranchId + "' ");
            s.Append(" GROUP BY PinPaiId , ");
            s.Append(" PinPaiName ");
            s.Append("  ");
            s.Append(" ) TableName  ");

            s.Append("  ");
            string TableName = s.ToString();
            DataSet ds = DAL.DalComm.GetPageList(TableName, " ", Order, CurrentPage, PageSize, "*");
            RePage2(ds);

        }





        private void GetProClass()
        {
            decimal MerId = ReDecimal("MerId", 0);
            DateTime dtm1 = ReTime("dtm1");
            DateTime dtm2 = ReTime("dtm2");
            decimal ParentProductClassId = ReDecimal("ParentProductClassId", 0);
            int CurrentPage = ReInt("CurrentPage", 1);
            string Order = ReStr("Order", " 销售金额 DESC ");
            int PageSize = ReInt("PageSize", 20);
            string BranchId = ReStr("BranchId", "");
            if (BranchId == "")
            {
                throw new Exception("BranchId不能为空!");
            }
            StringBuilder s = new StringBuilder();
            s.Append(" SELECT  ProductClassId ,");
            s.Append(" ProductClassName ,");
            s.Append(" ISNULL(( SELECT SUM(Quantity * Price) AS 类别销售额 ");
            s.Append(" FROM   dbo.DingDanDetailView WITH ( NOLOCK ) ");
            s.Append(" WHERE  Status >= 110 ");
            s.Append("  and BranchId='" + BranchId + "' ");
            s.Append(" AND EnTime BETWEEN '" + dtm1 + "' AND '" + dtm2 + "' ");
            s.Append(" AND ProClassId IN ( ");
            s.Append(" SELECT  ProductClassId ");
            s.Append(" FROM    [YYO2O].[dbo].[AllProClassId2](currentProClass.ProductClassId) ) ");
            s.Append(" ), 0) AS 类别销售额 ");
            s.Append(" FROM    dbo.ProductClass currentProClass WITH ( NOLOCK ) ");
            s.Append(" WHERE   MerchantId = " + MerId + " ");
            s.Append(" AND Invalid = 0 ");
            s.Append(" AND ParentProductClassId = " + ParentProductClassId + " ");
            s.Append("    ORDER BY 类别销售额 desc ");

            DataSet ds = DAL.DalComm.BackData(s.ToString());
            DataTable dtProClass = ds.Tables[0];

            ReDict.Add("jProClass", JsonHelper.ToJson(dtProClass));
            ReTrue();


        }

        private void GetPro()
        {
            decimal MerId = ReDecimal("MerId", 0);
            DateTime dtm1 = ReTime("dtm1");
            DateTime dtm2 = ReTime("dtm2");

            int CurrentPage = ReInt("CurrentPage", 1);
            string Order = ReStr("Order", " 销售金额 DESC ");
            int PageSize = ReInt("PageSize", 20);
            string BranchId = ReStr("BranchId", "");

            if (BranchId == "")
            {
                throw new Exception("BranchId不能为空!");
            }

            StringBuilder s = new StringBuilder();
            s.Append(@"  TRUNCATE TABLE YYHD.dbo.GetPro
        INSERT INTO YYHD.dbo.GetPro

        SELECT  ProId ,
        ProName ,
        ProCode,
        SUM(Price * Quantity) AS 销售金额 ,
        COUNT(0) AS 销售次数 ,
        SUM(Quantity) AS 销售数量 
FROM    dbo.DingDanDetailView with(nolock)");
            s.Append(" WHERE   MerchantId = '" + MerId + "' ");
            s.Append("  ");
            s.Append(" AND Status >= 110 ");
            s.Append(" AND DingDanDetailTypeId = 1 ");
            s.Append(" AND EnTime BETWEEN '" + dtm1 + "' AND '" + dtm2 + "' ");
            s.Append(" and BranchId='" + BranchId + "' ");
            s.Append(" GROUP BY ProId, ProName,ProCode ");

            DAL.DalComm.ExReInt(s.ToString());

            string TableName = "YYHD.dbo.GetPro";

            DataSet ds = DAL.DalComm.GetPageList(TableName, "1=1", Order, CurrentPage, PageSize, "*");
            RePage2(ds);


        }

        private void GetJiFen()
        {
            decimal MerId = ReDecimal("MerId", 0);
            DateTime dtm1 = ReTime("dtm1");
            DateTime dtm2 = ReTime("dtm2");

            string BranchId = ReStr("BranchId", "");

            if (BranchId == "")
            {
                throw new Exception("BranchId不能为空!");
            }


            StringBuilder s = new StringBuilder();
            s.Append(@" SELECT SUM(JiFenChangeNum) as 增加积分 , CONVERT(VARCHAR(7), CreateTime, 126) AS 月份 FROM dbo.JiFenChangeView with(nolock) ");   //统计增加积分
            s.Append(" WHERE CreateTime BETWEEN '" + dtm1 + "' AND '" + dtm2 + "' ");
            s.Append(" AND MerId='" + MerId + "' ");
            s.Append(" AND JiFenChangeNum>0 ");
            s.Append(" GROUP BY CONVERT(VARCHAR(7), CreateTime, 126)");

            s.Append(@" SELECT SUM(JiFenChangeNum)*-1 as 减少积分, CONVERT(VARCHAR(7), CreateTime, 126) AS 月份 FROM dbo.JiFenChangeView with(nolock) ");   //统计减少积分
            s.Append(" WHERE CreateTime BETWEEN '" + dtm1 + "' AND '" + dtm2 + "' ");
            s.Append(" AND MerId='" + MerId + "' ");
            s.Append(" AND JiFenChangeNum<0 ");

            s.Append(" GROUP BY CONVERT(VARCHAR(7), CreateTime, 126)");
            DataSet ds = DAL.DalComm.BackData(s.ToString());
            DataTable dtAdd = ds.Tables[0];
            DataTable dtCut = ds.Tables[1];
            ReDict.Add("jAdd", JsonHelper.ToJson(dtAdd));
            ReDict.Add("jCut", JsonHelper.ToJson(dtCut));
            ReTrue();
        }

        private void GetDingDan()
        {
            decimal MerId = ReDecimal("MerId", 0);

            DateTime dtm1 = ReTime("dtm1");
            DateTime dtm2 = ReTime("dtm2");
            string BranchId = ReStr("BranchId", "");

            if (BranchId == "")
            {
                throw new Exception("BranchId不能为空!");
            }

            StringBuilder s = new StringBuilder();


            s.Append(@" SELECT 

        COUNT(DingDanId) AS 订单数量 ,
        CONVERT(VARCHAR(7), CreateTime, 126) AS 月份
FROM    dbo.DingDanView
WHERE   Status >= 100 ");
            s.Append(" and BranchId='" + BranchId + "' ");
            s.Append(" AND EnTime BETWEEN '" + dtm1 + "' AND '" + dtm2 + "' ");

            s.Append(" AND MerchantId='" + MerId + "' ");

            s.Append(" GROUP BY CONVERT(VARCHAR(7), CreateTime, 126) ");

            DataSet ds = DAL.DalComm.BackData(s.ToString());

            DataTable dtAmount = ds.Tables[0];

            ReDict.Add("jDingDan", JsonHelper.ToJson(dtAmount));
            ReTrue();


        }

        private void GetXiaoShou()
        {
            decimal MerId = ReDecimal("MerId", 0);

            DateTime dtm1 = ReTime("dtm1");
            DateTime dtm2 = ReTime("dtm2");

            string BranchId = ReStr("BranchId", "");

            if (BranchId == "")
            {
                throw new Exception("BranchId不能为空!");
            }
            StringBuilder s = new StringBuilder();


            s.Append(@" SELECT  SUM(PayAmount) AS 支付金额 ,
        SUM(Amount) AS 订单总金额 ,
        SUM(UseJiFen) AS 消耗积分 ,
        COUNT(DingDanId) AS 订单数量 ,
        CONVERT(VARCHAR(7), CreateTime, 126) AS 月份
FROM    dbo.DingDanView
WHERE   Status >= 100");
            s.Append(" and BranchId='" + BranchId + "' ");
            s.Append(" AND EnTime BETWEEN '" + dtm1 + "' AND '" + dtm2 + "' ");

            s.Append(" AND MerchantId='" + MerId + "' ");

            s.Append(" GROUP BY CONVERT(VARCHAR(7), CreateTime, 126) ");

            DataSet ds = DAL.DalComm.BackData(s.ToString());

            DataTable dtAmount = ds.Tables[0];

            ReDict.Add("jAmount", JsonHelper.ToJson(dtAmount));
            ReTrue();

        }
    }
}
