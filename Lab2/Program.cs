using System;

// // 1
// // демонстрация конструктора
// var tri = new RightTriangle(10, 5);
// // демонстрация вычисления площади
// Console.WriteLine("Площадь треугольника: " + tri.GetArea());
// // метод ToString
// Console.WriteLine((object)tri);

// // 2
// // демо операторов ++ и --
// Console.WriteLine(tri + " было");
// Console.WriteLine(tri++ + " стало после ++");
// Console.WriteLine(tri-- + " стало после --");

// // конвертация в дабл
// Console.WriteLine((double)tri + 35.0);

// // конвертация в булл
// if (tri)
// {
//     Console.WriteLine("Такой треугольник существует");
// }
// var tri2 = new RightTriangle(10, 10);

// if(tri >= tri2) Console.WriteLine("1вый треугольник больше или равен 2ому треугольник");
// if(tri <= tri2) Console.WriteLine("2ой треугольник больше или равен 1ому треугольник");



Bozo bozo = new Bozo();

Console.WriteLine(bozo);

public class Bozo {public int Pub = 3; int priv = 10;}