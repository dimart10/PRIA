using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EjerciciosMartes13 : MonoBehaviour
{
    // Podemos hacerlo con un array de char, pero es mucho más engorroso crearlo
    private char[] arrayDNI = {'T', 'R', 'W', 'A', 'G', '5'};

    // También nos sirve un string, porque se comporta como un array de caracteres
    private string letrasDNI = "TRWAGMYFPDXBNJZSQVHLCKE";

    // Start is called before the first frame update
    void Start()
    {
        //Ejercicio1_Impares();

        /*
        Debug.Log(Ejercicio2_Factorial(0));
        Debug.Log(Ejercicio2_Factorial(1));
        Debug.Log(Ejercicio2_Factorial(2));
        Debug.Log(Ejercicio2_Factorial(5));
        Debug.Log(Ejercicio2_Factorial(8));
        */

        /*
        int DNI_Ejemplo1 = 99999999;
        int DNI_Ejemplo2 = 00000000;

        Debug.Log(DNI_Ejemplo1.ToString() + Ejercicio3_DNI(DNI_Ejemplo1));
        Debug.Log(DNI_Ejemplo2.ToString() + Ejercicio3_DNI(DNI_Ejemplo2));
        */

        int[] listaNumeros1 = { 0, 2, 4, 5, 23, 2432, 34354, 111, 353533, 0, 13 };
        int[] listaNumeros2 = { -5, -300, -1, -2000, -5000, -949994 };

        Debug.Log(Ejercicio5_MinimoLista(listaNumeros1));
        Debug.Log(Ejercicio5_MinimoLista(listaNumeros2));
    }


    // Ejercicio 1 - Muestra impares entre 0 y 30
    public void Ejercicio1_Impares()
    {
        for(int i = 0; i <= 30; i++)
        {
            // Un número es impar, si su módulo de 2 es distinto de 0 (numero%2!=0)
            if(i%2 != 0) // Si i es impar (su módulo de 2 es distinto de 0)
            {
                Debug.Log(i); // Escribimos i
            }
        }
    }

    // Ejercicio 2 - Factorial de un número
    public int Ejercicio2_Factorial(int n)
    {
        // Creo variable factorial
        int factorial = 1; // Su valor mínimo siempre será 1

        // Calculo del factorial para el resto de números
        // P.ej. factorial del 3, sería 3 x 2 x 1
        // factorial *= 3; factorial *= 2; factorial *= 1; 

        // Recorremos desde el número del que sacamos el factorial hasta el 1 (no incluido)
        for(int i = n; i > 1; i--)
        {
            // Multiplicamos el factorial por el número i
            factorial *= i; // factorial = factorial * i;
        }

        // Devuelvo el factorial
        return factorial;
    }

    // Ejercicio 3 - Letra del DNI
    public char Ejercicio3_DNI(int numeroDNI)
    {
        // Calcular el módulo 23 del número del DNI ( %23)
        int modulo = numeroDNI%23;

        // Una vez lo tenga, puedo devolver la letra en la posición módulo 23 del string de letras DNI
        char letra = letrasDNI[modulo];

        return letra; // Devuelvo la letra
    }

    // Ejercicio 4 - Máximo elemento en una lista
    public int Ejercicio4_MaximoLista(int[] lista)
    {
        // Creamos una variable int para el máximo, inicializada con el mínimo valor posible
        // También podríamos crearla con el valor del primer número lista[0]
        int max = int.MinValue;

        // Recorremos la lista en un bucle for
        for(int i = 0; i < lista.Length; i++)
        {
            // Si el número de la posición i de la lista es mayor que el máximo, lo actualizamos
            if (max < lista[i]) max = lista[i];
            // max = Mathf.Max(max, lista[i]); //Para comparar dos números podemos usar la Mathf.Max
        }

        // Devolvemos el mayor número que hubiera en la lista
        return max;
    }

        // Ejercicio 5 - Mínimo elemento en una lista
    public int Ejercicio5_MinimoLista(int[] lista)
    {
        // Creamos una variable int para el mínimo, inicializada con el máximo valor posible
        // También podríamos crearla con el valor del primer número lista[0]
        int min = int.MaxValue;

        // Recorremos la lista en un bucle for
        for (int i = 0; i < lista.Length; i++)
        {
            // Si el número de la posición i de la lista es menor que el mínumo, lo actualizamos
            if (lista[i] < min) min = lista[i];
            // min = Mathf.Min(min, lista[i]); //Para comparar dos números podemos usar la Mathf.Min
        }

        // Devolvemos el menor número que hubiera en la lista
        return min;
    }
}
