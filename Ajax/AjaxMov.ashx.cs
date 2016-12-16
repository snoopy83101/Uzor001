using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ajax
{
    /// <summary>
    /// AjaxMov 的摘要说明
    /// </summary>
    public class AjaxMov :BPage.Mov, IHttpHandler
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