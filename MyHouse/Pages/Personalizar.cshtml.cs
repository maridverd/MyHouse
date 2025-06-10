using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class PersonalizarModel : PageModel
{
    // Propriedade para receber o caminho da imagem
    [BindProperty(SupportsGet = true)]
    public string ImagemCasa { get; set; }

    public void OnGet()
    {
        // Você pode inicializar algo aqui se precisar
    }
}
