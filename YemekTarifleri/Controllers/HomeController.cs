using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Yemektarifleri.Models;

namespace Yemektarifleri.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        
        yemektarifleriDbContext db = new yemektarifleriDbContext();

        public IActionResult Index(int id)
        {
            var sayfa = db.Sayfalars.Where(a=>a.Silindi==false && a.Aktif==true && a.SayfaId==id).FirstOrDefault();
            return View(sayfa);
        }

        public IActionResult TumTarifler()
        {
            var tarifler = db.YemekTarifleris.Include(k =>k.Kategori).Where(y => y.Silindi == false && y.Aktif == true ).OrderBy(s=>s.Sira).ToList();
            return View(tarifler);
        }

        public IActionResult Tarif(int id)
        {
            TarifYorumlar t=new TarifYorumlar();
            var tarifler = db.YemekTarifleris.Include(k => k.Kategori).Where(y => y.Silindi == false && y.Aktif == true && y.TarifId==id).FirstOrDefault();
            t.tarif = tarifler;
            var yorumlar = db.Yorumlars.Include(u=>u.Uye).Where(y => y.Silindi == false && y.Aktif == true && y.TarifId == id).ToList();
            t.yorumlar= yorumlar;
            return View(t);
        }
        public IActionResult YorumYap(Yorumlar yor)
        {
            yor.Silindi=false;
            yor.Aktif=false;    
            yor.Eklemetarihi=DateTime.Now;
            db.Yorumlars.Add(yor);
            db.SaveChanges();
            TempData["mesaj"] = "Yorumunuz alýndý. Yönetici onayýndan sonra görünecektir.";
            return Redirect("/Home/Tarif/"+yor.TarifId);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
