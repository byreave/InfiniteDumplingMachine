using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopControl : MonoBehaviour
{
    float PoopingTime = 2.0f;
    float MovingSpeed = 0.5f;
    bool isPooping = true;
    float PoopingTimer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isPooping)
        {
            PoopingTimer += Time.deltaTime;
            transform.Translate(Vector3.down * Time.deltaTime * MovingSpeed);
            if(PoopingTimer >= PoopingTime)
            {
                isPooping = false;
                GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Grass"))
        {
            GameManager.instance.AddPoop();
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Destroy(gameObject, 1.0f);
        }
        else if (collision.CompareTag("Dumpling"))
        {
            GameManager.instance.WasteDumpling();
            collision.gameObject.GetComponent<DumplingControl>().Die();
        }
    }
}
