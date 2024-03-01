using Microsoft.AspNetCore.Mvc;
using MvcMovie.Models;

namespace MvcMovie.Controllers;

public class EmployeeController : Controller{
    private static List<Employee> employees = new List<Employee>();
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
    
    public IActionResult Display(){
        return View(employees);
    }
    [HttpPost]
    public IActionResult AddEmployee(Employee employee)
    {
        employees.Add(employee);
        // return RedirectToAction("Display");
        return View();
    }
}