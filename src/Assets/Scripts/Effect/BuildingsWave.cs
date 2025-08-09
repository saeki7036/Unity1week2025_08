using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsWave : MonoBehaviour
{
    [SerializeField]
    float MoveSpeed_X = 0.02f;

    [SerializeField]
    float LimitPos_X = 30f;

    [SerializeField]
    float TransPos_X = 20f;

    [SerializeField]
    float TransPos_Y = 2f;

    [SerializeField]
    float ScaleAdd_2D = 0.02f;

    [SerializeField]
    float ScaleMax_2D = 8f;

    [SerializeField]
    float TransScale_2D = 0.2f;

    [SerializeField]
    float AdjustmentValue = 0.01f;

    [SerializeField]
    List<Transform> transforms;

    SR_System system => SR_System.instance;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (system.IsMainGamePlay() == false) return;

        for (int i = 0; i < transforms.Count; i++)
        {
            float scale = Mathf.Min(transforms[i].localScale.y + ScaleAdd_2D, ScaleMax_2D);
            transforms[i].localScale = new Vector3(scale, scale, 1);
        }

        transforms[0].position += new Vector3(MoveSpeed_X, 0, 0);

        for (int i = 1; i < transforms.Count; i++)
        {
            float prevScale = transforms[i - 1].localScale.x;
            float currentScale = transforms[i].localScale.x;

            float distance = (prevScale + currentScale) / 2 * AdjustmentValue;

            transforms[i].position = new Vector3(
                transforms[i - 1].position.x + distance,
                transforms[i].position.y,
                0
            );
        }

        // 4. 最後尾が境界を超えたら先頭に戻す
        var last = transforms[transforms.Count - 1];
        if (Mathf.Abs(last.position.x) > Mathf.Abs(LimitPos_X))
        {
            
            last.position = new Vector3(TransPos_X, TransPos_Y, 0);
            last.localScale = new Vector3(TransScale_2D, TransScale_2D, 1);

            // リストの順番を入れ替え
            transforms.RemoveAt(transforms.Count - 1);
            transforms.Insert(0, last);
        }
    }
}
