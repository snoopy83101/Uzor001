using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Config
    {





        public static  string GetAppValue(string Key)
        {


            return System.Configuration.ConfigurationSettings.AppSettings[Key].ToString();



        }
        public static string GetAppValue(string Key, string CatchStr)
        {
            try
            {

                return GetAppValue(Key);
            }
            catch (Exception)
            {

                return CatchStr;
            }

        }
    }
}
