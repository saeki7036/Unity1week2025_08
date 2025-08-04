using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HakusennMove : MonoBehaviour
{
    Vector3 StartHakusenn;

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

    void FixedUpdate()
    {
        Vector3 pos = transform.position;

        pos.y += -1 * MoveSpeed;

        if (pos.y < TransPos_Y)
        {
            pos = StartHakusenn;

            transform.localScale = TransScale;
        }

        if(pos.y < StartScaleUp_Y)
        {
            Vector3 scale = transform.localScale;

            scale.x = Mathf.Min(scale.x + ScaleAddValue.x, ScaleUpMax.x);
            scale.y = Mathf.Min(scale.y + ScaleAddValue.y, ScaleUpMax.y);

            transform.localScale = scale;
        }

        transform.position = pos;
    }
}
