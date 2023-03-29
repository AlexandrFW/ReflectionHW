using System.Reflection;
using System.Text;

namespace ReflectionHW.Tools;

public static class CustomSerializer
{
    public static string SerializeObject<T>(T? objectToSerialize, string delimeter)
    {
        if (objectToSerialize == null)
            throw new ArgumentNullException("Передаваемы объект не должен быть NULL");

        var name = objectToSerialize.GetType().Name;
        Type? type = objectToSerialize.GetType();
        var fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);

        var resultBuilder = new StringBuilder();
        resultBuilder.Append("name");
        resultBuilder.Append(delimeter);

        foreach (var field in fields)
        {
            resultBuilder.Append(field.Name);
            resultBuilder.Append(delimeter);
        }

        resultBuilder.Append("\r\n");

        resultBuilder.Append(name);
        resultBuilder.Append(delimeter);

        foreach (var field in fields)
        {
            resultBuilder.Append($"{field.GetValue(objectToSerialize)!.ToString()}");
            resultBuilder.Append(delimeter);
        }

        resultBuilder.Append("\r\n");

        return resultBuilder.ToString();
    }

    public static string SerializeObjectList<T>(List<T>? objectToSerializeList, string delimeter)
    {
        if (objectToSerializeList == null)
            throw new ArgumentNullException("Передаваемы объект не должен быть NULL");

        var name = objectToSerializeList[0]!.GetType().Name;
        Type? type = objectToSerializeList[0]!.GetType();
        var fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);

        var resultBuilder = new StringBuilder();
        resultBuilder.Append("name");
        resultBuilder.Append(delimeter);

        foreach (var field in fields)
        {
            resultBuilder.Append(field.Name);
            resultBuilder.Append(delimeter);
        }

        resultBuilder.Append("\r\n");

        foreach (var item in objectToSerializeList)
        {
            resultBuilder.Append(item!.GetType().Name);
            resultBuilder.Append(delimeter);

            foreach (var field in fields)
            {
                resultBuilder.Append($"{field.GetValue(item)!.ToString()}");
                resultBuilder.Append(delimeter);
            }
            resultBuilder.Append("\r\n");
        }

        resultBuilder.Append("\r\n");

        return resultBuilder.ToString();
    }

    public static List<T> DeserializeObject<T>(string serializedObjectByCsv, string delimeter)
    {
        if (string.IsNullOrEmpty(serializedObjectByCsv))
            throw new ArgumentNullException("Проверьте сериализованную строку. Строка не должна быть пустой");

        //if (serializedObjectByCsv.Contains("name") == false)
        //    throw new FormatException("Не корректный формат CSV файла");

        var type = typeof(T);

        var objectName = type.Name;

        var sliptSerializedObjectArray = serializedObjectByCsv.Split("\r\n");

        var splitFildsNamesArray = sliptSerializedObjectArray[0].Split(";");

        var listF = new List<T>();

        if (sliptSerializedObjectArray.Length > 1)
        {
            for (int i = 1; i < sliptSerializedObjectArray.Length - 1; i++)
            {
                var splitValuesArray = sliptSerializedObjectArray[i].Split(";");

                if (objectName == splitValuesArray[0])
                {
                    var inctance = (T)Activator.CreateInstance(typeof(T))!;

                    for(int j = 1;  j < splitValuesArray.Length - 1; j++)
                    {
                        var name = type.GetField(splitFildsNamesArray[j], BindingFlags.Instance | BindingFlags.NonPublic);
                        name?.SetValue(inctance, int.Parse(splitValuesArray[j]));
                    }

                    listF.Add(inctance);
                }
            }
        }

        return listF;
    }
}