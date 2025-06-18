using Newtonsoft.Json;

namespace MyHouse;

public readonly struct Nome {
    [JsonProperty]
    public readonly string PrimeiroNome;
    [JsonProperty]
    public readonly string UltimoNome;
    [JsonIgnore]
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