using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using myMember.Models;
using System.Web.Security;
using System.Data;

namespace myMember.Controllers
{
    public class MemberController : Controller
    {
        private myMemberEntities db = new myMemberEntities();
        private MemberService mem = new MemberService();
        //
        // GET: /Member/
        public ActionResult googlemap()
        {
            return View();
        }
        #region 註冊
        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Member");
            }

            return View();
            
        }
        [HttpPost]
        public ActionResult Register(member memstr)
        {
            if (ModelState.IsValid)
            {
                if (mem.usernameCheck(memstr.username))
                {
                    if (mem.register(memstr))
                    {
                        TempData["state"] = "註冊成功";
                        return RedirectToAction("RegisterResult", "Member");
                    }   
                }
                else
                {
                    ModelState.AddModelError("username","帳號已經註冊過");
                }
                
            }
            return View();
        }
        #endregion

        #region 會員驗證
        public ActionResult auth()
        {
            if (Request.QueryString["code"] == null)
            {
                return RedirectToAction("Index", "Member");
            }
            else
            {
                if(mem.memAuth(Request.QueryString["code"].ToString()))
                {
                    TempData["result"]="驗證成功";
                }
                else
                {
                    TempData["result"]="驗證失敗";
                }
            }
            return View();
        }
        #endregion

        #region 註冊成功
        public ActionResult RegisterResult()
        {
            if (TempData["state"] == null)
            {
                return RedirectToAction("Index", "Member");
            }
            
            return View();
        }
        #endregion

        #region 登入
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Member");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                    if (mem.login(login.username, login.password))
                    {
                        if (mem.checkAuth(login.username))
                        {
                            if (login.remember)
                            {
                                FormsAuth(login, 43400);
                                return RedirectToAction("Index", "Member");
   
                            }
                            else
                            {
                                FormsAuth(login, 60);
                                return RedirectToAction("Index", "Member");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "您的帳號尚未驗證");
                        }

                    }
                    else
                    {
                        ModelState.AddModelError("", "帳號或密碼有誤");
                    }
                
            }
            return View();
        }
        #endregion

        #region 登出
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.RemoveAll();
            return RedirectToAction("Login", "Member");
        }
        #endregion

        #region 修改密碼
        public ActionResult Changepw()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Member");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Changepw(string password, string newpassword)
        {
            if (ModelState.IsValid)
            {
                if (mem.changePasssword(User.Identity.Name.ToString(), password, newpassword))
                {
                    TempData["result"] = "密碼修改成功";
                    return RedirectToAction("memResult", "Member");
                }
                else
                {
                    ModelState.AddModelError("", "資料輸入有誤");
                }
            }
            return View();
        }
        #endregion

        #region 修改密碼成功
        public ActionResult Changeresult()
        {
            if (!User.Identity.IsAuthenticated && TempData["changePwState"] == null)
            {
                return RedirectToAction("Login", "Member");
            }
            return View();
        }
        #endregion

        #region 修改會員資料
 
        public ActionResult changeMemData()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Member");
            }
            else
            {
                changeMemData memData = new changeMemData();
                var memDatas = memData.mems.Where(m => m.username == "saii2006").FirstOrDefault();
                return View(memDatas);
            }
            return View();
        }

        [HttpPost]
        public ActionResult changeMemData(string name, string mail)
        {
            if (ModelState.IsValid)
            {
                if (mem.changeMemData(User.Identity.Name, name, mail))
                {
                    TempData["result"] = "修改成功";
                    return RedirectToAction("memresult", "Member");
                }
                else
                {
                    ModelState.AddModelError("", "修改資料失敗");
                }
            }
            return View();
        }
        #endregion

        #region 修改會員資料成功

        public ActionResult changeOk()
        {
            if (!User.Identity.IsAuthenticated)
            {
                if (TempData["changeMemDataState"] == null)
                {
                    return RedirectToAction("Index", "Member");
                }
            }
            return View();
        }
        #endregion

        #region 忘記密碼
        public ActionResult forgetPw()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Member");
            }
            return View();
        }
        [HttpPost]
        public ActionResult forgetPw(string username,string mail)
        {
            if (ModelState.IsValid)
            {
                if (mem.forgetPw(username, mail))
                {
                    TempData["result"] = "新密碼已經寄送到您的信箱";
                    return RedirectToAction("Result", "Member");

                }
                else
                {
                    ModelState.AddModelError("", "您輸入的資料有誤");
                }
            }
            return View();
        }
        #endregion

        #region 忘記密碼成功
        public ActionResult Result()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Member");
            }
            return View();
        }
        #endregion

        #region 結果
        public ActionResult memResult()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Member");
            }            
            return View();
        }
        #endregion

        public ActionResult Map()
        {
            return View();
        }

        public ActionResult Intro()
        {
            return View();
        }
        public ActionResult Intros()
        {
            return View();
        }

        //----------------------------------------------------管理權限----------------------------------------------------//
        //---------------------------------------------------------------------------------------------------------------//
        

        #region 全部會員資料
        public ActionResult Datalist()
        {
            if (User.Identity.IsAuthenticated)
            {
                FormsAuthenticationTicket tick = ((FormsIdentity)User.Identity).Ticket;
                if (Convert.ToBoolean(tick.UserData) == false)
                {
                    return RedirectToAction("Index", "Member");

                }
            }
            else
            {
                return RedirectToAction("Index", "Member");
            }
            return View(db.member.ToList());
        }
        #endregion
        #region 新增資料
        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                FormsAuthenticationTicket tick = ((FormsIdentity)User.Identity).Ticket;
                if (Convert.ToBoolean(tick.UserData) == false)
                {
                    return RedirectToAction("Index", "Member");

                }
            }
            else
            {
                return RedirectToAction("Index", "Member");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Create([Bind(Exclude="id")] member memStr)
        {
            if (ModelState.IsValid)
            {
                member mems = new member();
                if (db.member.Where(m => m.username == memStr.username).FirstOrDefault() == null)
                {
                    mems.username = memStr.username;
                    mems.password = FormsAuthentication.HashPasswordForStoringInConfigFile(memStr.password, "MD5");
                    mems.name = memStr.name;
                    mems.sex = memStr.sex;
                    mems.mail = memStr.mail;
                    mems.authcode = null;
                    mems.isadmin = memStr.isadmin;

                    db.member.AddObject(mems);
                    db.SaveChanges();
                    return RedirectToAction("Datalist", "Member");
                }
                else
                {
                    ModelState.AddModelError("", "此帳號已經使用");
                }
            }
            return View();
        }
        #endregion


        #region 編輯會員資料
        public ActionResult Edit(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                FormsAuthenticationTicket ticket = ((FormsIdentity)User.Identity).Ticket;
                if (Convert.ToBoolean(ticket.UserData) == false)
                {
                    return RedirectToAction("Index", "Member");

                }
                else
                {
                    if (id != null)
                    {
                        return View(db.member.Where(m => m.id == id).FirstOrDefault());
                    }
                    else
                    {
                        return RedirectToAction("Index", "Member");
                    }
                }
            }
            else
            {
                return RedirectToAction("Index", "Member");
            }
        }
        [HttpPost]
        public ActionResult Edit(member memStr)
        {
            myMemberEntities db = new myMemberEntities();

            if (ModelState.IsValid)
            {
                var mem = db.member.Where(m => m.id == memStr.id).FirstOrDefault();
                mem.name = memStr.name;
                mem.sex = memStr.sex;
                mem.mail = memStr.mail;
                mem.isadmin = memStr.isadmin;
                db.SaveChanges();
                return RedirectToAction("Datalist", "Member");
            }
            return View();

        }
        #endregion

        #region 搜尋會員資料
        public ActionResult Search(string searchBy,string searchStr)
        {

            if (User.Identity.IsAuthenticated)
            {
                FormsAuthenticationTicket tick = ((FormsIdentity)User.Identity).Ticket;
                if (Convert.ToBoolean(tick.UserData) == false)
                {
                    return RedirectToAction("Index", "Member");

                }
                else
                {
                    var mems = from m in db.member
                               select m;
                    if (!string.IsNullOrEmpty(searchStr))
                    {
                        if (searchBy == "username")
                        {
                            mems = mems.Where(m => m.username.Contains(searchStr));
                        }
                        else
                        {
                            mems = mems.Where(m => m.sex.Contains(searchStr));
                        }
                    }
                    return View(mems);
                }
            }
            else
            {
                return RedirectToAction("Index", "Member");
            }    
        }
        #endregion

        #region 刪除資料
        public ActionResult Delete(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                FormsAuthenticationTicket tick = ((FormsIdentity)User.Identity).Ticket;
                if (Convert.ToBoolean(tick.UserData) == false)
                {
                    return RedirectToAction("Index", "Member");

                }
                else
                {
                    if (id == null)
                    {
                        return RedirectToAction("Index", "Member");
                    }
                    var mems = db.member.Where(m => m.id == id).FirstOrDefault();
                    if (mems == null)
                    {
                        return HttpNotFound();
                    }
                    return View(mems);
                }
            }
            else
            {
                return RedirectToAction("Index", "Member");
            }         
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var mems = db.member.Where(m => m.id == id).FirstOrDefault();
            if (mems != null)
            {
                db.member.DeleteObject(mems);
                db.SaveChanges();
                return RedirectToAction("Datalist","Member");
            }

            return View();
        }
        #endregion

        #region 表單票據
        private void FormsAuth(LoginViewModel login, int expiration)
        {
            FormsAuthenticationTicket tick = new FormsAuthenticationTicket(1,
                        login.username,
                        DateTime.Now,
                        DateTime.Now.AddMinutes(expiration),
                        false,
                        mem.isadmins(login.username).ToString(),
                        FormsAuthentication.FormsCookiePath);

            string encrypt = FormsAuthentication.Encrypt(tick);
            Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encrypt));
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
