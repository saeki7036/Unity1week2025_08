using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMove : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer spriteRenderer;

    [SerializeField]
    float MoveSpeed_X = 0.002f;

    [SerializeField]
    float LimitPos_X = 30f;

    [SerializeField]
    float TransPos_X = 20f;


    [SerializeField]
    float MoveSpeed_Y = 0.002f;

    [SerializeField]
    float TransPos_Y = 2f;


    [SerializeField]
    float ScaleAdd_Y = 0.002f;

    [SerializeField]
    float ScaleMax_Y = 6f;

    [SerializeField]
    float TransScale_Y = 0f;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += new Vector3(MoveSpeed_X, MoveSpeed_Y, 0);

        float scale = Mathf.Min(transform.localScale.y + ScaleAdd_Y, ScaleMax_Y);

        transform.localScale = new Vector3(scale, scale, 1);

        if(Mathf.Abs(LimitPos_X) <= Mathf.Abs(transform.position.x))
        {
            transform.position = new Vector3(TransPos_X, TransPos_Y, transform.position.z);

            transform.localScale = new Vector3(1, TransScale_Y, 1);
        }

        spriteRenderer.sortingOrder = (int)transform.localScale.x;
    }
}
