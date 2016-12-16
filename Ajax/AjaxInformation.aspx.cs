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


    public partial class AjaxInformation : Common.BPageSetting
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                string para = ReStr("para");
                switch (para)
                {

                    case "doInvalid":
                        doInvalid();
                        break;

                    case "GetInformationList":
                        GetInformationList();  //获得分类信息列表
                        break;

                    case "SaveInformation":
                        SaveInformation();      //保存一条分类i型奶昔
                        break;
                    case "GetInformation":
                        GetInformation();      //获得一条分类信息数据
                        break;

                }
            }
            catch (Exception ex)
            {

                ReThrow(ex);
            }
            Response.End();

        }

        private void doInvalid()
        {
            bool Invalid = ReBool("doInvalid", true);
            decimal InformationId = ReDecimal("InformationId", 0);
            StringBuilder s=new StringBuilder();
            s.Append(" update dbo.Information set Invalid='" + Invalid + "' where InformationId='" + InformationId + "' ");
            DAL.DalComm.ExInt(s.ToString());
            ReTrue();

        }

        private void GetInformation()
        {

        }

        private void SaveInformation()
        {

            Model.InformationModel model = new InformationModel();
            BLL.InformationBLL bll = new BLL.InformationBLL();
            model.InformationId = ReDecimal("InformationId", 0);
            if (model.InformationId > 0)
            {
                bll.GetInformationModel(model.InformationId);
            }


            model.InformationClassId = 0;
            model.InformationTypeId = ReInt("InformationTypeId", 1);

            model.InformationContent = ReStrDeCode("InformationContent");
            model.ContactName = ReStr("ContactName");
            model.Property = "<root></root>";
            model.InformationTitle = ReStr("InformationTitle", "");


            string title = Common.StringPlus.OutHtmlText(model.InformationContent);
            title = Common.StringPlus.GetLeftStr(title, 40, "...");
            model.InformationTitle = title;
            model.QQ = ReStr("QQ", "");
            model.Tel = ReStr("Tel", "");
            model.Email = ReStr("Email");

            #region 事务开启

            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
            #endregion
                bll.SaveInformation(model);
                DataTable dtImg = ReTable("imgArray");
                dtImg = BLL.BJ.ImgSetting.ImgArraySetting(dtImg, model.InformationContent);
                if (dtImg != null)
                {
                    if (dtImg.Rows.Count > 0)
                    {
                        model.InformationImgId = dtImg.Rows[0]["ImgId"].ToString();
                    }

                }

                DataTable dtKeyWord = ReTable("KeyWordArray");
                if (dtKeyWord != null)
                {
                    DAL.InformationVsKeyWordDAL IvkDal = new DAL.InformationVsKeyWordDAL();
                    IvkDal.DeleteList(" InformationId='" + model.InformationId + "' ");  //删除所有关联
                    if (dtKeyWord.Rows.Count > 0)
                    {
                        foreach (DataRow drKeyWord in dtKeyWord.Rows)
                        {
                            Model.InformationVsKeyWordModel IvK = new InformationVsKeyWordModel();
                            IvK.InformationId = model.InformationId;
                            IvK.KeyWord = drKeyWord["KeyWord"].ToString();   //重新绑定关联
                            IvK.vsType = "sys";
                            IvkDal.Add(IvK);
                        }

                    }
                }




                bll.DeleteInformationImg(" InformationId='" + model.InformationId + "' ");



                if (dtImg != null)
                {

                    foreach (DataRow dr in dtImg.Rows)
                    {
                        Model.InformationVsImgModel IvI = new InformationVsImgModel();
                        IvI.ImgId = dr["ImgId"].ToString();
                        IvI.InformationId = model.InformationId;
                        IvI.vsType = "";
                        bll.SaveInformationImg(IvI);
                    }
                }

                #region 事务结束

                transactionScope.Complete();


            }
                #endregion

            ReTrue();
        }

        private void GetInformationList()
        {


            decimal InformationTypeId = ReDecimal("InformationTypeId", 1);
            StringBuilder s = new StringBuilder();
            string inputStr = ReStr("inputStr", "");
            bool Invalid=ReBool("Invalid",false);
            s.Append(" 1=1 ");
            if (inputStr.Trim() == "")
            {

            }
            else
            {
                s.Append(" and InformationContent like '%" + inputStr + "%' ");

            }



            BLL.InformationBLL bll = new BLL.InformationBLL();


            s.Append(" and InformationTypeId='" + InformationTypeId + "'  ");
            s.Append(" and Invalid='" + Invalid + "' ");
            DataTable dtKeyWord = ReTable("KeyWordArray");
            if (dtKeyWord != null)
            {
                if (dtKeyWord.Rows.Count > 0)
                {


                    List<string> keyWords = new List<string>();
                    foreach (DataRow dr in dtKeyWord.Rows)
                    {
                        keyWords.Add("'"+dr["KeyWord"].ToString()+"'");
                    }

                    s.Append(" and  InformationId in ( select InformationId from  dbo.InformationVsKeyWord  WITH(NOLOCK)  where KeyWord in (" + string.Join(",", keyWords) + ")) ");
                }
          
            }

            s.Append(" order by RecommendLv desc, CreateTime desc ");

            DataSet ds = bll.GetInformationPageList(s.ToString(), ReInt("CurrentPage"), ReInt("i", 60));
            RePage(ds);
        }
    }
