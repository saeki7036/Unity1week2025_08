using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BardHeadMovement : MonoBehaviour
{
    [SerializeField]
    RectTransform HeadRT;

    [SerializeField]
    RectTransform[] NeckRTs;

    [SerializeField]
    RectTransform NeckAnchorRT;

    void FixedUpdate()
    {
        Vector2 headPos = HeadRT.position;

        for (int i = 0; i < NeckRTs.Count(); i++)
        {
            float t = (float)i / NeckRTs.Count();               // 0.0 ~ 1.0

            Vector2 currentPos = Vector2.Lerp(NeckAnchorRT.position, headPos, t); // 等間隔の位置

            NeckRTs[i].position = currentPos;
        }
    }
}
