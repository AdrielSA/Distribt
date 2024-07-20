using Distribt.Shared.Serialization.Interfaces;
using Newtonsoft.Json;
using System.Text;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Distribt.Shared.Serialization.Implementations
{
    public class Serializer(JsonSerializerSettings serializerSettings) : ISerializer
    {
        private static readonly Encoding _encoding = new UTF8Encoding(false);
        private static readonly JsonSerializerSettings _defaultSerializerSettings = new()
        {
            NullValueHandling = NullValueHandling.Ignore,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            TypeNameHandling = TypeNameHandling.Auto
        };

        private const int _bufferSize = 1024;
        private readonly Newtonsoft.Json.JsonSerializer _serializer = Newtonsoft.Json.JsonSerializer.Create(serializerSettings);

        public Serializer() : this(_defaultSerializerSettings)
        {

        }

        public T DeserializeObject<T>(string input)
            => JsonSerializer.Deserialize<T>(input) ?? throw new InvalidOperationException();

        public T DeserializeObject<T>(byte[] input) where T : class
            => (DeserializeByteArrayToObject<T>(input) as T)!;

        public string SerializeObject<T>(T obj)
            => JsonSerializer.Serialize(obj);

        public byte[] SerializeObjectToByteArray<T>(T obj)
        {
            using var memoryStream = new MemoryStream(_bufferSize);
            using (var streamWriter = new StreamWriter(memoryStream, _encoding, _bufferSize, true))
            using (var jsonWriter = new JsonTextWriter(streamWriter))
            {
                jsonWriter.Formatting = _serializer.Formatting;
                _serializer.Serialize(jsonWriter, obj, obj!.GetType());
            }

            return memoryStream.ToArray();
        }

        public object? DeserializeObject(byte[] input, Type type)
        {
            using var memoryStream = new MemoryStream(input, false);
            using var streamReader = new StreamReader(memoryStream, _encoding, false, _bufferSize, true);
            using var reader = new JsonTextReader(streamReader);
            return _serializer.Deserialize(reader, type) ?? throw new InvalidOperationException();
        }


        #region private methods

        private object DeserializeByteArrayToObject<T>(byte[] input)
        {
            using var memoryStream = new MemoryStream(input, false);
            using var streamReader = new StreamReader(memoryStream, _encoding, false, _bufferSize, true);
            using var reader = new JsonTextReader(streamReader);
            return _serializer.Deserialize(reader, typeof(T)) ?? throw new InvalidOperationException();
        }

        #endregion
    }
}
