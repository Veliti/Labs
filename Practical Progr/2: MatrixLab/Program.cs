using System;

var matrixArr1 = new int[,]{{3, 2, 5, 6}, {4, 1, 11, 51}, {0, -11, 3, 515}, {1, -199, 2, 63}};

var matrixArr2 = new int[,]{{2, 3 ,3}, {3, 2, 3}, {1, -5, 0}};
var ext2 = new int[]{2, 3, 8};

var matrixArr3 = new int[,]{{2, 3 ,3}, {0, 0, 0}, {1, -5, 0}};
var ext3 = new int[]{2, 3, 8};

var matrixArr4 = new int[,]{{2, 6}, {11, -2}};


//Квадратная матрица
var matrix = new SquareMatrix(matrixArr1);
Console.WriteLine($"Matrix det: {matrix.GetDeterminant()}");


//Система лин. уравнений
var system2 = new SquareLinierSystems(matrixArr2, ext2);
PrintSolutions(system2);

var system3 = new SquareLinierSystems(matrixArr3, ext3);
PrintSolutions(system3);

    
//списки квадратных матриц
var matrices = new SquareMatrixList(new SquareMatrix[]{matrix, system2, system3, new SquareMatrix(matrixArr2)});
matrices.Sort();
System.Console.WriteLine(matrices);


void PrintSolutions(SquareLinierSystems system)
{
    if(system.TryToCalculateSolution(out float[]? solutions))
    {
        System.Console.Write("Solution: ");
        foreach (var item in solutions!)
        {
            Console.Write($"{item:F2} ");
        }
        Console.Write(Environment.NewLine);
    }
    else 
        System.Console.WriteLine("No solution found");
}

//нужен вар для последнего задания