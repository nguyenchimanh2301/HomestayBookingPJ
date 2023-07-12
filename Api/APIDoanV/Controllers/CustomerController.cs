﻿using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using APIDoanV.Model;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;

namespace APIDoanV.Controllers
{
    [ApiController]
    public class CustomerController : Controller
    {
        QuanlyhomestayContext dbc = new QuanlyhomestayContext();
        /* [Route("send")]

         [HttpPost]
         public IActionResult SendEmail(EmailModel email)
         {
             try
             {
                 using (var client = new System.Net.Mail.SmtpClient())
                 {
                     var credentials = new NetworkCredential
                     {
                         UserName = "nmanh230101@gmail.com",
                         Password = "rnwmfjzwgsepypbg"
                     };

                     client.Credentials = credentials;
                     client.Host = "smtp.gmail.com";
                     client.Port = 587;
                     client.EnableSsl = true;

                     var fromEmail = new MailAddress("nmanh230101@gmail.com");
                     var toEmailObj = new MailAddress(email.ToEmail);

                     var mail = new MailMessage(fromEmail, toEmailObj)
                     {
                         Subject = email.Subject,
                         Body = email.Body,
                         IsBodyHtml = true
                     };

                     client.Send(mail);
                 }

                 return Ok();
             }
             catch (Exception ex)
             {
                 return StatusCode(500, ex.Message);
             }
         }
 */


        [Route("getacc")]
        [HttpGet]
        public IActionResult Getuser()
        {
            var result = dbc.Accounts.Select(x => new
            {
                TaiKhoan = x.TaiKhoan,
                MatKhau = x.MatKhau
            }).ToList();
            return Json(result);

        }
        [Route("update_kh")]
        [HttpPut]
        public void update( KhachHang kh)
        {
            try
            {
                dbc.KhachHangs.Attach(kh);
                dbc.Entry(kh).State = EntityState.Modified;
                dbc.SaveChanges();
            }
            catch (Exception e)
            {

                throw e;
            }
        }
       
        [Route("add_bill")]
        [HttpPost]
        public void postbill(DatPhong dp)
        {
            dbc.DatPhongs.Add(dp);
            dbc.SaveChanges();
        }
        [Route("get_Cus")]
        [HttpGet]
        public IActionResult Getcus()
        {
            var cus = dbc.KhachHangs.Select(x => new
            {
                TenKh = x.TenKh,
                Id = x.Id,
                DiaChi = x.DiaChi,
                Email = x.Email,
                Sdt = x.Sdt,
            }).ToList();
            return Json(cus);
        }
        [Route("get_cus_by_id")]
        [HttpGet]
        public IActionResult Getcusid(int id)
        {
            var cus = dbc.KhachHangs.Select(x => new
            {
                TenKh = x.TenKh,
                Id = x.Id,
                DiaChi = x.DiaChi,
                Email = x.Email,
                Sdt = x.Sdt,
            }).FirstOrDefault(x=>x.Id==id);
            return Json(cus);
        }
        [Route("checkout_cokh")]
        [HttpPost]
        public IActionResult Create([FromBody] Checkout model)
        {
            
            model.datphong.Thanhtoan = true;
            model.datphong.IdkhNavigation = null;
            dbc.DatPhongs.Add(model.datphong);
            dbc.SaveChanges();
            int MaDonHang = model.datphong.Id;
            model.donhang.Iddondat = MaDonHang;
            // Xóa navigation property trước khi thêm vào cơ sở dữ liệu
            model.donhang.IdpNavigation = null;
            dbc.ChitietDatPhongs.Add(model.donhang);
            dbc.SaveChanges();

            return Ok(new { data = "OK" });
        }
        [Route("checkout")]
        [HttpPost]
        public IActionResult CreateBill([FromBody] CheckoutModel model)
        {
            dbc.KhachHangs.Add(model.kh);
            dbc.SaveChanges();
            int MaKhachHang = model.kh.Id;
            model.datphong.Idkh = MaKhachHang;
            model.datphong.Tenkh = model.kh.TenKh;
            model.datphong.Thanhtoan = true;
                model.datphong.IdkhNavigation = null;
                dbc.DatPhongs.Add(model.datphong);
                dbc.SaveChanges();
                int MaDonHang = model.datphong.Id;
                model.donhang.Iddondat = MaDonHang;
                // Xóa navigation property trước khi thêm vào cơ sở dữ liệu
                model.donhang.IdpNavigation = null;
                dbc.ChitietDatPhongs.Add(model.donhang);
                dbc.SaveChanges();
           
            return Ok(new { data = "OK" });
        }
        public class Checkout
        {
            public ChitietDatPhong donhang { get; set; }
            public DatPhong datphong { get; set; }

        }
        public class CheckoutModel
        {
            public KhachHang kh { get; set; }
            public ChitietDatPhong donhang { get; set; }
            public DatPhong datphong { get; set; }

        }
        public class EmailModel
        {
            public string ToEmail { get; set; }
            public string Subject { get; set; }
            public string Body { get; set; }
        }
        /*Scaffold-DbContext "Server=LAPTOP-LLHPT87U\SQLEXPRESS;Database=QUANLYHOMESTAY;Trusted_Connection=True;Encrypt=False" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Model -force*/
    }
}
