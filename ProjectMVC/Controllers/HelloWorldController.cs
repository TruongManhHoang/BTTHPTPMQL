using Microsoft.AspNetCore.Mvc;

namespace ProjectMVC.Controllers;

    public class HelloWorldController : Controller{
        //GET:./HelloWorld/
        public IActionResult Index(){
            return View();
        }
        //GET:./HelloWorld/Welcome
        public string Welcome(){
            return "This is the Welcome action method";
        }
    }
