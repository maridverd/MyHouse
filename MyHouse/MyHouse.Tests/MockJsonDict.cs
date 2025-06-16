using System.Collections.Generic;

namespace MyHouse.Tests.Mocks
{
    public class MockJsonDict<TKey, TValue> where TKey : notnull
    {
        public Dictionary<TKey, TValue> Data { get; set; } = new Dictionary<TKey, TValue>();

        public void Save()
        {
            // Não faz nada, só para simular o método real
        }
    }
}
