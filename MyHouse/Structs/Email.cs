using System.Text.Json.Serialization;

namespace MyHouse;

public readonly struct Email {
    [JsonInclude]
    private readonly string _Email;
    public Email(string email) {
        _Email = email;
    }
    [JsonIgnore]
    public bool Valido {
        get {
            return _Email.Contains('@'); // Colocar lógica de validação do email aqui.
        }
    }
    public static implicit operator Email(string email) => new(email);
    public static implicit operator string(Email email) => email._Email;
}