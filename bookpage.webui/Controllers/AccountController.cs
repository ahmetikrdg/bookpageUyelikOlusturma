using System.Threading.Tasks;
using bookpage.webui.Identity;
using bookpage.webui.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace bookpage.webui.Controllers
{
    public class AccountController:Controller
    {
        private UserManager<User> _userManager;//UserManager ıdentity içinde varolan bir yapı. <User> clasımız.Usermanager kullanıcı oluşturma login parla sıfırlama gibi temel işlemleri barındırır.
        private SignInManager<User> _singInManager;//cookie işleri

        public AccountController(UserManager<User> userManager,SignInManager<User> singInManager)
        {
            _userManager=userManager;
            _singInManager=singInManager;
        }
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            var user=new User()
            {
                FirstName=model.FirstName,
                LastName=model.LastName,
                UserName=model.UserName,
                Email=model.EMail,
            };//modeldekileri usere attım passwordi kashlememiz şifrelemememiz gerek açık olmaması gerek usermanager aracılığı ile halledeceğiz
            
            var result=await _userManager.CreateAsync(user,model.Password);//creatasync bir user bekliyodu verdik birde şifre onuda modelden gönderdik
            if(result.Succeeded)
            {
                //gitmeden önce token olması lazım
                return RedirectToAction("Login","Account");
            }
            ModelState.AddModelError("","Bilinmeyen bir hata oluştu, lütfen tekrar deneyiniz");//modalstateadderror mantığı model state içine gönderiyosun "" boş olduğu için en üstte verdiğin hata mesajı yazar
            return View(model);
        }





        
    }
}