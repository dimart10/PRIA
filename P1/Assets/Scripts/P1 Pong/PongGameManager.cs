using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PongGameManager : MonoBehaviour
{
    private int puntosJ1 = 0;
    private int puntosJ2 = 0;

    public TextMeshPro marcadorJ1;
    public TextMeshPro marcadorJ2;
    public TextMeshPro textoVictoria;

    private PongPlayer J1;
    private PongAI J2;
    private PongBall ball;

    private bool rondaAcabada = true;
    public int golesMaximos = 5;

    // Start is called before the first frame update
    void Start()
    {
        J1 = FindObjectOfType<PongPlayer>();    // Buscamos referencia a J1
        J2 = FindObjectOfType<PongAI>();        // Buscamos referencia a J2
        ball = FindObjectOfType<PongBall>();    // Buscamos referencia a la pelota
    }

    // Update is called once per frame
    void Update()
    {
        if(rondaAcabada && Input.GetKeyDown(KeyCode.Space)) ResetRonda();
    }

    public void ResetRonda()
    {
        // Reset de la pelota
        ball.ResetBall();
        // Reset de J1
        J1.InicioRonda();
        // Reset de J2
        J2.ResetIA();
        // Ronda vuelve a empezar
        rondaAcabada = false;
        // Actualizar marcadores
        marcadorJ1.text = puntosJ1.ToString();
        marcadorJ2.text = puntosJ2.ToString();
        // Desactivo el textoVictoria
        textoVictoria.gameObject.SetActive(false);
    }

    public void GolJugador1()
    {
        puntosJ1++; // Anota un punto
        // Actualizar el texto del marcador
        marcadorJ1.text = puntosJ1.ToString();
        rondaAcabada = true;
        // Reset de J1
        J1.ResetJugador();
        // Reset de J2
        J2.ResetIA();
        ComprobarVictoria();
    }

    public void GolJugador2()
    {
        puntosJ2++; // Anota un punto
        // Actualizar el texto del marcador
        marcadorJ2.text = puntosJ2.ToString();
        rondaAcabada = true;
        // Reset de J1
        J1.ResetJugador();
        // Reset de J2
        J2.ResetIA();
        ComprobarVictoria();
    }

    private void ComprobarVictoria() // Vemos si alguien ha ganado
    {
        // Comprobar fin de partido (goles maximos)
        if (puntosJ1 >= golesMaximos)
        {
            Debug.Log("Gana J1");
            // Activo el objeto textoVictoria
            textoVictoria.gameObject.SetActive(true);
            // Cambio su texto a "¡Gana J1!"
            textoVictoria.text = "¡Gana J1!";
            puntosJ1 = puntosJ2 = 0;
        }
        else if (puntosJ2 >= golesMaximos)
        {
            Debug.Log("Gana J2");
            // Activo el objeto textoVictoria
            textoVictoria.gameObject.SetActive(true);
            // Cambio su texto a "¡Gana CPU!"
            textoVictoria.text = "¡Gana la CPU!";
            puntosJ1 = puntosJ2 = 0;
        }
    }
}
