using Newtonsoft.Json;

namespace FirstAPI
{
    internal sealed class ValuesHolder
    {
        [JsonProperty("Date")]
        public DateTime Date { get; set; }


        [JsonProperty("Temperature in Celsius")]
        public int Celsius { get; set; }


        [JsonProperty("Temperature in Fahrenheit")]
        public int Fahrenheit => 32 + (int)(Celsius / 0.5556);



        [JsonConstructor]
        public ValuesHolder(int tempC)
        {
            Celsius = tempC;
        }


        public ValuesHolder(int tempC, DateTime date)
        {
            Celsius = tempC;
            Date = date;
        }
    }
}

