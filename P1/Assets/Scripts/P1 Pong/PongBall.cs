using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongBall : MonoBehaviour
{
    public enum Ball_Type {FIJA, LINEAL, EXPONENCIAL};
    public Ball_Type tipoBola = Ball_Type.FIJA; 

    public float velocidadPelota = 5f;
    private float velocidadInicial = 5f;
    private Rigidbody2D rbPelota;
    public PongGameManager gm;

    public float incrVelLineal = 1f;
    public float multiplicadorVel = 1.2f; 

    // Start is called before the first frame update
    void Start()
    {
        rbPelota = GetComponent<Rigidbody2D>();
        // Guardamos velocidad inicial para poder resetear
        velocidadInicial = velocidadPelota;
    }

    void BallLaunch()
    {
        // Generar dirección aleatoria (X aleatoria e Y aleatoria)
        float dirX = Random.Range(-1f, 1f);
        float dirY = Random.Range(-1f, 1f);
        // Corregimos la X
        if (dirX > 0 && dirX < 0.3f) dirX = 0.3f;
        if (dirX < 0 && dirX > -0.3f) dirX = -0.3f;

        // Creamos dirección y la normalizamos
        Vector2 direccionRandom = new Vector2(dirX, dirY);
        direccionRandom = direccionRandom.normalized;

        rbPelota.velocity = direccionRandom * velocidadPelota;
    }

    public void ResetBall()
    {
        transform.position = Vector3.zero;
        velocidadPelota = velocidadInicial;
        BallLaunch();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Podemos comprobar con qué objeto hemos chocado con su nombre,
        // tag, layer, componentes

        // Si el trigger es la portería de J2
        if (collision.gameObject.name == "Porteria2") {
            // J1 marca un gol -> Llamar a GolJ1 del GameManager
            gm.GolJugador1();
            transform.position = Vector3.zero;  // Devolvemos la pelota al centro
            rbPelota.velocity = Vector2.zero;   // Paramos la pelota
        }
        // Si el trigger es la portería de J1
        else if (collision.gameObject.name == "Porteria1") {
            // J2 marca un gol -> Llamar a GolJ2 del GameManager
            gm.GolJugador2();
            transform.position = Vector3.zero;  // Devolvemos la pelota al centro
            rbPelota.velocity = Vector2.zero;   // Paramos la pelota
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ACELERA EXPONENCIAL
        if(tipoBola == Ball_Type.EXPONENCIAL)
            AcelerarPelotaExponencial();

        // ACELERA LINEAL
        else if(tipoBola == Ball_Type.LINEAL)
            AcelerarPelotaLineal();
     
        // NO ACELERA
    }

    private void AcelerarPelotaLineal()
    {
        velocidadPelota += incrVelLineal;
        rbPelota.velocity = rbPelota.velocity.normalized * velocidadPelota;
    }

    private void AcelerarPelotaExponencial()
    {
        rbPelota.velocity *= multiplicadorVel;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
