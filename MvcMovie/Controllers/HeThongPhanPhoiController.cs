using Microsoft.AspNetCore.Mvc;
using MvcMovie.Models;
namespace MvcMovie.Controllers;


public class HeThongPhanPhoiController : Controller{
    //GET:/HeThongPhanPhoi/
    public IActionResult index(){
        return View();
    }
    [HttpPost]
    public IActionResult index(HeThongPhanPhoi ht){
        string st = "Xin chào " + ht.MaHTPP + "-" + ht.TenHTPP;
        ViewBag.info = st;
        return View();
    }
}