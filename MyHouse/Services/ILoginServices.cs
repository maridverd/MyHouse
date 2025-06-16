namespace MyHouse.Services
{
    public interface ILoginService
    {
        bool AutenticaUsuario(string? email, string? senha, HttpContext context);
    }
}