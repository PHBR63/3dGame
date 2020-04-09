using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    public Rigidbody rdb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (rdb.velocity.magnitude > 10)
        {
            rdb.isKinematic = true;
            transform.parent = other.transform;
        }
    }
}
