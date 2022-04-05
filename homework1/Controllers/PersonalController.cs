using Microsoft.AspNetCore.Mvc;


namespace FirstAPI.Controllers
{
    [ApiController]
    [Route("console/")]
    public class PersonalController : Controller
    {
        private MethodsForValuesHolder _methods = new();      //решил в отдельный класс вынести методы, потому что так удобней
        private StreamReader sr;



        [HttpGet("savetemp")]
        public string Save([FromQuery] int input)        //сохранение температуры
        {
            _methods.SaveToFile(input);
            return "Saved!";
        }


        [HttpGet("display")]
        public string Display()         //показывает записанные данные
        {
            sr = new("log.json");
            return $"{sr.ReadLine()}";
        }


        [HttpGet("edit")]
        public string Edit([FromQuery] int input)         //редактируем не изменяя времени
        {
            _methods.Edit(input);
            return "Edited!";
        }


        [HttpGet("delete")]
        public string Delete([FromQuery] int input)         //удаляем
        {
            _methods.DeleteFile();
            return "Deleted!";
        }
    }
}
