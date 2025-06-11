using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;

namespace MyHouse;

public readonly struct Senha {
    [JsonInclude]
    public readonly string HashSenha;
    [JsonInclude]
    public readonly string Sal;
    public Senha(string senha) {
        Sal = Convert.ToBase64String(RandomNumberGenerator.GetBytes(16));
        senha += Sal;
        HashSenha = Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(senha)));
    }
    public readonly bool Validate(string senha) {
        return Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(senha + Sal))) == HashSenha;
    }
    public static bool Valida(string senha) => senha.Length >= 6; // Exemplo de validação
}