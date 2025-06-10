using System.Text.Json.Serialization;

namespace MyHouse;

public class Pergunta {
    [JsonInclude, JsonPropertyName("pergunta")]
    private readonly string _Pergunta;
    [JsonInclude]
    public readonly DateTime Hora;
    public Usuario Usuario { get; private set; }
    public Resposta? Resposta { get; private set; }
    public Pergunta(string pergunta, DateTime hora, Usuario usuario) {
        _Pergunta = pergunta;
        Hora = hora;
        Usuario = usuario;
    }

    public bool PreencherResposta(Resposta resposta) {
        Resposta = resposta;
        return true;
    }
}