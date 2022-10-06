using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using StokMvc.Models.Entity;

namespace StokMvc.Controllers
{
    public class SatisController : Controller
    {
        DbMvcStokEntities db = new DbMvcStokEntities();

        // GET: Satis
        public ActionResult Index()
        {
            var satislar = db.Tbl_Satis.ToList();
            return View(satislar);
        }

        [HttpGet]
        public ActionResult YeniEkle()
        {
            List<SelectListItem> urunler = (from item in db.Tbl_Urun.Where(x=>x.UrunDurum==true).ToList()
                                            select new SelectListItem
                                            {
                                                Text = item.UrunAdi,
                                                Value = item.UrunId.ToString()
                                            }).ToList();
            ViewBag.urunler = urunler;

            List<SelectListItem> personel = (from item in db.Tbl_Personel.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = item.PerAdi + item.PerSoyadi,
                                                 Value = item.PerId.ToString()
                                             }).ToList();
            ViewBag.personel = personel;

            List<SelectListItem> musteri = (from item in db.Tbl_Musteri.Where(x=>x.MusteriDurum==true).ToList()
                                            select new SelectListItem
                                            {
                                                Text = item.MusteriAdi + item.MusteriSoyadi,
                                                Value = item.MusteriId.ToString()
                                            }).ToList();
            ViewBag.musteri = musteri;

            return View();
        }

        [HttpPost]
        public ActionResult YeniEkle(Tbl_Satis p)
        {
            var uruns = db.Tbl_Urun.Where(x => x.UrunId == p.Tbl_Urun.UrunId).FirstOrDefault();
            p.Tbl_Urun = uruns;

            var personels = db.Tbl_Personel.Where(x => x.PerId == p.Tbl_Personel.PerId).FirstOrDefault();
            p.Tbl_Personel = personels;

            var musteris = db.Tbl_Musteri.Where(x => x.MusteriId == p.Tbl_Musteri.MusteriId).FirstOrDefault();
            p.Tbl_Musteri = musteris;

            db.Tbl_Satis.Add(p);
            db.SaveChanges();
            
            return RedirectToAction("Index","Satis");
        }

      
    }
}