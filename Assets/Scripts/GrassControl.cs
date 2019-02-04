using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassControl : MonoBehaviour
{
    [SerializeField]
    GameObject[] Levels;
    int currentLevel = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelUp()
    {
        Levels[currentLevel++].SetActive(false);
        Levels[currentLevel].SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Dumpling"))
        {
            collision.gameObject.GetComponent<DumplingControl>().Die();
            GameManager.instance.WasteDumpling();
        }
    }
}
