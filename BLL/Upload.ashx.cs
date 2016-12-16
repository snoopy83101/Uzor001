using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace localweb
{
    /// <summary>
    /// Summary description for upload
    /// </summary>
    public class Upload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int result = UploadFile(context);
            context.Response.Write(result.ToString());
        }
        private int UploadFile(HttpContext context) {
            try
            {
                HttpFileCollection hfc = context.Request.Files;
                if (hfc != null && hfc.Count > 0)
                {
                    string dir = AppDomain.CurrentDomain.SetupInformation.ApplicationBase+"Files";
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                    HttpPostedFile hpf = hfc[0];
                    string filePath = dir + "\\" + GetFileName(hpf.FileName);
                    
                    if (!CheckFileType(hpf.FileName))
                    {
                        return (int)ResultType.FileTypeInvalidated;
                    }
                    else if (!CheckFileSize(hpf.ContentLength))
                    {
                        return (int)ResultType.FileSizeInvalidated;
                    }
                    else
                    {
                        hpf.SaveAs(filePath);
                        return (int)ResultType.Success;
                    }
                }
            }
            catch{}
            return (int)ResultType.Fail;
        }
        private bool CheckFileType(string fileName)
        {
            if (string.IsNullOrEmpty(fileName) || !fileName.Contains("."))
            {
                return false;
            }
            string fileExtensions = "|.jpg|.bmp|.png|.gif|.txt|";
            string extention = Path.GetExtension(fileName).ToLower();
            if (fileExtensions.Contains("|" + extention + "|"))
            {
                return true;
            }
            return false;
        }
        private bool CheckFileSize(int size)
        {
            if (size > 300 * 1024 * 1024)
            {
                return false;
            }
            return true;
        }
        private string GetFileName(string fileName)
        {
            return string.Format("{0}-{1}{2}", DateTime.Now.ToString("yyyyMMddHHmmss"), new Random().Next(100000, 999999),Path.GetExtension(fileName));
        }
        private enum ResultType
        {
            Fail=0,
            Success=1,
            FileSizeInvalidated=2,
            FileTypeInvalidated=3
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}