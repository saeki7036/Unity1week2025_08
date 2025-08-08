using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class BardBody : MonoBehaviour
{
    [SerializeField]
    RectTransform HeadRT;

    [SerializeField]
    RectTransform[] NeckRTs;

    [SerializeField]
    RectTransform NeckAnchorRT;

    [SerializeField]
    float NeckRange = 1.0f;

    void FixedUpdate()
    {
        Vector2 headPos = HeadRT.position;
        Vector2 neckPos = NeckAnchorRT.position;

        Vector2 dir = headPos - neckPos;

        int EnableUIIndex = Mathf.Min(NeckRTs.Count() -1, (int)(Vector2.Distance(headPos, neckPos) / NeckRange));

        //Debug.Log((int)(Vector2.Distance(headPos, neckPos) / NeckRange));

        for (int i = 0; i < NeckRTs.Count(); i++)
        {
            if (EnableUIIndex <= i)
            {
                NeckRTs[i].gameObject.SetActive(false);
                continue;
            }
            else
            {
                NeckRTs[i].gameObject.SetActive(true);

                float t = (float)i / NeckRTs.Count();               // 0.0 ~ 1.0

                Vector2 currentPos = Vector2.Lerp(NeckAnchorRT.position, headPos, t); // 等間隔の位置

                NeckRTs[i].position = currentPos;

                NeckRTs[i].up = dir;
            }
        }
    }
}
