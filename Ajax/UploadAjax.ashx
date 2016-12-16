<%@ WebHandler Language="C#"  Class="UploadAjax" %>
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;



    /// <summary>
    /// UploadAjax 的摘要说明
    /// </summary>
    public class UploadAjax : IHttpHandler
    {
        string FilePath = "";
        string FileUrl = "";
        string ImgType = "";
        string FileType = "";
        string IsDel = "";//是否显示删除按钮
        protected string imgW = "auto";
        int w = 0;
        int h = 0;

        string title = "";
        public void ProcessRequest(HttpContext context)
        {

            doUpload();
        }

        protected void doUpload()
        {
            try
            {

                Page current = HttpContext.Current.CurrentHandler as Page;

                BLL.UserBLL ubll = new BLL.UserBLL();
                HttpPostedFile file;

                file = HttpContext.Current.Request.Files["file"];
                w = 700;
                h = 700;
                FileUrl = "/upload/IMGS";
                FilePath = HttpContext.Current.Server.MapPath("/upload/IMGS");
                string strNewPath = GetSaveFilePath() + "_ls" + GetExtension(file.FileName);
   
                file.SaveAs(FilePath + strNewPath);
                string urlPath = FilePath + strNewPath;
                urlPath = urlPath.Replace("\\", "/");

                //     urlPath = current.ResolveUrl(urlPath);

                Model.ImageInfoModel model = new Model.ImageInfoModel();


                urlPath = StringPlus.GetLastStr(urlPath, "upload/");
                model.ImgType = ImgType;
                model.ImgUrl = urlPath;
                string 临时路径 = "/upload/" + urlPath;
                string 正式路径 = 临时路径.Replace("_ls", "");
                model.ImgUrl = 正式路径;
                model.CreateTime = DateTime.Now;
                model.CreateUser = Common.CookieSings.GetCurrentUserId();
                model.IsBind = false;
                try
                {
                    Common.ImgHelper.MakeThumNail(HttpContext.Current.Server.MapPath(临时路径), HttpContext.Current.Server.MapPath(正式路径), w, h);
                    BLL.ImageBLL bll = new BLL.ImageBLL();
                    bll.AddNewImages(model);
                    string ImgId = model.ImgId;
                    HttpContext.Current.Response.Write("{\"ImgId\":\"" + model.ImgId + "\", \"ImgUrl\":\"" + model.ImgUrl + "\" }");

                }
                catch (Exception ex)
                {

                    HttpContext.Current.Response.Write(ex.ToString());
                }
                finally {

                    HttpContext.Current.Response.End();
                }
        

            }
            catch (Exception ex)
            {
                //WriteJs("parent.uploaderror();");
            }
        }
        private string GetSaveFilePath()
        {
            try
            {
                DateTime dateTime = DateTime.Now;
                string yearStr = dateTime.Year.ToString(); ;
                string monthStr = dateTime.Month.ToString();
                string dayStr = dateTime.Day.ToString();
                string hourStr = dateTime.Hour.ToString();
                string minuteStr = dateTime.Minute.ToString();
                string dir = dateTime.ToString(@"\\yyyyMMdd");
                if (!Directory.Exists(FilePath + dir))
                {
                    Directory.CreateDirectory(FilePath + dir);
                }
                return dir + dateTime.ToString("\\\\yyyyMMddhhmmssffff");
            }
            catch (Exception ex)
            {
                //WriteJs("parent.uploaderror();");
                return string.Empty;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        /// <summary>
        /// 获得扩展名
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private string GetExtension(string fileName)
        {
            try
            {
                int startPos = fileName.LastIndexOf(".");
                string ext = fileName.Substring(startPos, fileName.Length - startPos);
                return ext;
            }
            catch (Exception ex)
            {
                //   WriteJs("parent.uploaderror('" + FileType + "');");
                return string.Empty;
            }
        }
    }
