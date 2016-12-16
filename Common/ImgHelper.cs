using mshtml;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Net;
using System.Text;
using System.Drawing;
using System.Web;
using System.IO;
using System.Drawing.Drawing2D;
namespace Common
{
    public class ImgHelper
    {



        public static void MakeThumNail(string originalImagePath, string thumNailPath, int width, int height)
        {
            string m = "HW";
            System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);

            if (width == 0 || height == 0)
            { //如果长度和宽度有一个等于0 那么就别特么压缩了.
                width = originalImage.Width;
                height = originalImage.Height;

            }

            if (originalImage.Width > originalImage.Height)
            {//这是一个宽图片 
                if (originalImage.Width >= width)
                {

                    m = "W";
                }
                else
                {//而且宽度还不够最宽 
                    m = "Cut";
                    width = originalImage.Width;
                    height = originalImage.Height;
                }




            }
            else if (originalImage.Width == originalImage.Height)
            { //这是一个正方形图片
                if (originalImage.Width >= width)
                {

                    m = "W";
                }
                else
                {

                    m = "Cut";
                    width = originalImage.Width;
                    height = originalImage.Height;
                }

            }
            else
            {//这是一个长图片

                if (originalImage.Height >= height)
                {

                    m = "H";
                }
                else
                {//而且长度还不够最长 

                    m = "Cut";

                    width = originalImage.Width;
                    height = originalImage.Height;

                }

            }
            MakeThumNail(originalImagePath, thumNailPath, width, height, m);
        }

        /// <summary>
        /// 压缩图片
        /// </summary>
        /// <param name="originalImagePath">图片的临时路径(必须是已经存在于服务器上的图片)</param>
        /// <param name="thumNailPath">图片的正式路径</param>
        /// <param name="width">图片的宽度</param>
        /// <param name="height">图片的高度</param>
        /// <param name="model">HW指定高宽缩放,可能变形,W指定宽度,H指定高度,Cut不指定长宽</param>
        public static void MakeThumNail(string originalImagePath, string thumNailPath, int width, int height, string model)
        {
            if (width == 0 || height == 0)
            {
                model = "Cut";
            }

            System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);

            int thumWidth = new int();
            int thumHeight = new int();
            if (originalImage.Width > 100 && originalImage.Height > 70)
            {
                thumWidth = width;      //缩略图的宽度
                thumHeight = height;    //缩略图的高度
            }
            else
            {
                model = "HW";
                thumWidth = originalImage.Width;      //缩略图的宽度
                thumHeight = originalImage.Height;   //缩略图的高度
            }
            int x = 0;
            int y = 0;

            int originalWidth = originalImage.Width;    //原始图片的宽度
            int originalHeight = originalImage.Height;  //原始图片的高度


            switch (model)
            {
                case "HW":      //指定高宽缩放,可能变形
                    break;
                case "W":       //指定宽度,高度按照比例缩放
                    thumHeight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H":       //指定高度,宽度按照等比例缩放
                    thumWidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "Cut":
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)thumWidth / (double)thumHeight)
                    {
                        originalHeight = originalImage.Height;
                        originalWidth = originalImage.Height * thumWidth / thumHeight;
                        y = 0;
                        x = (originalImage.Width - originalWidth) / 2;
                    }
                    else
                    {
                        originalWidth = originalImage.Width;
                        originalHeight = originalWidth * height / thumWidth;
                        x = 0;
                        y = (originalImage.Height - originalHeight) / 2;
                    }
                    break;
                default:
                    break;

            }



            //新建一个bmp图片
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(thumWidth, thumHeight);

            //新建一个画板
            System.Drawing.Graphics graphic = System.Drawing.Graphics.FromImage(bitmap);

            //设置高质量查值法
            graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
            ;
            ////设置高质量，低速度呈现平滑程度
            graphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            graphic.CompositingQuality = CompositingQuality.HighSpeed;
            //清空画布并以透明背景色填充
            graphic.Clear(System.Drawing.Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分
            graphic.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, thumWidth, thumHeight), new System.Drawing.Rectangle(x, y, originalWidth, originalHeight), System.Drawing.GraphicsUnit.Pixel);

            try
            {
                var fileName = Common.FileString.GetFileName(thumNailPath);

                var folder = thumNailPath.Replace("\\" + fileName + "", "");


                if (!System.IO.Directory.Exists(folder))
                {

                    System.IO.Directory.CreateDirectory(folder);

                }

                bitmap.Save(thumNailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                graphic.Dispose();
            }

        }
        /// <summary>  
        /// 获取图片编码类型信息  
        /// </summary>  
        /// <param name="coderType">编码类型</param>  
        /// <returns>ImageCodecInfo</returns>  
        private ImageCodecInfo getImageCoderInfo(string coderType)
        {
            ImageCodecInfo[] iciS = ImageCodecInfo.GetImageEncoders();

            ImageCodecInfo retIci = null;

            foreach (ImageCodecInfo ici in iciS)
            {
                if (ici.MimeType.Equals(coderType))
                    retIci = ici;
            }
            return retIci;


        }


        /// <summary>
        /// 取得一个HTML中的所有图片集合
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static List<string> GetImgsForHTML(string html)
        {

            IHTMLDocument2 doc = new HTMLDocumentClass();
            doc.write(new object[] { html });
            doc.close();
          //  System.Net.WebClient wc = new System.Net.WebClient();
            List<string> imgs = new List<string>();

            foreach (IHTMLImgElement image in doc.images)
            {

                imgs.Add(image.src.Replace("about:", ""));


            }
            if (imgs.Count == 0)
            {
                return null;
            }
            else
            {
                return imgs;
            }
        }


        public static string SetImgLoadingHTML(string html)
        {

            IHTMLDocument2 doc = new HTMLDocumentClass();
            doc.write(new object[] { html });
            doc.close();
        //    System.Net.WebClient wc = new System.Net.WebClient();
            List<string> imgs = new List<string>();

            foreach (IHTMLImgElement image in doc.images)
            {

                IHTMLElement element = (IHTMLElement)image;

                string src = (string)element.getAttribute("src", 2);
                element.setAttribute("data-original", src, 1);
                element.setAttribute("src", Common.HtmlHelper.LoadImg200px(), 1);
                //if (src != null)
                //{
                //    //  Uri addr = new Uri(src);
                //    image.src = Common.HtmlHelper.LoadImg200px();

                //}


            }
            return doc.body.innerHTML;
        }




        /// <summary>
        /// 上传图片到固定目录,而后替换HTML字符串
        /// </summary>
        /// <param name="html">传入HTML</param>
        /// <param name="mypage">传入当前PAGE对象,一般为this</param>
        /// <returns></returns>
        //public static string PicUrl(string html, string id, string type)
        //{

        //    //string j = string.Empty;
        //    //string ImgForder = "/UploadFiles/news" + DAL.TimeString.GetYM();
        //    //string fwqLj = System.Web.HttpContext.Current.Server.MapPath(ImgForder);//文件保存在服务器上的路径
        //    //string fwqlinshi = System.Web.HttpContext.Current.Server.MapPath("/UploadFiles/linshi/");//文件保存在服务器上的临时路径
        //    //IHTMLDocument2 doc = new HTMLDocumentClass();
        //    //doc.write(new object[] { html });
        //    //doc.close();
        //    //System.Net.WebClient wc = new System.Net.WebClient();
        //    //foreach (IHTMLImgElement image in doc.images)
        //    //{

        //    //    if (LTP.Common.StringPlus.GetStrCount(image.src, "about:") == 0)
        //    //    {
        //    //        //需要判断IMG中是否包含HTTP;//
        //    //        string picName = DAL.TimeString.GetNow_ff();//取得时间字符串(作为文件名)
        //    //        IHTMLElement element = (IHTMLElement)image;
        //    //        string src = (string)element.getAttribute("src", 2);
        //    //        string fex = LTP.Common.FileString.GetFex(src);//获得文件后缀
        //    //        string fileName = picName + "." + fex;//获得文件名
        //    //        wc.Headers.Add(HttpRequestHeader.Referer, src);//突破反盗链,模拟IE打开页面
        //    //        wc.DownloadFile(src, fwqlinshi + fileName);//下载图片到临时路径   
        //    //        string ImgLinShi = fwqlinshi + fileName;//临时文件在服务器硬盘上的路径
        //    //        string ImgSrc = fwqLj + fileName;//正式文件保存到服务器硬盘上的路径
        //    //        DAL.tupian.MakeThumNail(ImgLinShi, ImgSrc, 400, 300, "W");//处理临时路径中的图片,并存在正式路径中
        //    //        DAL.zjsc.DeleteFile(fwqlinshi + fileName);//删除临时路径中的图片
        //    //        string ImgUrl = ImgForder + fileName;//类似/uploadfiles....这种路径
        //    //        html = html.Replace(src, ImgUrl);//替换原有HTML中的图片路径为本站点的路径
        //    //        DAL.UpLoadFiles.InsertFiles(ImgUrl, id, type);
        //    //    }

        //    //}
        //    //return html;
        //}
        //public static void InsertNewsHtmlPoto(string html, string zhuantiId)
        //{
        //    string imgString = string.Empty;

        //    IHTMLDocument2 doc = new HTMLDocumentClass();
        //    doc.write(new object[] { html });
        //    doc.close();
        //    foreach (IHTMLImgElement image in doc.images)
        //    {

        //        imgString = image.href.ToString();
        //        imgString = System.IO.Path.GetFileName(imgString);
        //        if (imgString.Length > 0)
        //        {
        //            InsertPoto("~/UploadFiles/news/" + imgString, "news", zhuantiId);

        //        }

        //    }

        //}
        /// <summary>
        /// 返回第一张图片,如果没有图片,则返回空字符串;
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string GetFristImg(string html)
        {
            string imgString = "";

            IHTMLDocument2 doc = new HTMLDocumentClass();
            doc.write(new object[] { html });
            doc.close();
            foreach (IHTMLImgElement image in doc.images)
            {

                if (imgString == "")
                {

                    imgString = image.src;

                }


            }

            return imgString;
        }



        /// <summary>
        /// 判断图片长宽类型,返回三种string分别是长,宽,无.
        /// </summary>
        /// <param name="url">传入图片的路径,类似:"~/img/1.jpg"</param>
        /// <returns></returns>
        public static string ImgChangKuan(string url)
        {
            if (url != "" && url != null)
            {
                string originalImagePath = System.Web.HttpContext.Current.Server.MapPath(url);
                System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);
                int w = originalImage.Width;
                int h = originalImage.Height;
                if (w > h)
                {
                    return "宽";
                }
                else
                {
                    return "长";
                }
            }
            else
            {
                return "无";
            }
        }

        public static void downImg(string html)
        {

            string fwqlinshi = System.Web.HttpContext.Current.Server.MapPath("~/upload/HttpDown/");//文件保存在服务器上的临时路径
            IHTMLDocument2 doc = new HTMLDocumentClass();
            doc.write(new object[] { html });
            doc.close();
            WebClient wc = new WebClient();
            foreach (IHTMLImgElement image in doc.images)
            {

                IHTMLElement element = (IHTMLElement)image;
                string src = (string)element.getAttribute("src", 2);
                string picName = System.IO.Path.GetFileName(src);
                wc.Headers.Add(HttpRequestHeader.Referer, src);//突破反盗链,模拟IE打开页面
                wc.DownloadFile(src, fwqlinshi + picName);//下载图片到临时路径
            }

        }


        /// <summary>
        /// 下载一个图片，返回图片路径
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string downOneImg(string url)
        {

            return downOneImg(url, "/upload/HttpDown/");


        }

        /// <summary>
        /// 下载图片
        /// </summary>
        /// <param name="url">图片url</param>
        /// <param name="SaveUrl">下载地址/upload/httpDown/Mov/</param>
        /// <returns></returns>
        public static string downOneImg(string url, string SaveUrl)
        {
            try
            {

                WebClient wc = new WebClient();
                wc.Headers.Add(HttpRequestHeader.Referer, url);//突破反盗链,模拟IE打开页面
                string picName = System.IO.Path.GetFileName(url);
                string fex = Common.FileString.GetFex(picName);

                string timeStr = Common.TimeString.GetNow_ff();
                picName = timeStr + "." + fex;
                string 未转换 = SaveUrl + "_wzh_" + picName;  //未经转换图片
                string fwqlinshi = System.Web.HttpContext.Current.Server.MapPath(未转换);//文件保存在服务器上的临时路径

                wc.DownloadFile(url, fwqlinshi);//下载图片到临时路径

                string 已转换 = 未转换.Replace("_wzh_", "");
                Common.ImgHelper.MakeThumNail(System.Web.HttpContext.Current.Server.MapPath(未转换), System.Web.HttpContext.Current.Server.MapPath(已转换), 0, 0);//将图片转换格式
                return 已转换;


            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        /// <summary>
        /// 下载一个图片，返回图片路径
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string downOneImg(string url, int i)
        {
            try
            {

                WebClient wc = new WebClient();
                wc.Headers.Add(HttpRequestHeader.Referer, url);//突破反盗链,模拟IE打开页面
                string picName = System.IO.Path.GetFileName(url);
                string fex = Common.FileString.GetFex(picName);

                string timeStr = Common.TimeString.GetNow_ff();
                picName = i + "." + fex;
                string 未转换 = "/upload/HttpDown/" + picName;  //未经转换图片
                string fwqlinshi = System.Web.HttpContext.Current.Server.MapPath(未转换);//文件保存在服务器上的临时路径

                wc.DownloadFile(url, fwqlinshi);//下载图片到临时路径

                string 已转换 = 未转换.Replace("_wzh_", "");
                Common.ImgHelper.MakeThumNail(System.Web.HttpContext.Current.Server.MapPath(未转换), System.Web.HttpContext.Current.Server.MapPath(已转换), 500, 500);//将图片转换格式
                return 已转换;


            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public static void LuPai1(string str)
        {

            string s = str;
            Bitmap bmp = new Bitmap(HttpContext.Current.Server.MapPath(@"/images/map/lupai1.png"));
            Graphics g = Graphics.FromImage(bmp);
            //Brush br = new SolidBrush(Color.Red);
            //g.FillRectangle(br, new Rectangle(0, 0, 100, 100));
            g.DrawString(s, new Font("微软雅黑", 12, FontStyle.Bold), Brushes.White, new Point(9, 2));
            HttpContext.Current.Response.ContentType = "image/jpeg";
            bmp.Save(HttpContext.Current.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Png);

            g.Dispose();
            bmp.Dispose();
            HttpContext.Current.Response.End();
        }


        public static void CreateMerLuPai1(decimal MerId, string MerName)
        {

            string s = Common.StringPlus.GetLeftStr(MerName, 1, "");

            Bitmap bmp = new Bitmap(HttpContext.Current.Server.MapPath(@"/images/map/lupai1.png"));
            Graphics g = Graphics.FromImage(bmp);
            //Brush br = new SolidBrush(Color.Red);
            //g.FillRectangle(br, new Rectangle(0, 0, 100, 100));
            g.DrawString(s, new Font("微软雅黑", 12, FontStyle.Bold), Brushes.White, new Point(9, 2));
            HttpContext.Current.Response.ContentType = "image/jpeg";
            //   bmp.Save(HttpContext.Current.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Png);
            bmp.Save(HttpContext.Current.Server.MapPath("/upload/mIcon/" + MerId + ".png"), ImageFormat.Png);
            g.Dispose();
            bmp.Dispose();

        }


    }



}
