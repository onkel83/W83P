using System.Runtime.Serialization.Formatters.Binary;
#pragma warning disable SYSLIB0011, CS8604 // Type or member is obsolete
namespace W83P.Basic
{
    public class BinarySerializer<T>
    {
        public static byte[] Serialize(T obj)
    {
        using (var stream = new MemoryStream())
        {
            var formatter = new BinaryFormatter();

                formatter.Serialize(stream, obj);

                stream.Position = 0;
            var binaryData = new byte[stream.Length];
            stream.Read(binaryData, 0, binaryData.Length);
            return binaryData;
        }
    }

    public static T Deserialize(byte[] binaryData)
    {
        using (var stream = new MemoryStream(binaryData))
        {
            var formatter = new BinaryFormatter();
            stream.Position = 0;
                return (T)formatter.Deserialize(stream);
        }
    }
    }
}
#pragma warning restore SYSLIB0011, CS8604 // Type or member is obsolete