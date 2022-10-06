using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StokMvc.Models.Entity;

namespace StokMvc.Controllers
{
    public class KategoriController : Controller
    {
        DbMvcStokEntities db=new DbMvcStokEntities();

        // GET: Kategori
        public ActionResult Index()
        {
            var kategori = db.Tbl_Kategori.ToList();
            
            return View(kategori);
        }

        [HttpGet]
        public ActionResult YeniEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniEkle(Tbl_Kategori p)
        {
            db.Tbl_Kategori.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Sil(int id)
        {
            var ktg=db.Tbl_Kategori.Find(id);
            db.Tbl_Kategori.Remove(ktg);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Guncelle(int id)
        {
            var kategoris = db.Tbl_Kategori.Find(id);
            return View("Guncelle",kategoris);
        }

        [HttpPost]
        public ActionResult Guncelle(Tbl_Kategori p)
        {
            var kategoris = db.Tbl_Kategori.Find(p.KategoriId);
            kategoris.KategoriAdi = p.KategoriAdi;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}