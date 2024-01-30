using Microsoft.AspNetCore.Mvc;
using ProjectMVC.Models;

namespace ProjectMVC.Controllers;

public class PersonController : Controller{
    //GET:/Person/
    public IActionResult index(){
        return View();
    }
    [HttpPost]
    public IActionResult index(Person ps){
        string stOutput = "Xin chào " + ps.PersonId + "-" + ps.FullName + "-" + ps.Address;
        ViewBag.info = stOutput;
        return View();
    }
}