using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CoffeePiccer : MonoBehaviour
{
    [SerializeField]
    Transform OpendBagParent;

    [SerializeField]
    float PicRadius = 2.5f;

    [SerializeField]
    float OpenBagMoveValue = 3f;

    [SerializeField]
    float RemovePower = 20f;

    [SerializeField]
    SR_CoffeeController sr_CoffeeController;

    [SerializeField]
    int beenSpwanValue = 12;

    float MoveValueCounter = 0;

    Vector2 picVelocity;
    Transform picTransform;
    Rigidbody2D rb2D;

    CoffeeBag picCoffeeBag;

    public void InputRegister(InputManager input)
    {
        input.LeftDownEvent += PicCoffeeBag;
        input.LeftDlagEvent += DragCoffeeBag;
        input.LeftUpEvent += ReleaseCoffeeBag;
    }

    /// <summary>
    /// UI座標をワールド座標に変換する
    /// </summary>
    Vector2 GetWorldPoin2D(Vector3 UIPos)
    {
        return Camera.main.ScreenToWorldPoint(UIPos);
    }


    void PicCoffeeBag(Vector3 DownPos)
    {
        Vector2 WorldPos2D = GetWorldPoin2D(DownPos);

        picTransform = CoffeeBagGeneretor.Instance.GetInSideTransform(WorldPos2D, PicRadius);

        if(picTransform != null) 
        {
            picCoffeeBag = picTransform.GetComponent<CoffeeBag>();

            rb2D = picCoffeeBag.GetRB2D();

            picTransform.position = picTransform.position + Vector3.left;

            picVelocity = rb2D.velocity;

            rb2D.velocity = Vector2.zero;
            MoveValueCounter = 0;
        }
    }

    void DragCoffeeBag(Vector3 DragPos)
    {
        if (picTransform == null) return;

        Vector2 WorldPos2D = GetWorldPoin2D(DragPos) + Vector2.left;

        MoveValueCounter += Vector2.Distance(picTransform.position, WorldPos2D);

        if(OpenBagMoveValue > MoveValueCounter)
        {
            picTransform.position = WorldPos2D;
        }
        else
        {
            picTransform.parent = OpendBagParent;

            Vector2 RemoveVelocity = new Vector2()
            {
                x = WorldPos2D.x - picTransform.position.x,
                y = 1
            };

            rb2D.AddForce(RemoveVelocity.normalized * RemovePower, ForceMode2D.Impulse);

            picCoffeeBag.OpenBag();

            for (int i = 0; i < beenSpwanValue; i++)
            {
                sr_CoffeeController.SpawnCoffeeBeans(picTransform);
            }
          
            ResetPic();
        }
    }

    void ReleaseCoffeeBag(Vector3 UpPos)
    {
        if (picTransform == null) return;

        rb2D.velocity = picVelocity;

        ResetPic();
    }

    void ResetPic()
    {
        picTransform = null;
        picCoffeeBag = null;
        rb2D = null;

        MoveValueCounter = 0;
        picVelocity = Vector2.zero;
    }
}
