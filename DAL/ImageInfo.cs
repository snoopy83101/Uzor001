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
    //图片表
    public partial class ImageInfoDAL
    {

        #region  //数据操作

        /// <summary>
        /// 数据库帮助对象
        /// </summary>
        private MSSQLHelper helper = new MSSQLHelper();
        #endregion


        public bool BindImage(string ImgIds)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update ImageInfo set IsBind='True' Where ImgId in (" + ImgIds + ") ");
            try
            {
                this.helper.ExecSqlReInt(strSql.ToString());
                return true;
            }
            catch (Exception ex)
            {


                this.helper.Close();
                throw ex;
            }


        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(ImageInfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CORE.dbo.ImageInfo (");
            strSql.Append("ImgId,ImgType,ImgUrl,IsBind,CreateTime,CreateUser");
            strSql.Append(") values (");
            strSql.Append("@ImgId,@ImgType,@ImgUrl,@IsBind,@CreateTime,@CreateUser");
            strSql.Append(") ");

            SqlParameter[] parameters = {
                        new SqlParameter("@ImgId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ImgType", SqlDbType.VarChar,10) ,
                        new SqlParameter("@ImgUrl", SqlDbType.VarChar,200) ,
                        new SqlParameter("@IsBind", SqlDbType.Bit,1) ,
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,
                        new SqlParameter("@CreateUser", SqlDbType.VarChar,50)

            };

            parameters[0].Value = model.ImgId;
            parameters[1].Value = model.ImgType;
            parameters[2].Value = model.ImgUrl;
            parameters[3].Value = model.IsBind;
            parameters[4].Value = model.CreateTime;
            parameters[5].Value = model.CreateUser;

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
        public bool Update(ImageInfoModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CORE.dbo.ImageInfo set ");

            strSql.Append(" ImgId = @ImgId , ");
            strSql.Append(" ImgType = @ImgType , ");
            strSql.Append(" ImgUrl = @ImgUrl , ");
            strSql.Append(" IsBind = @IsBind , ");
            strSql.Append(" CreateTime = @CreateTime , ");
            strSql.Append(" CreateUser = @CreateUser  ");
            strSql.Append(" where ImgId=@ImgId and ImgType=@ImgType  ");

            SqlParameter[] parameters = {
                        new SqlParameter("@ImgId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ImgType", SqlDbType.VarChar,10) ,
                        new SqlParameter("@ImgUrl", SqlDbType.VarChar,200) ,
                        new SqlParameter("@IsBind", SqlDbType.Bit,1) ,
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,
                        new SqlParameter("@CreateUser", SqlDbType.VarChar,50)

            };

            parameters[0].Value = model.ImgId;
            parameters[1].Value = model.ImgType;
            parameters[2].Value = model.ImgUrl;
            parameters[3].Value = model.IsBind;
            parameters[4].Value = model.CreateTime;
            parameters[5].Value = model.CreateUser; try
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
        public ImageInfoModel GetModel(string ImgId, string ImgType)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ImgId, ImgType, ImgUrl, IsBind, CreateTime, CreateUser  ");
            strSql.Append("  from CORE.dbo.ImageInfo ");
            strSql.Append(" where ImgId=@ImgId and ImgType=@ImgType ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ImgId", SqlDbType.VarChar,50),
                    new SqlParameter("@ImgType", SqlDbType.VarChar,10)          };
            parameters[0].Value = ImgId;
            parameters[1].Value = ImgType;


            ImageInfoModel model = new ImageInfoModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.ImgId = ds.Tables[0].Rows[0]["ImgId"].ToString();
                model.ImgType = ds.Tables[0].Rows[0]["ImgType"].ToString();
                model.ImgUrl = ds.Tables[0].Rows[0]["ImgUrl"].ToString();
                if (ds.Tables[0].Rows[0]["IsBind"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsBind"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsBind"].ToString().ToLower() == "true"))
                    {
                        model.IsBind = true;
                    }
                    else
                    {
                        model.IsBind = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                model.CreateUser = ds.Tables[0].Rows[0]["CreateUser"].ToString();

                return model;
            }
            else
            {
                return model;
            }
        }




        public void DoBind(List<string> ImgUrl, string UserId)
        {

            StringBuilder s = new StringBuilder();
            if (ImgUrl.Count == 0)
            {
                return;
            }

            s.Append(" UPDATE  dbo.ImageInfo SET IsBind=1 WHERE  imgId IN ( SELECT top 50 ImgId FROM  dbo.ImageInfo WHERE ImgUrl IN ");
            s.Append("('" + string.Join("','", ImgUrl) + "')");
            s.Append("  AND CreateUser='" + UserId + "' order by createTime desc ) ");
            this.helper.ExecSqlReInt(s.ToString());
        }

        /// <summary>
        /// 删除duo条数据
        /// </summary>
        public bool DeleteList(string strWhere)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ImageInfo ");
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
            strSql.Append(" FROM ImageInfo  WITH(NOLOCK)  ");
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



        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM ImageInfo  WITH(NOLOCK)  ");
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
            strSql.Append(" FROM ImageInfo  WITH(NOLOCK)  ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

