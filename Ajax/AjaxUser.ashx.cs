using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ajax
{
    /// <summary>
    /// AjaxUser 的摘要说明
    /// </summary>
    public class AjaxUser :BPage.User, IHttpHandler
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