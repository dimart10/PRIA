using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SExplosion : MonoBehaviour
{
    public float lifeTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }
}
