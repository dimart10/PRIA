using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MineSweep : MonoBehaviour
{
    //public dir[8]{dir(0,1), dir(0,-1), dir(1,0), dir(-1,0), dir(1,1), dir(-1-1), dir(1,-1), dir(-1,1)};

    public Vector2[] dirs; 
    public int width = 8;
    public int height = 8;
    public int nMines = 10;
    public Cell[,] field;
    public int revealedCells = 0;
    public GameObject cellPrefab;

    // Start is called before the first frame update
    void Start()
    {
        dirs = new Vector2[8] { new Vector2(1, 1), new Vector2(-1, -1), new Vector2(1, 0), new Vector2(-1, 0), new Vector2(0, 1), new Vector2(0, -1), new Vector2(-1, 1), new Vector2(1, -1) };
        generateField();
        printField();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void generateField()
    {
        if (nMines >= width * height) { // Too many mines check
         Debug.Log("Too many mines for that field!!");
            return;
        }
        field = new Cell[width, height];
       
        //Debug.Log("WIDTH: " + width);
        //Debug.Log("HEIGHT: " + height);

        for(int h=0; h < height; h++)
        {
            for(int w=0; w < width; w++)
            {
                Cell aux = Instantiate(cellPrefab,
                              new Vector3(transform.position.x - width / 2 + w, - transform.position.y + height / 2 - h, 0),
                              Quaternion.identity,
                              this.gameObject.transform).GetComponent<Cell>();
                aux.w = w;
                aux.h = h;
                field[w, h] = aux; 
            }
        }


        int n = 0;
        while(n < nMines)
        {
            int randomW = Random.Range(0, width);
            int randomH = Random.Range(0, height);

            if (!field[randomW, randomH].bomb)
            {
                field[randomW, randomH].bomb = true;
                //Debug.Log("W: " + randomW + "H: " + randomH);
                n++;
            }
        }
    }

    public void printField()
    {
        string row = "";
        for(int h=0; h < height; h++)
        {
            //Debug.Log("FILA");
            for(int w=0; w < width; w++)
            {
                row += field[w, h].bomb + " ";
            }
            //Debug.Log(row);
            row = "";
        }
    }

    public void checkAdjacents(int w, int h)
    {
        int bombCount = 0;
        List<Cell> cellsToCheck = new List<Cell>();
        foreach (Vector2 dir in dirs)
        {
            if ((w + dir.x < width) && (h + dir.y < height) && (w + dir.x >= 0) && (h+dir.y >=0))
            {
                Cell aux = field[w + (int)dir.x, h + (int)dir.y];
                //Debug.Log("Checking dir x:" + dir.x + " y:" + dir.y);
                if (aux.bomb)
                {
                    bombCount++;
                }
                else if (!aux.revealed)
                {
                    if (dir.x == 0 || dir.y == 0)
                        cellsToCheck.Add(aux);
                }
            }
        }
        //Debug.Log("en w:" + w + " h:" + h + " hay adyacentes" + bombCount);
        if (!field[w, h].revealed)
        {
            revealedCells++;
            field[w, h].revealCell(bombCount);

            foreach (Cell c in cellsToCheck)
            {
                checkAdjacents(c.w, c.h);
            }
        }
        if (revealedCells >= ((width * height)-nMines)) Debug.Log("GANASTEEEE!!!!111!!!!");
    }

}
