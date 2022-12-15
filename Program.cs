using System.Diagnostics;
using System.Runtime.Serialization;

Console.WriteLine("Reflection homework");


var stopwatch = new Stopwatch();

stopwatch.Start();



stopwatch.Stop();


[Serializable]
class F : ISerializable
{ 
    int i1, i2, i3, i4, i5;

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        throw new NotImplementedException();
    }

    void Get() => new F() { i1 = 1, i2 = 2, i3 = 3, i4 = 4, i5 = 5 }; 
}
