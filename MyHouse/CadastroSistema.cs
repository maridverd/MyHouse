using System.Text.Json;

namespace MyHouse;

public static class CadastroSistema {
    private static readonly string ArquivoUsuarios = "usuarios.json";
    public static Dictionary<string, Usuario> Usuarios { get; private set; }

    static CadastroSistema() {
        CarregarDados();
        Usuarios ??= new();
    }

    // Carrega os dados existentes do arquivo JSON
    private static void CarregarDados() {
        if (!File.Exists(ArquivoUsuarios)) {
            Log.Instance.WriteLine("Sem arquivo");
            File.Create(ArquivoUsuarios);
            return;
        }
        else {
            try {
                Usuarios = JsonSerializer.Deserialize<Dictionary<string, Usuario>>(File.ReadAllText(ArquivoUsuarios))!;
            }
            catch (JsonException jsonException) {
                Log.Instance.WriteLine($"Json Exception: {jsonException}. Creating empty dictionary instead");
            }
            catch (Exception exception) {
                Log.Instance.WriteLine($"Exception: {exception}. Creating empty dictionary instead");
            }
        }
    }
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
