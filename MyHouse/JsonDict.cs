using System.Text.Json;

namespace MyHouse;

public class JsonDict<TKey, TValue> {
    public readonly string CaminhoArquivo;
    public Dictionary<TKey, TValue> Data;
    public JsonDict(string caminhoArquivo) {
        CaminhoArquivo = caminhoArquivo;
        CarregarDados();
    }
    public void Save() {
        File.WriteAllText(CaminhoArquivo, JsonSerializer.Serialize(Data));
    }
    // Carrega os dados existentes do arquivo JSON
    private void CarregarDados() {
        if (!File.Exists(CaminhoArquivo)) {
            Log.Instance.WriteLine($"Sem arquivo {CaminhoArquivo}");
            File.WriteAllText(CaminhoArquivo, "{}");
            Data = new();
            return;
        }
        else {
            string FileContent = File.ReadAllText(CaminhoArquivo);
            try {
                Data = JsonSerializer.Deserialize<Dictionary<TKey, TValue>>(FileContent) ?? new();
            }
            catch (JsonException jsonException) {
                Log.Instance.WriteLine($"Json Exception: {jsonException}. Backing up and creating empty dictionary instead");
                File.WriteAllText(CaminhoArquivo + "backup", FileContent);
            }
            catch (Exception exception) {
                Log.Instance.WriteLine($"Exception: {exception}. Backing up and creating empty dictionary instead");
                File.WriteAllText(CaminhoArquivo + "backup", FileContent);
            }
        }
    }
}