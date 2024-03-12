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

    // Jugador
    private SPlayer player;

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
    // Tiempo que dura la animación de daño del jugador
    public float playerDamageDelay = 1.5f;
    // Multiplicador que aumenta la velocidad de los aliens
    public float incVel = 3;

    // SINGLETON
    public static SGameManager instance = null;

    // Interfaz
    public TextMeshPro scoreText; // Texto de la puntuación
    public TextMeshPro lifesText; // Texto con nº vidas
    public GameObject spriteVida3; // Sprites de las vidas del jugador
    public GameObject spriteVida2;

    // OVNIs
    public GameObject prefabOVNI; // Prefab del OVNI
    public Transform spawnIzqOVNI; // Posicion de OVNI a la izquierda
    public Transform spawnDerOVNI; // Posición de OVNI a la derecha
    public float spawnOVNITime = 15f; // Tiempo que tarda un OVNI en aparecer

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        // Busco al jugador y lo guardo
        player = FindObjectOfType<SPlayer>();
        
        // Decimos que matrizAliens es una nueva matriz de SInvaders de nColumnas x nFilas
        // - INICIALIZACIÓN
        matrizAliens = new SInvader[nColumnas, nFilas];
        SpawnAliens();

        InvokeRepeating("SelectAlienShoot", tiempoEntreDisparos, tiempoEntreDisparos);
        InvokeRepeating("SpawnOVNI", spawnOVNITime, spawnOVNITime);
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

    void SpawnOVNI()
    {
        // Elegir una dirección aleatoria
        int random = Random.Range(0, 2); // entre 0 y 1

        if (random == 0) // Si sale 0, lo colocamos a la izquierda
        {
            // Crearlo y Ponerle la dirección apropiada
            Instantiate(prefabOVNI, spawnIzqOVNI).GetComponent<SOvni>().dir = 1; 
        }
        else if (random == 1)  // Si sale 1, lo colocamos a la derecha
        {
            // Crearlo y asignar la dirección
            Instantiate(prefabOVNI, spawnDerOVNI).GetComponent<SOvni>().dir = -1;
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
    public void PlayerGameOver()
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
        if (!gameOver && player.GetCanMove())
        {
            vidas--;
            UpdateLifeUI();
            // Animacíon de daño del jugador
            player.PlayerDamaged();
            padreAliens.canMove = false; // Bloqueo los aliens
            SetInvadersAnim(false);
            Invoke("UnlockDamagedPlayer", playerDamageDelay);
            if (vidas <= 0)
            {
                PlayerGameOver();
            }
        }
    }

    private void UnlockDamagedPlayer()
    {
        player.PlayerReset(); // Desbloqueo al jugador
        padreAliens.canMove = true; // Desbloqueo los aliens
        SetInvadersAnim(true);
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
        
        // Actualizar velocidad de los aliens según cuántos quedan
        // Suma incVelocidad / aliensTotales
        padreAliens.speed += (incVel / (float)(nFilas * nColumnas));

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

    // Recorre la lista de aliens, y les pone la animacíon indicada
    public void SetInvadersAnim(bool movement)
    {
        for(int i = 0; i < nColumnas; i++)
        {
            for (int j = 0; j < nFilas; j++)
            {
                if (matrizAliens[i, j] != null) // comprobamos que existe
                {
                    // Asignamos la animación que toque
                    if (movement) matrizAliens[i, j].MovementAnimation();
                    else matrizAliens[i, j].StunAnimation();
                }
            }
        }
    }
}
