using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMovie.Models;

[Table("Persons")]

public class Person{
    [Key]
    public string PersonId {get; set;}
    public string FullName{get; set;}

    public string Address {get; set;}

    public int age {get; set;}
    public void EnterData(){
        System.Console.Write("Full name = ");
        FullName = Console.ReadLine();
        System.Console.Write("Adress = ");
        Address = Console.ReadLine();
        System.Console.Write("Age = ");
        age = Convert.ToInt16(Console.ReadLine());
    }
    public void Display(){
        System.Console.WriteLine("{0} - {1} - {2} tuoi", FullName, Address, age);

    }
    public int GetYearOfBirth(int Age){
        int yearOfBirth = 2023 - Age;
        return yearOfBirth;
    }
}