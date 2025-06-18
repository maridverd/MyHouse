using Newtonsoft.Json;

namespace MyHouse;

public class Resposta {
    [JsonProperty]
    public readonly string Texto;
    [JsonProperty]
    public readonly DateTime Hora;
    [JsonProperty]
    public string SuporteEmail { get; private set; }
    public Usuario? Suporte => SuporteEmail != null ? Cadastro.Usuarios.Data[SuporteEmail] : null;
    public Resposta(string texto, DateTime hora, string suporteEmail) {
        Texto = texto;
        Hora = hora;
        SuporteEmail = suporteEmail;
    }
}