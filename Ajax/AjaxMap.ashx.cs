using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ajax
{
    /// <summary>
    /// AjaxMap 的摘要说明
    /// </summary>
    public class AjaxMap :BPage.Map, IHttpHandler
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