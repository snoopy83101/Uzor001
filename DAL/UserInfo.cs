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
    //UserInfo
    public partial class UserInfoDAL
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
            strSql.Append(" FROM UserInfo WITH(NOLOCK)  ");
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
        public bool Add(UserInfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CORE.dbo.UserInfo (");
            strSql.Append("UserId,PwdMd5,Tell,Phone,Validated,IdNo,StreetId,UserLv,Power,Currency,Money,UserTitle,UserTypeId,FlagMerchant,TownId,qq,PicBig,PicMid,PicSmall,WxOpenID,UserCode,Nickname,Sex,RealName,Email,Memo,Birthday,CreateTime,Pwd");
            strSql.Append(") values (");
            strSql.Append("@UserId,@PwdMd5,@Tell,@Phone,@Validated,@IdNo,@StreetId,@UserLv,@Power,@Currency,@Money,@UserTitle,@UserTypeId,@FlagMerchant,@TownId,@qq,@PicBig,@PicMid,@PicSmall,@WxOpenID,@UserCode,@Nickname,@Sex,@RealName,@Email,@Memo,@Birthday,@CreateTime,@Pwd");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@UserId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@PwdMd5", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Tell", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Phone", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Validated", SqlDbType.Bit,1) ,            
                        new SqlParameter("@IdNo", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@StreetId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@UserLv", SqlDbType.Int,4) ,            
                        new SqlParameter("@Power", SqlDbType.Int,4) ,            
                        new SqlParameter("@Currency", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Money", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@UserTitle", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@UserTypeId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@FlagMerchant", SqlDbType.Bit,1) ,            
                        new SqlParameter("@TownId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@qq", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@PicBig", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@PicMid", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@PicSmall", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@WxOpenID", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@UserCode", SqlDbType.Int,4) ,            
                        new SqlParameter("@Nickname", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Sex", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@RealName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Email", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@Memo", SqlDbType.VarChar,500) ,            
                        new SqlParameter("@Birthday", SqlDbType.Date,3) ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@Pwd", SqlDbType.VarChar,50)             
              
            };

            parameters[0].Value = model.UserId;
            parameters[1].Value = model.PwdMd5;
            parameters[2].Value = model.Tell;
            parameters[3].Value = model.Phone;
            parameters[4].Value = model.Validated;
            parameters[5].Value = model.IdNo;
            parameters[6].Value = model.StreetId;
            parameters[7].Value = model.UserLv;
            parameters[8].Value = model.Power;
            parameters[9].Value = model.Currency;
            parameters[10].Value = model.Money;
            parameters[11].Value = model.UserTitle;
            parameters[12].Value = model.UserTypeId;
            parameters[13].Value = model.FlagMerchant;
            parameters[14].Value = model.TownId;
            parameters[15].Value = model.qq;
            parameters[16].Value = model.PicBig;
            parameters[17].Value = model.PicMid;
            parameters[18].Value = model.PicSmall;
            parameters[19].Value = model.WxOpenID;
            parameters[20].Value = model.UserCode;
            parameters[21].Value = model.Nickname;
            parameters[22].Value = model.Sex;
            parameters[23].Value = model.RealName;
            parameters[24].Value = model.Email;
            parameters[25].Value = model.Memo;
            parameters[26].Value = model.Birthday;
            parameters[27].Value = model.CreateTime;
            parameters[28].Value = model.Pwd;

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
        public bool Update(UserInfoModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CORE.dbo.UserInfo set ");

            strSql.Append(" UserId = @UserId , ");
            strSql.Append(" PwdMd5 = @PwdMd5 , ");
            strSql.Append(" Tell = @Tell , ");
            strSql.Append(" Phone = @Phone , ");
            strSql.Append(" Validated = @Validated , ");
            strSql.Append(" IdNo = @IdNo , ");
            strSql.Append(" StreetId = @StreetId , ");
            strSql.Append(" UserLv = @UserLv , ");
            strSql.Append(" Power = @Power , ");
            strSql.Append(" Currency = @Currency , ");
            strSql.Append(" Money = @Money , ");
            strSql.Append(" UserTitle = @UserTitle , ");
            strSql.Append(" UserTypeId = @UserTypeId , ");
            strSql.Append(" FlagMerchant = @FlagMerchant , ");
            strSql.Append(" TownId = @TownId , ");
            strSql.Append(" qq = @qq , ");
            strSql.Append(" PicBig = @PicBig , ");
            strSql.Append(" PicMid = @PicMid , ");
            strSql.Append(" PicSmall = @PicSmall , ");
            strSql.Append(" WxOpenID = @WxOpenID , ");
            strSql.Append(" UserCode = @UserCode , ");
            strSql.Append(" Nickname = @Nickname , ");
            strSql.Append(" Sex = @Sex , ");
            strSql.Append(" RealName = @RealName , ");
            strSql.Append(" Email = @Email , ");
            strSql.Append(" Memo = @Memo , ");
            strSql.Append(" Birthday = @Birthday , ");
            strSql.Append(" CreateTime = @CreateTime , ");
            strSql.Append(" Pwd = @Pwd  ");
            strSql.Append(" where UserId=@UserId  ");

            SqlParameter[] parameters = {
			            new SqlParameter("@UserId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@PwdMd5", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Tell", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Phone", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Validated", SqlDbType.Bit,1) ,            
                        new SqlParameter("@IdNo", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@StreetId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@UserLv", SqlDbType.Int,4) ,            
                        new SqlParameter("@Power", SqlDbType.Int,4) ,            
                        new SqlParameter("@Currency", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Money", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@UserTitle", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@UserTypeId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@FlagMerchant", SqlDbType.Bit,1) ,            
                        new SqlParameter("@TownId", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@qq", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@PicBig", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@PicMid", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@PicSmall", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@WxOpenID", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@UserCode", SqlDbType.Int,4) ,            
                        new SqlParameter("@Nickname", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Sex", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@RealName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Email", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@Memo", SqlDbType.VarChar,500) ,            
                        new SqlParameter("@Birthday", SqlDbType.Date,3) ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@Pwd", SqlDbType.VarChar,50)             
              
            };

            parameters[0].Value = model.UserId;
            parameters[1].Value = model.PwdMd5;
            parameters[2].Value = model.Tell;
            parameters[3].Value = model.Phone;
            parameters[4].Value = model.Validated;
            parameters[5].Value = model.IdNo;
            parameters[6].Value = model.StreetId;
            parameters[7].Value = model.UserLv;
            parameters[8].Value = model.Power;
            parameters[9].Value = model.Currency;
            parameters[10].Value = model.Money;
            parameters[11].Value = model.UserTitle;
            parameters[12].Value = model.UserTypeId;
            parameters[13].Value = model.FlagMerchant;
            parameters[14].Value = model.TownId;
            parameters[15].Value = model.qq;
            parameters[16].Value = model.PicBig;
            parameters[17].Value = model.PicMid;
            parameters[18].Value = model.PicSmall;
            parameters[19].Value = model.WxOpenID;
            parameters[20].Value = model.UserCode;
            parameters[21].Value = model.Nickname;
            parameters[22].Value = model.Sex;
            parameters[23].Value = model.RealName;
            parameters[24].Value = model.Email;
            parameters[25].Value = model.Memo;
            parameters[26].Value = model.Birthday;
            parameters[27].Value = model.CreateTime;
            parameters[28].Value = model.Pwd; try
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
        public UserInfoModel GetModel(string UserId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select UserId, PwdMd5, Tell, Phone, Validated, IdNo, StreetId, UserLv, Power, Currency, Money, UserTitle, UserTypeId, FlagMerchant, TownId, qq, PicBig, PicMid, PicSmall, WxOpenID, UserCode, Nickname, Sex, RealName, Email, Memo, Birthday, CreateTime, Pwd  ");
            strSql.Append("  from CORE.dbo.UserInfo ");
            strSql.Append(" where UserId=@UserId ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.VarChar,50)			};
            parameters[0].Value = UserId;


            UserInfoModel model = new UserInfoModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.UserId = ds.Tables[0].Rows[0]["UserId"].ToString();
                model.PwdMd5 = ds.Tables[0].Rows[0]["PwdMd5"].ToString();
                model.Tell = ds.Tables[0].Rows[0]["Tell"].ToString();
                model.Phone = ds.Tables[0].Rows[0]["Phone"].ToString();
                if (ds.Tables[0].Rows[0]["Validated"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Validated"].ToString() == "1") || (ds.Tables[0].Rows[0]["Validated"].ToString().ToLower() == "true"))
                    {
                        model.Validated = true;
                    }
                    else
                    {
                        model.Validated = false;
                    }
                }
                model.IdNo = ds.Tables[0].Rows[0]["IdNo"].ToString();
                if (ds.Tables[0].Rows[0]["StreetId"].ToString() != "")
                {
                    model.StreetId = decimal.Parse(ds.Tables[0].Rows[0]["StreetId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UserLv"].ToString() != "")
                {
                    model.UserLv = int.Parse(ds.Tables[0].Rows[0]["UserLv"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Power"].ToString() != "")
                {
                    model.Power = int.Parse(ds.Tables[0].Rows[0]["Power"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Currency"].ToString() != "")
                {
                    model.Currency = decimal.Parse(ds.Tables[0].Rows[0]["Currency"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Money"].ToString() != "")
                {
                    model.Money = decimal.Parse(ds.Tables[0].Rows[0]["Money"].ToString());
                }
                model.UserTitle = ds.Tables[0].Rows[0]["UserTitle"].ToString();
                if (ds.Tables[0].Rows[0]["UserTypeId"].ToString() != "")
                {
                    model.UserTypeId = decimal.Parse(ds.Tables[0].Rows[0]["UserTypeId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FlagMerchant"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["FlagMerchant"].ToString() == "1") || (ds.Tables[0].Rows[0]["FlagMerchant"].ToString().ToLower() == "true"))
                    {
                        model.FlagMerchant = true;
                    }
                    else
                    {
                        model.FlagMerchant = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["TownId"].ToString() != "")
                {
                    model.TownId = decimal.Parse(ds.Tables[0].Rows[0]["TownId"].ToString());
                }
                model.qq = ds.Tables[0].Rows[0]["qq"].ToString();
                model.PicBig = ds.Tables[0].Rows[0]["PicBig"].ToString();
                model.PicMid = ds.Tables[0].Rows[0]["PicMid"].ToString();
                model.PicSmall = ds.Tables[0].Rows[0]["PicSmall"].ToString();
                model.WxOpenID = ds.Tables[0].Rows[0]["WxOpenID"].ToString();
                if (ds.Tables[0].Rows[0]["UserCode"].ToString() != "")
                {
                    model.UserCode = int.Parse(ds.Tables[0].Rows[0]["UserCode"].ToString());
                }
                model.Nickname = ds.Tables[0].Rows[0]["Nickname"].ToString();
                model.Sex = ds.Tables[0].Rows[0]["Sex"].ToString();
                model.RealName = ds.Tables[0].Rows[0]["RealName"].ToString();
                model.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                model.Memo = ds.Tables[0].Rows[0]["Memo"].ToString();
                if (ds.Tables[0].Rows[0]["Birthday"].ToString() != "")
                {
                    model.Birthday = DateTime.Parse(ds.Tables[0].Rows[0]["Birthday"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                model.Pwd = ds.Tables[0].Rows[0]["Pwd"].ToString();

                return model;
            }
            else
            {
                return model;
            }
        }
	
        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="Pwd"></param>
        /// <returns></returns>
        public bool ChangePwd(string UserId, string Pwd)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update UserInfo set Pwd='" + Pwd + "' ,PwdMd5='"+Common.JiaMi.MD5(Pwd)+"' ");
            strSql.Append(" where UserId='" + UserId + "' ");

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
        /// 删除duo条数据
        /// </summary>
        public bool DeleteList(string strWhere)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from UserInfo ");
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

        public DataSet GetUserInfo(string userId)
        {

            StringBuilder s = new StringBuilder();
            s.Append(" select * from dbo.UserView  WITH(NOLOCK) where UserId='" + userId + "' ");

            s.Append(" SELECT * FROM dbo.UserVsRole WITH(NOLOCK)  where RoleUserId ='" + userId + "'  ");


            return helper.ExecSqlReDs(s.ToString());
        }

        /// <summary>
        /// 获得fenye数据列表
        /// </summary>
        public DataSet GetPageList(string strWhere, int currentpage, int pagesize)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 3000 * ");
            strSql.Append(" FROM UserInfo WITH(NOLOCK)  ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            //strSql.Append(" ORDER BY CREATE_TIME DESC ");
            DataSet ds = null;
            string[] fenyeParmName = new string[] { "sqlstr", "currentpage", "pagesize" };
            object[] fenyeParmValue = new object[] { strSql.ToString(), currentpage, pagesize };
            ds = helper.ExecProc_ReDs("dbo.fenye", fenyeParmName, fenyeParmValue);
            return ds;


        }



        public DataSet GetUserInfoByWxOpenId(string OpenId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM UserView WITH(NOLOCK) where WxOpenID='"+OpenId+"'  ");
       
            return helper.ExecSqlReDs(strSql.ToString()); 
        
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM UserView WITH(NOLOCK)  ");
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
            strSql.Append(" FROM UserInfo WITH(NOLOCK)  ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

