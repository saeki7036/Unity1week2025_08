using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SR_CoffeeController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] SR_System _System;
    [SerializeField] Slider CoffeeSlider;

    [SerializeField] GameObject CoffeeBeansObject;

    SR_AudioManager audioManager => SR_AudioManager.instance;
    [SerializeField] AudioClip CoffeePop;

    public int GetCoffeeBeans_namber=0;
    public float CoffeeWater = 0;
    public float MaxCoffeeWater = 100;

    //public float DecreaseCoffee = 1;//一秒の減少量

    void Start()
    {
        CoffeeSlider.maxValue = MaxCoffeeWater;
        CoffeeSlider.minValue = 0;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        CoffeeSlider.value = CoffeeWater;
        if (_System.gameMode == SR_System.GameMode.MainGame) 
        {
             isDecreaseCoffee(_System.DecreaseCoffee);
        }
    }
    public void SpawnCoffeeBeans(GameObject SpawnPointObject) //コーヒー豆を召喚する処理。指定したオブジェクトから発生する
    {
        GameObject CL_CoffeeBeans =  Instantiate(CoffeeBeansObject, SpawnPointObject.transform.position, Quaternion.identity);
        SR_CoffeeBeans sR_CoffeeBeans = CL_CoffeeBeans.GetComponent<SR_CoffeeBeans>();

        audioManager.isPlaySE(CoffeePop);

        sR_CoffeeBeans._CoffeeController = this;
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
