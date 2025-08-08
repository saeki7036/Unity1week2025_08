using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeePiccer : MonoBehaviour
{
    [SerializeField]
    float PicRadius = 2.5f;

    [SerializeField]
    float OpenBagMoveValue = 3f;

    [SerializeField]
    float RemovePower = 20f;

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

            picVelocity = rb2D.velocity;

            rb2D.velocity = Vector2.zero;
            MoveValueCounter = 0;
        }
    }

    void DragCoffeeBag(Vector3 DragPos)
    {
        if (picTransform == null) return;

        Vector2 WorldPos2D = GetWorldPoin2D(DragPos);

        MoveValueCounter += Vector2.Distance(picTransform.position, WorldPos2D);

        if(OpenBagMoveValue > MoveValueCounter)
        {
            picTransform.position = WorldPos2D;
        }
        else
        {
            Vector2 RemoveVelocity = new Vector2()
            {
                x = WorldPos2D.x - picTransform.position.x,
                y = 1
            };

            rb2D.AddForce(RemoveVelocity.normalized * RemovePower, ForceMode2D.Impulse);

            picCoffeeBag.OpenBag();

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
