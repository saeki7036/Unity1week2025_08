using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_TEST : MonoBehaviour
{

    SR_AudioManager audioManager => SR_AudioManager.instance;
    SR_System system => SR_System.instance;
    [SerializeField] AudioClip Clip;
    [SerializeField] SR_ScoreManager scoreManager;
    [SerializeField] SR_CoffeeController _CoffeeController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) 
        {

            //system.NINGEN_KOROSU();
            _CoffeeController.LeverClipPlay();
        
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) 
        {
            system.gameMode = SR_System.GameMode.After;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            system.gameMode = SR_System.GameMode.GameStart;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            system.TutorialNext();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // _CoffeeController.MakeCoffeeWater();
            scoreManager.isAddPoint_Lever();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            scoreManager.isAddScore(10);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            _CoffeeController.SpawnCoffeeBeans(transform);
        }
    }
}
