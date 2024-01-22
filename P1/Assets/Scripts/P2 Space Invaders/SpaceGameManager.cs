using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceGameManager : MonoBehaviour
{
    public GameObject squidPrefab;
    public GameObject octopusPrefab;
    public GameObject crabPrefab;

    public int iRow = 5;
    public int iCol = 11;
    public float cellSize = 2.5f;

    public SpaceInvadersMovement invadersTransform;

    // Start is called before the first frame update
    void Start()
    {
        SpawnInvaders();
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
                if (j < 2) invader = octopusPrefab;
                else if (j < 4) invader = crabPrefab;
                else invader = squidPrefab;
                GameObject aux = Instantiate(invader, invadersTransform.transform);
                aux.transform.position = new Vector3(invadersTransform.transform.position.x + (-iCol/2+i) * cellSize, 
                invadersTransform.transform.position.y + (-iRow/2+j) * cellSize, 0);
                aux.GetComponent<SpaceInvader>().father = invadersTransform;
            }
        }
    }
}
