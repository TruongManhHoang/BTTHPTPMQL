namespace ProjectMVC.Models;

public class Student : Person{
    public string FullName {get; set;}
    public string Address {get; set;}
    public int Age {get; set;}
    public int PhoneNumber {get; set;}

    public void EnterData(){
        base.EnterData();
        System.Console.Write("Student code = ");
        PhoneNumber = Convert.ToInt16(Console.ReadLine());
    }
    public void Display(){
        base.Display();
        System.Console.Write("Ma sinh vien: {0} ", PhoneNumber);

    }
}