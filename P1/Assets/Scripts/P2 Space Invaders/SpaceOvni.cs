using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceOvni : MonoBehaviour
{
    public float ovniSpeed = 2;
    public GameObject ovniExplosion;
    public AudioClip ovniSound;
    public float xMovement = 1f;
    public int puntosOVNI = 50;

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.instance.PlayMusic(ovniSound);
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
            OVNIDeath();
        }
        else if(collision.tag == "SPlayerBullet")
        {
            // Suma puntos por destruir el ovni
            SpaceGameManager.instance.AddScore(puntosOVNI);
            Destroy(collision.gameObject);
            Instantiate(ovniExplosion, transform.position, Quaternion.identity);
            OVNIDeath();
        }
    }

    private void OVNIDeath()
    {
        SoundManager.instance.StopMusic();
        Destroy(this.gameObject);
    }
}
