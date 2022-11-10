//Task 21

// var matrix1 = new MatrixLab3();
// System.Console.WriteLine(matrix1);

// var upRangeFrom = -65;
// var upRangeTo = 120;
// var lowRangeFrom = -3.5;
// var lowRangeTo = 10.75;
// var matrix2 = new MatrixLab3(4, upRangeFrom, upRangeTo, lowRangeFrom, lowRangeTo);
// System.Console.WriteLine(matrix2);

// var matrix3 = new MatrixLab3(4);
// System.Console.WriteLine(matrix3);

//Task 22

// var array = new double[,]{{3, 5, 4, 2}, {7, 1, -8, 33}, {-13, 0, 6, -2}, {3, 15, 17, 4.5}};
// var matrix4 = new MatrixLab3(array);
// System.Console.WriteLine(matrix4);

// matrix4.DiagonalSort();
// System.Console.WriteLine(matrix4);

//Task 23

var A = new MatrixLab3(new double[,]{{1, 2 ,3}, {3, 2, 1}, {4, 5, 6}});
var B = new MatrixLab3(new double[,]{{2, 6, 1}, {5, 2, 7}, {7, 1, 0}});
var C = new MatrixLab3(new double[,]{{4, 11, 1}, {5, 2, 7}, {7, 1, 31}});

System.Console.WriteLine(2 * A - B.Transpose());
