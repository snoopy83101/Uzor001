using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Linq;


namespace BeeCloud
{

    public static class BeeCloud
    {
        /// <summary>
        /// 注册APP，masterSecret为需要退款/打款功能时注册
        /// </summary>
        public static void registerApp(string appID, string appSecret, string masterSecret, string testSecret) 
        {
            BCCache.Instance.appId = appID;
            BCCache.Instance.appSecret = appSecret;
            BCCache.Instance.masterSecret = masterSecret;
            BCCache.Instance.testSecret = testSecret;
        }

        /// <summary>
        /// 设置是否为测试模式
        /// </summary>
        /// <param name="testMode"></param>
        public static void setTestMode(bool testMode)
        {
            BCCache.Instance.testMode = testMode;
        }

        public static void setNetworkTimeout(int timeout)
        {
            BCCache.Instance.networkTimeout = timeout;
        }

        
    }
}
