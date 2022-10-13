using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_BookStore.Models
{
    public class CartItem
    {
        //thuộc tính biểu diễn thông tin của sản phẩm (Sách)
        public Sach Sach { set; get; }
        //thuộc tính lưu thông tin số lượng của sản phẩm
        public int SoLuong { set; get; }
        //thuộc tính trả về giá trị thành tiền của sản phẩm
        public double ThanhTien
        {
            get { return (double)Sach.Dongia * SoLuong; }
        }
    }
    //-----------định nghĩa lớp biểu diễn thông tin giỏ hàng---------------
    public class Cart
    {
        //sử dụng lớp List kiểu <CartItem>
        //để lưu trữ danh sách các sản phẩm trong giỏ hàng
        private List<CartItem> _items;
        //hàm khởi tạo
        public Cart()
        {
            _items = new List<CartItem>();
        }
        //thuộc tính trả về danh sách sản phẩm trong giỏ hàng
        public List<CartItem> Items
        {
            get { return _items; }
        }
        //hàm thêm sản phẩm (Sách) vào giỏ
        public void AddItem(Sach s, int soluong)
        {
            CartItem item = Items.Find(x => x.Sach.MaSach == s.MaSach);
            if (item != null)
            {
                item.SoLuong += soluong;
            }
            else
            {
                Items.Add(new CartItem { Sach = s, SoLuong = soluong });
            }
        }
        //hàm cập nhật số lượng sản phẩm trong giỏ
        public void UpdateItem(Sach s, int soluong)
        {
            CartItem item = Items.Find(x => x.Sach.MaSach == s.MaSach);
            if (item != null)
            {
                if (soluong > 0)
                {
                    item.SoLuong = soluong;
                }
                else //số lượng <=0
                {
                    Items.Remove(item); //xoá luôn sản phẩm khỏi giỏ
                }
            }
        }
        //hàm xoá sản phẩm trong giỏ
        public void DeleteItem(Sach s)
        {
            CartItem item = Items.Find(x => x.Sach.MaSach == s.MaSach);
            if (item != null)
            {
                Items.Remove(item);
            }
        }
        //thuộc tính trả về tổng thành tiền của giỏ hàng
        public double TongThanhTien
        {
            get
            {
                double sum = Items.Sum(x => x.ThanhTien);
                return sum;
            }
        }
    }
}