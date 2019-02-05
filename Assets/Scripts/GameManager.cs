using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
    public Text DumplingConText;
    public Text DumplingWasText;
    public GameObject Grass;
    public GameObject Pots;
    [SerializeField]
    int level = 0;
    int DumplingConsumed = 0, DumplingWasted = 0, PoopCount = 0;
    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        switch(level)
        {
            case 0:
                if (PoopCount >= 1)
                    LevelUp();
                break;
            case 1:
                if (PoopCount >= 3)
                    LevelUp();
                break;
            case 2:
                break;
            default:
                break;
        }

        if (Input.GetKeyDown(KeyCode.Q))
            Application.Quit();
    }

    public void ConsumeDumpling ()
    {
        DumplingConsumed++;
        DumplingConText.text = "Dumpling Consumed: " + DumplingConsumed.ToString();
    }
    
    public void WasteDumpling()
    {
        DumplingWasted++;
        DumplingWasText.text = "Dumpling Waste: " + DumplingWasted.ToString();
    }

    public void AddPoop()
    {
        PoopCount++;
    }
    void LevelUp()
    {
        GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
        level++;
        Grass.GetComponent<GrassControl>().LevelUp();
        Pots.GetComponent<InfinitePots>().LevelUp();
    }
}
