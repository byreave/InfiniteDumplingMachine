using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumplingControl : MonoBehaviour
{
    public float xSpeed = 2.0f;
    bool isDying = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDying)
            transform.Translate(Vector3.left * xSpeed * Time.deltaTime);
    }
    public void Die()
    {
        isDying = true;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<ParticleSystem>().Play();
        Destroy(gameObject, 1.0f);
    }
}
