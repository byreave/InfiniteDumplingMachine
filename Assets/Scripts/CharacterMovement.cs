using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float HorizontalSpeed = 2.0f;
    public float VerticalSpeed = 2.0f;
    public GameObject PoopObject;
    float screenWidth, screenHeight;
    float characterWidth, characterHeight;
    int MaxPoopCount = 10;
    int PoopLevel = 0;
    bool isPooping = false;
    Color color;
    // Start is called before the first frame update
    void Start()
    {
        screenWidth = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, 0.0f)).x;
        screenHeight = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, Screen.height, 0.0f)).y;
        characterWidth = gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
        characterHeight = gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
        color = GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isPooping)
        {
            transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * HorizontalSpeed * Time.deltaTime);
            transform.Translate(Vector3.up * Input.GetAxis("Vertical") * VerticalSpeed * Time.deltaTime);
            Vector3 playerPos = transform.position;
            if (playerPos.x <= -screenWidth + characterWidth / 2.0f)
                playerPos.x = -screenWidth + characterWidth / 2.0f;
            if (playerPos.x >= screenWidth - characterWidth / 2.0f)
                playerPos.x = screenWidth - characterWidth / 2.0f;
            if (playerPos.y <= -screenHeight + characterHeight / 2.0f)
                playerPos.y = -screenHeight + characterHeight / 2.0f;
            if (playerPos.y >= screenHeight - characterHeight / 2.0f)
                playerPos.y = screenHeight - characterHeight / 2.0f;
            transform.position = playerPos;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dumpling"))
        {
            collision.gameObject.GetComponent<DumplingControl>().Die();
            GameManager.instance.ConsumeDumpling();
            PoopUp();
        }
    }
    void PoopUp()
    {
        PoopLevel++;
        GetComponent<SpriteRenderer>().color = new Color(color.r, (1.0f - PoopLevel / 10.0f) * color.g, (1.0f - PoopLevel / 10.0f) * color.b);
        if(PoopLevel >= MaxPoopCount)
        {
            PoopLevel = 0;
            StartPooping();
        }
    }

    void StartPooping()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        Invoke("Poop", 1.0f);
    }

    void Poop()
    {
        Debug.Log("POPOP");
        isPooping = true;
        GetComponent<Animator>().SetBool("Pooping", true);
        GetComponent<BoxCollider2D>().enabled = false;
        Instantiate(PoopObject, gameObject.transform.position, gameObject.transform.rotation);
        Invoke("StopPooping", 2.0f);
        StartCoroutine(FinishPoop(2.0f));
    }
    void StopPooping()
    {
        isPooping = false;
        GetComponent<Animator>().SetBool("Pooping", false);
        gameObject.transform.localScale += new Vector3(0.1f, 0.1f, 0.0f);
        GetComponent<BoxCollider2D>().enabled = true;
    }

    IEnumerator FinishPoop(float time)
    {
        while(GetComponent<SpriteRenderer>().color.g < 1.0f)
        {
            Color currentColor = GetComponent<SpriteRenderer>().color;
            GetComponent<SpriteRenderer>().color = new Color(currentColor.r, currentColor.g + Time.deltaTime / time, currentColor.b + Time.deltaTime / time);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
