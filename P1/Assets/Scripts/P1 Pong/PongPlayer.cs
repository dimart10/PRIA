using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongPlayer : MonoBehaviour
{
    public float velocidadPaleta = 3f;
    private Rigidbody2D rb;
    public bool jugadorActivo = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (jugadorActivo)
        {
            InputJugador();
        }
    }

    public void ResetJugador() // Reinicio la posición
    {
        Vector3 aux = transform.position;
        aux.y = 0;
        transform.position = aux;
        jugadorActivo = false;
        rb.velocity = Vector3.zero;
    }
    public void InicioRonda() // Reinicio la posición
    { 
        jugadorActivo = true;
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
