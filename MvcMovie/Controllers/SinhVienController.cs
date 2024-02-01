using Microsoft.AspNetCore.Mvc;
using MvcMovie.Models;

namespace MvcMovie.Controllers;

public class SinhVienController : Controller{
    //GET:/Person/
    public IActionResult index(){
        return View();
    }
    [HttpPost]
    public IActionResult index(SinhVien sv){
        string stOutput = "Xin chào " + sv.FullName + "-" + sv.Address + "-" + sv.Age + "-" + sv.PhoneNumber;
        ViewBag.info = stOutput;
        return View();
    }
}