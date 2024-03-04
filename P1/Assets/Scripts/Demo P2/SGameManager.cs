using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SGameManager : MonoBehaviour
{
    // Lista doble (matriz) de SInvaders - DECLARACIÓN
    public SInvader[,] matrizAliens;

    // Nº filas de invaders, alto
    public int nFilas = 5;
    // Nº columnas de invaders, ancho
    public int nColumnas = 11;

    // Prefab de alien
    public GameObject alien1Prefab;
    public GameObject alien2Prefab;
    public GameObject alien3Prefab;

    // GameObject padre de los aliens (para movimiento)
    public SinvaderMovement padreAliens;
    // Distancia entre aliens al spawnearlos
    public float distanciaAliens = 1;
    // Tiempo entre disparos de los aliens
    public float tiempoEntreDisparos = 2f;

    // CICLO DE JUEGO
    // Fin de la partida
    public bool gameOver = false;
    // Vidas actuales del jugador
    public int vidas = 3;
    // Puntuación actual del jugador
    public int score = 0;
    // Nº aliens derrotados
    private int defeatedAliens = 0;

    // SINGLETON
    public static SGameManager instance = null;

    // Interfaz
    public TextMeshPro scoreText; // Texto de la puntuación
    public TextMeshPro lifesText; // Texto con nº vidas
    public GameObject spriteVida3; // Sprites de las vidas del jugador
    public GameObject spriteVida2;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        // Decimos que matrizAliens es una nueva matriz de SInvaders de nColumnas x nFilas
        // - INICIALIZACIÓN
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
                if (j == nFilas-1) prefab = alien1Prefab; // la última fila
                else if (j < 2) prefab = alien3Prefab; // las dos primeras filas
                else prefab = alien2Prefab; // el resto de filas

                // Dentro de los dos bucles, intanciamos un alien y guardamos una referencia
                SInvader auxAlien =  Instantiate(prefab, padreAliens.gameObject.transform).GetComponent<SInvader>();
                // Lo guardamos en la posición de la matriz apropiada
                matrizAliens[i, j] = auxAlien;
                // Colocamos el alien
                auxAlien.transform.position += new Vector3(i-nColumnas/2,j-nFilas/2,0) * distanciaAliens;
                // Asignamos padre movement al alien
                auxAlien.padre = padreAliens;
            }
        }

    }

    // Busca el alien más cercano al jugador en una columna aleatoria y le dice que dispare
    private void SelectAlienShoot()
    {
        // Variable de control de la búsqueda, cuando está a true, ya he encontrado al alien y paro
        bool encontrado = false;

        while (!encontrado) // Se repite con columnas aleatorias hasta encontrar un alien
        {
            // 1 - Elegir una columna aleatoria que no esté vacía
            int randomCol = Random.Range(0, nColumnas); // Columna aleatoria

            // 2 - Buscar al alien más cercano al jugador en esa columna
            // En este for, tenemos dos condiciones, que j > -1 y que encontrado == false
            // como usamos && entre ellas (el AND), deben cumplirse las dos, o salimos del bucle for
            for (int j = 0; j < nFilas && !encontrado; j++) // Recorrer la columna aleatoria 
            {
                // Compruebo si el alien exite (no se ha destruido)
                if (matrizAliens[randomCol, j] != null) // Si la casilla no está vacía (null) el alien sigue vivo
                {
                    // Si encuentro un alien vivo, es el más cercano de la columna al jugador
                    // porque la estoy recorriendo de abajo a arriba
                    matrizAliens[randomCol, j].Shoot(); // El alien dispara
                    encontrado = true; // He acabado la búsqueda
                }
            }
        }
    }

    // Método que se llama cuando perdemos la partida (nos quedamos sin vidas, o los aliens llegan abajo)
    public void PlayerGamerOver()
    {
        gameOver = true;
        CancelInvoke(); // Interrumpimos todos los invokes de este componente (se deja de disparar)
        Debug.Log("el jugador ha perdido");
    }

    public void PlayerWin()
    {
        gameOver = true;
        CancelInvoke(); // Interrumpimos todos los invokes de este componente (se deja de disparar)
        Debug.Log("el jugador ha ganado");
    }

    public void DamagePlayer()
    {
        vidas--;
        UpdateLifeUI();
        if (vidas <= 0)
        {
            PlayerGamerOver();
        }
    }

    private void UpdateLifeUI()
    {
        // Actualiza el texto
        lifesText.text = vidas.ToString();
        // Actualizar los sprites de las vidas
        spriteVida2.SetActive(vidas >= 2); // Se activa si vidas >= 2
        spriteVida3.SetActive(vidas >= 3); // Se activa si vidas >= 3
    }

    // Comprueba si el jugador ha ganado (si ha destruido a todos los aliens)
    public void AlienDestroyed()
    {
        defeatedAliens++; // Aumento la cuenta de aliens derrotados
        if(defeatedAliens >= nFilas * nColumnas) // Si ha derrotado a todos los aliens
        {
            PlayerWin(); // El jugador gana
        }
    }

    public void ResetGame()
    {
        SceneManager.LoadScene("DemoInvaders");
    }
    
    // Suma points puntos a la puntuacion
    public void AddScore(int points)
    {
        score += points;
        scoreText.text = "SCORE\n" + score.ToString(); // Actualizar texto puntos
    }
}
