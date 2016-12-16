using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Manage.test
{
    public partial class AddMember : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            int jf = 0;
            for (int i = 50; i <= 99; i++)
            {

                //BLL.MemberBLL mbll = new BLL.MemberBLL();
                //Model.MemberModel model = new Model.MemberModel();
                //model.Address = "";
                //model.SfzNo = "370000000000000000";
                //model.Memo = "sys";
                //model.MerId = 1999;
                //model.ProcessLvStatusId = 20;
                //model.ProcessLvId = 10;
                //model.AreaId = "110100";
                //model.Phone = (18600000000 + i).ToString();
                //mbll.ZhuCe(model, ref jf);

            }
        }
    }
}