using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinvaderMovement : MonoBehaviour
{
    public float speed = 3f; // velocidad de movimiento en eje x
    public float despAbajo = 1f; // distancia que baja al cambiar de dirección
    private int dir = 1; // +1 para la derecha, -1 para la izquierda

    public bool canSwitch = true; // Bool que indica si puede girarse
    public float switchDelay = 0.5f; // Tiempo que debe pasar despues de girar, para poder volver a hacerlo

    /*
     * 1 - Después de girar, pongo canSwitch a false
     * 2 - Crear una función que ponga canSwitch a true
     * 3 - A la vez que pongo canSwitch a false, hago Invoke del método que cree antes, con tiempo switchDelay
     * 4 - En switchDir, el codigo sólo se ejecuta si canSwitch es true, es decir, if (canSwitch==true)
     */

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        transform.position += new Vector3(speed, 0, 0) * dir * Time.deltaTime;
    }

    public void SwitchDirection() // Invierte la dirección y lo mueve hacia abajo
    {
        if (canSwitch) // Solo gira si canSwitch == true
        {
            dir *= -1; // Invierto la dirección (1 y -1)
            transform.position += new Vector3(0, -despAbajo, 0); // Desplazarme hacia abajo

            canSwitch = false; // Desactivo el giro
            Invoke("EnableSwitch", switchDelay); // Reactivo el giro en switchDelay segundos
        }
    }

    public void EnableSwitch() // Pone canSwitch a true
    {
        canSwitch = true;
    }
}
