using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SPlayer : MonoBehaviour
{

    [Tooltip("Prefab de la bala")]
    public GameObject prefabBullet;

    [Tooltip("Velocidad del jugador en unidades de unity / segundo")]
    public float speed = 2; // Velocidad del jugador

    // Teclas para input configurable
    public KeyCode shootKey = KeyCode.Space;
    public KeyCode moveLeftKey = KeyCode.A;
    public KeyCode moveRightKey = KeyCode.D;

    public Transform posDisparo;

    public bool canShoot = true;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canShoot && Input.GetKeyDown(shootKey))
        {
            // DISPARA
            Shoot();
        }
        else if(Input.GetKey(moveLeftKey))
        {
            // Voy a la izquierda
            transform.position += new Vector3(-speed, 0, 0) * Time.deltaTime;
        }
        else if (Input.GetKey(moveRightKey))
        {
            // Voy a la derecha
            transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
        }
    }

    private void Shoot()
    {
        Debug.Log("Disparo");
        GameObject aux = Instantiate(prefabBullet, posDisparo.position, Quaternion.identity);
        SPlayerBullet bullet = aux.GetComponent<SPlayerBullet>();
        bullet.player = this;
        canShoot = false;
    }
}
