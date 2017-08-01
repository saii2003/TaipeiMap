using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace myMember.Models
{
    public class ChangePwViewModel
    {
        [DisplayName("舊密碼")]
        [Required(ErrorMessage = "請輸入舊密碼")]
        public string password { get; set; }

        [DisplayName("新密碼")]
        [Required(ErrorMessage = "請輸入新密碼")]
        public string newPassword { get; set; }

        [DisplayName("確認新密碼")]
        [Required(ErrorMessage = "請輸入確認新密碼")]
        [Compare("newPassword",ErrorMessage="新密碼和確認新密碼需一致")]
        public string confirmPassword { get; set; }

    }
}