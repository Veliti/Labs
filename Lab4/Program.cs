
System.Console.WriteLine("задание 1");
Lab.Task1(200);

System.Console.WriteLine("задание 2");
Lab.Task2();

System.Console.WriteLine("задание 3");
var toys = new ToyData[]
{
    new ToyData("amogus", 100, (2, 10)),
    new ToyData("doll", 200, (6, 16)),
    new ToyData("bomb", 1000, (16, 16)),
    new ToyData("car", 520, (5, 12)),
    new ToyData("cat", 0, (12, 60)),
    new ToyData("slime", 10, (0, 3)),
};
Lab.Task3(toys, priceFilter: 700, ageFilter: 10);

System.Console.WriteLine("задание 4");
Lab.Lab4(80);

System.Console.WriteLine("задание 5");
Lab.Task5(40);

System.Console.WriteLine("задание 6");
Lab.Task6("hello");