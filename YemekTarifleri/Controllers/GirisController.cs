using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using yemektarifleri.Controllers;
using Yemektarifleri.Models;

namespace Yemektarifleri.Controllers
{
    [Route("Giris")]

    public class GirisController : Controller
    {
        [HttpGet("GirisYap")]

        public IActionResult GirisYap()
        {
            return View();
        }
        [HttpPost]
        [HttpGet("GirisYap2")]

        public async Task<IActionResult> GirisYap(Kullanicilar k, string ReturnUrl)
        {
            yemektarifleriDbContext db = new yemektarifleriDbContext();
            var kullanici = db.Kullanicilars.FirstOrDefault(kul => kul.Eposta == k.Eposta && kul.Parola == MD5Sifrele(k.Parola)
            && kul.Silindi == false && kul.Aktif == true);
            if(kullanici != null)
            {
                string yetki = (bool)kullanici.Yetki ? "Yonetici" : "Uye";
                var talepler = new List<Claim>()
                {
                    new Claim(ClaimTypes.Email,kullanici.Eposta),
                    new Claim(ClaimTypes.Role,yetki),
                    new Claim(ClaimTypes.NameIdentifier,kullanici.KullaniciId.ToString())
                };
                ClaimsIdentity kimlik=new ClaimsIdentity(talepler,"Login");
                ClaimsPrincipal kural=new ClaimsPrincipal(kimlik);
                await HttpContext.SignInAsync(kural);
                if (!String.IsNullOrEmpty(ReturnUrl))
                {
                    return Redirect(ReturnUrl);
                }
                else
                {
                    if ((bool)kullanici.Yetki)
                    {
                        return Redirect("/Yonetim/Index");
                    }
                    else
                    {
                        return Redirect("/Home/Index");

                    }
                }
            }
            return View();
        }
        [HttpGet("CikisYap")]

        public async Task<IActionResult> CikisYap()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index","Home");
        }

        public static string MD5Sifrele(string sifrelenecekMetin)
        {

            // MD5CryptoServiceProvider sınıfının bir örneğini oluşturduk.
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            //Parametre olarak gelen veriyi byte dizisine dönüştürdük.
            byte[] dizi = Encoding.UTF8.GetBytes(sifrelenecekMetin);
            //dizinin hash'ini hesaplattık.
            //dizi = md5.ComputeHash(dizi);
            //Hashlenmiş verileri depolamak için StringBuilder nesnesi oluşturduk.
            StringBuilder sb = new StringBuilder();
            //Her byte'i dizi içerisinden alarak string türüne dönüştürdük.

            foreach (byte ba in dizi)
            {
                sb.Append(ba.ToString("x2").ToLower());
            }

            //hexadecimal(onaltılık) stringi geri döndürdük.
            return sb.ToString();
        }
    }
}
