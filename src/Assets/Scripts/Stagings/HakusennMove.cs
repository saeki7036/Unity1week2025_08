using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HakusennMove : MonoBehaviour
{
    [SerializeField]
    float ResetPos_Y = 1f;

    [SerializeField]
    float StartScaleUp_Y = -3f;

    [SerializeField]
    Vector2 ScaleUpMax = new(1,1);

    [SerializeField]
    Vector2 ScaleAddValue = new(0.01f, 0.05f);

    [SerializeField]
    float TransPos_Y = -5f;

    [SerializeField]
    Vector3 TransScale = new Vector3(0.8f, 0, 1);

    [SerializeField]
    float MoveSpeed = 0.1f;

    Vector3 ResetPos;

    private void Start()
    {
        ResetPos = new Vector3(transform.position.x,ResetPos_Y, transform.position.z);
    }

    void FixedUpdate()
    {
        Vector3 pos = transform.position;

        pos.y += -1 * GetMoveValue(pos.y);

        if (pos.y < TransPos_Y)
        {
            pos = ResetPos;

            transform.localScale = TransScale;
        }

        if(pos.y < StartScaleUp_Y)
        {
            Vector3 scale = transform.localScale;

            scale.x = Mathf.Min(scale.x + ScaleAddValue.x, ScaleUpMax.x);
            scale.y = Mathf.Min(scale.y + ScaleAddValue.y, ScaleUpMax.y);

            if(scale.y != ScaleUpMax.y)
                pos.y += -1 * ScaleAddValue.y;

            transform.localScale = scale;
        }

        transform.position = pos;
    }

    float GetMoveValue(float crrentPos_Y)
    {
        float clamp01 = Mathf.Clamp(TransPos_Y - crrentPos_Y, ResetPos_Y, TransPos_Y);

        return MoveSpeed;
    }
}
