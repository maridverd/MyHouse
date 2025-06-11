using System.Text.Json.Serialization;

namespace MyHouse;

public class Pergunta {
    private static int cont = 0;
    [JsonInclude]
    public readonly string Texto;
    [JsonInclude]
    public readonly DateTime Hora;
    [JsonInclude]
    public readonly long Id;
    [JsonInclude]
    public string? UsuarioEmail => Usuario?.Email;
    public Usuario Usuario { get; private set; }
    [JsonInclude]
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
        Usuario = CadastroSistema.Usuarios.Data[usuarioEmail];
        Resposta = resposta;
    }

    public bool PreencherResposta(Resposta resposta) {
        Resposta = resposta;
        return true;
    }
}