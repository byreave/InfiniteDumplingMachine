using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float HorizontalSpeed = 2.0f;
    public float VerticalSpeed = 2.0f;

    float screenWidth, screenHeight;
    float characterWidth, characterHeight;
    // Start is called before the first frame update
    void Start()
    {
        screenWidth = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, 0.0f)).x;
        screenHeight = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, Screen.height, 0.0f)).y;
        characterWidth = gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
        characterHeight = gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
    }

    // Update is called once per frame
    void Update()
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
