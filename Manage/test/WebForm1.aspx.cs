using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Manage.test
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BLL.MsgBLL bll = new BLL.MsgBLL();
            bll.SendMsgToDevice(10,"111111", "CheckOrderToWork", "1", "messager");
        }
    }
}