using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_BookStore.Models
{
    public class RegisterAdmin
    {
        [Required(ErrorMessage = "Chưa nhập họ tên")]
        public string Name { set; get; }
        [Required(ErrorMessage = "Chưa nhập tên đăng nhập")]
        public string User { set; get; }
        [Required(ErrorMessage = "Chưa nhập mật khẩu")]
        public string Pass { set; get; }
        [Compare("Pass", ErrorMessage = "Mật khẩu nhập lại không khớp")]
        public string RePass { set; get; }
        public DateTime Born { set; get; }
        public string Email { set; get; }
        [Required(ErrorMessage = "Chưa nhập số điện thoại")]
        [RegularExpression("0[0-9]{9}", ErrorMessage = "Số điện thoại không hợp lệ")]
        public string Phone { set; get; }
    }
}