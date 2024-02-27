using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceExplosion : MonoBehaviour
{
    public float timeToLive = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timeToLive);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
