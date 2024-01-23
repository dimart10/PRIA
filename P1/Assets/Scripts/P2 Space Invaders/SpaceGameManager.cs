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
        invaders = new SpaceInvader[iCol,iRow];
        SpawnInvaders();
        InvokeRepeating("RandomShot", 3, 3);
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
        vidas--;
        textoVidas.text = (vidas+1).ToString();
        if (vidas < 0) Debug.Log("GAME OVER");
        else spritesVidas[vidas].SetActive(false);

    }

    public void AddScore(int p)
    {
        score += p;
        scoreText.text = score.ToString();
    }

    public void UpdateTimeScale()
    {
        deadInvaders++;
        Time.timeScale = 1 + (deadInvaders / invaders.Length)*5;
    }
}
