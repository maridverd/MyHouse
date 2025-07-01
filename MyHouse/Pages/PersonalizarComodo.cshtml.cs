using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyHouse.Pages
{
    public class PersonalizarComodoModel : PageModel {
        [BindProperty(SupportsGet = true)]
        public string? ComodoId { get; set; }
        public Modelo? Dados { get; set; }
        [BindProperty]
        public string? Mensagem { get; set; }

        public void OnGet() {
            ComodoId = Request.Query["id"].ToString().ToLower();

            if (!string.IsNullOrEmpty(ComodoId)) {
                Dados = Modelo.Create(ComodoId);
            }
        }

        public void OnPost() {
            string? id = Request.Query["id"].ToString().ToLower();

            if (string.IsNullOrEmpty(id)) {
                Mensagem = "ID da casa não informado";
                return;
            }

            Modelo model = Modelo.Create(id);

            if (model.Personalization != null) {
                foreach (var categoria in model.Personalization[ComodoId!].Keys) {
                    var opcoes = model.Personalization[ComodoId!][categoria];
                    var keys = new List<string>(opcoes.Keys);
                    foreach (var opcao in keys) {
                        opcoes[opcao] = false;
                    }
                }
            }

            foreach (var key in Request.Form.Keys) {
                if (key.StartsWith("personalizacao[")) {
                    string categoria = ExtrairEntre(key, "personalizacao[", "]");
                    string opcao = ExtrairEntre(key, $"personalizacao[{categoria}][", "]");
                    if (model.Personalization != null) {
                        if (model.Personalization.ContainsKey(categoria) &&
                            model.Personalization[categoria][ComodoId!].ContainsKey(opcao)) {
                            model.Personalization[categoria][ComodoId!][opcao] = true;
                        }
                    }
                }
            }

            model.Store();

            // Continua na mesma página, setando a mensagem para mostrar
            Mensagem = "Personalização salva com sucesso";

            // Atualiza os dados para refletir as mudanças no form
            Dados = model;
        }
        
        private static string ExtrairEntre(string input, string start, string end)
        {
            int inicio = input.IndexOf(start) + start.Length;
            int fim = input.IndexOf(end, inicio);
            if (inicio >= 0 && fim >= inicio)
                return input[inicio..fim];
            return string.Empty;
        }
    }
}
