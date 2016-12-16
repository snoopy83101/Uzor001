using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using Model;
using DBTools;
using Common;
namespace DAL
{
    //OrderInfo
    public partial class OrderInfoDAL
    {

        #region  //数据操作

        /// <summary>
        /// 数据库帮助对象
        /// </summary>
        private MSSQLHelper helper = new MSSQLHelper();
        #endregion

        /// <summary>
        /// 检查是否存在
        /// </summary>
        public int ExInt(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(0) ");
            strSql.Append(" FROM  CORE.dbo.OrderInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            int i = int.Parse(helper.ExecuteSqlScalar(strSql.ToString()));
            return i;
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(OrderInfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CORE.dbo.OrderInfo (");
            strSql.Append("OrderId,OrderContacts,OrderTel,OrderAddress,Unit,ProcessLocationTypeId,OrderClassId,OrderImgId,Invalid,OrderStatusId,CreateUserId,OrderTitle,CreateTime,DoneTime,LimitTime,PlanningTime,MinQuantity,Places,OnlyPlaces,ReceivedTime,VsMemberArray,CheckQuantity,OrderContent,ReleaseTypeId,MaxExpectNum,WorkQuantity,PendingDay,OutPlaces,PlanningDay,ProcessLvId,OrderCode,ClientsCode,OrderQuantity,CarryOnQuantity,OrderWages");
            strSql.Append(") values (");
            strSql.Append("@OrderId,@OrderContacts,@OrderTel,@OrderAddress,@Unit,@ProcessLocationTypeId,@OrderClassId,@OrderImgId,@Invalid,@OrderStatusId,@CreateUserId,@OrderTitle,@CreateTime,@DoneTime,@LimitTime,@PlanningTime,@MinQuantity,@Places,@OnlyPlaces,@ReceivedTime,@VsMemberArray,@CheckQuantity,@OrderContent,@ReleaseTypeId,@MaxExpectNum,@WorkQuantity,@PendingDay,@OutPlaces,@PlanningDay,@ProcessLvId,@OrderCode,@ClientsCode,@OrderQuantity,@CarryOnQuantity,@OrderWages");
            strSql.Append(") ");

            SqlParameter[] parameters = {
                        new SqlParameter("@OrderId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@OrderContacts", SqlDbType.VarChar,50) ,
                        new SqlParameter("@OrderTel", SqlDbType.VarChar,50) ,
                        new SqlParameter("@OrderAddress", SqlDbType.VarChar,500) ,
                        new SqlParameter("@Unit", SqlDbType.VarChar,20) ,
                        new SqlParameter("@ProcessLocationTypeId", SqlDbType.Int,4) ,
                        new SqlParameter("@OrderClassId", SqlDbType.Int,4) ,
                        new SqlParameter("@OrderImgId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@Invalid", SqlDbType.Bit,1) ,
                        new SqlParameter("@OrderStatusId", SqlDbType.Int,4) ,
                        new SqlParameter("@CreateUserId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@OrderTitle", SqlDbType.VarChar,500) ,
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,
                        new SqlParameter("@DoneTime", SqlDbType.DateTime) ,
                        new SqlParameter("@LimitTime", SqlDbType.DateTime) ,
                        new SqlParameter("@PlanningTime", SqlDbType.DateTime) ,
                        new SqlParameter("@MinQuantity", SqlDbType.Decimal,9) ,
                        new SqlParameter("@Places", SqlDbType.Int,4) ,
                        new SqlParameter("@OnlyPlaces", SqlDbType.Int,4) ,
                        new SqlParameter("@ReceivedTime", SqlDbType.DateTime) ,
                        new SqlParameter("@VsMemberArray", SqlDbType.VarChar,-1) ,
                        new SqlParameter("@CheckQuantity", SqlDbType.Decimal,9) ,
                        new SqlParameter("@OrderContent", SqlDbType.VarChar,-1) ,
                        new SqlParameter("@ReleaseTypeId", SqlDbType.Int,4) ,
                        new SqlParameter("@MaxExpectNum", SqlDbType.Decimal,9) ,
                        new SqlParameter("@WorkQuantity", SqlDbType.Decimal,9) ,
                        new SqlParameter("@PendingDay", SqlDbType.Int,4) ,
                        new SqlParameter("@OutPlaces", SqlDbType.Int,4) ,
                        new SqlParameter("@PlanningDay", SqlDbType.Int,4) ,
                        new SqlParameter("@ProcessLvId", SqlDbType.Int,4) ,
                        new SqlParameter("@OrderCode", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ClientsCode", SqlDbType.VarChar,50) ,
                        new SqlParameter("@OrderQuantity", SqlDbType.Decimal,9) ,
                        new SqlParameter("@CarryOnQuantity", SqlDbType.Decimal,9) ,
                        new SqlParameter("@OrderWages", SqlDbType.Decimal,9)

            };

            parameters[0].Value = model.OrderId;
            parameters[1].Value = model.OrderContacts;
            parameters[2].Value = model.OrderTel;
            parameters[3].Value = model.OrderAddress;
            parameters[4].Value = model.Unit;
            parameters[5].Value = model.ProcessLocationTypeId;
            parameters[6].Value = model.OrderClassId;
            parameters[7].Value = model.OrderImgId;
            parameters[8].Value = model.Invalid;
            parameters[9].Value = model.OrderStatusId;
            parameters[10].Value = model.CreateUserId;
            parameters[11].Value = model.OrderTitle;
            parameters[12].Value = model.CreateTime;
            parameters[13].Value = model.DoneTime;
            parameters[14].Value = model.LimitTime;
            parameters[15].Value = model.PlanningTime;
            parameters[16].Value = model.MinQuantity;
            parameters[17].Value = model.Places;
            parameters[18].Value = model.OnlyPlaces;
            parameters[19].Value = model.ReceivedTime;
            parameters[20].Value = model.VsMemberArray;
            parameters[21].Value = model.CheckQuantity;
            parameters[22].Value = model.OrderContent;
            parameters[23].Value = model.ReleaseTypeId;
            parameters[24].Value = model.MaxExpectNum;
            parameters[25].Value = model.WorkQuantity;
            parameters[26].Value = model.PendingDay;
            parameters[27].Value = model.OutPlaces;
            parameters[28].Value = model.PlanningDay;
            parameters[29].Value = model.ProcessLvId;
            parameters[30].Value = model.OrderCode;
            parameters[31].Value = model.ClientsCode;
            parameters[32].Value = model.OrderQuantity;
            parameters[33].Value = model.CarryOnQuantity;
            parameters[34].Value = model.OrderWages;

            bool result = false;
            try
            {


                helper.ExecSqlReInt(strSql.ToString(), parameters);


                result = true;
            }
            catch (Exception ex)
            {

                this.helper.Close();
                throw ex;
            }
            finally
            {

            }
            return result;
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(OrderInfoModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CORE.dbo.OrderInfo set ");

            strSql.Append(" OrderId = @OrderId , ");
            strSql.Append(" OrderContacts = @OrderContacts , ");
            strSql.Append(" OrderTel = @OrderTel , ");
            strSql.Append(" OrderAddress = @OrderAddress , ");
            strSql.Append(" Unit = @Unit , ");
            strSql.Append(" ProcessLocationTypeId = @ProcessLocationTypeId , ");
            strSql.Append(" OrderClassId = @OrderClassId , ");
            strSql.Append(" OrderImgId = @OrderImgId , ");
            strSql.Append(" Invalid = @Invalid , ");
            strSql.Append(" OrderStatusId = @OrderStatusId , ");
            strSql.Append(" CreateUserId = @CreateUserId , ");
            strSql.Append(" OrderTitle = @OrderTitle , ");
            strSql.Append(" CreateTime = @CreateTime , ");
            strSql.Append(" DoneTime = @DoneTime , ");
            strSql.Append(" LimitTime = @LimitTime , ");
            strSql.Append(" PlanningTime = @PlanningTime , ");
            strSql.Append(" MinQuantity = @MinQuantity , ");
            strSql.Append(" Places = @Places , ");
            strSql.Append(" OnlyPlaces = @OnlyPlaces , ");
            strSql.Append(" ReceivedTime = @ReceivedTime , ");
            strSql.Append(" VsMemberArray = @VsMemberArray , ");
            strSql.Append(" CheckQuantity = @CheckQuantity , ");
            strSql.Append(" OrderContent = @OrderContent , ");
            strSql.Append(" ReleaseTypeId = @ReleaseTypeId , ");
            strSql.Append(" MaxExpectNum = @MaxExpectNum , ");
            strSql.Append(" WorkQuantity = @WorkQuantity , ");
            strSql.Append(" PendingDay = @PendingDay , ");
            strSql.Append(" OutPlaces = @OutPlaces , ");
            strSql.Append(" PlanningDay = @PlanningDay , ");
            strSql.Append(" ProcessLvId = @ProcessLvId , ");
            strSql.Append(" OrderCode = @OrderCode , ");
            strSql.Append(" ClientsCode = @ClientsCode , ");
            strSql.Append(" OrderQuantity = @OrderQuantity , ");
            strSql.Append(" CarryOnQuantity = @CarryOnQuantity , ");
            strSql.Append(" OrderWages = @OrderWages  ");
            strSql.Append(" where OrderId=@OrderId  ");

            SqlParameter[] parameters = {
                        new SqlParameter("@OrderId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@OrderContacts", SqlDbType.VarChar,50) ,
                        new SqlParameter("@OrderTel", SqlDbType.VarChar,50) ,
                        new SqlParameter("@OrderAddress", SqlDbType.VarChar,500) ,
                        new SqlParameter("@Unit", SqlDbType.VarChar,20) ,
                        new SqlParameter("@ProcessLocationTypeId", SqlDbType.Int,4) ,
                        new SqlParameter("@OrderClassId", SqlDbType.Int,4) ,
                        new SqlParameter("@OrderImgId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@Invalid", SqlDbType.Bit,1) ,
                        new SqlParameter("@OrderStatusId", SqlDbType.Int,4) ,
                        new SqlParameter("@CreateUserId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@OrderTitle", SqlDbType.VarChar,500) ,
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,
                        new SqlParameter("@DoneTime", SqlDbType.DateTime) ,
                        new SqlParameter("@LimitTime", SqlDbType.DateTime) ,
                        new SqlParameter("@PlanningTime", SqlDbType.DateTime) ,
                        new SqlParameter("@MinQuantity", SqlDbType.Decimal,9) ,
                        new SqlParameter("@Places", SqlDbType.Int,4) ,
                        new SqlParameter("@OnlyPlaces", SqlDbType.Int,4) ,
                        new SqlParameter("@ReceivedTime", SqlDbType.DateTime) ,
                        new SqlParameter("@VsMemberArray", SqlDbType.VarChar,-1) ,
                        new SqlParameter("@CheckQuantity", SqlDbType.Decimal,9) ,
                        new SqlParameter("@OrderContent", SqlDbType.VarChar,-1) ,
                        new SqlParameter("@ReleaseTypeId", SqlDbType.Int,4) ,
                        new SqlParameter("@MaxExpectNum", SqlDbType.Decimal,9) ,
                        new SqlParameter("@WorkQuantity", SqlDbType.Decimal,9) ,
                        new SqlParameter("@PendingDay", SqlDbType.Int,4) ,
                        new SqlParameter("@OutPlaces", SqlDbType.Int,4) ,
                        new SqlParameter("@PlanningDay", SqlDbType.Int,4) ,
                        new SqlParameter("@ProcessLvId", SqlDbType.Int,4) ,
                        new SqlParameter("@OrderCode", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ClientsCode", SqlDbType.VarChar,50) ,
                        new SqlParameter("@OrderQuantity", SqlDbType.Decimal,9) ,
                        new SqlParameter("@CarryOnQuantity", SqlDbType.Decimal,9) ,
                        new SqlParameter("@OrderWages", SqlDbType.Decimal,9)

            };

            parameters[0].Value = model.OrderId;
            parameters[1].Value = model.OrderContacts;
            parameters[2].Value = model.OrderTel;
            parameters[3].Value = model.OrderAddress;
            parameters[4].Value = model.Unit;
            parameters[5].Value = model.ProcessLocationTypeId;
            parameters[6].Value = model.OrderClassId;
            parameters[7].Value = model.OrderImgId;
            parameters[8].Value = model.Invalid;
            parameters[9].Value = model.OrderStatusId;
            parameters[10].Value = model.CreateUserId;
            parameters[11].Value = model.OrderTitle;
            parameters[12].Value = model.CreateTime;
            parameters[13].Value = model.DoneTime;
            parameters[14].Value = model.LimitTime;
            parameters[15].Value = model.PlanningTime;
            parameters[16].Value = model.MinQuantity;
            parameters[17].Value = model.Places;
            parameters[18].Value = model.OnlyPlaces;
            parameters[19].Value = model.ReceivedTime;
            parameters[20].Value = model.VsMemberArray;
            parameters[21].Value = model.CheckQuantity;
            parameters[22].Value = model.OrderContent;
            parameters[23].Value = model.ReleaseTypeId;
            parameters[24].Value = model.MaxExpectNum;
            parameters[25].Value = model.WorkQuantity;
            parameters[26].Value = model.PendingDay;
            parameters[27].Value = model.OutPlaces;
            parameters[28].Value = model.PlanningDay;
            parameters[29].Value = model.ProcessLvId;
            parameters[30].Value = model.OrderCode;
            parameters[31].Value = model.ClientsCode;
            parameters[32].Value = model.OrderQuantity;
            parameters[33].Value = model.CarryOnQuantity;
            parameters[34].Value = model.OrderWages; try
            {//异常处理
                reCount = this.helper.ExecSqlReInt(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {

                this.helper.Close();
                throw ex;
            }
            if (reCount <= 0)
            {
                reValue = false;
            }
            return reValue;
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public OrderInfoModel GetModel(string OrderId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select OrderId, OrderContacts, OrderTel, OrderAddress, Unit, ProcessLocationTypeId, OrderClassId, OrderImgId, Invalid, OrderStatusId, CreateUserId, OrderTitle, CreateTime, DoneTime, LimitTime, PlanningTime, MinQuantity, Places, OnlyPlaces, ReceivedTime, VsMemberArray, CheckQuantity, OrderContent, ReleaseTypeId, MaxExpectNum, WorkQuantity, PendingDay, OutPlaces, PlanningDay, ProcessLvId, OrderCode, ClientsCode, OrderQuantity, CarryOnQuantity, OrderWages  ");
            strSql.Append("  from CORE.dbo.OrderInfo ");
            strSql.Append(" where OrderId=@OrderId ");
            SqlParameter[] parameters = {
                    new SqlParameter("@OrderId", SqlDbType.VarChar,50)          };
            parameters[0].Value = OrderId;


            OrderInfoModel model = new OrderInfoModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.OrderId = ds.Tables[0].Rows[0]["OrderId"].ToString();
                model.OrderContacts = ds.Tables[0].Rows[0]["OrderContacts"].ToString();
                model.OrderTel = ds.Tables[0].Rows[0]["OrderTel"].ToString();
                model.OrderAddress = ds.Tables[0].Rows[0]["OrderAddress"].ToString();
                model.Unit = ds.Tables[0].Rows[0]["Unit"].ToString();
                if (ds.Tables[0].Rows[0]["ProcessLocationTypeId"].ToString() != "")
                {
                    model.ProcessLocationTypeId = int.Parse(ds.Tables[0].Rows[0]["ProcessLocationTypeId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["OrderClassId"].ToString() != "")
                {
                    model.OrderClassId = int.Parse(ds.Tables[0].Rows[0]["OrderClassId"].ToString());
                }
                model.OrderImgId = ds.Tables[0].Rows[0]["OrderImgId"].ToString();
                if (ds.Tables[0].Rows[0]["Invalid"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Invalid"].ToString() == "1") || (ds.Tables[0].Rows[0]["Invalid"].ToString().ToLower() == "true"))
                    {
                        model.Invalid = true;
                    }
                    else
                    {
                        model.Invalid = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["OrderStatusId"].ToString() != "")
                {
                    model.OrderStatusId = int.Parse(ds.Tables[0].Rows[0]["OrderStatusId"].ToString());
                }
                model.CreateUserId = ds.Tables[0].Rows[0]["CreateUserId"].ToString();
                model.OrderTitle = ds.Tables[0].Rows[0]["OrderTitle"].ToString();
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DoneTime"].ToString() != "")
                {
                    model.DoneTime = DateTime.Parse(ds.Tables[0].Rows[0]["DoneTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LimitTime"].ToString() != "")
                {
                    model.LimitTime = DateTime.Parse(ds.Tables[0].Rows[0]["LimitTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PlanningTime"].ToString() != "")
                {
                    model.PlanningTime = DateTime.Parse(ds.Tables[0].Rows[0]["PlanningTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MinQuantity"].ToString() != "")
                {
                    model.MinQuantity = decimal.Parse(ds.Tables[0].Rows[0]["MinQuantity"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Places"].ToString() != "")
                {
                    model.Places = int.Parse(ds.Tables[0].Rows[0]["Places"].ToString());
                }
                if (ds.Tables[0].Rows[0]["OnlyPlaces"].ToString() != "")
                {
                    model.OnlyPlaces = int.Parse(ds.Tables[0].Rows[0]["OnlyPlaces"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ReceivedTime"].ToString() != "")
                {
                    model.ReceivedTime = DateTime.Parse(ds.Tables[0].Rows[0]["ReceivedTime"].ToString());
                }
                model.VsMemberArray = ds.Tables[0].Rows[0]["VsMemberArray"].ToString();
                if (ds.Tables[0].Rows[0]["CheckQuantity"].ToString() != "")
                {
                    model.CheckQuantity = decimal.Parse(ds.Tables[0].Rows[0]["CheckQuantity"].ToString());
                }
                model.OrderContent = ds.Tables[0].Rows[0]["OrderContent"].ToString();
                if (ds.Tables[0].Rows[0]["ReleaseTypeId"].ToString() != "")
                {
                    model.ReleaseTypeId = int.Parse(ds.Tables[0].Rows[0]["ReleaseTypeId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MaxExpectNum"].ToString() != "")
                {
                    model.MaxExpectNum = decimal.Parse(ds.Tables[0].Rows[0]["MaxExpectNum"].ToString());
                }
                if (ds.Tables[0].Rows[0]["WorkQuantity"].ToString() != "")
                {
                    model.WorkQuantity = decimal.Parse(ds.Tables[0].Rows[0]["WorkQuantity"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PendingDay"].ToString() != "")
                {
                    model.PendingDay = int.Parse(ds.Tables[0].Rows[0]["PendingDay"].ToString());
                }
                if (ds.Tables[0].Rows[0]["OutPlaces"].ToString() != "")
                {
                    model.OutPlaces = int.Parse(ds.Tables[0].Rows[0]["OutPlaces"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PlanningDay"].ToString() != "")
                {
                    model.PlanningDay = int.Parse(ds.Tables[0].Rows[0]["PlanningDay"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ProcessLvId"].ToString() != "")
                {
                    model.ProcessLvId = int.Parse(ds.Tables[0].Rows[0]["ProcessLvId"].ToString());
                }
                model.OrderCode = ds.Tables[0].Rows[0]["OrderCode"].ToString();
                model.ClientsCode = ds.Tables[0].Rows[0]["ClientsCode"].ToString();
                if (ds.Tables[0].Rows[0]["OrderQuantity"].ToString() != "")
                {
                    model.OrderQuantity = decimal.Parse(ds.Tables[0].Rows[0]["OrderQuantity"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CarryOnQuantity"].ToString() != "")
                {
                    model.CarryOnQuantity = decimal.Parse(ds.Tables[0].Rows[0]["CarryOnQuantity"].ToString());
                }
                if (ds.Tables[0].Rows[0]["OrderWages"].ToString() != "")
                {
                    model.OrderWages = decimal.Parse(ds.Tables[0].Rows[0]["OrderWages"].ToString());
                }

                return model;
            }
            else
            {
                return model;
            }
        }



        /// <summary>
        /// 删除duo条数据
        /// </summary>
        public bool DeleteList(string strWhere)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from CORE.dbo.OrderInfo ");
            strSql.Append(" where " + strWhere);
            try
            {//异常处理
                reCount = this.helper.ExecSqlReInt(strSql.ToString());
            }
            catch (Exception ex)
            {

                this.helper.Close();
                throw ex;
            }
            if (reCount <= 0)
            {
                reValue = false;
            }
            return reValue;
        }


        /// <summary>
        /// 获得fenye数据列表
        /// </summary>
        public DataSet GetPageList(string strWhere, string Order, int currentpage, int pagesize, string col)
        {
            DataSet ds = null;
            string[] fenyeParmName = new string[] { "TableName", "ReFieldsStr", "OrderString", "WhereString", "PageSize", "PageIndex", "TotalRecord" };
            object[] fenyeParmValue = new object[] { "CORE.dbo.OrderView  WITH(NOLOCK)", col, Order, strWhere, pagesize, currentpage, 0 };
            ds = helper.ExecProc_ReDs("dbo.fenye2", fenyeParmName, fenyeParmValue);
            ds = Common.DataSetting.DataPageSetting(ds, pagesize, currentpage);
            return ds;


        }



        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetPageList(string strWhere, int currentpage, int pagesize, string cols)
        {
            if (cols == "")
            {
                cols = " * ";
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 3000 " + cols + " ");
            strSql.Append(" FROM CORE.dbo.OrderInfo  WITH(NOLOCK)  ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            //strSql.Append(" ORDER BY CREATE_TIME DESC ");
            DataSet ds = null;
            string[] fenyeParmName = new string[] { "sqlstr", "currentpage", "pagesize" };
            object[] fenyeParmValue = new object[] { strSql.ToString(), currentpage, pagesize };
            ds = helper.ExecProc_ReDs("CORE.dbo.fenye", fenyeParmName, fenyeParmValue);
            return ds;


        }



        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM CORE.dbo.OrderInfo  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return helper.ExecSqlReDs(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM CORE.dbo.OrderInfo  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

