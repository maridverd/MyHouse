using MyHouse;

public class JsonDictMock<TKey, TValue> : IJsonDictServices<TKey, TValue> where TKey : notnull
{
    public Dictionary<TKey, TValue> Data { get; set; } = new Dictionary<TKey, TValue>();
    public void CarregarDados() { }
    public void Save() { /* não faz nada no teste */ }
}