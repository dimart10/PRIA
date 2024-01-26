using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceOvni : MonoBehaviour
{
    public float ovniSpeed = 2;
    public GameObject ovniExplosion;
    public AudioClip ovniSound;
    public float xMovement = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(xMovement, 0, 0) * ovniSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "SBorder")
        {
            Instantiate(ovniExplosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else if(collision.tag == "SPlayerbullet")
        {
            // Suma puntos por destruir el ovni
            Instantiate(ovniExplosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
