<%@ WebHandler Language="C#" Class="AjaxMerchant" %>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

using Common;
using System.Text;
using System.Transactions;
using Model;

public class AjaxMerchant : BPage.Merchant, IHttpHandler
{



  
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}