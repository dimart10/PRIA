using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongBall : MonoBehaviour
{
    public float velocidadPelota = 5f;
    private Rigidbody2D rbPelota;

    // Start is called before the first frame update
    void Start()
    {
        rbPelota = GetComponent<Rigidbody2D>();
        BallLaunch();
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

    void ResetBall()
    {
        transform.position = Vector3.zero;
        BallLaunch();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) ResetBall(); // DEBUG eliminar después
    }
}
