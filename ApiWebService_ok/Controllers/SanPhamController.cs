using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApiWebService_ok.Controllers
{
    // link dưới dành cho sql local
    // connectionString="Data Source=quangtien;Initial Catalog=QLSPWebServer;Persist Security Info=True;User ID=sa;Password=123"
    // connectionString="workstation id=QLSPWebserver.mssql.somee.com;packet size=4096;user id=quangtien1009_SQLLogin_1;pwd=34a27cs5gz;data source=QLSPWebserver.mssql.somee.com;persist security info=False;initial catalog=QLSPWebserver"

    public class SanPhamController : ApiController
    {
        //Lấy toàn bộ danh sách Sản phẩm
        [HttpGet]
        public List<SanPham> AllSanPham()
        {
            QLSPWebserverDataContext context = new QLSPWebserverDataContext();
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
            QLSPWebserverDataContext context = new QLSPWebserverDataContext();
            SanPham sp = context.SanPhams.FirstOrDefault(x => x.MaSP == id);
            if (sp != null)
                sp.DanhMuc = null;
            return sp;
        }
        //Lấy ds Sản phẩm theo dm
        [HttpGet]
        public List<SanPham> ListSanPhamTheoDM(String madm)
        {
            QLSPWebserverDataContext context = new QLSPWebserverDataContext();
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
            QLSPWebserverDataContext context = new QLSPWebserverDataContext();
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
                QLSPWebserverDataContext context = new QLSPWebserverDataContext();
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
                QLSPWebserverDataContext context = new QLSPWebserverDataContext();
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
                QLSPWebserverDataContext context = new QLSPWebserverDataContext();
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
