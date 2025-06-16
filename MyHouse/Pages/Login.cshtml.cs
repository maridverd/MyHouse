using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using MyHouse.Services;

namespace MyHouse.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ILoginService _loginService;

        public LoginModel(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [BindProperty] public string? Email { get; set; }
        [BindProperty] public string? Senha { get; set; }

        public List<CodigoRetorno> Mensagens { get; set; } = new();

        public void OnGet() {}

        public IActionResult OnPost()
        {
            Mensagens.Clear();

            if (_loginService.AutenticaUsuario(Email, Senha, HttpContext)) // ✅ aqui
            {
                HttpContext.Session.SetString("UsuarioEmail", Email!);
                return RedirectToPage("/PainelUsuario");
            }

            Log.Instance.WriteLine("Erro no Login");
            Mensagens.Add(new CodigoRetorno(1, "Email e/ou senha incorretos"));

            return Page();
        }
    }
}