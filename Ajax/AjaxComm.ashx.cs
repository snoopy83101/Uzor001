using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ajax
{
    /// <summary>
    /// AjaxComm 的摘要说明
    /// </summary>
    public class AjaxComm: BPage.Comm , IHttpHandler
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