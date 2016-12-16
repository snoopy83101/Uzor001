using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common
{
    public class Mail
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="Subject">主题</param>
        /// <param name="Body">发送内容</param>
        /// <param name="To">目标邮件地址，多个邮件用，逗号分隔</param>
        /// <returns></returns>
        public static bool Send(string Subject, string Body, string To)
        {
            string SysEmail = "nmj_yyinfo@yeah.net";
            string SiteName = "南麻街 - 沂源信息港旗下生活服务平台";
            string smtpSite = "smtp.yeah.net";
            int Port = 25;
            bool SSL = true;
            string MailUser = "nmj_yyinfo";
            string MailPwd = "WANGLI83";
            //NormalConfigInfo  configInfo = NormalConfig.Load();
            MailMessage message = new MailMessage();
            message.From = new MailAddress(SysEmail, SiteName);
            message.IsBodyHtml = true;
            message.BodyEncoding = Encoding.UTF8;
            message.SubjectEncoding = Encoding.UTF8;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = smtpSite;
            smtp.Port = Port;
            smtp.EnableSsl = SSL;

            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential(MailUser, MailPwd);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            bool reBool = true;
            message.Subject = Subject;
            message.Body = Body;
            string[] tos = Regex.Split(To, ",");
            for (int i = 0; i < tos.Length; i++)
            {
                message.To.Add(tos[i]);
            }

            try
            {
                smtp.Send(message);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return reBool;
        }




    }
}
