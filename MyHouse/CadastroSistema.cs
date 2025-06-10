using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MyHouse;

public struct Senha {
    [JsonInclude]
    public string HashSenha;
    [JsonInclude]
    public string Sal;
    public Senha(string senha) {
        Sal = Convert.ToBase64String(RandomNumberGenerator.GetBytes(16));
        senha += Sal;
        HashSenha = Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(senha)));
    }
    public readonly bool Validate(string senha) {
        return Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(senha + Sal))) == HashSenha;
    }
}

public static class CadastroSistema {
    private static readonly string ArquivoUsuarios = "usuarios.json";
    public static Dictionary<string, Usuario> Usuarios;

    static CadastroSistema() {
        CarregarDados();
        if (Usuarios == null) Usuarios = new();
    }

    // Carrega os dados existentes do arquivo JSON
    private static void CarregarDados() {
        if (!File.Exists(ArquivoUsuarios)) {
            Log.Instance.WriteLine("Sem arquivo");
            File.Create(ArquivoUsuarios);
            Usuarios = new();
            return;
        }
        else {
            try {
                Usuarios = JsonSerializer.Deserialize<Dictionary<string, Usuario>>(File.ReadAllText(ArquivoUsuarios))!;
            }
            catch (JsonException jsonException) {
                Log.Instance.WriteLine($"Json Exception: {jsonException}. Creating empty dictionary instead");
                Usuarios = new();
            }
            catch (Exception exception) {
                Log.Instance.WriteLine($"Exception: {exception}. Creating empty dictionary instead");
                Usuarios = new();
            }
        }
    }
    private const int caracteresMinimosSenha = 12;
    // private static CodigoRetorno AvaliarSenha(string senha) {
    //     if (senha.Length < caracteresMinimosSenha) {
    //         return new CodigoRetorno(1, $"Senha deve ter comprimento maior ou igual a {caracteresMinimosSenha} caracteres.");
    //     }
    //     if (senha.)
    // }

    public static List<CodigoRetorno> CadastrarUsuario(string email, string senha, Nome nome, string cpf, string endereco) {
        List<CodigoRetorno> codigos = new();
        if (email != null && senha != null && cpf != null && endereco != null) {
            if (!Usuario.ValidarEmail(email)) {
                codigos.Add(new(1, "Email inválido."));
            }
            if (!Usuario.ValidarCPF(cpf)) {
                codigos.Add(new(2, "CPF inválido."));
            }
            if (codigos.Count == 0) {
                if (Usuarios.ContainsKey(email)) {
                    codigos.Add(new(3, "Usuário já cadastrado."));
                }
                else {
                    Usuarios.Add(email, new Usuario(email, new Senha(senha), nome, cpf, endereco));
                    codigos.Add(new(0, "Usuário cadastrado com sucesso."));
                }
            }
        }
        else {
            codigos.Add(new(4, "Informações nulas."));
        }
        return codigos;
    }

    public static bool AutenticaUsuario(string email, string senha) {
        Usuario usuario;
        if (!Usuarios.TryGetValue(email, out usuario!)) {
            return false;
        }
        return usuario.Senha.Validate(senha);
    }

    // Salva os dados atualizados no arquivo JSON
    public static void SalvarDados() {
        File.WriteAllText(ArquivoUsuarios, JsonSerializer.Serialize(Usuarios));
    }
}
