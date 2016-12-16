using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices;

namespace Common
{
    public class ServerSetting
    {


        public static void ReAppPool(string AppPoolName)
        {

            try
            {
                ConfigAppPool("Recycle", AppPoolName);
            }
            catch
            {
               
            }
        }

        //method是管理应用程序池的方法，有三种Start、S­top、Recycle，而 AppPoolName是应用程序池名称  
        public static void ConfigAppPool(string method, string AppPoolName)
        {
            DirectoryEntry appPool = new DirectoryEntry("IIS://localhos­t/W3SVC/AppPools");
            DirectoryEntry findPool = appPool.Children.Find(AppPoolName, "IIsApplicationPool");
            findPool.Invoke(method, null);
            appPool.CommitChanges();
            appPool.Close();
        }

    }
}
