using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongAI : MonoBehaviour
{
    private Rigidbody2D rbAI;
    public Transform pelota;
    public float velocidadAI;

    // Start is called before the first frame update
    void Start()
    {
        rbAI = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        // pelota.position.y <- Coordenada y de la pelota
        // transform.position.y <- Coordenada y de la pala IA

        float distancia = pelota.position.y - transform.position.y;
        distancia = Mathf.Abs(distancia);

        if (distancia > 0.5f && pelota.position.x > 0)
        {
            // Si la pelota está arriba
            if (pelota.position.y > transform.position.y)
            {
                rbAI.velocity = new Vector2(0, velocidadAI); // Ir hacia arriba
            }
            // Si la pelota está abajo
            else if (pelota.position.y < transform.position.y)
            {
                rbAI.velocity = new Vector2(0, -velocidadAI); // Ir hacia abajo
            }
        }
        else rbAI.velocity = Vector2.zero;

    }

    public void ResetIA() // Reinicio la posicion
    {
        Vector3 aux = transform.position;
        aux.y = 0;
        transform.position = aux;
    }
}
