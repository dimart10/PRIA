using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceInvaderBullet : MonoBehaviour
{

    public float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(0, speed, 0) * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger enter con " + collision.gameObject.name);
        if (collision.tag == "Player")
        {
            Debug.Log("JUGADOR GOLPEADO");
            // Restar vida, actualizar sprites
            Destroy(gameObject);
        }
        else if (collision.tag == "SBarrier")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if(collision.tag == "SBorder")
        {
            Destroy(gameObject);
        }
    }
}
