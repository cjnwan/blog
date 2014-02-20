using System;
using System.Net.Mail;
using System.Web.Helpers;
using Domain.Model;

namespace Infrastructure
{
    public class MailSend
    {
        public static bool SendEmailReply(Comment commentSend,Comment commentReceive)
        {
            WebMail.SmtpServer = "smtp.qq.com";
            WebMail.SmtpPort = 25;//端口号，不同SMTP服务器可能不同，可以查一下
            WebMail.EnableSsl = false;//禁用SSL
            WebMail.UserName = "1743236613@qq.com";
            WebMail.Password = "lys405920747";//lys405920747
            WebMail.From = "1743236613@qq.com";
            WebMail.Send(commentReceive.CommentEmail, "博客回复", commentSend.CommentUserName+"回复了你"+commentReceive.CommentContent);
            return true;
        }

        public static bool SendEmail(Comment commentReceive)
        {
            try
            {
                WebMail.SmtpServer = "smtp.qq.com";
                WebMail.SmtpPort = 25;//端口号，不同SMTP服务器可能不同，可以查一下
                WebMail.EnableSsl = false;//禁用SSL
                WebMail.UserName = "1743236613@qq.com";
                WebMail.Password = "lys405920747";//lys405920747
                WebMail.From = "1743236613@qq.com";
                WebMail.Send("1278671543@qq.com", "博客回复", commentReceive.CommentContent);
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
           
            
        }

    }
}