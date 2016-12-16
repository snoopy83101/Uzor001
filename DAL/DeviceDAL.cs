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
    //设备表
    public partial class DeviceDAL
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
            strSql.Append(" FROM  DBMSG.dbo.Device ");
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
        public bool Add(DeviceModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into DBMSG.dbo.Device (");
            strSql.Append("DeviceId,DeviceTypeId,DeviceHardwareId,UserId,Lng,Lat,MsgNum,DeviceName,DevicePicImgId,MerId,MemberId,SystemType,AppVersion,SystemVersion,LastTime,RongToken,IpAddress");
            strSql.Append(") values (");
            strSql.Append("@DeviceId,@DeviceTypeId,@DeviceHardwareId,@UserId,@Lng,@Lat,@MsgNum,@DeviceName,@DevicePicImgId,@MerId,@MemberId,@SystemType,@AppVersion,@SystemVersion,@LastTime,@RongToken,@IpAddress");
            strSql.Append(") ");

            SqlParameter[] parameters = {
                        new SqlParameter("@DeviceId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@DeviceTypeId", SqlDbType.VarChar,10) ,
                        new SqlParameter("@DeviceHardwareId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@UserId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@Lng", SqlDbType.Decimal,9) ,
                        new SqlParameter("@Lat", SqlDbType.Decimal,9) ,
                        new SqlParameter("@MsgNum", SqlDbType.Int,4) ,
                        new SqlParameter("@DeviceName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@DevicePicImgId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@MerId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@MemberId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@SystemType", SqlDbType.VarChar,20) ,
                        new SqlParameter("@AppVersion", SqlDbType.VarChar,20) ,
                        new SqlParameter("@SystemVersion", SqlDbType.VarChar,20) ,
                        new SqlParameter("@LastTime", SqlDbType.DateTime) ,
                        new SqlParameter("@RongToken", SqlDbType.VarChar,500) ,
                        new SqlParameter("@IpAddress", SqlDbType.VarChar,50)

            };

            parameters[0].Value = model.DeviceId;
            parameters[1].Value = model.DeviceTypeId;
            parameters[2].Value = model.DeviceHardwareId;
            parameters[3].Value = model.UserId;
            parameters[4].Value = model.Lng;
            parameters[5].Value = model.Lat;
            parameters[6].Value = model.MsgNum;
            parameters[7].Value = model.DeviceName;
            parameters[8].Value = model.DevicePicImgId;
            parameters[9].Value = model.MerId;
            parameters[10].Value = model.MemberId;
            parameters[11].Value = model.SystemType;
            parameters[12].Value = model.AppVersion;
            parameters[13].Value = model.SystemVersion;
            parameters[14].Value = model.LastTime;
            parameters[15].Value = model.RongToken;
            parameters[16].Value = model.IpAddress;

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
        public bool Update(DeviceModel model)
        {
            bool reValue = true;
            int reCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update DBMSG.dbo.Device set ");

            strSql.Append(" DeviceId = @DeviceId , ");
            strSql.Append(" DeviceTypeId = @DeviceTypeId , ");
            strSql.Append(" DeviceHardwareId = @DeviceHardwareId , ");
            strSql.Append(" UserId = @UserId , ");
            strSql.Append(" Lng = @Lng , ");
            strSql.Append(" Lat = @Lat , ");
            strSql.Append(" MsgNum = @MsgNum , ");
            strSql.Append(" DeviceName = @DeviceName , ");
            strSql.Append(" DevicePicImgId = @DevicePicImgId , ");
            strSql.Append(" MerId = @MerId , ");
            strSql.Append(" MemberId = @MemberId , ");
            strSql.Append(" SystemType = @SystemType , ");
            strSql.Append(" AppVersion = @AppVersion , ");
            strSql.Append(" SystemVersion = @SystemVersion , ");
            strSql.Append(" LastTime = @LastTime , ");
            strSql.Append(" RongToken = @RongToken , ");
            strSql.Append(" IpAddress = @IpAddress  ");
            strSql.Append(" where DeviceId=@DeviceId  ");

            SqlParameter[] parameters = {
                        new SqlParameter("@DeviceId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@DeviceTypeId", SqlDbType.VarChar,10) ,
                        new SqlParameter("@DeviceHardwareId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@UserId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@Lng", SqlDbType.Decimal,9) ,
                        new SqlParameter("@Lat", SqlDbType.Decimal,9) ,
                        new SqlParameter("@MsgNum", SqlDbType.Int,4) ,
                        new SqlParameter("@DeviceName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@DevicePicImgId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@MerId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@MemberId", SqlDbType.Decimal,9) ,
                        new SqlParameter("@SystemType", SqlDbType.VarChar,20) ,
                        new SqlParameter("@AppVersion", SqlDbType.VarChar,20) ,
                        new SqlParameter("@SystemVersion", SqlDbType.VarChar,20) ,
                        new SqlParameter("@LastTime", SqlDbType.DateTime) ,
                        new SqlParameter("@RongToken", SqlDbType.VarChar,500) ,
                        new SqlParameter("@IpAddress", SqlDbType.VarChar,50)

            };

            parameters[0].Value = model.DeviceId;
            parameters[1].Value = model.DeviceTypeId;
            parameters[2].Value = model.DeviceHardwareId;
            parameters[3].Value = model.UserId;
            parameters[4].Value = model.Lng;
            parameters[5].Value = model.Lat;
            parameters[6].Value = model.MsgNum;
            parameters[7].Value = model.DeviceName;
            parameters[8].Value = model.DevicePicImgId;
            parameters[9].Value = model.MerId;
            parameters[10].Value = model.MemberId;
            parameters[11].Value = model.SystemType;
            parameters[12].Value = model.AppVersion;
            parameters[13].Value = model.SystemVersion;
            parameters[14].Value = model.LastTime;
            parameters[15].Value = model.RongToken;
            parameters[16].Value = model.IpAddress; try
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
        public DeviceModel GetModel(string DeviceId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select DeviceId, DeviceTypeId, DeviceHardwareId, UserId, Lng, Lat, MsgNum, DeviceName, DevicePicImgId, MerId, MemberId, SystemType, AppVersion, SystemVersion, LastTime, RongToken, IpAddress  ");
            strSql.Append("  from DBMSG.dbo.Device ");
            strSql.Append(" where DeviceId=@DeviceId ");
            SqlParameter[] parameters = {
                    new SqlParameter("@DeviceId", SqlDbType.VarChar,50)         };
            parameters[0].Value = DeviceId;


            DeviceModel model = new DeviceModel();
            DataSet ds = helper.ExecSqlReDs(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.DeviceId = ds.Tables[0].Rows[0]["DeviceId"].ToString();
                model.DeviceTypeId = ds.Tables[0].Rows[0]["DeviceTypeId"].ToString();
                model.DeviceHardwareId = ds.Tables[0].Rows[0]["DeviceHardwareId"].ToString();
                model.UserId = ds.Tables[0].Rows[0]["UserId"].ToString();
                if (ds.Tables[0].Rows[0]["Lng"].ToString() != "")
                {
                    model.Lng = decimal.Parse(ds.Tables[0].Rows[0]["Lng"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Lat"].ToString() != "")
                {
                    model.Lat = decimal.Parse(ds.Tables[0].Rows[0]["Lat"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MsgNum"].ToString() != "")
                {
                    model.MsgNum = int.Parse(ds.Tables[0].Rows[0]["MsgNum"].ToString());
                }
                model.DeviceName = ds.Tables[0].Rows[0]["DeviceName"].ToString();
                model.DevicePicImgId = ds.Tables[0].Rows[0]["DevicePicImgId"].ToString();
                if (ds.Tables[0].Rows[0]["MerId"].ToString() != "")
                {
                    model.MerId = decimal.Parse(ds.Tables[0].Rows[0]["MerId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MemberId"].ToString() != "")
                {
                    model.MemberId = decimal.Parse(ds.Tables[0].Rows[0]["MemberId"].ToString());
                }
                model.SystemType = ds.Tables[0].Rows[0]["SystemType"].ToString();
                model.AppVersion = ds.Tables[0].Rows[0]["AppVersion"].ToString();
                model.SystemVersion = ds.Tables[0].Rows[0]["SystemVersion"].ToString();
                if (ds.Tables[0].Rows[0]["LastTime"].ToString() != "")
                {
                    model.LastTime = DateTime.Parse(ds.Tables[0].Rows[0]["LastTime"].ToString());
                }
                model.RongToken = ds.Tables[0].Rows[0]["RongToken"].ToString();
                model.IpAddress = ds.Tables[0].Rows[0]["IpAddress"].ToString();

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
            strSql.Append("delete from DBMSG.dbo.Device ");
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
            object[] fenyeParmValue = new object[] { "DBMSG.dbo.Device  WITH(NOLOCK)", col, Order, strWhere, pagesize, currentpage, 0 };
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
            strSql.Append(" FROM DBMSG.dbo.Device  WITH(NOLOCK)  ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            //strSql.Append(" ORDER BY CREATE_TIME DESC ");
            DataSet ds = null;
            string[] fenyeParmName = new string[] { "sqlstr", "currentpage", "pagesize" };
            object[] fenyeParmValue = new object[] { strSql.ToString(), currentpage, pagesize };
            ds = helper.ExecProc_ReDs("DBMSG.dbo.fenye", fenyeParmName, fenyeParmValue);
            return ds;


        }



        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM DBMSG.dbo.Device  WITH(NOLOCK) ");
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
            strSql.Append(" FROM DBMSG.dbo.Device  WITH(NOLOCK) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return helper.ExecSqlReDs(strSql.ToString());
        }


    }
}

