using UnityEngine;

public class MatrixTest : MonoBehaviour {

	Matrix<int> myMatrix;

	private void Start()
	{
		Debug.LogWarning("Acá inicia el test de Matrix");
		 myMatrix = new Matrix<int>(4, 3);

		//{ 1 , 2 , 3 , 4 }
		//{ 5 , 6 , 7 , 8 }
		//{ 9 , 10, 11, 12}

		if (myMatrix.Width != 4) print("With incorrecto");
		if (myMatrix.Height != 3) print("With incorrecto");
		if (myMatrix.Lenght != 12) print("Lenght incorrecto");

		myMatrix[0, 0] = 1;
		myMatrix[1, 0] = 2;
		myMatrix[2, 0] = 3;
		myMatrix[3, 0] = 4;
		myMatrix[0, 1] = 5;
		myMatrix[1, 1] = 6;
		myMatrix[2, 1] = 7;
		myMatrix[3, 1] = 8;
		myMatrix[0, 2] = 9;
		myMatrix[1, 2] = 10;
		myMatrix[2, 2] = 11;
		myMatrix[3, 2] = 12;

		if (!myMatrix.Contains(12)) print("Hay un error en el Contains");
		if (myMatrix.Contains(13)) print("Hay un error en el Contains");

		var acum = 1;
		for (int y = 0; y < 3; y++)
		{
			for (int x = 0; x < 4; x++)
			{
				//print("width: " + x + " Heigh: " + y + "Value should Be: " + acum);
				if (myMatrix[x, y] != acum) print(string.Format("El valor [{0},{1}] es incorrecto",x,y));
				acum++;
			}
		}

		var range = myMatrix.GetRange(1, 1, 2, 2);
		if (range[0] != 6) print("El rango es incorrecto, E1");
		if (range[1] != 7) print("El rango es incorrecto, E2");
		if (range[2] != 10) print("El rango es incorrecto, E3");
		if (range[3] != 11) print("El rango es incorrecto, E4");

		myMatrix.Clear();

		if (myMatrix.Width != 0) print("With incorrecto");
		if (myMatrix.Height != 0) print("With incorrecto");
		if (myMatrix.Lenght != 0) print("Lenght incorrecto");

		Debug.LogWarning("Acá Termina el test de Marix.");
		Debug.LogWarning("Si no debuguea nada esta todo Ok.");
	}
}
