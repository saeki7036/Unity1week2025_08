using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SR_ScoreManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Animator ScoreAnimator;
    [SerializeField] TextMeshProUGUI ScoreText;
    [SerializeField] TextMeshProUGUI MileageText;
    [SerializeField] TextMeshProUGUI CoffeeBounusText;
    [SerializeField] GameObject GetCcoreText;
    [SerializeField] Vector2 SpawnPoint_GetCcoreText = new Vector2(140,220);
    [SerializeField] GameObject Canvas;

    [SerializeField] AudioClip ScoreGetClip;

    [SerializeField] TextMeshProUGUI ScoreBoard_Length_point;
    [SerializeField] TextMeshProUGUI ScoreBoard_Lever_Point;
    [SerializeField] TextMeshProUGUI ScoreBoard_All_Point;
    [Space]
    public float LeverPoint =0;
    [SerializeField] float AddScore_CiffeeLever = 10;
    [SerializeField] float Double_AddScore_CiffeeLever = 0.4f;
    [SerializeField] float Count_Double_AddScore_CiffeeLever = 0;
    [SerializeField] float LeverBonus_Limit = 3;
    [SerializeField] float LeverBonus_Count = 0;


    public float Length_point =0;




    SR_AudioManager audioManager => SR_AudioManager.instance;

    public float AllScore =0;
    public float SaveScore = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeScoreBoard() 
    {

       ScoreBoard_Length_point.text = Length_point.ToString("F0");
       ScoreBoard_Lever_Point.text = LeverPoint.ToString("F0");
       ScoreBoard_All_Point.text = AllScore.ToString("F0");

    }

    private void FixedUpdate()
    {
        if (Count_Double_AddScore_CiffeeLever > 0)
        {
            LeverBonus_Count += Time.deltaTime;
            if (LeverBonus_Limit < LeverBonus_Count)
            {
                LeverBonus_Count = 0;
                Count_Double_AddScore_CiffeeLever = 0;
            }
        }
    }

    public void ResetScore() 
    {

        LeverBonus_Count = 0;
        Count_Double_AddScore_CiffeeLever = 0;
        AllScore = 0;
        Length_point = 0;
        LeverPoint = 0;

}
    public void isAddPoint_Lever() 
    {

        GameObject CL_GetScoreText = Instantiate(GetCcoreText);
        CL_GetScoreText.transform.parent = Canvas.transform;
        RectTransform rectTransform = CL_GetScoreText.GetComponent<RectTransform>();
        rectTransform.position = SpawnPoint_GetCcoreText;
        Destroy(CL_GetScoreText, 1);

        SR_LeverDouble sR_LeverDouble = CL_GetScoreText.GetComponent<SR_LeverDouble>();
        sR_LeverDouble.DoubleText.text = (1 + Count_Double_AddScore_CiffeeLever).ToString("F1");
        sR_LeverDouble.GetScore.text = (AddScore_CiffeeLever * (1 + Count_Double_AddScore_CiffeeLever)).ToString("F0");

        LeverPoint += AddScore_CiffeeLever * (1 + Count_Double_AddScore_CiffeeLever);
        isAddScore((AddScore_CiffeeLever * (1 + Count_Double_AddScore_CiffeeLever)));

        Count_Double_AddScore_CiffeeLever += Double_AddScore_CiffeeLever;


        LeverBonus_Count = 0;

        
    }

    public void isAddScore(float AddScore) 
    {
        //スコアの初期化はSR_SystemのisAfterでしている。
        AllScore += AddScore;
        ScoreText.text = AllScore.ToString();
        ScoreAnimator.Play("加算", 0, 0);

        audioManager.isPlaySE(ScoreGetClip);
    }
    public void isClearScoreText()
    {
        
        ScoreText.text = AllScore.ToString();
        ScoreAnimator.Play("加算", 0, 0);

    }
}
