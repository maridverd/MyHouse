using System.Text.Json.Serialization;

namespace MyHouse;

public class Pergunta {
    private static int cont = 0;
    public readonly string Texto;
    [JsonInclude]
    public readonly DateTime Hora;
    public readonly long Id;
    public Usuario Usuario { get; private set; }
    public Resposta? Resposta { get; private set; }
    public Pergunta(string texto, DateTime hora, Usuario usuario) {
        Id = cont++;
        Texto = texto;
        Hora = hora;
        Usuario = usuario;
    }

    public bool PreencherResposta(Resposta resposta) {
        Resposta = resposta;
        return true;
    }
}