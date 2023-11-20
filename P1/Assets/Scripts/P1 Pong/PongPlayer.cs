using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongPlayer : MonoBehaviour
{
    public float velocidadPaleta = 3f;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        InputJugador();
    }

    private void InputJugador()
    {
        // Si pulso w
        if(Input.GetKey(KeyCode.W))
        {
            rb.velocity = new Vector2(0, velocidadPaleta);
        }
        // si pulso s
        else if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = new Vector2(0, -velocidadPaleta);
        }
        // Si no pulso nada
        else rb.velocity = Vector2.zero;
    }
}
