using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace myMember.Models
{
    public class LoginViewModel
    {
        [DisplayName("帳號")]
        [Required(ErrorMessage = "請輸入帳號")]
        public string username { get; set; }

        [DisplayName("密碼")]
        [Required(ErrorMessage = "請輸入密碼")]
        public string password { get; set; }

        [DisplayName("記住我")]
        public bool remember { get; set; }
    }
}