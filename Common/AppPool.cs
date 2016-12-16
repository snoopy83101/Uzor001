using Microsoft.Web.Administration;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class AppPool
    {

        public static void Recycle(string AppPoolName)
        {

            //IIS7的方法
            ServerManager iisManager = new ServerManager();
        
            iisManager.ApplicationPools[AppPoolName].Recycle();


            #region IIS6的方法
            //if (!IsAppPoolName(AppPoolName))
            //{
            //    throw new Exception("不存在");
            //}


            //try
            //{


            //    DirectoryEntry appPool = new DirectoryEntry("IIS://localhost/W3SVC/AppPools","administrator1","wangli83");
            //    DirectoryEntry findPool = appPool.Children.Find(AppPoolName, "IIsApplicationPool");
            //    findPool.Invoke("Recycle", null);
            //    appPool.CommitChanges();
            //    appPool.Close();

            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(ex.Message);

            //}  


            #endregion

        
        }

        /// <summary>
        /// 判断程序池是否存在
        /// </summary>
        /// <param name="AppPoolName">程序池名称</param>
        /// <returns>true存在 false不存在</returns>
        private static bool IsAppPoolName(string AppPoolName)
        {
            bool result = false;
            DirectoryEntry appPools = new DirectoryEntry("IIS://localhost/W3SVC/AppPools");
            foreach (DirectoryEntry getdir in appPools.Children)
            {
                if (getdir.Name.Equals(AppPoolName))
                {
                    result = true;
                }
            }
            return result;
        }


    }
}
