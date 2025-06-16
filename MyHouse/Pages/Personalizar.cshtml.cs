using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

public class PersonalizarModel : PageModel
{
    public IActionResult OnPost()
    {
        string? id = Request.Query["id"].ToString().ToLower();

        if (string.IsNullOrEmpty(id))
            return RedirectToPage("/Index", new { mensagem = "ID da casa não informado" });

        // Cria o modelo a partir do JSON
        Modelo model = Modelo.Create(id);

        foreach (var key in Request.Form.Keys)
        {
            Console.WriteLine($"Aqui {key}");
            // Exemplo de chave: personalizacao[Automação e Segurança][Câmeras]
            if (key.StartsWith("personalizacao["))
            {
                string fullKey = key; // chave bruta
                string categoria = ExtrairEntre(fullKey, "personalizacao[", "]");
                string opcao = ExtrairEntre(fullKey, $"personalizacao[{categoria}][", "]");

                // if (!novasPersonalizacoes.ContainsKey(categoria))
                //     novasPersonalizacoes[categoria] = new Dictionary<string, bool>();
                Console.WriteLine($"[{categoria}][{opcao}]");
                model.Personalization[categoria][opcao] = true;
            }
        }

        model.Store();

        return RedirectToPage("/Index", new { mensagem = "Personalização salva com sucesso" });
    }

    // Função auxiliar para extrair strings entre colchetes
    private static string ExtrairEntre(string input, string start, string end)
    {
        int inicio = input.IndexOf(start) + start.Length;
        int fim = input.IndexOf(end, inicio);
        if (inicio >= 0 && fim >= inicio)
            return input[inicio..fim];
        return string.Empty;
    }
}
