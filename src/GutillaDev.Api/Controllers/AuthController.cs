using GutillaDev.Api.ViewModels;
using GutillDev.Business.Intefaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GutillaDev.Api.Controllers
{
    [Route("api")]
    public class AuthController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInmanager; //autenticacao do usuario
        private readonly UserManager<IdentityUser> _userManager; //cria do ususario

        public AuthController(INotificador notificador,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager) : base(notificador)
        {
            _signInmanager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("nova-conta")]
        public async Task<ActionResult> RegiterUser(RegisterUserViewModel registerUser)
        {
            if(!ModelState.IsValid) return CustomResponse(ModelState);

            var user = new IdentityUser //criando um identityUser
            {
                UserName = registerUser.Email,
                Email = registerUser.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, registerUser.Password); //criando um usuario
            if(result.Succeeded) //se deu bom
            {
                await _signInmanager.SignInAsync(user, false); //faz o login
                return CustomResponse(registerUser);
            }

            foreach(var erro in result.Errors)
            {
                NotificarErros(erro.Description);
            }

            return CustomResponse(registerUser);
        }

        [HttpPost("entrar")]
        public async Task<ActionResult> Login(LoginUserViewModel loginUser)
        {
            if(!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _signInmanager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true); //login com usuario e senha

            if(result.Succeeded)
            {
                return CustomResponse(loginUser);
            }

            if(result.IsLockedOut)
            {
                NotificarErros("Usuário temporariamente bloqueado por tentativas inválidas!");
                return CustomResponse(loginUser);
            }

            NotificarErros("Usuário ou Senha incorretos"); //minimo de informação 
            return CustomResponse(loginUser);
        }
    }
}
