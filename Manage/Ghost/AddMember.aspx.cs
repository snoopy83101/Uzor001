using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Manage.Ghost
{
    public partial class AddMember : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BLL.MemberBLL bll = new BLL.MemberBLL();



            decimal phone = 18600000000;
            for (int i = 0; i < 40; i++)
            {
                var p = phone + i;
                bll.YzmLogin(1999, p.ToString(), "1666");
            }

        }
    }
}