using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using myMember.Models;

namespace myMember.Models
{
    public class changeMemData
    {
        public string username { set; get; }

        [DisplayName("暱稱")]
        [Required(ErrorMessage = "請輸入暱稱")]
        public string name { set; get; }
        [DisplayName("電子郵件")]
        [Required(ErrorMessage = "請輸入電子郵件")]   
        public string mail { set; get; }

        public List<changeMemData> mems { get; set; }
    }

}