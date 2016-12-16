using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ajax
{
    /// <summary>
    /// AjaxBus 的摘要说明
    /// </summary>
    public class AjaxBus :BPage.Bus, IHttpHandler
    {

    
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}