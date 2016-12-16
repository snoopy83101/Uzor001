using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Xml;
using System.IO;
using System.Web;
using System.Web.Caching;

namespace Common
{
    public class DataSetting
    {
        /// <summary>
        /// 去重, 去掉重复行
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="filedNames"></param>
        /// <returns></returns>
        public static DataTable Distinct(DataTable dt, string[] filedNames)
        {
            DataView dv = dt.DefaultView;
            DataTable DistTable = dv.ToTable("Dist", true, filedNames);
            return DistTable;
        }

        public static int totalPage(DataSet ds)
        {

            return int.Parse(ds.Tables[1].Rows[0]["总页数"].ToString());
        }
        public static int RowCount(DataSet ds)
        {
            return int.Parse(ds.Tables[4].Rows[0]["RowsCount"].ToString());
        }

        /// <summary>
        /// 将内存中的DataTable转成Excel
        /// </summary>
        /// <param name="excelSavePath">Excel保存路径</param>
        /// <param name="sourceTable">内存中的DataTable</param>
        /// <param name="sheetName">在Excel中保存的Sheet名称</param>
        public static void Table2Excel(string excelSavePath, DataTable tb, string tbname)
        {

            FileString.FileDel2(excelSavePath);
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + excelSavePath + ";" + "Extended Properties=Excel 8.0;";
            using (OleDbConnection connex = new OleDbConnection(strConn))
            {
                connex.Open();
                string ct = "CREATE TABLE " + tbname + " (";                //以下生成一个sql命令向excel中插入一个表
                foreach (DataColumn clmn in tb.Columns)
                {
                    switch (clmn.DataType.Name)                                     //根据不同数据类型分别处理
                    {
                        case "Decimal":
                            ct += clmn.ColumnName + " Decimal,";
                            break;
                        case "Double":
                            ct += clmn.ColumnName + " Double,";
                            break;
                        default:
                            ct += clmn.ColumnName + " NTEXT,";
                            break;
                    }
                }
                ct = ct.Substring(0, ct.Length - 1) + ")";
                OleDbCommand cmd1 = new OleDbCommand(ct, connex);
                cmd1.ExecuteNonQuery();                                                              //向excel中插入一个表

                foreach (DataRow r in tb.Rows)                                                       //下面向excel中一行一行写入数据
                {
                    string fs = "", vs = "";
                    foreach (DataColumn clmn in tb.Columns)
                    {
                        fs += clmn.ColumnName + ",";
                        if (r[clmn.ColumnName] == DBNull.Value)
                        {
                            vs += "null,";
                            continue;
                        }
                        switch (clmn.DataType.Name)                                                   //根据不同数据类型分别处理
                        {
                            case "Decimal":
                                vs += ((decimal)r[clmn.ColumnName]).ToString("0.00") + ",";
                                break;
                            case "Double":
                                vs += ((double)r[clmn.ColumnName]).ToString("0.00") + ",";
                                break;
                            case "DateTime":
                                vs += "'" + ((DateTime)r[clmn.ColumnName]).ToShortDateString() + "',";
                                break;
                            default:
                                vs += "'" + StringPlus.DanYinHao(r[clmn.ColumnName].ToString()) + "',";
                                break;
                        }
                    }
                    string sqlstr = "insert into [" + tbname + "$] (" + fs.Substring(0, fs.Length - 1) + ") values (" + vs.Substring(0, vs.Length - 1) + ")";
                    OleDbCommand cmd = new OleDbCommand(sqlstr, connex);
                    cmd.ExecuteNonQuery();                                            //向excel中插入数据
                }
                connex.Close();
            }
        }



        /// <summary>
        /// 从<paramref name="sourceTable"/>构建字段字符串
        /// </summary>
        /// <param name="sourceTable"></param>
        /// <returns></returns>
        private static string BuildColumnsString(DataTable sourceTable)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < sourceTable.Columns.Count; i++)
            {
                if (i > 0)
                {
                    sb.Append(",");
                }
                string colName = sourceTable.Columns[i].ColumnName;
                sb.Append(colName);
                sb.Append("_ ");//为了避免系统关键字，将所有字段名后添加下划线
                sb.Append(SwitchToSqlType(sourceTable.Columns[i]));
            }
            return sb.ToString();
        }
        /// <summary>
        /// 将<paramref name="column"/>的DataType转成数据库关键字
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        private static string SwitchToSqlType(DataColumn column)
        {
            string TypeFullName = column.DataType.FullName;
            switch (TypeFullName)
            {
                //case "System.Int32":
                //    return "INTEGER";
                //case "System.Int16":
                //    return "SMALLINT";
                //case "System.String":
                //    return string.Format("VARCHAR ({0})", column.MaxLength);
                //case "System.Int64":
                //    return "BIGINT";
                //case "System.Double":
                //case "System.Float":
                //case "System.Single":
                //    return "REAL";
                //case "System.Numeric":
                //    return "NUMERIC";
                //case "System.DateTime":
                //    return "DATETIME";

                //case "System.Decimal":
                //    return "Decimal";
                default:
                    return "VARCHAR(4000)";
            }
        }
        /// <summary>
        /// 获取EXCEL的所有表单
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        private static string[] GetSheets(OleDbConnection conn)
        {
            //返回Excel的架构，包括各个sheet表的名称,类型，创建时间和修改时间等
            DataTable dtSheetName = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "Table" });

            //包含excel中表名的字符串数组
            string[] strTableNames = new string[dtSheetName.Rows.Count];
            for (int k = 0; k < dtSheetName.Rows.Count; k++)
            {
                DataRow row = dtSheetName.Rows[k];
                strTableNames[k] = dtSheetName.Rows[k]["TABLE_NAME"].ToString();
            }
            return strTableNames;


        }




        public static DataTable TableSelect(string select, DataTable dt)
        {
            StringBuilder s = new StringBuilder();

            DataRow[] drs = dt.Select(select);
            DataTable dt2 = dt.Clone();
            foreach (DataRow dr in drs)
            {
                dt2.Rows.Add(dr.ItemArray);

            }

            return dt2;

        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="select"></param>
        /// <param name="sort"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DataTable TableSelect(string select, string sort, DataTable dt)
        {
            StringBuilder s = new StringBuilder();

            DataRow[] drs = dt.Select(select, sort);
            DataTable dt2 = dt.Clone();
            foreach (DataRow dr in drs)
            {
                dt2.Rows.Add(dr.ItemArray);

            }

            return dt2;

        }

        /// <summary>
        /// 返回一个DataTable的i-j之间的所有行,i从0开始
        /// </summary>
        /// <param name="i">起始的行号</param>
        /// <param name="j">结束的行号</param>
        /// <param name="dt">数据表</param>
        /// <returns></returns>
        public static DataTable TableSelect(int i, int j, DataTable dt)
        {
            if (dt.Rows.Count == 0)
            {
                throw new Exception("DataTable中没有任何行!");
            }

            if (i > j)
            {
                throw new Exception("j不能比i还大!");
            }

            if (dt.Rows.Count < j + 1)
            {

                j = dt.Rows.Count - 1;
            }



            DataTable dt2 = dt.Clone();
            for (int x = i; x <= j; x++)
            {
                DataRow dr = dt.Rows[x];

                dt2.Rows.Add(dr.ItemArray);


            }

            return dt2;

        }

        /**/
        /// <summary>  
        /// 将Xml内容字符串转换成DataSet对象  
        /// </summary>  
        /// <param name="xmlStr">Xml内容字符串</param>  
        /// <returns>DataSet对象</returns>  
        public static DataSet CXmlToDataSet(string xmlStr)
        {
            if (!string.IsNullOrEmpty(xmlStr))
            {
                StringReader StrStream = null;
                XmlTextReader Xmlrdr = null;
                try
                {
                    DataSet ds = new DataSet();
                    //读取字符串中的信息  
                    StrStream = new StringReader(xmlStr);
                    //获取StrStream中的数据  
                    Xmlrdr = new XmlTextReader(StrStream);

                    //ds获取Xmlrdr中的数据                  
                    ds.ReadXml(Xmlrdr);
                    return ds;
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    //释放资源  
                    if (Xmlrdr != null)
                    {
                        Xmlrdr.Close();
                        StrStream.Close();
                        StrStream.Dispose();
                    }
                }
            }
            else
            {
                return null;
            }
        }

        public static DataSet DataPageSetting(DataSet ds, int pagesize, int currentpage)
        {
            if (ds.Tables.Count == 0)
            {
                ds.Tables.Add("NoData");
                ds.Tables.Add("Page");
                ds.Tables[1].Columns.Add("总行数");
                ds.Tables[1].Rows.Add(ds.Tables[1].NewRow());
                ds.Tables[1].Rows[0]["总行数"] = "0";
            }

            DataTable dt = ds.Tables[1];
            dt.Columns.Add("当前页");
            dt.Columns.Add("总页数");
            DataRow dr = dt.Rows[0];
            int 总行数 = int.Parse(ds.Tables[1].Rows[0]["总行数"].ToString());
            decimal d = (decimal)总行数 / (decimal)pagesize;
            int 总页数 = int.Parse(Math.Ceiling(d).ToString());
            dr["当前页"] = currentpage;
            dr["总页数"] = 总页数;


            return ds;
        }

        /**/
        /// <summary>  
        /// 将Xml字符串转换成DataTable对象  
        /// </summary>  
        /// <param name="xmlStr">Xml字符串</param>  
        /// <param name="tableIndex">Table表索引</param>  
        /// <returns>DataTable对象</returns>  
        public static DataTable CXmlToDatatTable(string xmlStr, int tableIndex)
        {
            return CXmlToDataSet(xmlStr).Tables[tableIndex];
        }
        /**/
        /// <summary>  
        /// 将Xml字符串转换成DataTable对象  
        /// </summary>  
        /// <param name="xmlStr">Xml字符串</param>  
        /// <returns>DataTable对象</returns>  
        public static DataTable CXmlToDatatTable(string xmlStr)
        {
            try
            {
                return CXmlToDataSet(xmlStr).Tables[0];
            }
            catch (Exception ex)
            {

                return new DataTable();
            }
        }
        /**/
        /// <summary>  
        /// 读取Xml文件信息,并转换成DataSet对象  
        /// </summary>  
        /// <remarks>  
        /// DataSet ds = new DataSet();  
        /// ds = CXmlFileToDataSet("/XML/upload.xml");  
        /// </remarks>  
        /// <param name="xmlFilePath">Xml文件地址</param>  
        /// <returns>DataSet对象</returns>  
        public static DataSet CXmlFileToDataSet(string xmlFilePath)
        {
            if (!string.IsNullOrEmpty(xmlFilePath))
            {
                string path = HttpContext.Current.Server.MapPath(xmlFilePath);
                StringReader StrStream = null;
                XmlTextReader Xmlrdr = null;
                try
                {
                    XmlDocument xmldoc = new XmlDocument();
                    //根据地址加载Xml文件  
                    xmldoc.Load(path);

                    DataSet ds = new DataSet();
                    //读取文件中的字符流  
                    StrStream = new StringReader(xmldoc.InnerXml);
                    //获取StrStream中的数据  
                    Xmlrdr = new XmlTextReader(StrStream);
                    //ds获取Xmlrdr中的数据  
                    ds.ReadXml(Xmlrdr);
                    return ds;
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    //释放资源  
                    if (Xmlrdr != null)
                    {
                        Xmlrdr.Close();
                        StrStream.Close();
                        StrStream.Dispose();
                    }
                }
            }
            else
            {
                return null;
            }
        }
        /**/
        /// <summary>  
        /// 读取Xml文件信息,并转换成DataTable对象  
        /// </summary>  
        /// <param name="xmlFilePath">xml文江路径</param>  
        /// <param name="tableIndex">Table索引</param>  
        /// <returns>DataTable对象</returns>  
        public static DataTable CXmlToDataTable(string xmlFilePath, int tableIndex)
        {
            return CXmlFileToDataSet(xmlFilePath).Tables[tableIndex];
        }
        /**/
        /// <summary>  
        /// 读取Xml文件信息,并转换成DataTable对象  
        /// </summary>  
        /// <param name="xmlFilePath">xml文江路径</param>  
        /// <returns>DataTable对象</returns>  
        public static DataTable CXmlToDataTable(string xmlFilePath)
        {
            return CXmlFileToDataSet(xmlFilePath).Tables[0];
        }

        /**/
        /// <summary>
        /// 将DataTable对象转换成XML字符串
        /// </summary>
        /// <param name="dt">DataTable对象</param>
        /// <returns>XML字符串</returns>
        public static string CDataToXml(DataTable dt)
        {
            if (dt != null)
            {
                MemoryStream ms = null;
                XmlTextWriter XmlWt = null;
                try
                {
                    ms = new MemoryStream();
                    //根据ms实例化XmlWt
                    XmlWt = new XmlTextWriter(ms, Encoding.Unicode);
                    //获取ds中的数据
                    dt.WriteXml(XmlWt);
                    int count = (int)ms.Length;
                    byte[] temp = new byte[count];
                    ms.Seek(0, SeekOrigin.Begin);
                    ms.Read(temp, 0, count);
                    //返回Unicode编码的文本
                    UnicodeEncoding ucode = new UnicodeEncoding();
                    string returnValue = ucode.GetString(temp).Trim();
                    return returnValue;
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    //释放资源
                    if (XmlWt != null)
                    {
                        XmlWt.Close();
                        ms.Close();
                        ms.Dispose();
                    }
                }
            }
            else
            {
                return "";
            }
        }
    }
}
