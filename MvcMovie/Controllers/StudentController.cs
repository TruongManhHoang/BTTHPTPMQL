using Microsoft.AspNetCore.Mvc;
using MvcMovie.Models;

namespace ProjectMVC.Controllers;

public class StudentController : Controller{
    //GET:/Person/
    public IActionResult index(){
        return View();
    }
    [HttpPost]
    public IActionResult index(Student sv){
        string stOutput = "Xin chào " + sv.FullName + "-" + sv.Address + "-" + sv.Age + "-" + sv.PhoneNumber;
        ViewBag.info = stOutput;
        return View();
    }
}