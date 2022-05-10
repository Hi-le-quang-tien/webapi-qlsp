using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyApiService.Controllers
{
    public class SanPhamController : ApiController
    {
        //Lấy toàn bộ danh sách Sản phẩm
        [HttpGet]
        public List<SanPham> AllSanPham()
        {

            DataClasses1DataContext context = new DataClasses1DataContext();
            List<SanPham> dsSP = context.SanPhams.ToList();
            // khử đệ quy
            foreach (SanPham sp in dsSP)
                sp.DanhMuc = null;
            return dsSP;
        }
        //Lấy chi tiết Sản phẩm
        [HttpGet]
        public SanPham DetailSanPham(String id)
        {
            DataClasses1DataContext context = new DataClasses1DataContext();
            SanPham sp = context.SanPhams.FirstOrDefault(x => x.MaSP == id);
            if (sp != null)
                sp.DanhMuc = null;
            return sp;
        }
        //Lấy ds Sản phẩm theo dm
        [HttpGet]
        public List<SanPham> ListSanPhamTheoDM(String madm)
        {
            DataClasses1DataContext context = new DataClasses1DataContext();
            List<SanPham> dssp = context.SanPhams
                                        .Where(x => x.MaDM == madm)
                                        .ToList();
            foreach (SanPham sp in dssp)
                sp.DanhMuc = null;
            return dssp;
        }
        //
        [HttpGet]
        public List<SanPham> TimDanhSachSanPhamCoGiaTrongDoanAB(int a, int b)
        {
            DataClasses1DataContext context = new DataClasses1DataContext();
            List<SanPham> dsSP = context.SanPhams
                                    .Where(x => x.DonGia >= a && x.DonGia <= b)
                                    .ToList();
            foreach (SanPham sp in dsSP)
                sp.DanhMuc = null;
            return dsSP;
        }
        [HttpPost]
        public bool LuuSanPham(string masp, string tensp, int soluong, double dongia, string madm)
        {
            try
            {
                DataClasses1DataContext context = new DataClasses1DataContext();
                SanPham sp = new SanPham();
                sp.MaSP = masp;
                sp.TenSP = tensp;
                sp.SoLuong = soluong;
                sp.DonGia = dongia;
                sp.MaDM = madm;
                context.SanPhams.InsertOnSubmit(sp);
                context.SubmitChanges();
                return true;
            }
            catch { }
            return false;
        }
        [HttpPut]
        public bool SuaSanPham(string masp, string tensp, int soluong, double dongia)
        {
            try
            {
                DataClasses1DataContext context = new DataClasses1DataContext();
                SanPham sp = context.SanPhams.FirstOrDefault(x => x.MaSP == masp);
                if (sp != null)
                {
                    sp.TenSP = tensp;
                    sp.DonGia = dongia;
                    sp.SoLuong = soluong;
                    context.SubmitChanges();
                    return true;
                }
            }
            catch { }
            return false;
        }
        [HttpDelete]
        public bool XoaSanPham(string masp)
        {
            try
            {
                DataClasses1DataContext context = new DataClasses1DataContext();
                SanPham sp = context.SanPhams.FirstOrDefault(x => x.MaSP == masp);
                if (sp != null)
                {
                    context.SanPhams.DeleteOnSubmit(sp);
                    context.SubmitChanges();
                    return true;
                }
            }
            catch { }
            return false;
        }
    }
}
