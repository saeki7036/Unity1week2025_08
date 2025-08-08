using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_CoffeeBeans : MonoBehaviour
{
    [SerializeField] GameObject Effect;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float MinVerocity = 2;
    [SerializeField] float MaxVerocity = 6;

    [SerializeField] float MinRotateVerocity = 2;
    [SerializeField] float MaxRotateVerocity = 6;

    [SerializeField] float Speed = 15;

    [SerializeField] int Phase = 0;

    [SerializeField] Vector2 TarGetPos = Vector2.zero;

    SR_AudioManager audioManager => SR_AudioManager.instance;
    [SerializeField] AudioClip CoffeeGet;
    public SR_CoffeeController _CoffeeController;

    float Count = 0;

    Transform targetTransform;

    public void  SetTarget(Transform tf) => targetTransform = tf;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    
    private void FixedUpdate()
    {
        Count += Time.deltaTime;
        if (Phase == 0) 
        {
            SpawnCoffee();
            NEXT();
        }
        if (Phase == 1) 
        {
            if (Count >= 0.2) 
            {
                NEXT();
            }
        }
        if (Phase == 2) 
        {
            rb.velocity = rb.velocity * 0.95f;
            if (Count >= 1) 
            {
                NEXT();
            }
        }
        if (Phase == 3) 
        {
            Vector2 target = targetTransform == null ? TarGetPos : targetTransform.position;

            Vector2 Direction = target - (Vector2)transform.position;

            float Distance = Vector2.Distance(target, (Vector2)transform.position);
            rb.velocity = Direction.normalized * Speed;

            if (Distance < 1) 
            {
                NEXT();
            }
        }
        if (Phase == 4) 
        {
            audioManager.isPlaySE(CoffeeGet);
            _CoffeeController.GetCoffeeBeans_namber++;
            GameObject CL_Effect = Instantiate(Effect, transform.position, Quaternion.identity);
            Destroy(CL_Effect, 0.3f);

            Destroy(gameObject);
        }
    }
    void SpawnCoffee() 
    { 
    float angle = Random.Range(0f, 360f); // 0～360度
            Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
            rb.velocity = direction * Random.Range(MinVerocity, MaxVerocity);
            rb.AddTorque(Random.Range(MinRotateVerocity,MaxRotateVerocity));
    }
    void NEXT() 
    {

        Phase++;
        Count = 0;

    }

}
