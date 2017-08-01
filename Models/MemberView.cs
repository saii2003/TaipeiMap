using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace myMember.Models
{
    public class MemberView
    {
        public string id { get; set; }

        [DisplayName("帳號")]
        [Required(ErrorMessage = "請輸入帳號")]
        [StringLength(12,ErrorMessage="請輸入6~12字元",MinimumLength=6)]
        public string username { get; set; }

        [DisplayName("密碼")]
        [Required(ErrorMessage = "請輸入密碼")]
        [StringLength(12, ErrorMessage = "請輸入6~12字元", MinimumLength = 6)]
        public string password { get; set; }

        [DisplayName("確認密碼")]
        [Required(ErrorMessage = "請輸入確認密碼")]
        [Compare("password", ErrorMessage = "密碼和確認密碼必須一致")]
        public string confirmPw { get; set; }

        [DisplayName("暱稱")]
        [Required(ErrorMessage = "請輸入暱稱")]
        public string name { get; set; }

        [DisplayName("性別")]
        [Required(ErrorMessage = "請選擇性別")]
        public string sex { get; set; }

        [DisplayName("電子郵件")]
        [Required(ErrorMessage = "請輸入電子郵件")]
        public string mail { get; set; }

    }
}