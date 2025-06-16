using System.Text.Json.Serialization;

namespace MyHouse;

public class Resposta {
    [JsonInclude]
    public readonly string Texto;
    [JsonInclude]
    public readonly DateTime Hora;
    [JsonInclude]
    public string SuporteEmail { get; private set; }
    public Usuario? Suporte => SuporteEmail != null ? Cadastro.Usuarios.Data[SuporteEmail] : null;
    public Resposta(string texto, DateTime hora, string suporteEmail) {
        Texto = texto;
        Hora = hora;
        SuporteEmail = suporteEmail;
    }
}