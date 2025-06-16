using Microsoft.AspNetCore.Http;
using MyHouse;

namespace MyHouse.Services
{
    public class LoginService : ILoginService
    {
        public bool AutenticaUsuario(string? email, string? senha, HttpContext context)
        {
            return Cadastro.AutenticaUsuario(email, senha, context); 
        }
    }
}