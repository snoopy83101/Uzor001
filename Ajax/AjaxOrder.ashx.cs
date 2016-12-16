using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ajax
{
    /// <summary>
    /// AjaxOrder 的摘要说明
    /// </summary>
    public class AjaxOrder : BPage.Order, IHttpHandler
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