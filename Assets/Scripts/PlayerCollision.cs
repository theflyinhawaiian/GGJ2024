using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    GameObject playertransform;


    // Start is called before the first frame update
    void Start()
    {
        playertransform = GetComponent<Transform>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Word")
        {
            
        }
    }
}
