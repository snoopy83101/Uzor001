using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
namespace Manage.test
{
    public partial class TestCountOrderToWork : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            StringBuilder s = new StringBuilder();
            s.Append(" select * from  dbo.OrderToWork WHERE   OrderToWorkStatusId = 60 ");
            DataSet ds= DAL.DalComm.BackData(s.ToString());


            DataTable dt = ds.Tables[0];

            BLL.OrderBLL bll = new BLL.OrderBLL();
            foreach (DataRow  dr in dt.Rows)
            {

                decimal OrderToWorkId = decimal.Parse(dr["OrderToWorkId"].ToString());
                bll.CountOrderToWork(OrderToWorkId);

                string OrderId = dr["OrderId"].ToString();
                bll.CountOrder(OrderId);


            }

          

        }
    }
}