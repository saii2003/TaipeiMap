using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using myMember.Models;

namespace myMember.Models
{
    public class memDatalist
    {
      
        public string searchBy { get; set; }
        [Required(ErrorMessage="請輸入查詢字串")]
        public string searchStr { get; set; }
        public List<member> Datalist { get; set; }

        


    }
}