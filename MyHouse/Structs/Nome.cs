using System.Text.Json.Serialization;

namespace MyHouse;

public readonly struct Nome {
    [JsonInclude]
    public readonly string PrimeiroNome;
    [JsonInclude]
    public readonly string UltimoNome;
    public string Completo => PrimeiroNome + ' ' + UltimoNome;
    [JsonConstructor]
    public Nome(string primeiroNome, string ultimoNome) {
        PrimeiroNome = primeiroNome;
        UltimoNome = ultimoNome;
    }
    public override string ToString() {
        return Completo;
    }
}