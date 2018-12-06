using System;
using System.Net;
using System.Net.Mail;
using YK.Platform.Unity.Extensions;

namespace YK.Platform.Unity.Email
{
    /// <summary>
    ///EmailHelper 发送邮件类
    /// </summary>
    public class EmailHelper
    {
        private EmailSet es;
        public EmailHelper(EmailSet emailSet)
        {
            es = emailSet;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="content">发送内容</param>
        /// <param name="toEmail">目标邮件,多邮件发送以“;”号隔开</param>
        /// <returns></returns>
        public bool SendEmail(string title, string content, string toEmail,string fileName)
        {
            try
            {
                MailAddress from = new MailAddress(es.EmailName);
                MailAddress reply = new MailAddress(es.EmailName);
                foreach (string add in toEmail.Split(';'))
                {
                    MailAddress to = new MailAddress(add);
                    MailMessage msg1 = new MailMessage(from, to);
                    msg1.ReplyTo = reply;
                    msg1.Subject = title;
                    msg1.Body = content;
                    msg1.BodyEncoding = System.Text.Encoding.UTF8;
                    msg1.IsBodyHtml = true;
                    //msg1.Sender = sender;

                    if (!string.IsNullOrEmpty(fileName))
                    {
                        Attachment attachment = new Attachment(fileName);
                        msg1.Attachments.Add(attachment);
                    }

                    SmtpClient client = new SmtpClient(es.SMTP, es.Port.ToInt());//SMTP地址,"端口号"
                    client.Credentials = new NetworkCredential(es.EmailName, es.EmailPwd);
                    client.Send(msg1);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
