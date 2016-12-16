using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ajax
{
    /// <summary>
    /// AjaxAbout 的摘要说明
    /// </summary>
    public class AjaxAbout :BPage.About, IHttpHandler
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