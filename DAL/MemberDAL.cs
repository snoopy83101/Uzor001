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
    //Member
    public partial class MemberDAL
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
            strSql.Append(" FROM  CORE.dbo.Member ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            int i = int.Parse(helper.ExecuteSqlScalar(strSql.ToString()));
            return i;
        }

        public int GetMemberId(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MemberId ");
            strSql.Append(" FROM  CORE.dbo.Member with(nolock) ");
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
        public bool Add(MemberModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CORE.dbo.Member (");
            strSql.Append("Memo,WxPtId,WxOpenId,Invalid,Email,Phone,CreateTime,LastTime,MemberLv,Status,MemberCode,MerId,PicImgId,Integral,MemoName,NickName,LastBuyTime,IsPromoter,ExtMemberId,ExtMemberLv,LastSiteId,ProcessLvId,ProcessLvStatusId,SfzNo,SfzImg1,Sex,SfzImg2,AreaId,Address,Amount,OldAmount,OrderWorkMaxNum,MaxOrderPlanningTime,CheckOrderNum,TeamId,TeamLvId,Pwd,PwdMd5,MemberName,RealName,Birthday");
            strSql.Append(") values (");
            strSql.Append("@Memo,@WxPtId,@WxOpenId,@Invalid,@Email,@Phone,@CreateTime,@LastTime,@MemberLv,@Status,@MemberCode,@MerId,@PicImgId,@Integral,@MemoName,@NickName,@LastBuyTime,@IsPromoter,@ExtMemberId,@ExtMemberLv,@LastSiteId,@ProcessLvId,@ProcessLvStatusId,@SfzNo,@SfzImg1,@Sex,@SfzImg2,@AreaId,@Address,@Amount,@OldAmount,@OrderWorkMaxNum,@MaxOrderPlanningTime,@CheckOrderNum,@TeamId,@TeamLvId,@Pwd,@PwdMd5,@MemberName,@RealName,@Birthday");
            strSql.Append(") ");
            strSql.Append(";");
            SqlParameter[] parameters = {
                        new SqlParameter("@Memo", SqlDbType.VarChar,200) ,
                        new SqlParameter("@WxPtId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@WxOpenId", SqlDbType.VarChar,80) ,
                        new SqlParameter("@Invalid", SqlDbType.Bit,1) ,
                        new SqlParameter("@Email", SqlDbType.VarChar,50) ,
                        new SqlParameter("@Phone", SqlDbType.VarChar,15) ,
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,
                        new SqlParameter("@LastTime", SqlDbType.DateTime) ,
                        new SqlParameter("@MemberLv", SqlDbType.Int,4) ,
                        new SqlParameter("@Status", SqlDbType.Int,4) ,
                        new SqlParameter("@MemberCode", SqlDbType.VarChar,8) ,
                        new SqlParameter("@MerId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@PicImgId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@Integral", SqlDbType.Decimal,9) ,
                        new SqlParameter("@MemoName", SqlDbType.VarChar,100) ,
                        new SqlParameter("@NickName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@LastBuyTime", SqlDbType.DateTime) ,
                        new SqlParameter("@IsPromoter", SqlDbType.Bit,1) ,
                        new SqlParameter("@ExtMemberId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@ExtMemberLv", SqlDbType.Int,4) ,
                        new SqlParameter("@LastSiteId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@ProcessLvId", SqlDbType.Int,4) ,
                        new SqlParameter("@ProcessLvStatusId", SqlDbType.Int,4) ,
                        new SqlParameter("@SfzNo", SqlDbType.VarChar,50) ,
                        new SqlParameter("@SfzImg1", SqlDbType.VarChar,50) ,
                        new SqlParameter("@Sex", SqlDbType.VarChar,4) ,
                        new SqlParameter("@SfzImg2", SqlDbType.VarChar,50) ,
                        new SqlParameter("@AreaId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@Address", SqlDbType.VarChar,1000) ,
                        new SqlParameter("@Amount", SqlDbType.Decimal,9) ,
                        new SqlParameter("@OldAmount", SqlDbType.Decimal,9) ,
                        new SqlParameter("@OrderWorkMaxNum", SqlDbType.Int,4) ,
                        new SqlParameter("@MaxOrderPlanningTime", SqlDbType.DateTime) ,
                        new SqlParameter("@CheckOrderNum", SqlDbType.Decimal,9) ,
                        new SqlParameter("@TeamId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@TeamLvId", SqlDbType.Int,4) ,
                        new SqlParameter("@Pwd", SqlDbType.VarChar,20) ,
                        new SqlParameter("@PwdMd5", SqlDbType.VarChar,50) ,
                        new SqlParameter("@MemberName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@RealName", SqlDbType.VarChar,20) ,
                        new SqlParameter("@Birthday", SqlDbType.DateTime)

            };

            parameters[0].Value = model.Memo;
            parameters[1].Value = model.WxPtId;
            parameters[2].Value = model.WxOpenId;
            parameters[3].Value = model.Invalid;
            parameters[4].Value = model.Email;
            parameters[5].Value = model.Phone;
            parameters[6].Value = model.CreateTime;
            parameters[7].Value = model.LastTime;
            parameters[8].Value = model.MemberLv;
            parameters[9].Value = model.Status;
            parameters[10].Value = model.MemberCode;
            parameters[11].Value = model.MerId;
            parameters[12].Value = model.PicImgId;
            parameters[13].Value = model.Integral;
            parameters[14].Value = model.MemoName;
            parameters[15].Value = model.NickName;
            parameters[16].Value = model.LastBuyTime;
            parameters[17].Value = model.IsPromoter;
            parameters[18].Value = model.ExtMemberId;
            parameters[19].Value = model.ExtMemberLv;
            parameters[20].Value = model.LastSiteId;
            parameters[21].Value = model.ProcessLvId;
            parameters[22].Value = model.ProcessLvStatusId;
            parameters[23].Value = model.SfzNo;
            parameters[24].Value = model.SfzImg1;
            parameters[25].Value = model.Sex;
            parameters[26].Value = model.SfzImg2;
            parameters[27].Value = model.AreaId;
            parameters[28].Value = model.Address;
            parameters[29].Value = model.Amount;
            parameters[30].Value = model.OldAmount;
            parameters[31].Value = model.OrderWorkMaxNum;
            parameters[32].Value = model.MaxOrderPlanningTime;
            parameters[33].Value = model.CheckOrderNum;
            parameters[34].Value = model.TeamId;
            parameters[35].Value = model.TeamLvId;
            parameters[36].Value = model.Pwd;
            parameters[37].Value = model.PwdMd5;
            parameters[38].Value = model.MemberName;
            parameters[39].Value = model.RealName;
            parameters[40].Value = model.Birthday;

            bool result = false;
            try
            {


                model.MemberId = decimal.Parse(helper.ExecuteNonQueryBackId(strSql.ToString(), "MemberId", parameters));


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
        public bool Update(MemberModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CORE.dbo.Member set ");

            strSql.Append(" Memo = @Memo , ");
            strSql.Append(" WxPtId = @WxPtId , ");
            strSql.Append(" WxOpenId = @WxOpenId , ");
            strSql.Append(" Invalid = @Invalid , ");
            strSql.Append(" Email = @Email , ");
            strSql.Append(" Phone = @Phone , ");
            strSql.Append(" CreateTime = @CreateTime , ");
            strSql.Append(" LastTime = @LastTime , ");
            strSql.Append(" MemberLv = @MemberLv , ");
            strSql.Append(" Status = @Status , ");
            strSql.Append(" MemberCode = @MemberCode , ");
            strSql.Append(" MerId = @MerId , ");
            strSql.Append(" PicImgId = @PicImgId , ");
            strSql.Append(" Integral = @Integral , ");
            strSql.Append(" MemoName = @MemoName , ");
            strSql.Append(" NickName = @NickName , ");
            strSql.Append(" LastBuyTime = @LastBuyTime , ");
            strSql.Append(" IsPromoter = @IsPromoter , ");
            strSql.Append(" ExtMemberId = @ExtMemberId , ");
            strSql.Append(" ExtMemberLv = @ExtMemberLv , ");
            strSql.Append(" LastSiteId = @LastSiteId , ");
            strSql.Append(" ProcessLvId = @ProcessLvId , ");
            strSql.Append(" ProcessLvStatusId = @ProcessLvStatusId , ");
            strSql.Append(" SfzNo = @SfzNo , ");
            strSql.Append(" SfzImg1 = @SfzImg1 , ");
            strSql.Append(" Sex = @Sex , ");
            strSql.Append(" SfzImg2 = @SfzImg2 , ");
            strSql.Append(" AreaId = @AreaId , ");
            strSql.Append(" Address = @Address , ");
            strSql.Append(" Amount = @Amount , ");
            strSql.Append(" OldAmount = @OldAmount , ");
            strSql.Append(" OrderWorkMaxNum = @OrderWorkMaxNum , ");
            strSql.Append(" MaxOrderPlanningTime = @MaxOrderPlanningTime , ");
            strSql.Append(" CheckOrderNum = @CheckOrderNum , ");
            strSql.Append(" TeamId = @TeamId , ");
            strSql.Append(" TeamLvId = @TeamLvId , ");
            strSql.Append(" Pwd = @Pwd , ");
            strSql.Append(" PwdMd5 = @PwdMd5 , ");
            strSql.Append(" MemberName = @MemberName , ");
            strSql.Append(" RealName = @RealName , ");
            strSql.Append(" Birthday = @Birthday  ");
            strSql.Append(" where MemberId=@MemberId ");

            SqlParameter[] parameters = {
                        new SqlParameter("@MemberId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@Memo", SqlDbType.VarChar,200) ,
                        new SqlParameter("@WxPtId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@WxOpenId", SqlDbType.VarChar,80) ,
                        new SqlParameter("@Invalid", SqlDbType.Bit,1) ,
                        new SqlParameter("@Email", SqlDbType.VarChar,50) ,
                        new SqlParameter("@Phone", SqlDbType.VarChar,15) ,
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,
                        new SqlParameter("@LastTime", SqlDbType.DateTime) ,
                        new SqlParameter("@MemberLv", SqlDbType.Int,4) ,
                        new SqlParameter("@Status", SqlDbType.Int,4) ,
                        new SqlParameter("@MemberCode", SqlDbType.VarChar,8) ,
                        new SqlParameter("@MerId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@PicImgId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@Integral", SqlDbType.Decimal,9) ,
                        new SqlParameter("@MemoName", SqlDbType.VarChar,100) ,
                        new SqlParameter("@NickName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@LastBuyTime", SqlDbType.DateTime) ,
                        new SqlParameter("@IsPromoter", SqlDbType.Bit,1) ,
                        new SqlParameter("@ExtMemberId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@ExtMemberLv", SqlDbType.Int,4) ,
                        new SqlParameter("@LastSiteId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@ProcessLvId", SqlDbType.Int,4) ,
                        new SqlParameter("@ProcessLvStatusId", SqlDbType.Int,4) ,
                        new SqlParameter("@SfzNo", SqlDbType.VarChar,50) ,
                        new SqlParameter("@SfzImg1", SqlDbType.VarChar,50) ,
                        new SqlParameter("@Sex", SqlDbType.VarChar,4) ,
                        new SqlParameter("@SfzImg2", SqlDbType.VarChar,50) ,
                        new SqlParameter("@AreaId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@Address", SqlDbType.VarChar,1000) ,
                        new SqlParameter("@Amount", SqlDbType.Decimal,9) ,
                        new SqlParameter("@OldAmount", SqlDbType.Decimal,9) ,
                        new SqlParameter("@OrderWorkMaxNum", SqlDbType.Int,4) ,
                        new SqlParameter("@MaxOrderPlanningTime", SqlDbType.DateTime) ,
                        new SqlParameter("@CheckOrderNum", SqlDbType.Decimal,9) ,
                        new SqlParameter("@TeamId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@TeamLvId", SqlDbType.Int,4) ,
                        new SqlParameter("@Pwd", SqlDbType.VarChar,20) ,
                        new SqlParameter("@PwdMd5", SqlDbType.VarChar,50) ,
                        new SqlParameter("@MemberName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@RealName", SqlDbType.VarChar,20) ,
                        new SqlParameter("@Birthday", SqlDbType.DateTime)

            };

            parameters[0].Value = model.MemberId;
            parameters[1].Value = model.Memo;
            parameters[2].Value = model.WxPtId;
            parameters[3].Value = model.WxOpenId;
            parameters[4].Value = model.Invalid;
            parameters[5].Value = model.Email;
            parameters[6].Value = model.Phone;
            parameters[7].Value = model.CreateTime;
            parameters[8].Value = model.LastTime;
            parameters[9].Value = model.MemberLv;
            parameters[10].Value = model.Status;
            parameters[11].Value = model.MemberCode;
            parameters[12].Value = model.MerId;
            parameters[13].Value = model.PicImgId;
            parameters[14].Value = model.Integral;
            parameters[15].Value = model.MemoName;
            parameters[16].Value = model.NickName;
            parameters[17].Value = model.LastBuyTime;
            parameters[18].Value = model.IsPromoter;
            parameters[19].Value = model.ExtMemberId;
            parameters[20].Value = model.ExtMemberLv;
            parameters[21].Value = model.LastSiteId;
            parameters[22].Value = model.ProcessLvId;
            parameters[23].Value = model.ProcessLvStatusId;
            parameters[24].Value = model.SfzNo;
            parameters[25].Value = model.SfzImg1;
            parameters[26].Value = model.Sex;
            parameters[27].Value = model.SfzImg2;
            parameters[28].Value = model.AreaId;
            parameters[29].Value = model.Address;
            parameters[30].Value = model.Amount;
            parameters[31].Value = model.OldAmount;
            parameters[32].Value = model.OrderWorkMaxNum;
            parameters[33].Value = model.MaxOrderPlanningTime;
            parameters[34].Value = model.CheckOrderNum;
            parameters[35].Value = model.TeamId;
            parameters[36].Value = model.TeamLvId;
            parameters[37].Value = model.Pwd;
            parameters[38].Value = model.PwdMd5;
            parameters[39].Value = model.MemberName;
            parameters[40].Value = model.RealName;
            parameters[41].Value = model.Birthday; try
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
        public MemberModel GetModel(decimal MemberId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MemberId, Memo, WxPtId, WxOpenId, Invalid, Email, Phone, CreateTime, LastTime, MemberLv, Status, MemberCode, MerId, PicImgId, Integral, MemoName, NickName, LastBuyTime, IsPromoter, ExtMemberId, ExtMemberLv, LastSiteId, ProcessLvId, ProcessLvStatusId, SfzNo, SfzImg1, Sex, SfzImg2, AreaId, Address, Amount, OldAmount, OrderWorkMaxNum, MaxOrderPlanningTime, CheckOrderNum, TeamId, TeamLvId, Pwd, PwdMd5, MemberName, RealName, Birthday  ");
            strSql.Append("  from CORE.dbo.Member ");
            strSql.Append(" where MemberId=@MemberId");
            SqlParameter[] parameters = {
                    new SqlParameter("@MemberId", SqlDbType.Decimal)
            };
            parameters[0].Value = MemberId;


            MemberModel model = new MemberModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["MemberId"].ToString() != "")
                {
                    model.MemberId = decimal.Parse(ds.Tables[0].Rows[0]["MemberId"].ToString());
                }
                model.Memo = ds.Tables[0].Rows[0]["Memo"].ToString();
                if (ds.Tables[0].Rows[0]["WxPtId"].ToString() != "")
                {
                    model.WxPtId = decimal.Parse(ds.Tables[0].Rows[0]["WxPtId"].ToString());
                }
                model.WxOpenId = ds.Tables[0].Rows[0]["WxOpenId"].ToString();
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
                model.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                model.Phone = ds.Tables[0].Rows[0]["Phone"].ToString();
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LastTime"].ToString() != "")
                {
                    model.LastTime = DateTime.Parse(ds.Tables[0].Rows[0]["LastTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MemberLv"].ToString() != "")
                {
                    model.MemberLv = int.Parse(ds.Tables[0].Rows[0]["MemberLv"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Status"].ToString() != "")
                {
                    model.Status = int.Parse(ds.Tables[0].Rows[0]["Status"].ToString());
                }
                model.MemberCode = ds.Tables[0].Rows[0]["MemberCode"].ToString();
                if (ds.Tables[0].Rows[0]["MerId"].ToString() != "")
                {
                    model.MerId = decimal.Parse(ds.Tables[0].Rows[0]["MerId"].ToString());
                }
                model.PicImgId = ds.Tables[0].Rows[0]["PicImgId"].ToString();
                if (ds.Tables[0].Rows[0]["Integral"].ToString() != "")
                {
                    model.Integral = decimal.Parse(ds.Tables[0].Rows[0]["Integral"].ToString());
                }
                model.MemoName = ds.Tables[0].Rows[0]["MemoName"].ToString();
                model.NickName = ds.Tables[0].Rows[0]["NickName"].ToString();
                if (ds.Tables[0].Rows[0]["LastBuyTime"].ToString() != "")
                {
                    model.LastBuyTime = DateTime.Parse(ds.Tables[0].Rows[0]["LastBuyTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsPromoter"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsPromoter"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsPromoter"].ToString().ToLower() == "true"))
                    {
                        model.IsPromoter = true;
                    }
                    else
                    {
                        model.IsPromoter = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["ExtMemberId"].ToString() != "")
                {
                    model.ExtMemberId = decimal.Parse(ds.Tables[0].Rows[0]["ExtMemberId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ExtMemberLv"].ToString() != "")
                {
                    model.ExtMemberLv = int.Parse(ds.Tables[0].Rows[0]["ExtMemberLv"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LastSiteId"].ToString() != "")
                {
                    model.LastSiteId = decimal.Parse(ds.Tables[0].Rows[0]["LastSiteId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ProcessLvId"].ToString() != "")
                {
                    model.ProcessLvId = int.Parse(ds.Tables[0].Rows[0]["ProcessLvId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ProcessLvStatusId"].ToString() != "")
                {
                    model.ProcessLvStatusId = int.Parse(ds.Tables[0].Rows[0]["ProcessLvStatusId"].ToString());
                }
                model.SfzNo = ds.Tables[0].Rows[0]["SfzNo"].ToString();
                model.SfzImg1 = ds.Tables[0].Rows[0]["SfzImg1"].ToString();
                model.Sex = ds.Tables[0].Rows[0]["Sex"].ToString();
                model.SfzImg2 = ds.Tables[0].Rows[0]["SfzImg2"].ToString();
                model.AreaId = ds.Tables[0].Rows[0]["AreaId"].ToString();
                model.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                if (ds.Tables[0].Rows[0]["Amount"].ToString() != "")
                {
                    model.Amount = decimal.Parse(ds.Tables[0].Rows[0]["Amount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["OldAmount"].ToString() != "")
                {
                    model.OldAmount = decimal.Parse(ds.Tables[0].Rows[0]["OldAmount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["OrderWorkMaxNum"].ToString() != "")
                {
                    model.OrderWorkMaxNum = int.Parse(ds.Tables[0].Rows[0]["OrderWorkMaxNum"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MaxOrderPlanningTime"].ToString() != "")
                {
                    model.MaxOrderPlanningTime = DateTime.Parse(ds.Tables[0].Rows[0]["MaxOrderPlanningTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CheckOrderNum"].ToString() != "")
                {
                    model.CheckOrderNum = decimal.Parse(ds.Tables[0].Rows[0]["CheckOrderNum"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TeamId"].ToString() != "")
                {
                    model.TeamId = decimal.Parse(ds.Tables[0].Rows[0]["TeamId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TeamLvId"].ToString() != "")
                {
                    model.TeamLvId = int.Parse(ds.Tables[0].Rows[0]["TeamLvId"].ToString());
                }
                model.Pwd = ds.Tables[0].Rows[0]["Pwd"].ToString();
                model.PwdMd5 = ds.Tables[0].Rows[0]["PwdMd5"].ToString();
                model.MemberName = ds.Tables[0].Rows[0]["MemberName"].ToString();
                model.RealName = ds.Tables[0].Rows[0]["RealName"].ToString();
                if (ds.Tables[0].Rows[0]["Birthday"].ToString() != "")
                {
                    model.Birthday = DateTime.Parse(ds.Tables[0].Rows[0]["Birthday"].ToString());
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
            strSql.Append("delete from CORE.dbo.Member ");
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
            object[] fenyeParmValue = new object[] { "CORE.dbo.MemberView  WITH(NOLOCK)", col, Order, strWhere, pagesize, currentpage, 0 };
            ds = helper.ExecProc_ReDs("dbo.fenye2", fenyeParmName, fenyeParmValue);
            ds = Common.DataSetting.DataPageSetting(ds, pagesize, currentpage);
            return ds;


        }
     


        /// <summary>
        /// 获得fenye数据列表
        /// </summary>
        public DataSet GetPageList(string strWhere, int currentpage, int pagesize, string cols)
        {
            if (cols == "")
            {
                cols = " * ";
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 3000 " + cols + " ");
            strSql.Append(" FROM CORE.dbo.MemberView  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM CORE.dbo.Member  WITH(NOLOCK) ");
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
            strSql.Append(" FROM CORE.dbo.Member  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

