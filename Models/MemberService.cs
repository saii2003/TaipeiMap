using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using myMember.Models;
using System.Net.Mail;
using System.Net;

namespace myMember.Models
{
    public class MemberService
    {
        private myMemberEntities db = new myMemberEntities();

        #region 檢查帳號是否重複
        public bool usernameCheck(string username)
        {
            var result = db.member.Where(m => m.username == username).FirstOrDefault();
            if (result != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region 會員註冊
        public bool register(member memstr)
        {
            string memAuthcode = authCode();

            member mem = new member();
            mem.username = memstr.username;
            mem.password = FormsAuthentication.HashPasswordForStoringInConfigFile(memstr.password,"MD5");
            mem.name = memstr.name;
            mem.sex = memstr.sex;
            mem.mail = memstr.mail;
            mem.authcode = memAuthcode;
            mem.isadmin = false;

            string authUrl = "<a href=http://" + HttpContext.Current.Request.ServerVariables["HTTP_HOST"] + "/Member/auth?code=" + memAuthcode + ">連結</a>";
            sendmail(memstr.mail, "myMember會員驗證信", "請點選以下連結進行會員認證：" + authUrl);

            db.member.AddObject(mem);
            db.SaveChanges();
            

            return true;

        }
        #endregion

        #region 登入
        public bool login(string username, string password)
        {
            string encrytpw = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");
            var mem = db.member.Where(m => m.username == username && m.password == encrytpw).FirstOrDefault();
            if (mem != null)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
        #endregion

        #region 檢查帳號是否驗證
        public bool checkAuth(string uername)
        {
            bool result = false;
            var mem = db.member.Where(m => m.username == uername && m.authcode == null).FirstOrDefault();
            if (mem != null)
            {
                result = true;
            }
            return result;
        }
        #endregion

        #region 修改密碼
        public bool changePasssword(string username,string password,string newPassword)
        {
            var result = false;
            var mem = db.member.Where(m => m.username == username).FirstOrDefault();
            if (mem != null)
            {
                if (mem.password == encryptPw(password))
                {
                    mem.password = encryptPw(newPassword);
                    db.SaveChanges();
                    result = true;
                }
            }
            return result;
        }
        #endregion

        #region 修改會員資料
        public bool changeMemData(string username, string name, string mail)
        {
            bool result = false;
            var mem = db.member.Where(m => m.username == username).FirstOrDefault();
            if (mem != null)
            {
                mem.name = name;
                mem.mail = mail;

                db.SaveChanges();
                result = true;
            }
            return result;
        }
        #endregion

        #region 忘記密碼
        public bool forgetPw(string uername, string mail)
        {
            bool result = false;
            var mem = db.member.Where(m => m.username == uername && m.mail == mail).FirstOrDefault();
            if (mem != null)
            {
                string newPw = authCode().ToLower();
                mem.password = encryptPw(newPw);
                db.SaveChanges();
               sendmail(mem.mail, "取的新密碼", "您的新密碼為:" + newPw + "<br>登入後請重修改您的密碼。");
               result = true;
            }
            return result;
        }
        #endregion

        #region 密碼加密
        public string encryptPw(string str)
        {
            string encrypt = FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5");
            return encrypt;
        }
        #endregion

        #region 權限 
        public bool isadmins(string username)
        {
            var mem = db.member.Where(m => m.username == username).FirstOrDefault();
            return mem.isadmin;
        }
        #endregion

        #region 寄信
        public void sendmail(string tomail,string subject,string body)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(tomail);
            mail.From = new MailAddress("ggogopro@gmail.com","myMember");
            mail.Subject = subject;
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = body;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("ggogopro@gmail.com", "i'4namaaeljo3xjp6");
            smtp.EnableSsl = true;
            smtp.Send(mail);

        }
        #endregion

        #region 會員查詢
        public IEnumerable<member> getDataList(string searchBy, string searchStr)
        {
            if (searchBy == "username")
            {
                IEnumerable<member> memlist = db.member.Where(m => m.username.Contains(searchStr));
                return memlist.ToList();
            }
            else
            {
                IEnumerable<member> memlist = db.member.Where(m => m.sex.Contains(searchStr));
                return memlist.ToList();
            }

        }
        #endregion

        #region 會員驗證
        public bool memAuth(string code)
        {
            bool result = false;
            var mem = db.member.Where(m => m.authcode == code).FirstOrDefault();
            if (mem != null)
            {
                mem.authcode = null;
                db.SaveChanges();
                result = true;
            }
            return result;
        }


        #endregion

        #region 驗證碼
        public string authCode()
        {
            string str = "0,1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            string[] code = str.Split(',');
            string authcode = string.Empty;
            Random rd = new Random();

            for (int i = 0; i < 15; i++)
            {
                authcode += code[rd.Next(code.Count())];
            }
            return authcode;
        }
        #endregion




    }
}