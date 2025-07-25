using Newtonsoft.Json;

namespace MyHouse;

public class JsonDict<TKey, TValue> : IJsonDictServices<TKey, TValue> where TKey : notnull{
    public readonly string CaminhoArquivo;
    public Dictionary<TKey, TValue> Data{ get; set; }
    public JsonDict(string caminhoArquivo) {
        CaminhoArquivo = caminhoArquivo;
        CarregarDados();
        Data ??= new();
    }
    public void Save() {
        File.WriteAllText(CaminhoArquivo, JsonConvert.SerializeObject(Data));
    }
    // Carrega os dados existentes do arquivo JSON
    public void CarregarDados() {
        if (!File.Exists(CaminhoArquivo)) {
            Log.Instance.WriteLine($"Sem arquivo {CaminhoArquivo}");
            File.WriteAllText(CaminhoArquivo, "{}");
            Data = new();
            return;
        }
        else {
            string FileContent = File.ReadAllText(CaminhoArquivo);
            try {
                Data = JsonConvert.DeserializeObject<Dictionary<TKey, TValue>>(FileContent) ?? new();
            }
            catch (JsonException jsonException) {
                Log.Instance.WriteLine($"Json Exception: {jsonException}.\nFile: {CaminhoArquivo}\nBacking up and creating empty dictionary instead");
                File.WriteAllText(CaminhoArquivo + "backup", FileContent);
                Data = new();
            }
            catch (Exception exception) {
                Log.Instance.WriteLine($"Exception: {exception}.\nFile: {CaminhoArquivo}\nBacking up and creating empty dictionary instead");
                File.WriteAllText(CaminhoArquivo + "backup", FileContent);
                Data = new();
            }
        }
    }
}