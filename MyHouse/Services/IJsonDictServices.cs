namespace MyHouse;

public interface IJsonDictServices<TKey, TValue> where TKey : notnull {
    public Dictionary<TKey, TValue> Data { get; set; }
    public void Save();
    public void CarregarDados();
}