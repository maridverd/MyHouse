using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyHouse.Data;
using MyHouse.Services;
using System;

namespace MyHouse.Pages
{
    public class ResponderPerguntasModel : PageModel
    {
        private readonly IRespostaService _respostaService;
        private readonly JsonDict<long, Pergunta> _perguntas;

        [BindProperty]
        public long PerguntaId { get; set; }

        [BindProperty]
        public string? NovaResposta { get; set; }

        public JsonDict<long, Pergunta>? Perguntas { get; private set; }

        public ResponderPerguntasModel(IRespostaService respostaService, JsonDict<long, Pergunta> perguntas)
        {
            _respostaService = respostaService;
            _perguntas = perguntas;
        }

        public void OnGet()
        {
            Perguntas = _perguntas;
        }

        public IActionResult OnPostResposta()
        {
            string? email = HttpContext.Session.GetString("UsuarioEmail");
            if (email == null) return RedirectToPage("/Login");

            bool resultado = _respostaService.ResponderPergunta(PerguntaId, NovaResposta!, email);

            if (!resultado)
            {
                // (Opcional) Exibir uma mensagem de erro
            }

            return RedirectToPage();
        }
    }
}
