using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinitePots : MonoBehaviour
{
    public GameObject Dumpling;
    [SerializeField]
    float BaseSpawnTime = 2.0f;
    [SerializeField]
    float BaseSpawnProb = 0.15f;
    int level = 1;
    Transform[] DumplingSpawn;
    float timer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        DumplingSpawn = GetComponentsInChildren<Transform>();
        //Debug.Log(DumplingSpawn.Length);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= BaseSpawnTime / level)
        {
            SpawnDumplings();
            timer = 0.0f;
        }
        
    }

    public void LevelUp()
    {
        level++;
    }

    void SpawnDumplings()
    {
        foreach(Transform t in DumplingSpawn)
        {
            if (t != gameObject.transform && Random.Range(0, 1.0f) <= BaseSpawnProb * level)
            {
                Instantiate(Dumpling, t.position, t.rotation);
            }
        }
    }
}
