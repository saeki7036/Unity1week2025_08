using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SR_ScoreManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Animator ScoreAnimator;
    [SerializeField] TextMeshProUGUI ScoreText;
    [SerializeField] AudioClip ScoreGetClip;

    SR_AudioManager audioManager => SR_AudioManager.instance;

    public float AllScore =0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void isAddScore(float AddScore) 
    {

        AllScore += AddScore;
        ScoreText.text = AllScore.ToString();
        ScoreAnimator.Play("加算", 0, 0);

        audioManager.isPlaySE(ScoreGetClip);
    }
}
