using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models;

public class Student{
    [Key]
    public String StudentId {get; set;}
    public string FullName {get; set;}
    public string Address {get; set;}
    public int Age {get; set;}
    public int PhoneNumber {get; set;}
}