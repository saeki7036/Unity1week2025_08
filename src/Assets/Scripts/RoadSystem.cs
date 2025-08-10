using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSystem : MonoBehaviour
{
    List<RoadObject> RoadValue;

    [SerializeField]
    Transform CameraTransform;// = Camera.main.transform;

    [SerializeField]
    float HitDistance = 5.0f;

    [SerializeField]
    float UpMove = 0.002f;

    [SerializeField]
    float DownMove = 0.002f;

    [SerializeField]
    float Approach_Y = -2f;

    [SerializeField]
    float FadeOut_Y = -8f;

    [SerializeField]
    int SpriteIndexMin = 10;

    [SerializeField]
    int SpriteIndexMax = 50;

    public void AddRoadValue(RoadObject roadObject) => RoadValue.Add(roadObject);

    float GetDictanceToCamera_X(float objectPos_x)
    {
        return Mathf.Abs(CameraTransform.position.x - objectPos_x); 
    }

    /// <summary>
    /// ある範囲の浮動小数点値を、別の整数範囲に線形的にマッピングする関数。
    /// 結果は四捨五入されて整数で返される。
    /// </summary>
    /// <param name="value">変換対象の値（元の範囲に属する）</param>
    /// <param name="min1">元の範囲の最小値</param>
    /// <param name="max1">元の範囲の最大値</param>
    /// <param name="min2">新しい範囲の最小整数値</param>
    /// <param name="max2">新しい範囲の最大整数値</param>
    /// <returns>value を元の範囲 [min1, max1] から新しい範囲 [min2, max2] にマッピングした整数値</returns>
    int mapRangeInt(float value, float min1, float max1, int min2, int max2)
    {
        if (Mathf.Approximately(max1, min1))
        {
            Debug.LogWarning("元の範囲の幅が0です");
            return min2;
        }

        float normalized = (value - min1) / (max1 - min1);// 0〜1に正規化
        float mapped = normalized * (max2 - min2) + min2; // 新しい範囲に変換
        return Mathf.RoundToInt(mapped);                  // 四捨五入してintに
    }

    // Start is called before the first frame update
    void Start()
    {
        RoadValue = new List<RoadObject>();
    }

    SR_System system => SR_System.instance;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (system.gameMode == SR_System.GameMode.Before) DestroyAll();
        if (system.IsMainGamePlay() == false) return;

        for (int i = RoadValue.Count - 1; i >= 0; i--)
        {
            if (RoadValue[i] == null)
            {
                RoadValue.RemoveAt(i);
                continue;
            }

            if (RoadValue[i].IsLayerFlont())
            {
                Transform roadTransform = RoadValue[i].GetTransform();

                MoveTransForm(roadTransform, -DownMove);

                float scaleMax = RoadValue[i].GetScaleMax();

                ScaleTransForm(roadTransform, scaleMax, RoadValue[i].GetAddScaleValue());

                int SpriteIndex = mapRangeInt(
                    roadTransform.position.y,
                    Approach_Y,
                    FadeOut_Y,
                    SpriteIndexMin,
                    SpriteIndexMax);

                RoadValue[i].SetLayerNumber(SpriteIndex);

                if (roadTransform.localScale.x == scaleMax)
                {
                    float CameraDictance_X = GetDictanceToCamera_X(roadTransform.position.x);
                    bool IsHit = (HitDistance > CameraDictance_X);
                    
                    if (IsHit)
                        RoadValue[i].ObjectHit();
                    else
                        RoadValue[i].SideRemove();

                    RoadValue.RemoveAt(i);
                }
                else if (roadTransform.position.y < FadeOut_Y)
                {
                    RoadValue[i].Destroy();
                    RoadValue.RemoveAt(i);
                }
            }
            else
            {
                Transform roadTransform = RoadValue[i].GetTransform();
                MoveTransForm(roadTransform, UpMove);
                
                if(roadTransform.position.y > Approach_Y)
                {
                    RoadValue[i].SetLayerFront();
                }
            }
        }
    }

    void MoveTransForm(Transform transform, float MoveValue)
    {
        Vector3 pos = transform.position;
        pos = new Vector3()
        {
            x = pos.x,
            y = pos.y + MoveValue,
            z = pos.z
        };
        transform.position = pos;
    }

    void ScaleTransForm(Transform transform, float scaleMax, float AddScaleValue)
    {
        Vector3 scale = transform.localScale;
        scale = new Vector3()
        {
            x = Mathf.Min(scale.x + AddScaleValue, scaleMax),
            y = Mathf.Min(scale.x + AddScaleValue, scaleMax),
            z = 1
        };
        transform.localScale = scale;
    }

    void DestroyAll()
    {
        for (int i = RoadValue.Count - 1; i >= 0; i--)
        {
            if (RoadValue[i] == null)
            {
                RoadValue.RemoveAt(i);
                continue;
            }
            else
            {
                RoadValue[i].Destroy();
                RoadValue.RemoveAt(i);
            }
        }
    }
}
