using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SR_CoffeeController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] SR_ScoreManager scoreManager;
    [SerializeField] SR_System _System;
    [SerializeField] Slider CoffeeSlider;
    [SerializeField] GameObject Canvas;
    
    [SerializeField] GameObject CoffeeBeansObject;

    [SerializeField] Transform BeenTarget;
    [SerializeField] GameObject CoffeeMountainBody;
    [SerializeField] float CoffeeMountainBody_Double =0.1f;
    SR_AudioManager audioManager => SR_AudioManager.instance;
    [SerializeField] AudioClip CoffeePop;
    [Space]
    [SerializeField] int MakeCoffee_Decrease_GetCoffeeBeans_namber = 5;
    [SerializeField] float MakeCoffee_Increase_CoffeeWater = 10;
    [SerializeField] AudioClip AddCoffeeClip;
    [SerializeField] AudioClip AddCoffeeClip1;
    [SerializeField] AudioClip NoCoffeeBeanseClip;
    [SerializeField] AudioClip LevarClip;
    [SerializeField] GameObject CoffeeBeanse_TARINAI_TEXT;
    [SerializeField] Vector2 TARINAI_SpawnPoint = Vector2.zero;
    [SerializeField] float LeverTimer = 0.1f;
    float LeverCount = 0;
    [Space]
    public int GetCoffeeBeans_namber=0;
    public float CoffeeWater = 0;
    public float MaxCoffeeWater = 100;

   

    //public float DecreaseCoffee = 1;//一秒の減少量

    void Start()
    {
        CoffeeSlider.maxValue = MaxCoffeeWater;
        CoffeeSlider.minValue = 0;
    }

    public void MakeCoffeeWater() 
    {
        if (GetCoffeeBeans_namber >= MakeCoffee_Decrease_GetCoffeeBeans_namber)
        {
            scoreManager.isAddPoint_Lever();

            audioManager.isPlaySE(AddCoffeeClip);
            audioManager.isPlaySE(AddCoffeeClip1);
            GetCoffeeBeans_namber -= MakeCoffee_Decrease_GetCoffeeBeans_namber;
            CoffeeWater += MakeCoffee_Increase_CoffeeWater;
        }
        else 
        {
            audioManager.isPlaySE(NoCoffeeBeanseClip);

            GameObject CL_CoffeeBeansTARINAI = Instantiate(CoffeeBeanse_TARINAI_TEXT);
            CL_CoffeeBeansTARINAI.transform.parent = Canvas.transform;
            RectTransform rectTransform = CL_CoffeeBeansTARINAI.GetComponent<RectTransform>();
            rectTransform.position = TARINAI_SpawnPoint;
            Destroy(CL_CoffeeBeansTARINAI, 1);
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        CoffeeSlider.value = CoffeeWater;
        CoffeeMountainBody.transform.localScale = new Vector3(1,GetCoffeeBeans_namber * CoffeeMountainBody_Double,1);
        if (_System.gameMode == SR_System.GameMode.MainGame) 
        {
             isDecreaseCoffee(_System.DecreaseCoffee);
        }
        if (LeverTimer < LeverCount) 
        {
            LeverCount = 0;
            audioManager.isPlaySE(LevarClip);
        }
    }
    public void LeverClipPlay() 
    {
        LeverCount += Time.deltaTime;
    }
    public void SpawnCoffeeBeans(Transform SpawnPointObject) //コーヒー豆を召喚する処理。指定したオブジェクトから発生する
    {
        GameObject CL_CoffeeBeans =  Instantiate(CoffeeBeansObject, SpawnPointObject.transform.position, Quaternion.identity);
        SR_CoffeeBeans sR_CoffeeBeans = CL_CoffeeBeans.GetComponent<SR_CoffeeBeans>();

        audioManager.isPlaySE(CoffeePop);

        sR_CoffeeBeans._CoffeeController = this;

        if(BeenTarget != null)

            sR_CoffeeBeans.SetTarget(BeenTarget);
    }

    public void isDecreaseCoffee(float DecreaseCoffee) 
    {
        CoffeeWater -= DecreaseCoffee * Time.deltaTime;
        if (CoffeeWater < 0) 
        {
            _System.isGameOver();
        }
    }
    public void isAddCoffee(float AddCoffee)
    {
        CoffeeWater += AddCoffee;
        if (CoffeeWater < 0)
        {
            _System.isGameOver();
        }
    }

}
