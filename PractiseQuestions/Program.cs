using PractiseQuestions;
using System;
using System.Xml.Serialization;
class Program
{
    static void Main(string[] args)
    {
        Console.Write("Enter the Size of FizzBuzz Arryay: ");
        int lengthOfArrya=Convert.ToInt32(Console.ReadLine());
        List<string> result = new List<string>();
        result = FizzBuzz(lengthOfArrya);
        foreach (string str in result)
        {
            Console.Write(str + " ");
        }
        
        Console.WriteLine("\n\nPolymoephism\n");

        Square square = new Square();
        square.Area();
        Console.WriteLine("\n");
        Rectangle rectangle = new Rectangle();  
        rectangle.Area();
        Console.WriteLine("\n");
        Circle circle = new Circle();   
        circle.Area();
    }
    static List<string> FizzBuzz(int n)
    {
        List<string> fizzBuzzPatterns = new List<string>();
        for (int i = 1; i <= n; i++)
        {
            if ((i % 3 == 0) && (i % 5 == 0))
            {
                fizzBuzzPatterns.Add("FizzBuzz");
            }
            else if (i % 3 == 0)
            {
                fizzBuzzPatterns.Add("Fizz");
            }
            else if (i % 5 == 0) 
            {
                fizzBuzzPatterns.Add("Buzz");
            }
            else
            {
                fizzBuzzPatterns.Add(Convert.ToString(i));
            }
            
        }
        return fizzBuzzPatterns;
    }
}