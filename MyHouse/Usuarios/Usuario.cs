using Newtonsoft.Json;

namespace MyHouse;

public class Usuario {
    [JsonProperty]
    public string Email { get; private set; }
    [JsonProperty]
    public Senha Senha { get; private set; }
    [JsonProperty]
    public Nome Nome { get; private set; }
    [JsonProperty]
    public string CPF { get; private set; }
    [JsonProperty]
    public string Endereco { get; private set; }
    [JsonProperty]
    public bool IsSuporte { get; private set; }
    [JsonConstructor]
    public Usuario(string email, Senha senha, Nome nome, string cpf, string endereco, bool isSuporte = false) {
        Email = email;
        Senha = senha;
        Nome = nome;
        CPF = cpf;
        Endereco = endereco;
        IsSuporte = isSuporte;
    }
    public static bool ValidarEmail(string email) {
        return email.Contains('@'); // Colocar lógica de validação do email aqui.
    }
    public static bool ValidarCPF(string cpf) {
        if (cpf.Length != 11) return false;
        int total1 = 0;
        int total2 = 0;
        for (int i = 0; i < 9; i++) {
            int c = cpf[i] - '0';
            if (c < 0 || c > 9) return false;
            total1 += c * (i + 1);
            total2 += c * i;
        }
        total1 = total1 % 11 % 10;
        if (total1 != cpf[9] - '0') return false;
        total2 = (total2 + total1 * 9) % 11 % 10;

        return total2 == cpf[10] - '0';
    }
}