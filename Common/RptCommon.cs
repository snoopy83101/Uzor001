using System;
using System.Text;
using System.Data;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;


namespace Common
{
    /// <summary>
    /// RptCommon 的摘要说明
    /// </summary>
    /// 
    public class RptCommon
    {
        public RptCommon()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }


        public static void ReData(DataSet ds)
        {

            string DataText = ds.GetXml();

            ReData(ref DataText);
        }

        public static void ReData(DataSet ds, Dictionary<string, string> canshu)
        {
            string DataText = ds.GetXml();
            StringBuilder w = new StringBuilder();
            string ParameterPart = "";
            if (canshu.Count == 0)
            {
                ParameterPart = "";
            }
            else
            {
                w.Append("<_grparam>");
                foreach (KeyValuePair<string, string> item in canshu)
                {

                    w.Append("<" + item.Key + ">");
                    w.Append(item.Value);
                    w.Append("</" + item.Key + ">");
                }
                w.Append("</_grparam>");

                ParameterPart = w.ToString();
            }

            string XMLText = "<report>\r\n" + DataText + ParameterPart + "</report>";
            ReData(ref XMLText);
        }

        public static void ReData(ref string DataText)
        {
            HttpContext.Current.Response.ClearContent();
            System.Text.UTF8Encoding converter = new System.Text.UTF8Encoding();
            byte[] XmlBytes = converter.GetBytes(DataText);


            //在 HTTP 头信息中写入报表数据压缩信息
            HttpContext.Current.Response.AppendHeader("gr_zip_type", "deflate");                  //指定压缩方法
            HttpContext.Current.Response.AppendHeader("gr_zip_size", XmlBytes.Length.ToString()); //指定数据的原始长度
            HttpContext.Current.Response.AppendHeader("gr_zip_encode", converter.HeaderName);     //指定数据的编码方式 utf-8 utf-16 ...

            MemoryStream memStream = new MemoryStream();
            DeflateStream compressedzipStream = new DeflateStream(memStream, CompressionMode.Compress, true);
            compressedzipStream.Write(XmlBytes, 0, XmlBytes.Length);
            compressedzipStream.Close(); //这句很重要，这样数据才能全部写入 MemoryStream

            // Read bytes from the stream.
            memStream.Seek(0, SeekOrigin.Begin); // Set the position to the beginning of the stream.
            int count = (int)memStream.Length;
            byte[] byteArray = new byte[count];
            count = memStream.Read(byteArray, 0, count);

            string Base64Text = Convert.ToBase64String(byteArray);
            HttpContext.Current.Response.Write(Base64Text);


            //    ReportDataBase.ResponseData(DataPage, "",  ResponseDataType.ZipBase64);


        }


    }
}