using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;

namespace MvcMovie.Controllers;

public class EmployeeController : Controller{
    
    private readonly ApplicationDbContext _context;

    public EmployeeController(ApplicationDbContext context){
        _context = context;
    }
    public async Task<IActionResult> index(){
        var model = await _context.Employees.ToListAsync();
        return View(model);
    }
    public IActionResult create(){
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> create([Bind("PersonId, FullName, Address, EmployeeId, age")] Employee employee){
        if(ModelState.IsValid){
            _context.Add(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(index));
        }
        return View(employee);
    }
    public async Task<IActionResult> edit(string id){
        if(id == null || _context.Employees == null){
            return NotFound();
        }
        var employee = await _context.Employees.FindAsync(id);
        if(employee == null){
            return NotFound();
        }
        return View(employee);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> edit(string id, [Bind("PersonId, FullName, Address,EmployeeId, age")] Employee employee){
        if(id != employee.EmployeeId){
            return NotFound();
        }
        if(ModelState.IsValid){
            try{
                _context.Update(employee);
                await _context.SaveChangesAsync();
            }catch(DbUpdateConcurrencyException){
                if(!EmployeeExists(employee.EmployeeId)){
                    return NotFound();
                }else{
                    throw;
                }
             }
            return RedirectToAction(nameof(index));
        }
        return View(employee);
    }
    [HttpPost]
    public async Task<IActionResult> index(String name){
        return View( await _context.Employees.Where(e => e.FullName.Contains(name)).ToListAsync());
    }
    
    private bool EmployeeExists(string id)
    {
        return (_context.Employees?.Any(e => e.EmployeeId == id)).GetValueOrDefault();
    }
    public async Task<IActionResult> delete(string id){
        if(id == null || _context.Employees == null){
            return NotFound();
        }
        var employee = await _context.Employees.FirstOrDefaultAsync(m => m.EmployeeId == id);
        if(employee == null){
            return NotFound();
        }
        return View(employee);
    }
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> deleteConfirmed(string id){
        if(_context.Employees == null){
            return Problem("Entity set 'ApplicationDbContext.Person' is null.");
        }
        var employee = await _context.Employees.FindAsync(id);
        if(employee != null){
            _context.Employees.Remove(employee);
        }
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(index));
    } 
}