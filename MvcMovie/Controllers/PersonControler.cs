using System.Numerics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;
using MvcMovie.Models.Process;
using OfficeOpenXml;


namespace MvcMovie.Controllers;

public class PersonController : Controller{
    private readonly ApplicationDbContext _context;
    private ExcelProcess _excelProcess = new ExcelProcess();
    public PersonController(ApplicationDbContext context){
        _context = context;
    }
    public async Task<IActionResult> index(){
        var model = await _context.Person.ToListAsync();
        return View(model);
    }
    [HttpPost]
    public async Task<IActionResult> index(String name){
        return View( await _context.Person.Where(e => e.FullName.Contains(name)).ToListAsync());
    }
    public IActionResult create(){
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> create([Bind("PersonId, FullName, Address")] Person person){
        if(ModelState.IsValid){
            _context.Add(person);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(index));
        }
        return View(person);
    }
    public async Task<IActionResult> edit(string id){
        if(id == null || _context.Person == null){
            return NotFound();
        }
        var person = await _context.Person.FindAsync(id);
        if(person == null){
            return NotFound();
        }
        return View(person);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> edit(string id, [Bind("PersonId, FullName, Address")] Person person){
        if(id != person.PersonId){
            return NotFound();
        }
        if(ModelState.IsValid){
            try{
                _context.Update(person);
                await _context.SaveChangesAsync();
            }catch(DbUpdateConcurrencyException){
                if(!PersonExists(person.PersonId)){
                    return NotFound();
                }else{
                    throw;
                }
             }
            return RedirectToAction(nameof(index));
        }
        return View(person);
    }
    public async Task<IActionResult> delete(string id){
        if(id == null || _context.Person == null){
            return NotFound();
        }
        var person = await _context.Person.FirstOrDefaultAsync(m => m.PersonId == id);
        if(person == null){
            return NotFound();
        }
        return View(person);
    }
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> deleteConfirmed(string id){
        if(_context.Person == null){
            return Problem("Entity set 'ApplicationDbContext.Person' is null.");
        }
        var person = await _context.Person.FindAsync(id);
        if(person != null){
            _context.Person.Remove(person);
        }
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(index));
    }
    private bool PersonExists(string id){
        return (_context.Person?.Any(e => e.PersonId == id)).GetValueOrDefault();
    }    
    [HttpPost]
    public IActionResult index(Person ps){
        string stOutput = "Xin chào " + ps.PersonId + "-" + ps.FullName + "-" + ps.Address;
        ViewBag.info = stOutput;
        return View();
    }
    public async Task<IActionResult> Upload(){
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upload(IFormFile file){
        if(file != null){
            string fileExtentsion = Path.GetExtension(file.FileName);
            if(fileExtentsion != ".xls" && fileExtentsion != ".xlsx"){
                ModelState.AddModelError("", "Please choose excel file to upload!");
            }else{
                var fileName = DateTime.Now.ToShortTimeString + fileExtentsion;
                var filePath = Path.Combine(Directory.GetCurrentDirectory() + "/Uploads/Excels", fileName);
                var fileLocation = new FileInfo(filePath).ToString();
                using (var stream = new FileStream(filePath, FileMode.Create)){
                    await file.CopyToAsync(stream);
                    var dt = _excelProcess.ExcelToDataTable(fileLocation);
                    for(int i = 0; i < dt.Rows.Count; i++){
                        var ps = new Person();
                        ps.PersonId = dt.Rows[i][0].ToString();
                        ps.FullName = dt.Rows[i][1].ToString();
                        ps.Address = dt.Rows[i][2].ToString();

                        _context.Add(ps);
                    }
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

            }                
        }
            return View();   
    }

    public IActionResult Dowload(){
        var fileName = "person" + ".xlsx";
        using(ExcelPackage ep = new ExcelPackage()){
            ExcelWorksheet worksheet = ep.Workbook.Worksheets.Add("Sheet 1");
            worksheet.Cells["A1"].Value = "PersomId";
            worksheet.Cells["A2"].Value = "FullName";
            worksheet.Cells["A1"].Value = "Address";
            var personList = _context.Person.ToList();
            worksheet.Cells["A2"].LoadFromCollection(personList);
            var stream = new MemoryStream(ep.GetAsByteArray());
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
    }    
}
    