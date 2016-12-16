using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ajax
{
    /// <summary>
    /// AjaxWork 的摘要说明
    /// </summary>
    public class AjaxWork :BPage.Work, IHttpHandler
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