using Newtonsoft.Json;   //по другому перезапись json файла(метод Edit) никак не смог придумать

namespace FirstAPI
{
	internal sealed class MethodsForValuesHolder
	{
        private ValuesHolder _value;
        private string fileName = "log.json";



        public void SaveToFile(int tempC)           // метод записи в .json файл(он будет в папке проекта)
        {
            _value = new(tempC, DateTime.Now);
            string jsonString = JsonConvert.SerializeObject(_value);
            File.WriteAllText(fileName, jsonString);
        }


        public void Edit(int tempC)           // редактируем файл .json без изменения даты
        {
            ValuesHolder _values = JsonConvert.DeserializeObject<ValuesHolder>(File.ReadAllText(fileName));
            _values.Celsius = tempC;
            string jsonString = JsonConvert.SerializeObject(_values);
            File.WriteAllText(fileName, jsonString);
        }


        public void DeleteFile()           // удаление
        {
            File.Delete(fileName);
        }
    }
}

