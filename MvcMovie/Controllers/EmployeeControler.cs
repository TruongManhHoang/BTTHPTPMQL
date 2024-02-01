using Microsoft.AspNetCore.Mvc;
using MvcMovie.Models;

namespace MvcMovie.Controllers;

public class EmployeeController : Controller{
    //GET:/Employee/
    public IActionResult index(){
        return View();
    }
    [HttpPost]
    public IActionResult index(Employee ep){
        string stOutput = "Xin chào " + ep.EmployeeId + "-" + ep.FullName + "-" + ep.Age + "-" + ep.Salary;
        ViewBag.info = stOutput;
        return View();
    }
}