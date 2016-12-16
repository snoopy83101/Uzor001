using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ajax
{
    /// <summary>
    /// AjaxBBs 的摘要说明
    /// </summary>
    public class AjaxBBs :BPage.BBS, IHttpHandler
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