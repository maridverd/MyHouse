using Newtonsoft.Json;

namespace MyHouse;

public class Pergunta {
    private static int cont = 0;
    [JsonProperty]
    public readonly string Texto;
    [JsonProperty]
    public readonly DateTime Hora;
    [JsonProperty]
    public readonly long Id;
    [JsonProperty]
    public string? UsuarioEmail => Usuario?.Email;
    public Usuario Usuario { get; private set; }
    [JsonProperty]
    public Resposta? Resposta { get; private set; } = null;
    public Pergunta(string texto, DateTime hora, Usuario usuario) {
        Id = cont++;
        Texto = texto;
        Hora = hora;
        Usuario = usuario;
    }
    [JsonConstructor]
    public Pergunta(string texto, DateTime hora, string usuarioEmail, Resposta? resposta = null) {
        Id = cont++;
        Texto = texto;
        Hora = hora;
        Usuario = Cadastro.Usuarios.Data[usuarioEmail];
        Resposta = resposta;
    }

    public bool PreencherResposta(Resposta resposta) {
        Resposta = resposta;
        return true;
    }
}