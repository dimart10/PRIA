using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class ListasConBucles : MonoBehaviour
{
    [SerializeField]
    private int[] arrayNumeros;      // Array (tamaño fijo)
    [SerializeField]
    private List<int> listaNumeros;  // Lista (tamaño variable)
    [SerializeField]
    private int numero;

    // Start is called before the first frame update
    void Start()
    {
        /*
        Ejercicio1();
        Ejercicio2();
        Ejercicio3();

        TablaDeMultiplicarDe(5, 10);
        TablaDeMultiplicarDe(10, 3);
        TablaDeMultiplicarDe(1, 20);

        List<int> miLista = new List<int> { 3, 4, 325, 65, 2, 2, 12, 41 };
        List<int> miLista2 = new List<int>(new int[20]);

        EscribeLista(miLista);
        EscribeLista(miLista2);
        */

        EscribeLista(Ejercicio4V2(9));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Crea un bucle for que imprima los números entre 20 y 30
    void Ejercicio1()
    {
        for (int i = 20; i<=30; i++)
        {
            Debug.Log(i);
        }
    }

    // Crea un bucle for que imprima los números entre 100 y 90
    void Ejercicio2()
    {
        for (int i = 100; i >= 90; i--)
        {
            Debug.Log(i);
        }
    }

    // Crea un bucle for que imprima la tabla de multiplicar del 5
    void Ejercicio3()
    {
        /* Método David
        for(int i = 5; i<=50; i+=5)
        {
            Debug.Log(i);
        }
        */

        for(int i = 1; i<=10; i++)
        {
            Debug.Log(i * 5);
        }
    }

    // Este método debe devolver una lista de ints (List<int>) con los prieros N numeros
    // Necesitamos un parámetro de salida (return) que devuelva la lista (List<int>)
    // Necesitamos un parámetro de entrada que indique el valor de N (int)
    public List<int> Ejercicio4(int n)
    {
        // Creamos la lista de tamaño n (de momento contiene n ceros)
        List<int> nuevaLista = new List<int>(new int[n]); 

        // Habrá que añadir N números a la lista (0, 1, 2, 3, 4, ..., N)
        for(int i = 0; i < n; i++) // Llamamos al bucle n veces, i valdrá 0,1,2,3...
        {
            // Asignar al elemento en la posición i el valor i (0 es 0, 1 es 1, etc.)
            nuevaLista[i] = i;
        }

        return nuevaLista; // Devolvemos la lista
    }

    public List<int> Ejercicio4V2(int n)
    {
        // Creamos la lista vacía
        List<int> nuevaLista = new List<int>();

        // Habrá que añadir N números a la lista (0, 1, 2, 3, 4, ..., N)
        for (int i = 0; i < n; i++) // Llamamos al bucle n veces, i valdrá 0,1,2,3...
        {
            // Insertar el valor i al final de la lista (0, 1, 2, 3...)
            nuevaLista.Add(i);
        }
        return nuevaLista; // Devolvemos la lista
    }

    public void TablaDeMultiplicarDe(int numero, int nNumeros)
    {
        for (int i = 1; i <= nNumeros; i++)
        {
            Debug.Log(i * numero);
        }
    }

    public void EscribeLista(List<int> lista)
    {
        // Bucle que recorre lista y escribe sus elementos
        for(int i = 0; i<lista.Count; i++)
        {
            Debug.Log(lista[i]);
        }
    }

    void Explicacion()
    {
        numero = 10;

        // ARRAYS
        // Para inicializar un array, escribimos new tipo[N]
        // Especificamos el tipo de objeto que contiene, y su tamaño (N), p.ej. 5

        // Inicializar array de tamaño 10(valor por defecto, 0)
        arrayNumeros = new int[10]; // sería {0,0,0,0,0,0,0,0,0,0}

        // También podemos inicializar con valores específicos
        arrayNumeros = new int[] { 0, 1, 2, 3, 4, 5 }; // este array tiene los números del 0 al 5

        // LISTS
        // Para inicializar una lista, usamos new List<tipo>();
        listaNumeros = new List<int>();             // Crea una lista de ints vacía
        listaNumeros = new List<int>(new int[8]);   // Crea una lista con 9 ceros
        listaNumeros = new List<int>(arrayNumeros); // Crea una lista con los valores de ArrayNumeros
        listaNumeros = new List<int> { 1, 1, 2, 3, 5, 8 }; // Crea una lista con los valores indicados

        // Bucle for que escribe los contenidos de listaNumeros
        // Empezamos en 0 y vamos aumentando hasta llegar al tamaño de la lista (lista.Count)
        for (int i = 0; i < listaNumeros.Count; i++)
        {
            // la variable i nos indica en qué iteración estamos
            // vamos accediendo al elemento en la posición i de la lista
            Debug.Log(listaNumeros[i]);
        }

        // Bucle while, que escribe los elementos en un array
        int w = 0; // Variable de control, empieza en 0
        while (w < arrayNumeros.Length) // Condición, paramos al llegar al tamaño del array
        {
            Debug.Log(arrayNumeros[w]); // Escribimos el elemento en posición w
            w++;                        // Aumentamos w en 1 en cada vuelta del bucle
        }
    }
}
