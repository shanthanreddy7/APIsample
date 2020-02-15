using Microsoft.AspNetCore.Mvc;
using MvcSample.Controllers.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace MvcSample.Controllers
{
    public class HomeController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("Home/get-to-do-list")]
        public List<ToDoList> GetTodoList()
        {
            string myJsonString = System.IO.File.ReadAllText(@"./Data/TodoList.json");
            return JsonConvert.DeserializeObject<List<ToDoList>>(myJsonString);
        }
        [HttpPost]
        [Route("Home/save-to-do-list")]
        public bool SaveTodoList(ToDoList toDoItem)
        {
            string myJsonString = System.IO.File.ReadAllText(@"./Data/TodoList.json");
            List<ToDoList> existingData = JsonConvert.DeserializeObject<List<ToDoList>>(myJsonString);
            if (toDoItem.Id > 0)
            {
                existingData.Where(x => x.Id == toDoItem.Id).Select(y => y.Status = toDoItem.Status).ToList();
                System.IO.File.WriteAllText(@"./Data/TodoList.json", JsonConvert.SerializeObject(existingData));
                return true;
            }
            toDoItem.Id = existingData.Count() + 1;
            existingData.Add(toDoItem);
            System.IO.File.WriteAllText(@"./Data/TodoList.json", JsonConvert.SerializeObject(existingData));
            return true;

        }
    }
}