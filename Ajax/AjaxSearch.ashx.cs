using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ajax
{
    /// <summary>
    /// AjaxSearch 的摘要说明
    /// </summary>
    public class AjaxSearch : BPage.Search, IHttpHandler
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