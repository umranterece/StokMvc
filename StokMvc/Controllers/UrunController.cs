using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using StokMvc.Models.Entity;

namespace StokMvc.Controllers
{
    public class UrunController : Controller
    {
        DbMvcStokEntities db = new DbMvcStokEntities();
        // GET: Urun
        public ActionResult Index()
        {
            var urunler = db.Tbl_Urun.Where(x=>x.UrunDurum==true).ToList();
            return View(urunler);
        }

        [HttpGet]
        public ActionResult YeniEkle()
        {
            List<SelectListItem> degerler = (from item in db.Tbl_Kategori.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = item.KategoriAdi,
                                                 Value = item.KategoriId.ToString()
                                             }).ToList();
            ViewBag.Degerler = degerler;
            return View();
        }

        [HttpPost]
        public ActionResult YeniEkle(Tbl_Urun p)
        {
            var katg = db.Tbl_Kategori.Where(m => m.KategoriId == p.Tbl_Kategori.KategoriId).FirstOrDefault();
            p.Tbl_Kategori = katg;   

            db.Tbl_Urun.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //public ActionResult Sil(int id)
        //{
        //    var ogrencis = db.Tbl_Urun.Find(id);
        //    db.Tbl_Urun.Remove(ogrencis);
        //    db.SaveChanges();

        //    return RedirectToAction("Index");

        //}

        public ActionResult Sil(int id)
        {
            var uruns = db.Tbl_Urun.Find(id);
            uruns.UrunDurum = false;
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        [HttpGet]
        public ActionResult Guncelle(int id)
        {
            var katg = db.Tbl_Urun.Find(id);

            List<SelectListItem> list = (from item in db.Tbl_Kategori.ToList()
                                         select new SelectListItem
                                         {
                                             Text = item.KategoriAdi,
                                             Value = item.KategoriId.ToString()
                                         }).ToList();
            ViewBag.Kategori = list;
            return View("Guncelle", katg);
        }

        [HttpPost]
        public ActionResult Guncelle(Tbl_Urun p)
        {
            var uruns = db.Tbl_Urun.Find(p.UrunId);

            uruns.UrunAdi = p.UrunAdi;
            uruns.UrunStok= p.UrunStok;
            uruns.UrunAlisFiyat= p.UrunAlisFiyat;
            uruns.UrunSatisFiyat= p.UrunSatisFiyat;
            uruns.UrunMarka = p.UrunMarka;

            var katg = db.Tbl_Kategori.Where(m => m.KategoriId == p.Tbl_Kategori.KategoriId).FirstOrDefault();
            uruns.Tbl_Kategori = katg;
            db.SaveChanges();
            return RedirectToAction("Index", "Urun");
        }
    }
}