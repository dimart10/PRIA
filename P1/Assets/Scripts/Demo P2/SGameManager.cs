using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SGameManager : MonoBehaviour
{
    // Lista doble (matriz) de SInvaders - DECLARACI�N
    public SInvader[,] matrizAliens;

    // N� filas de invaders, alto
    public int nFilas = 5;
    // N� columnas de invaders, ancho
    public int nColumnas = 11;

    // Prefab de alien
    public GameObject alien1Prefab;
    public GameObject alien2Prefab;
    public GameObject alien3Prefab;

    // GameObject padre de los aliens (para movimiento)
    public SinvaderMovement padreAliens;
    // Distancia entre aliens al spawnearlos
    public float distanciaAliens = 1;

    public float tiempoEntreDisparos = 2f;

    // Start is called before the first frame update
    void Start()
    {
        // Decimos que matrizAliens es una nueva matriz de SInvaders de nColumnas x nFilas
        // - INICIALIZACI�N
        matrizAliens = new SInvader[nColumnas, nFilas];
        SpawnAliens();

        InvokeRepeating("SelectAlienShoot", tiempoEntreDisparos, tiempoEntreDisparos);
    }

    void SpawnAliens()
    {
        // Doble bucle (anidado) que recorra la matriz (de 11 x 5, rangos 0-10 y 0-4)
        for(int i = 0; i < nColumnas; i++)
        {
            for(int j = 0; j < nFilas; j++)
            {
                GameObject prefab; // prefab de alien que spawnaemos
                if (j == nFilas-1) prefab = alien1Prefab; // la �ltima fila
                else if (j < 2) prefab = alien3Prefab; // las dos primeras filas
                else prefab = alien2Prefab; // el resto de filas

                // Dentro de los dos bucles, intanciamos un alien y guardamos una referencia
                SInvader auxAlien =  Instantiate(prefab, padreAliens.gameObject.transform).GetComponent<SInvader>();
                // Lo guardamos en la posici�n de la matriz apropiada
                matrizAliens[i, j] = auxAlien;
                // Colocamos el alien
                auxAlien.transform.position += new Vector3(i-nColumnas/2,j-nFilas/2,0) * distanciaAliens;
                // Asignamos padre movement al alien
                auxAlien.padre = padreAliens;
            }
        }

    }

    // Busca el alien m�s cercano al jugador en una columna aleatoria y le dice que dispare
    private void SelectAlienShoot()
    {
        // Variable de control de la b�squeda, cuando est� a true, ya he encontrado al alien y paro
        bool encontrado = false;

        while (!encontrado) // Se repite con columnas aleatorias hasta encontrar un alien
        {
            // 1 - Elegir una columna aleatoria que no est� vac�a
            int randomCol = Random.Range(0, nColumnas); // Columna aleatoria

            // 2 - Buscar al alien m�s cercano al jugador en esa columna
            // En este for, tenemos dos condiciones, que j > -1 y que encontrado == false
            // como usamos && entre ellas (el AND), deben cumplirse las dos, o salimos del bucle for
            for (int j = 0; j < nFilas && !encontrado; j++) // Recorrer la columna aleatoria 
            {
                // Compruebo si el alien exite (no se ha destruido)
                if (matrizAliens[randomCol, j] != null) // Si la casilla no est� vac�a (null) el alien sigue vivo
                {
                    // Si encuentro un alien vivo, es el m�s cercano de la columna al jugador
                    // porque la estoy recorriendo de abajo a arriba
                    matrizAliens[randomCol, j].Shoot(); // El alien dispara
                    encontrado = true; // He acabado la b�squeda
                }
            }
        }
    }

}
