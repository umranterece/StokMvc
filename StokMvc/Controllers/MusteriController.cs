using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StokMvc.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace StokMvc.Controllers
{
    public class MusteriController : Controller
    {
        DbMvcStokEntities db = new DbMvcStokEntities();

        // GET: Musteri
        public ActionResult Index(int sayfa=1)
        {
            //var musteriliste = db.Tbl_Musteri.ToList();
            var musteriliste = db.Tbl_Musteri.Where(m=>m.MusteriDurum==true).ToList().ToPagedList(sayfa, 3);
            return View(musteriliste);
        }

        [HttpGet]
        public ActionResult YeniEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniEkle(Tbl_Musteri p)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniEkle");
            }
            p.MusteriDurum = true;
            db.Tbl_Musteri.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index","Musteri");
        }

        public ActionResult Sil(int id)
        {
            var musteri = db.Tbl_Musteri.Find(id);
            musteri.MusteriDurum = false;
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult Guncelle(int id)
        {
            var musteri = db.Tbl_Musteri.Find(id);

            return View("Guncelle",musteri);
        }

        [HttpPost]
        public ActionResult Guncelle(Tbl_Musteri p)
        {
            var musteri = db.Tbl_Musteri.Find(p.MusteriId);
            musteri.MusteriAdi = p.MusteriAdi;
            musteri.MusteriSoyadi = p.MusteriSoyadi;
            musteri.MusteriSehir = p.MusteriSehir;
            musteri.MusteriBakiye = p.MusteriBakiye;

            db.SaveChanges();
            return RedirectToAction("Index","Musteri");
        }
    }
}