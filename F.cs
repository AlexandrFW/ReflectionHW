using Newtonsoft.Json;

namespace ReflectionHW;

[Serializable]
internal class F
{
    [JsonProperty]
    int i1;

    [JsonProperty]
    int i2;

    [JsonProperty]
    int i3;

    [JsonProperty]
    int i4;

    [JsonProperty]
    int i5;

    public F()
    {
    }

    public void Get()
    {
        i1 = 1; i2 = 2; i3 = 3; i4 = 4; i5 = 5;
    }
}