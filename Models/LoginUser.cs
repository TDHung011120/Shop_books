using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_BookStore.Models
{
    public class LoginUser
    {
        [Required(ErrorMessage = "Chưa nhập tên đăng nhập")]
        public string User { set; get; }
        [Required(ErrorMessage = "Chưa nhập mật khẩu")]
        public string Pass { set; get; }
    }
}