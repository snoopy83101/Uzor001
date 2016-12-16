using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Ajax
{
    /// <summary>
    /// ImgUp 的摘要说明
    /// </summary>
    public class ImgUp : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            //上传配置
            int size = 2;           //文件大小限制,单位MB                             //文件大小限制，单位MB
            string[] filetype = { ".gif", ".png", ".jpg", ".jpeg", ".bmp" };         //文件允许格式


            //上传图片
            Hashtable info = new Hashtable();
            Uploader2 up = new Uploader2();

            string pathbase = null;
            int path = Convert.ToInt32(up.getOtherInfo(context, "dir"));
            if (path == 1)
            {
                pathbase = "/upload/Imgs/";

            }
            else
            {
                pathbase = "/upload1/";
            }

            info = up.upFile(context, pathbase, filetype, size, true);                   //获取上传状态



            string title = up.getOtherInfo(context, "pictitle");                   //获取图片描述
            string oriName = up.getOtherInfo(context, "fileName");                //获取原始文件名
            Model.ImageInfoModel imgModel = new Model.ImageInfoModel();
            imgModel.CreateTime = DateTime.Now;
            imgModel.ImgType = "Img";
            imgModel.ImgUrl = info["url"].ToString();
            imgModel.IsBind = false;
            imgModel.ImgId = Common.TimeString.GetNow_ff(); ;
            DAL.ImageInfoDAL ImgDal = new DAL.ImageInfoDAL();

            if (imgModel.CreateUser == "")
            {
                imgModel.CreateUser = "";
            }
            ImgDal.Add(imgModel);
            HttpContext.Current.Response.Write("{'ImgId':'" + imgModel.ImgId + "','url':'" + info["url"] + "','title':'" + title + "','original':'" + oriName + "','state':'" + info["state"] + "'}");  //向浏览器返回数据json数据
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}