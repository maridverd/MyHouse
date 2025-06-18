using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace MyHouse;

public readonly struct Senha {
    [JsonProperty]
    public readonly string HashSenha;
    [JsonProperty]
    public readonly string Sal;
    public Senha(string senha) {
        Sal = Convert.ToBase64String(RandomNumberGenerator.GetBytes(16));
        senha += Sal;
        HashSenha = Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(senha)));
    }
    [JsonConstructor]
    public Senha(string hashSenha, string sal) {
        HashSenha = hashSenha;
        Sal = sal;
    }
    public readonly bool Validate(string senha) {
        return Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(senha + Sal))) == HashSenha;
    }
    public static bool Valida(string senha) => senha.Length >= 6; // Exemplo de validação
}