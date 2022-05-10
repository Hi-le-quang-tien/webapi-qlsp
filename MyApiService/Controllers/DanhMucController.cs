using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyApiService.Controllers
{
    public class DanhMucController : ApiController
    {
        [HttpGet]
        public List<DanhMuc> LayToanBoDanhMuc()
        {
            DataClasses1DataContext context = new DataClasses1DataContext();
            List<DanhMuc> dsDM = context.DanhMucs.ToList();
            foreach (DanhMuc dm in dsDM)
                dm.SanPhams.Clear();
            return dsDM;
        }
        [HttpGet]
        public DanhMuc ChiTietDanhMuc(string id)
        {
            DataClasses1DataContext context = new DataClasses1DataContext();
            DanhMuc dm = context.DanhMucs
                                .FirstOrDefault(x => x.MaDM == id);
            if (dm != null)
                dm.SanPhams.Clear();
            return dm;
        }
        [HttpPost]
        public bool LuuDanhMuc(string madm, string tendm)
        {
            try
            {
                DataClasses1DataContext context = new DataClasses1DataContext();
                DanhMuc dm = new DanhMuc();
                dm.MaDM = madm;
                dm.TenDM = tendm;
                context.DanhMucs.InsertOnSubmit(dm);
                context.SubmitChanges();
                return true;
            }
            catch { }
            return false;
        }
        [HttpPut]
        public bool SuaDanhMuc(string madm, string tendm)
        {
            try
            {
                DataClasses1DataContext context = new DataClasses1DataContext();
                DanhMuc dm = context.DanhMucs
                                .FirstOrDefault(x => x.MaDM == madm);
                if (dm != null)
                {
                    dm.TenDM = tendm;
                    context.SubmitChanges();
                    return true;
                }
            }
            catch { }
            return false;
        }
        [HttpDelete]
        public bool XoaDanhMuc(string madm)
        {
            try
            {
                DataClasses1DataContext context = new DataClasses1DataContext();
                DanhMuc dm = context.DanhMucs
                                    .FirstOrDefault(x => x.MaDM == madm);
                if (dm != null)
                {
                    if (dm.SanPhams.Count > 0) return false;
                    context.DanhMucs.DeleteOnSubmit(dm);
                    context.SubmitChanges();
                    return true;
                }
            }
            catch { }
            return false;
        }
    }
}
