using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Net.Sockets;

public class SpaceGameManager : MonoBehaviour
{
    public GameObject squidPrefab;
    public GameObject octopusPrefab;
    public GameObject crabPrefab;
    public GameObject OVNIPrefab;

    public int iRow = 5;
    public int iCol = 11;
    public float cellSize = 2.5f;

    public SpaceInvadersMovement invadersTransform;

    private SpaceInvader[,] invaders;

    private int vidas = 2;
    public GameObject[] spritesVidas;
    public TextMeshPro textoVidas;
    private int score = 0;
    public TextMeshPro scoreText;

    static public SpaceGameManager instance = null;

    private int deadInvaders = 0;
    public float maxSpeed = 10;
    public float shotTime = 2;
    public float ovniDelay = 20f;
    public float ovniDelayRange = 5f;

    public AudioClip playerHitSFX;

    public Transform leftOVNISpawn;
    public Transform rightOVNISpawn;

    public float damagePlayerDelay = 1.5f;

    private SpacePlayer player;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<SpacePlayer>();
        invaders = new SpaceInvader[iCol,iRow];
        SpawnInvaders();
        InvokeRepeating("RandomShot", shotTime, shotTime);
        float firstOVNITime = Random.Range(ovniDelay - ovniDelayRange, ovniDelay + ovniDelayRange);
        Invoke("SpawnOVNI", firstOVNITime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnInvaders()
    {
        for(int i = 0; i < iCol; i++)
        {
            for (int j = 0; j < iRow; j++)
            {
                GameObject invader;
                if (j > 2) invader = octopusPrefab;
                else if (j > 0) invader = crabPrefab;
                else invader = squidPrefab;

                SpaceInvader aux = Instantiate(invader, invadersTransform.transform).GetComponent<SpaceInvader>();
                aux.transform.position = new Vector3(invadersTransform.transform.position.x + (-iCol/2+i) * cellSize, 
                invadersTransform.transform.position.y + (iRow/2-j) * cellSize, 0);
                aux.father = invadersTransform;
                invaders[i, j] = aux;
            }
        }
    }

    private void RandomShot()
    {
        int shotCol = -1;
        int maxRow = -1;
        while(shotCol == -1)
        {
            maxRow = -1;
            int randomCol = Random.Range(0, iCol);
            for(int i = 0; i < iRow; i++)
            {
                if (invaders[randomCol, i] != null && i > maxRow)
                {
                    maxRow = i;
                    shotCol = randomCol;
                }
            }
        }
        invaders[shotCol, maxRow].Shoot();
    }

    public void DamagePlayer()
    {
        SoundManager.instance.PlaySFX(playerHitSFX);
        vidas--;
        textoVidas.text = (vidas+1).ToString();
        if (vidas < 0) Debug.Log("GAME OVER");
        else spritesVidas[vidas].SetActive(false);
        PlayerDamagePause();
    }

    private void PlayerDamagePause()
    {
        invadersTransform.canMove = false;
        player.PlayerDamaged();
        Invoke("ResumeGameAfterDamage", damagePlayerDelay);
    }

    private void ResumeGameAfterDamage()
    {
        invadersTransform.canMove = true;
        player.Reset();
    }

    public void AddScore(int p)
    {
        score += p;
        scoreText.text = score.ToString();
    }

    public void SpawnOVNI()
    {
        SpaceOvni ovni;
        // Lo spawneo a la izquierda
        if (Random.Range(0, 2) == 0)
        {
            ovni = Instantiate(OVNIPrefab, leftOVNISpawn.position, Quaternion.identity).GetComponent<SpaceOvni>();
            // Lo modifico para que vaya a la derecha
            ovni.xMovement = 1;
        }
        // Lo spawneo a la derecha
        else
        {
            ovni = Instantiate(OVNIPrefab, rightOVNISpawn.position, Quaternion.identity).GetComponent<SpaceOvni>();
            // Lo modifico para que vaya a la izquierda
            ovni.xMovement = -1;
        }

        // Preparo el siguiente spawn de ovni
        float nextOVNITime = Random.Range(ovniDelay - ovniDelayRange, ovniDelay + ovniDelayRange);
        Invoke("SpawnOVNI", nextOVNITime);
    }

    public void UpdateTimeScale()
    {
        deadInvaders++;
        // En el juego parece que solo se aceleran los enemigos, 
        // con la línea de abajo, aceleraríamos todo el juego
        //Time.timeScale = 1 + ((float)deadInvaders / (float)invaders.Length)*maxSpeed;

        // Con esta, aceleramos solo la velocidad de los invaders
        invadersTransform.speed = 1 + ((float)deadInvaders / (float)invaders.Length) * maxSpeed;
    }

    public void SetTimeScale(float f)
    {
        Time.timeScale = f;
    }

    public void OnGameOver()
    {
        CancelInvoke();
    }
}
