using ReflectionHW;
using ReflectionHW.Tools;
using System.Diagnostics;
using System.Text;
using Newtonsoft.Json;

Console.WriteLine("Домашнее задание: Reflection");
Console.WriteLine();

Console.WriteLine("Демонстрация custom сериализации/десериализации (в CSV) с использование reflection");
Console.WriteLine();

var serializedObject = SerializationCustomObject();

DeserializeCustomObject(serializedObject);

Console.WriteLine("Демонстрация сериализации/десериализации (в JSON) с использование NewtonSoft.Json");
Console.WriteLine();

var serializedObject2 = SerializeObjectUsingStandardTools();

DeserializeObjectUsingStandardTools(serializedObject2);

Console.WriteLine("Тест завершен");
Console.ReadLine();



static string SerializationCustomObject()
{
    string delimeter = ";";

    var stopwatch = new Stopwatch();

    var objectList = new List<F>();

    for (int i = 0; i < 100000; i++)
    {
        var f = new F();
        f.Get();

        objectList.Add(f);
    }

    stopwatch.Start();

    var stringBuilder = new StringBuilder();

    var serializedToString = CustomSerializer.SerializeObjectList(objectList, delimeter);// SerializeItem(f);

    stringBuilder.Append(serializedToString);

    stopwatch.Stop();

    var serializedObject = stringBuilder.ToString();

    //Console.WriteLine("Сериализованная строка:\r\n");
    //Console.WriteLine(serializedObject);

    Console.WriteLine($"Время, затраченное на сериализацию = {stopwatch.ElapsedMilliseconds}ms");

    return serializedObject;
}

static void DeserializeCustomObject(string serializedObject)
{
    string delimeter = ";";

    var stopwatch = new Stopwatch();

    stopwatch.Start();

    var deserializedListOfObjects = CustomSerializer.DeserializeObject<F>(serializedObject, delimeter); 

    stopwatch.Stop();

    Console.WriteLine("Список десериализованных объектов содержит:\r");
    Console.WriteLine($"{deserializedListOfObjects!.Count} экземпляров");

    Console.WriteLine($"Время, затраченное на десериализацию = {stopwatch.ElapsedMilliseconds}ms");
}

static string SerializeObjectUsingStandardTools()
{
    var stopwatch = new Stopwatch();

    var objectList = new List<F>();

    for (int i = 0; i < 100000; i++)
    {
        var f = new F();
        f.Get();

        objectList.Add(f);
    }

    stopwatch.Start();

    var serializedToString = JsonConvert.SerializeObject(objectList); // Использование NewtonSoft.Json

    stopwatch.Stop();

    Console.WriteLine("Сериализованная строка:\r\n");
    //Console.WriteLine(serializedToString);

    Console.WriteLine($"Время, затраченное на сериализацию = {stopwatch.ElapsedMilliseconds}ms");

    return serializedToString;
}

static void DeserializeObjectUsingStandardTools(string serializedObject)
{
    var stopwatch = new Stopwatch();

    stopwatch.Start();

    var deserializedListOfObjects = JsonConvert.DeserializeObject<List<F>>(serializedObject);

    stopwatch.Stop();

    Console.WriteLine("Список десериализованных объектов содержит:\r");
    Console.WriteLine($"{deserializedListOfObjects!.Count} экземпляров");

    Console.WriteLine($"Время, затраченное на десериализацию = {stopwatch.ElapsedMilliseconds}ms");
}