using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Threading;

namespace Common
{
    public class FileString
    {



        /// <summary>
        /// 返回文件夹路径,不包括upload文件夹之前, 因为这个有可能被upload200用到
        /// </summary>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        public static string GetSaveFilePath(string FilePath)
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
                return  dir + dateTime.ToString("\\\\yyyyMMddhhmmssffff");
            }
            catch (Exception ex)
            {
                //WriteJs("parent.uploaderror();");
                return string.Empty;
            }
        }

        /// <summary>
        /// 获得文件扩展名
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetExtension(string fileName)
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
        public static string GetExtension(string fileName,string CatchStr)
        {
            string str = GetExtension(fileName);
            if (str.Trim() == "")
            {
                return CatchStr;
            }
            else
            {
                return str;
            }
        }

        /// <summary>
        /// 验证是否图片
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsImg(string path)
        {

            switch (GetFex(path).ToLower())
            {
                case "jpg":
                case "png":
                case "gif":
                case "tif":
                case "bmp":
                case "jpeg":

                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// 返回当前根目录
        /// </summary>
        /// <returns></returns>
        public static string RootDirectory()
        {
            System.Web.UI.Page mypage = (System.Web.UI.Page)HttpContext.Current.Handler;
            return mypage.ResolveUrl("~/");

        }

        /// <summary>
        /// 传入.NET中的根目录路径(例如:~/uploadfiles/123.doc")返回浏览器可是使用的路径.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string ForMatFileUrl(string url)
        {
            System.Web.UI.Page mypage = (System.Web.UI.Page)HttpContext.Current.Handler;
            return mypage.ResolveUrl(url);
        }
        /// <summary>
        /// 判断文件是否存在,传入.NET中的根目录路径(例如: ~/uploadfiles/123.doc)即可判定
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool IsFileCunZai(string url)
        {
            if (url.Trim() == "")
            {
                return false;
            }

            url = HttpContext.Current.Server.MapPath(url);
            if (System.IO.File.Exists(url) == true)
            {
                return true;

            }
            else
            {
                return false;
            }


        }

        ///<summary> 
        /// 计算字节大小函数，传入一以字节为单位的值。
        /// </summary> /// <param name="size">要计算的字节大小，单位为“字节”</param> 
        /// <returns>计算后的大小值</returns>
        public static string FormateSize(long size)
        {
            const double baseKB = 1024.00, baseMB = 1024 * 1024.00, baseGB = 1024 * 1024 * 1024.00;
            string strSize = "";
            if (size < baseKB)
            {
                strSize = size.ToString() + " B";
            }
            if (size > baseKB && size < baseMB)
            {
                strSize = string.Format("{0:0.##} KB", (size / baseKB));
            }
            else if (size > baseMB && size < baseGB)
            {
                strSize = string.Format("{0:0.##} MB", (size / baseMB));
            }
            else if (size > baseGB)
            {
                strSize = string.Format("{0:0.##} GB", (size / baseGB));
            }
            return strSize;
        }


        /// <summary>
        /// 获得当前文件的大小.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string FileLength(string url)
        {
            if (IsFileCunZai(url) == true)
            {
                url = HttpContext.Current.Server.MapPath(url);
                FileInfo fi = new FileInfo(url);
                return FormateSize(fi.Length);
            }
            else
            {
                return "";
            }

        }


        /// <summary>
        /// 将文件名称改名为时间字符串.后缀
        /// </summary>
        /// <param name="FileName">传入文件名称(123.jpg)不能是链接或者路径!</param>
        /// <returns></returns>
        public static string ChangeFileNameForTimeString(string FileName)
        {

            string fix = GetFex(FileName);
            string fileName = TimeString.GetString(DateTime.Now);
            return fileName + "." + fix;

        }

        public static string ChangeFileFix(string FileName, string Fix)
        {
            string fix = GetFex(FileName);
            string s = FileName.Replace(fix, Fix);
            return s;
        }

        #region 获取文件名称
        /// <summary>  
        /// 获取文件名称(包括后缀名)
        /// </summary>  
        /// <param name="path">路径</param>  
        /// <returns></returns>  
        public static string GetFileName(String path)
        {

            if (path.Contains("\\"))
            {

                string[] arr = path.Split('\\');

                return arr[arr.Length - 1];

            }

            else
            {

                string[] arr = path.Split('/');

                return arr[arr.Length - 1];

            }

        }
        /// <summary>  
        /// 获取文件名称(包括后缀名)
        /// </summary>  
        /// <param name="path">路径</param>  
        /// <returns></returns>  
        public static string GetFileUrl2(String path, string JoinStr)
        {

            string fex = "." + GetFex(path);

            return path.Replace(fex, JoinStr + fex);


        }

        /// <summary>
        /// 取得文件名称,不包括后缀
        /// </summary>
        /// <param name="FileName">传入文件名或者链接</param>
        /// <returns></returns>
        public static string GetFileTitle(string FileName)
        {
            FileName = GetFileName(FileName);

            string fix = GetFex(FileName);

            string s = FileName.Replace("." + fix, "");
            return s;
        }

        #endregion

        #region 获取文件后缀名

        /// <summary>  
        /// 获取文件后缀名，不包括.
        /// </summary>  
        /// <param name="filename">文件名</param>  
        /// <returns></returns>  
        public static String GetFex(string filename)
        {

            return filename.Substring(filename.LastIndexOf(".") + 1);

        }

        #endregion

        #region 获取文件目录
        /// <summary>  
        /// 获取文件后缀名  
        /// </summary>  
        /// <param name="filename">文件名</param>  
        /// <returns></returns>  

        public static String GetDirectory(string filename)
        {

            return filename.Substring(0, filename.LastIndexOf("/"));

        }

        #endregion

        #region 返回当前虚拟目录的真实目录

        /// <summary>  
        /// 返回当前虚拟目录的真实目录  
        /// </summary>  
        /// <param name="SavePath">虚拟目录</param>  
        /// <param name="iscreate">没有是否创建</param>  
        /// <returns></returns>  
        internal static string GetPath(string SavePath, bool iscreate)
        {

            string path = System.Web.HttpContext.Current.Server.MapPath(SavePath);

            if (!System.IO.Directory.Exists(path) && iscreate)
            {

                System.IO.Directory.CreateDirectory(path);

            }

            return path;

        }



        #endregion

        /// <summary>
        /// 判断文件夹是否存在
        /// </summary>
        /// <param name="url">绝对路径</param>
        /// <param name="b">文件夹不存在时,是否创建 True:创建</param>
        /// <returns>bool</returns>
        public static string is_show_jia(string url, bool b)
        {
            url = HttpContext.Current.Server.MapPath(url);
            bool bb = System.IO.Directory.Exists(url);
            if (b && !bb)//指定了 不存在时也创建目录 且 目录确实不存在
            {
                System.IO.Directory.CreateDirectory(url);//创建文件夹

            }
            return url;

        }


        public static string 创建文件目录(string url)
        {
            string fileName = GetFileName(url);
            url = url.Replace(fileName, "");
            url = HttpContext.Current.Server.MapPath(url);
            bool bb = System.IO.Directory.Exists(url);
            if (!bb)//指定了 不存在时也创建目录 且 目录确实不存在
            {
                System.IO.Directory.CreateDirectory(url);//创建文件夹

            }
            return url;

        }
        public static void CreatWenJianJia(string url)
        {
            url = HttpContext.Current.Server.MapPath(url);
            bool bb = System.IO.Directory.Exists(url);
            if (!bb)//指定了 不存在时也创建目录 且 目录确实不存在
            {
                System.IO.Directory.CreateDirectory(url);//创建文件夹

            }

        }


        private bool _alreadyDispose = false;



        protected virtual void Dispose(bool isDisposing)
        {
            if (_alreadyDispose) return;
            //if (isDisposing)
            //{
            //    if (xml != null)
            //    {
            //        xml = null;
            //    }
            //}
            _alreadyDispose = true;
        }
        public static string ChangeFileNameToTime(string fileName)
        {
            string fex = GetFex(fileName);

            return TimeString.GetNow_ff() + "." + fex;
        }




        #region IDisposable 成员

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region 取得文件后缀名
        /****************************************
         * 函数名称：GetPostfixStr
         * 功能说明：取得文件后缀名
         * 参    数：filename:文件名称
         * 调用示列：
         *           string filename = "aaa.aspx";        
         *           string s = EC.FileObj.GetPostfixStr(filename);         
        *****************************************/
        /// <summary>
        /// 取后缀名
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns>.gif|.html格式</returns>
        public static string GetPostfixStr(string filename)
        {
            int start = filename.LastIndexOf(".");
            int length = filename.Length;
            string postfix = filename.Substring(start, length - start);
            return postfix;
        }
        #endregion

        #region 写文件
        /****************************************
         * 函数名称：WriteFile
         * 功能说明：写文件,会覆盖掉以前的内容
         * 参    数：Path:文件路径,Strings:文本内容
         * 调用示列：
         *           string Path = Server.MapPath("Default2.aspx");       
         *           string Strings = "这是我写的内容啊";
         *           EC.FileObj.WriteFile(Path,Strings);
        *****************************************/
        /// <summary>
        /// 写文件
        /// </summary>
        /// <param name="Path">文件路径</param>
        /// <param name="Strings">文件内容</param>
        public static void WriteFile(string Path, string Strings)
        {
            if (!System.IO.File.Exists(Path))
            {
                System.IO.FileStream f = System.IO.File.Create(Path);
                f.Close();
            }
            System.IO.StreamWriter f2 = new System.IO.StreamWriter(Path, false, System.Text.Encoding.GetEncoding("gb2312"));
            f2.Write(Strings);
            f2.Close();
            f2.Dispose();
        }
        #endregion

        #region 读文件
        /****************************************
         * 函数名称：ReadFile
         * 功能说明：读取文本内容
         * 参    数：Path:文件路径
         * 调用示列：
         *           string Path = Server.MapPath("Default2.aspx");       
         *           string s = EC.FileObj.ReadFile(Path);
        *****************************************/
        /// <summary>
        /// 读文件
        /// </summary>
        /// <param name="Path">文件路径</param>
        /// <returns></returns>
        public static string ReadFile(string Path)
        {
            string s = "";
            if (!System.IO.File.Exists(Path))
                s = "不存在相应的目录";
            else
            {
                StreamReader f2 = new StreamReader(Path, System.Text.Encoding.GetEncoding("gb2312"));
                s = f2.ReadToEnd();
                f2.Close();
                f2.Dispose();
            }

            return s;
        }
        #endregion

        #region 追加文件
        /****************************************
         * 函数名称：FileAdd
         * 功能说明：追加文件内容
         * 参    数：Path:文件路径,strings:内容
         * 调用示列：
         *           string Path = Server.MapPath("Default2.aspx");     
         *           string Strings = "新追加内容";
         *           EC.FileObj.FileAdd(Path, Strings);
        *****************************************/
        /// <summary>
        /// 追加文件
        /// </summary>
        /// <param name="Path">文件路径</param>
        /// <param name="strings">内容</param>
        public static void FileAdd(string Path, string strings)
        {
            StreamWriter sw = File.AppendText(Path);
            sw.Write(strings);
            sw.Flush();
            sw.Close();
        }
        #endregion

        #region 拷贝文件
        /****************************************
         * 函数名称：FileCoppy
         * 功能说明：拷贝文件
         * 参    数：OrignFile:原始文件,NewFile:新文件路径
         * 调用示列：
         *           string orignFile = Server.MapPath("Default2.aspx");     
         *           string NewFile = Server.MapPath("Default3.aspx");
         *           EC.FileObj.FileCoppy(OrignFile, NewFile);
        *****************************************/
        /// <summary>
        /// 拷贝文件
        /// </summary>
        /// <param name="OrignFile">原始文件</param>
        /// <param name="NewFile">新文件路径</param>
        public static void FileCoppy(string orignFile, string NewFile)
        {
            File.Copy(orignFile, NewFile, true);
        }

        #endregion

        #region 删除文件
        /****************************************
         * 函数名称：FileDel
         * 功能说明：删除文件
         * 参    数：Path:文件路径
         * 调用示列：
         *           string Path = Server.MapPath("Default3.aspx");    
         *           EC.FileObj.FileDel(Path);
        *****************************************/
        /// <summary>
        /// 删除文件,传入类似"~/uploadfiles/"的.NET路径,如果文件存在就删除,如果文件不存在则不会作出任何操作!
        /// </summary>
        /// <param name="src">传入类似"~/uploadfiles/"的.NET路径</param>
        public static void FileDel(string src)
        {
            if (IsFileCunZai(src) == true)
            {
                src = HttpContext.Current.Server.MapPath(src);
                File.Delete(src);
            }
        }

        public static void FileDel2(string path)
        {

            File.Delete(path);

        }
        #endregion

        #region 移动文件
        /****************************************
         * 函数名称：FileMove
         * 功能说明：移动文件
         * 参    数：OrignFile:原始路径,NewFile:新文件路径
         * 调用示列：
         *            string orignFile = Server.MapPath("../说明.txt");    
         *            string NewFile = Server.MapPath("http://www.cnblogs.com/说明.txt");
         *            EC.FileObj.FileMove(OrignFile, NewFile);
        *****************************************/
        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="OrignFile">原始路径</param>
        /// <param name="NewFile">新路径</param>
        public static void FileMove(string orignFile, string NewFile)
        {
            File.Move(orignFile, NewFile);
        }
        #endregion

        #region 在当前目录下创建目录
        /****************************************
         * 函数名称：FolderCreate
         * 功能说明：在当前目录下创建目录
         * 参    数：OrignFolder:当前目录,NewFloder:新目录
         * 调用示列：
         *           string orignFolder = Server.MapPath("test/");    
         *           string NewFloder = "new";
         *           EC.FileObj.FolderCreate(OrignFolder, NewFloder); 
        *****************************************/
        /// <summary>
        /// 在当前目录下创建目录
        /// </summary>
        /// <param name="OrignFolder">当前目录</param>
        /// <param name="NewFloder">新目录</param>
        public static void FolderCreate(string orignFolder, string NewFloder)
        {
            Directory.SetCurrentDirectory(orignFolder);
            Directory.CreateDirectory(NewFloder);
        }
        #endregion

        #region 递归删除文件夹目录及文件
        /****************************************
         * 函数名称：DeleteFolder
         * 功能说明：递归删除文件夹目录及文件
         * 参    数：dir:文件夹路径
         * 调用示列：
         *           string dir = Server.MapPath("test/");  
         *           EC.FileObj.DeleteFolder(dir);       
        *****************************************/
  /// <summary>
  /// 删除文件夹下所有的文件
  /// </summary>
  /// <param name="url">url地址</param>
  /// <param name="deleteMe">是否连文件夹也删除</param>
        public static void DeleteFolder(string url,bool deleteMe)
        {

            string dir = HttpContext.Current.Server.MapPath(url);
            if (Directory.Exists(dir)) //如果存在这个文件夹删除之 
            {
                foreach (string d in Directory.GetFileSystemEntries(dir))
                {
                    if (File.Exists(d))
                        File.Delete(d); //直接删除其中的文件 
                    else
                        DeleteFolder(d, deleteMe); //递归删除子文件夹 
                }
                if (deleteMe)
                {
                    Directory.Delete(dir); //删除已空文件夹 
                }
            }

        }

        #endregion

        #region 将指定文件夹下面的所有内容copy到目标文件夹下面 果目标文件夹为只读属性就会报错。
        /****************************************
         * 函数名称：CopyDir
         * 功能说明：将指定文件夹下面的所有内容copy到目标文件夹下面 果目标文件夹为只读属性就会报错。
         * 参    数：srcPath:原始路径,aimPath:目标文件夹
         * 调用示列：
         *           string srcPath = Server.MapPath("test/");  
         *           string aimPath = Server.MapPath("test1/");
         *           EC.FileObj.CopyDir(srcPath,aimPath);   
        *****************************************/
        /// <summary>
        /// 指定文件夹下面的所有内容copy到目标文件夹下面
        /// </summary>
        /// <param name="srcPath">原始路径</param>
        /// <param name="aimPath">目标文件夹</param>
        public static void CopyDir(string srcPath, string aimPath)
        {
            try
            {
                // 检查目标目录是否以目录分割字符结束如果不是则添加之
                if (aimPath[aimPath.Length - 1] != Path.DirectorySeparatorChar)
                    aimPath += Path.DirectorySeparatorChar;
                // 判断目标目录是否存在如果不存在则新建之
                if (!Directory.Exists(aimPath))
                    Directory.CreateDirectory(aimPath);
                // 得到源目录的文件列表，该里面是包含文件以及目录路径的一个数组
                //如果你指向copy目标文件下面的文件而不包含目录请使用下面的方法
                //string[] fileList = Directory.GetFiles(srcPath);
                string[] fileList = Directory.GetFileSystemEntries(srcPath);
                //遍历所有的文件和目录
                foreach (string file in fileList)
                {
                    //先当作目录处理如果存在这个目录就递归Copy该目录下面的文件

                    if (Directory.Exists(file))
                        CopyDir(file, aimPath + Path.GetFileName(file));
                    //否则直接Copy文件
                    else
                        File.Copy(file, aimPath + Path.GetFileName(file), true);
                }

            }
            catch (Exception ee)
            {
                throw new Exception(ee.ToString());
            }
        }



        /// <summary>
        /// 遍历文件夹下所有的文件
        /// </summary>
        /// <param name="srcPath"></param>
        /// <returns></returns>
        public static List<string> FileNameList(string srcPath)
        {

            srcPath = HttpContext.Current.Server.MapPath(srcPath);
            string[] fileList = Directory.GetFileSystemEntries(srcPath);
            List<string> s = new List<string>();
            foreach (var item in fileList)
            {
                s.Add(GetFileName(item));
            }
            return s;
        }


        #endregion
        /// <summary>
        /// 将updatefiles下任何形式的链接,格式化为标准类似"~/uploadfiles/ad/1.jpg"的链接
        /// </summary>
        /// <param name="src">传入链接</param>
        /// <returns></returns>
        public static string FileSrcFormat(string src)
        {
            src = src.ToLower();
            if (src.IndexOf("uploadfiles/") > -1)
            {

                string LeftUploadFiles = StringPlus.GetFirstStr(src, "uploadfiles/");
                src = src.Replace(LeftUploadFiles, "~/");
            }

            return src;
        }
        /// <summary>
        /// 将updatefiles下任何形式的链接,转化为标准的类似"/uploadfiles/ad/1.jpg"的链接,方便链接调用.
        /// </summary>
        /// <param name="src">传入要处理的链接地址</param>
        /// <param name="page">一般为this</param>
        /// <returns></returns>
        public static string HttpSrcFormat(string src, Page page)
        {
            src = src.ToLower();
            if (src.IndexOf("uploadfiles/") > -1)
            {
                string Format_src = FileSrcFormat(src);
                return page.ResolveUrl(Format_src);
            }
            else
            {
                return src;
            }

        }

        public static bool ResponseFile(HttpRequest _Request, HttpResponse _Response, string _fileName, string _fullPath, long _speed)
        {
            try
            {
                FileStream myFile = new FileStream(_fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(myFile);
                try
                {
                    _Response.AddHeader("Accept-Ranges", "bytes");
                    _Response.Buffer = false;
                    long fileLength = myFile.Length;
                    long startBytes = 0;

                    double pack = 10240; //10K bytes
                    //int sleep = 200;   //每秒5次   即5*10K bytes每秒
                    int sleep = (int)Math.Floor(1000 * pack / _speed) + 1;
                    if (_Request.Headers["Range"] != null)
                    {
                        _Response.StatusCode = 206;
                        string[] range = _Request.Headers["Range"].Split(new char[] { '=', '-' });
                        startBytes = Convert.ToInt64(range[1]);
                    }
                    _Response.AddHeader("Content-Length", (fileLength - startBytes).ToString());
                    if (startBytes != 0)
                    {
                        //Response.AddHeader("Content-Range", string.Format(" bytes {0}-{1}/{2}", startBytes, fileLength-1, fileLength));
                    }
                    _Response.AddHeader("Connection", "Keep-Alive");
                    _Response.ContentType = "application/octet-stream";
                    _Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(_fileName, System.Text.Encoding.UTF8));

                    br.BaseStream.Seek(startBytes, SeekOrigin.Begin);
                    int maxCount = (int)Math.Floor((fileLength - startBytes) / pack) + 1;

                    for (int i = 0; i < maxCount; i++)
                    {
                        if (_Response.IsClientConnected)
                        {
                            _Response.BinaryWrite(br.ReadBytes(int.Parse(pack.ToString())));
                            Thread.Sleep(sleep);
                        }
                        else
                        {
                            i = maxCount;
                        }
                    }
                }
                catch
                {
                    return false;
                }
                finally
                {
                    br.Close();

                    myFile.Close();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }



        /// <summary>
        /// 文件流下载
        /// </summary>
        /// <param name="filepath">文件在服务器上的路径</param>
        /// <param name="name">文件名</param>
        /// <returns></returns>
        public static bool Down(string filepath, string name)
        {


            HttpResponse response = HttpContext.Current.Response;
            bool succ = false;
            response.Clear();
            System.IO.Stream iStream = null;
            byte[] buffer = new Byte[10000];
            int length;
            long dataToRead;

            string filename = System.IO.Path.GetFileName(filepath);
            string rex = GetFex(filename);

            try
            {
                iStream = new System.IO.FileStream(filepath, System.IO.FileMode.Open,
                System.IO.FileAccess.Read, System.IO.FileShare.Read);
                dataToRead = iStream.Length;
                string namestr = name + "." + rex;
                namestr = HttpContext.Current.Server.UrlEncode(namestr);
                response.ContentType = "application/octet-stream";
                response.AddHeader("Content-Disposition", "attachment; filename=" + namestr);
                while (dataToRead > 0)
                {
                    if (response.IsClientConnected)
                    {
                        length = iStream.Read(buffer, 0, 10000);
                        response.OutputStream.Write(buffer, 0, length);
                        response.Flush();
                        buffer = new Byte[10000];
                        dataToRead = dataToRead - length;
                    }
                    else
                    {
                        dataToRead = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                response.Write("Error : " + ex.Message);
            }
            finally
            {
                if (iStream != null)
                {
                    iStream.Close();
                }
            }

            response.End();
            return succ;
        }
    }



}

