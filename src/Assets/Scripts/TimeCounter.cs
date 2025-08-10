using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class TimeCounter : MonoBehaviour
{
    [SerializeField]
    SR_ScoreManager sr_ScoreManager;

    [SerializeField]
    float AddValueParFlame = 0.02f;

    SR_System system => SR_System.instance;

    float timeCounter;

    public float GetTravelCount() => timeCounter;

    void Start()
    {
        timeCounter = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       if(system.IsMainGamePlay())
        {
            timeCounter += AddValueParFlame;
            sr_ScoreManager.Length_point = timeCounter;
        }
        else
        {
            if(timeCounter != 0)
            {
                timeCounter = 0;
            }
        }
    }
}
