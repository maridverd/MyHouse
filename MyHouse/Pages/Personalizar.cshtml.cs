using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

public class PersonalizarModel : PageModel
{
    [BindProperty]
    public string ImagemCasa { get; set; }

    [BindProperty]
    public List<string> Personalizacao { get; set; } = new List<string>();

    public void OnGet()
    {
        // Pode deixar vazio ou carregar dados caso queira
    }

    public IActionResult OnPost()
    {
        // Aqui você pode salvar os dados da personalização se quiser
        // Exemplo: salvar no banco, sessão, etc.

        // Após salvar, redireciona para a Index com mensagem
        return RedirectToPage("/Index", new { mensagem = "Personalização salva" });
    }
}
