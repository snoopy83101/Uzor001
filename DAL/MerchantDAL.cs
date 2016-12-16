using DBTools;
using Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DAL
{
    //Merchant
    public partial class MerchantDAL
    {
        #region //数据操作

        /// <summary>
        /// 数据库帮助对象
        /// </summary>
        private MSSQLHelper helper = new MSSQLHelper();

        #endregion //数据操作

        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <summary>
        /// 增加一条数据
        /// </summary>
     	public bool Add(MerchantModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CORE.dbo.Merchant (");
            strSql.Append("MerchantId,Lng,Lat,WebSite,Logo,TownId,FlagInvalid,Tell,qq,Email,Address,InputCode,Phone,Name,CreateTime,CreateUser,MerchantTypeTarget,ToWebSite,MerchantName,MerchantMemo,MerchantContent,MerchantClassId,MerchantTypeId,Recommendlv,HotLv");
            strSql.Append(") values (");
            strSql.Append("@MerchantId,@Lng,@Lat,@WebSite,@Logo,@TownId,@FlagInvalid,@Tell,@qq,@Email,@Address,@InputCode,@Phone,@Name,@CreateTime,@CreateUser,@MerchantTypeTarget,@ToWebSite,@MerchantName,@MerchantMemo,@MerchantContent,@MerchantClassId,@MerchantTypeId,@Recommendlv,@HotLv");
            strSql.Append(") ");

            SqlParameter[] parameters = {
                        new SqlParameter("@MerchantId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@Lng", SqlDbType.Decimal,9) ,
                        new SqlParameter("@Lat", SqlDbType.Decimal,9) ,
                        new SqlParameter("@WebSite", SqlDbType.VarChar,100) ,
                        new SqlParameter("@Logo", SqlDbType.VarChar,200) ,
                        new SqlParameter("@TownId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@FlagInvalid", SqlDbType.Bit,1) ,
                        new SqlParameter("@Tell", SqlDbType.VarChar,50) ,
                        new SqlParameter("@qq", SqlDbType.VarChar,50) ,
                        new SqlParameter("@Email", SqlDbType.VarChar,50) ,
                        new SqlParameter("@Address", SqlDbType.VarChar,200) ,
                        new SqlParameter("@InputCode", SqlDbType.VarChar,50) ,
                        new SqlParameter("@Phone", SqlDbType.VarChar,20) ,
                        new SqlParameter("@Name", SqlDbType.VarChar,20) ,
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,
                        new SqlParameter("@CreateUser", SqlDbType.VarChar,50) ,
                        new SqlParameter("@MerchantTypeTarget", SqlDbType.VarChar,80) ,
                        new SqlParameter("@ToWebSite", SqlDbType.Bit,1) ,
                        new SqlParameter("@MerchantName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@MerchantMemo", SqlDbType.NVarChar,1000) ,
                        new SqlParameter("@MerchantContent", SqlDbType.NText) ,
                        new SqlParameter("@MerchantClassId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@MerchantTypeId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@Recommendlv", SqlDbType.Int,4) ,
                        new SqlParameter("@HotLv", SqlDbType.Int,4)

            };

            parameters[0].Value = model.MerchantId;
            parameters[1].Value = model.Lng;
            parameters[2].Value = model.Lat;
            parameters[3].Value = model.WebSite;
            parameters[4].Value = model.Logo;
            parameters[5].Value = model.TownId;
            parameters[6].Value = model.FlagInvalid;
            parameters[7].Value = model.Tell;
            parameters[8].Value = model.qq;
            parameters[9].Value = model.Email;
            parameters[10].Value = model.Address;
            parameters[11].Value = model.InputCode;
            parameters[12].Value = model.Phone;
            parameters[13].Value = model.Name;
            parameters[14].Value = model.CreateTime;
            parameters[15].Value = model.CreateUser;
            parameters[16].Value = model.MerchantTypeTarget;
            parameters[17].Value = model.ToWebSite;
            parameters[18].Value = model.MerchantName;
            parameters[19].Value = model.MerchantMemo;
            parameters[20].Value = model.MerchantContent;
            parameters[21].Value = model.MerchantClassId;
            parameters[22].Value = model.MerchantTypeId;
            parameters[23].Value = model.Recommendlv;
            parameters[24].Value = model.HotLv;

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
        public bool Update(MerchantModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Merchant set ");

            strSql.Append(" Lng = @Lng , ");
            strSql.Append(" Lat = @Lat , ");
            strSql.Append(" WebSite = @WebSite , ");
            strSql.Append(" Logo = @Logo , ");
            strSql.Append(" TownId = @TownId , ");
            strSql.Append(" FlagInvalid = @FlagInvalid , ");
            strSql.Append(" Tell = @Tell , ");
            strSql.Append(" qq = @qq , ");
            strSql.Append(" Email = @Email , ");
            strSql.Append(" Address = @Address , ");
            strSql.Append(" InputCode = @InputCode , ");
            strSql.Append(" Phone = @Phone , ");
            strSql.Append(" Name = @Name , ");
            strSql.Append(" CreateTime = @CreateTime , ");
            strSql.Append(" CreateUser = @CreateUser , ");
            strSql.Append(" MerchantTypeTarget = @MerchantTypeTarget , ");
            strSql.Append(" MerchantName = @MerchantName , ");
            strSql.Append(" MerchantMemo = @MerchantMemo , ");
            strSql.Append(" MerchantContent = @MerchantContent , ");
            strSql.Append(" MerchantClassId = @MerchantClassId , ");
            strSql.Append(" MerchantTypeId = @MerchantTypeId , ");
            strSql.Append(" Recommendlv = @Recommendlv , ");
            strSql.Append(" HotLv = @HotLv  ");
            strSql.Append(" where MerchantId=@MerchantId ");

            SqlParameter[] parameters = {
                        new SqlParameter("@MerchantId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@Lng", SqlDbType.Decimal,9) ,
                        new SqlParameter("@Lat", SqlDbType.Decimal,9) ,
                        new SqlParameter("@WebSite", SqlDbType.VarChar,100) ,
                        new SqlParameter("@Logo", SqlDbType.VarChar,200) ,
                        new SqlParameter("@TownId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@FlagInvalid", SqlDbType.Bit,1) ,
                        new SqlParameter("@Tell", SqlDbType.VarChar,50) ,
                        new SqlParameter("@qq", SqlDbType.VarChar,50) ,
                        new SqlParameter("@Email", SqlDbType.VarChar,50) ,
                        new SqlParameter("@Address", SqlDbType.VarChar,200) ,
                        new SqlParameter("@InputCode", SqlDbType.VarChar,50) ,
                        new SqlParameter("@Phone", SqlDbType.VarChar,20) ,
                        new SqlParameter("@Name", SqlDbType.VarChar,20) ,
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,
                        new SqlParameter("@CreateUser", SqlDbType.VarChar,50) ,
                        new SqlParameter("@MerchantTypeTarget", SqlDbType.VarChar,80) ,
                        new SqlParameter("@MerchantName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@MerchantMemo", SqlDbType.NVarChar,1000) ,
                        new SqlParameter("@MerchantContent", SqlDbType.NVarChar,2000) ,
                        new SqlParameter("@MerchantClassId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@MerchantTypeId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@Recommendlv", SqlDbType.Int,4) ,
                        new SqlParameter("@HotLv", SqlDbType.Int,4)
            };

            parameters[0].Value = model.MerchantId;
            parameters[1].Value = model.Lng;
            parameters[2].Value = model.Lat;
            parameters[3].Value = model.WebSite;
            parameters[4].Value = model.Logo;
            parameters[5].Value = model.TownId;
            parameters[6].Value = model.FlagInvalid;
            parameters[7].Value = model.Tell;
            parameters[8].Value = model.qq;
            parameters[9].Value = model.Email;
            parameters[10].Value = model.Address;
            parameters[11].Value = model.InputCode;
            parameters[12].Value = model.Phone;
            parameters[13].Value = model.Name;
            parameters[14].Value = model.CreateTime;
            parameters[15].Value = model.CreateUser;
            parameters[16].Value = model.MerchantTypeTarget;
            parameters[17].Value = model.MerchantName;
            parameters[18].Value = model.MerchantMemo;
            parameters[19].Value = model.MerchantContent;
            parameters[20].Value = model.MerchantClassId;
            parameters[21].Value = model.MerchantTypeId;
            parameters[22].Value = model.Recommendlv;
            parameters[23].Value = model.HotLv; try
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
        public MerchantModel GetModel(decimal MerchantId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MerchantId, Lng, Lat, WebSite, Logo, TownId, FlagInvalid, Tell, qq, Email, Address, InputCode, Phone, Name, CreateTime, CreateUser, MerchantTypeTarget, MerchantName, MerchantMemo, MerchantContent, MerchantClassId, MerchantTypeId, Recommendlv, HotLv  ");
            strSql.Append("  from Merchant ");
            strSql.Append(" where MerchantId=@MerchantId");
            SqlParameter[] parameters = {
                    new SqlParameter("@MerchantId", SqlDbType.Decimal)
            };
            parameters[0].Value = MerchantId;

            MerchantModel model = new MerchantModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["MerchantId"].ToString() != "")
                {
                    model.MerchantId = decimal.Parse(ds.Tables[0].Rows[0]["MerchantId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Lng"].ToString() != "")
                {
                    model.Lng = decimal.Parse(ds.Tables[0].Rows[0]["Lng"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Lat"].ToString() != "")
                {
                    model.Lat = decimal.Parse(ds.Tables[0].Rows[0]["Lat"].ToString());
                }
                model.WebSite = ds.Tables[0].Rows[0]["WebSite"].ToString();
                model.Logo = ds.Tables[0].Rows[0]["Logo"].ToString();
                if (ds.Tables[0].Rows[0]["TownId"].ToString() != "")
                {
                    model.TownId = decimal.Parse(ds.Tables[0].Rows[0]["TownId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FlagInvalid"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["FlagInvalid"].ToString() == "1") || (ds.Tables[0].Rows[0]["FlagInvalid"].ToString().ToLower() == "true"))
                    {
                        model.FlagInvalid = true;
                    }
                    else
                    {
                        model.FlagInvalid = false;
                    }
                }
                model.Tell = ds.Tables[0].Rows[0]["Tell"].ToString();
                model.qq = ds.Tables[0].Rows[0]["qq"].ToString();
                model.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                model.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                model.InputCode = ds.Tables[0].Rows[0]["InputCode"].ToString();
                model.Phone = ds.Tables[0].Rows[0]["Phone"].ToString();
                model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                model.CreateUser = ds.Tables[0].Rows[0]["CreateUser"].ToString();
                model.MerchantTypeTarget = ds.Tables[0].Rows[0]["MerchantTypeTarget"].ToString();
                model.MerchantName = ds.Tables[0].Rows[0]["MerchantName"].ToString();
                model.MerchantMemo = ds.Tables[0].Rows[0]["MerchantMemo"].ToString();
                model.MerchantContent = ds.Tables[0].Rows[0]["MerchantContent"].ToString();
                if (ds.Tables[0].Rows[0]["MerchantClassId"].ToString() != "")
                {
                    model.MerchantClassId = decimal.Parse(ds.Tables[0].Rows[0]["MerchantClassId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MerchantTypeId"].ToString() != "")
                {
                    model.MerchantTypeId = decimal.Parse(ds.Tables[0].Rows[0]["MerchantTypeId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Recommendlv"].ToString() != "")
                {
                    model.Recommendlv = int.Parse(ds.Tables[0].Rows[0]["Recommendlv"].ToString());
                }
                if (ds.Tables[0].Rows[0]["HotLv"].ToString() != "")
                {
                    model.HotLv = int.Parse(ds.Tables[0].Rows[0]["HotLv"].ToString());
                }

                return model;
            }
            else
            {
                return null;
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
            strSql.Append("delete from Merchant ");
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
        public DataSet GetPageList(string strWhere, int currentpage, int pagesize)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 3000 * ");
            strSql.Append(" FROM dbo.MerchantView ");
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

        //检查商家是否绑定用户
        public bool CheckMerBind(decimal MerId, string UserId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM dbo.MerVsUserView where UserId='" + UserId + "' and  MerchantId= " + MerId + " ");
            if (helper.ExecuteSqlScalar(strSql.ToString()) == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public DataSet GetUserMerList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM dbo.MerVsUserView  ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return helper.ExecSqlReDs(strSql.ToString());
        }

        /// <summary>
        /// 根据商家编号获取商家快速数据(不包括商家简介)
        /// </summary>
        /// <param name="MerId"></param>
        /// <returns></returns>
        public DataSet GetMerInfoFaseById(decimal MerId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@" select  MerchantId ,
         InputCode ,
         MerchantName ,
         MerchantMemo ,

         MerchantClassId ,
         MerchantTypeId ,
         Recommendlv ,
         HotLv ,
         Lng ,
         Lat ,
         WebSite ,
         Logo ,
         TownId ,
         FlagInvalid ,
         Tell ,
         qq ,
         Email ,
         Address ,
         Phone ,
         Name ,
         MerchantTypeName ,
         MapPng ,
         LogoUrl  FROM ");
            strSql.Append("  dbo.MerchantView  where MerchantId='" + MerId + "' ");

            return helper.ExecSqlReDs(strSql.ToString());
        }

        /// <summary>
        /// 根据商家编号获得商家数据
        /// </summary>
        /// <param name="MerId"></param>
        /// <returns></returns>
        public DataSet GetMerInfoById(decimal MerId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select * ");
            strSql.Append(" FROM dbo.MerchantView  WITH(NOLOCK) where MerchantId='" + MerId + "' "); //0

            strSql.Append("  SELECT * FROM  dbo.MerchantVsMerchantType MvsMT WITH(NOLOCK) LEFT JOIN dbo.MerchantType Mt WITH(NOLOCK) ON MvsMT.MerchantTypeId = Mt.MerchantTypeId where MerchantId='" + MerId + "' ");//1

            strSql.Append("  SELECT * FROM dbo.MerchantVsImg MVI WITH(NOLOCK) LEFT JOIN dbo.ImageInfo i WITH(NOLOCK) ON MVI.ImgId = i.ImgId where MerchantId='" + MerId + "' ");  //取得商家关联的图片//2
            strSql.Append("   SELECT * FROM  dbo.ProductClass WITH(NOLOCK) WHERE MerchantId='" + MerId + "' and Invalid=0 and ParentProductClassId=0 order by OrderNo desc ");  //取得商家的产品类别//3

            strSql.Append(" select * from dbo.MerVsUserView WITH(NOLOCK) where MerchantId='" + MerId + "' ");//取得商家的用户4
            DataSet ds = helper.ExecSqlReDs(strSql.ToString());
            ds.Tables[0].TableName = "Mer";
            ds.Tables[1].TableName = "MerType";
            ds.Tables[2].TableName = "MerImg";
            ds.Tables[3].TableName = "MerProClass";
            ds.Tables[4].TableName = "MerUser";
            return ds;
        }

        /// <summary>
        /// 地图根据类别查询
        /// </summary>
        /// <param name="MerchantTypeId"></param>
        /// <returns></returns>
        public DataSet GetMapMerPageList(string MerchantTypeIds, string InputStr, int currentpage)
        {
            StringBuilder s = new StringBuilder();

            #region 超级长的SQL语句

            s.Append(@"SELECT  m.MerchantId ,
        m.InputCode ,
        m.MerchantName ,
        m.MerchantMemo ,

        m.MerchantClassId ,
        m.MerchantTypeId ,
        m.Recommendlv ,
        m.HotLv ,
        m.Lng ,
        m.Lat ,
        m.WebSite ,
        m.Logo ,
        m.TownId ,
        m.FlagInvalid ,
        m.Tell ,
        m.qq ,
        m.Email ,
        m.Address ,
        m.Phone ,
        m.Name ,
        i.ImgUrl AS LogoUrl,
        t.TownName,
        m.Createtime,
        m.MerchantTypeTarget
FROM    dbo.Merchant AS m WITH(NOLOCK)
        LEFT OUTER JOIN dbo.ImageInfo AS i WITH(NOLOCK) ON m.Logo = i.ImgId
        LEFT JOIN dbo.MerchantVsMerchantType mVsmType WITH(NOLOCK) ON m.MerchantId = mVsmType.MerchantId
        LEFT JOIN dbo.MerchantType mType WITH(NOLOCK) ON mVsmType.MerchantTypeId = mType.MerchantTypeId
        LEFT JOIN dbo.Town t WITH(NOLOCK) ON m.TownId=t.TownId WHERE 1=1 ");
            if (MerchantTypeIds.Trim() != "")
            {
                s.Append(" and  mType.MerchantTypeId in( " + MerchantTypeIds + ") ");
            }
            else
            {
                //   s.Append(" and HotLv>0  ");
            }

            if (InputStr.Trim() != "")
            {
                s.Append(" and MerchantName like '%" + InputStr + "%' ");
            }
            s.Append(" and FlagInvalid=0 "); //除了作废的

            s.Append(@" GROUP BY m.MerchantId ,
        m.InputCode ,
        m.MerchantName ,
        m.MerchantMemo ,

        m.MerchantClassId ,
        m.MerchantTypeId ,
        m.Recommendlv ,
        m.HotLv ,
        m.Lng ,
        m.Lat ,
        m.WebSite ,
        m.Logo ,
        m.TownId ,
        m.FlagInvalid ,
        m.Tell ,
        m.qq ,
        m.Email ,
        m.Address ,
        m.Phone ,
        m.Name ,
        m.MerchantTypeTarget,
        i.ImgUrl ,
        t.TownName,
        m.Createtime
        ");
            s.Append(" order by createTime desc ");

            #endregion 超级长的SQL语句

            DataSet ds = null;
            string[] fenyeParmName = new string[] { "sqlstr", "currentpage", "pagesize" };
            object[] fenyeParmValue = new object[] { s.ToString(), currentpage, 25 };
            ds = helper.ExecProc_ReDs("dbo.fenye", fenyeParmName, fenyeParmValue);
            return ds;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM dbo.MerchantView WITH(NOLOCK) ");
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
            strSql.Append(" FROM dbo.MerchantView WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }
    }
}