<%@ WebHandler Language="C#" Class="AjaxMember" %>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using Common;
using Model;
using System.Transactions;

public class AjaxMember : BPage.Member, IHttpHandler
{

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}
