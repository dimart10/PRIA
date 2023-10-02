using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Cell : MonoBehaviour
{
    public int w = 0;
    public int h = 0;
    public bool bomb = false;
    public bool revealed = false;
    public int adyacents = 0;
    public SpriteRenderer spriteR = null;
    public TextMeshPro text;
    private MineSweep mineSweep = null;

    // Start is called before the first frame update
    void Start()
    {
        mineSweep = GetComponentInParent<MineSweep>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*private void OnMouseDown()
    {
        if (bomb) revealCell(0);
        else if(!revealed) mineSweep.checkAdjacents(w,h);   
    }*/

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown((int)MouseButton.Left))
        {
            if (bomb) revealCell(0);
            else if (!revealed) mineSweep.checkAdjacents(w, h);
        }
        else if (Input.GetMouseButtonDown((int)MouseButton.Right)){
            if (!revealed) spriteR.color = Color.blue;
        }

    }

    public void revealCell(int a)
    {
        adyacents = a;
        revealed = true;
        text.text = a.ToString();
        if (bomb) { 
            spriteR.color = Color.red;
            Debug.Log("PERDISTE BOBOOOOO!");
        }
        else spriteR.color = Color.white;
 
    }
}
