using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MillHandle : MonoBehaviour
{
    [SerializeField]
    RectTransform SenterRT;

    [SerializeField]
    float SenterRadius = 330f;

    [SerializeField]
    RectTransform[] RectPoints;

    [SerializeField]
    UnityEvent HasHandleEvent;

    [SerializeField]
    UnityEvent OneRotateEvent;

    [SerializeField]
    float RotateFixValue = 90;

    int currentPoint;
    int NextPoint;
    bool IsInsideFlag;

    public void InputRegister(InputManager input)
    {
        input.LeftDownEvent += InputPosCheck;
        input.LeftDlagEvent += RotateHandle;
        input.LeftUpEvent += ReleaseHandle;
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetHandle();
    }

    float DistancePaw(Vector3 a, Vector3 b)
    {
        float x = a.x - b.x;
        float y = a.y - b.y;
        return Mathf.Sqrt(x * x + y * y);
    }

    int GetNearestIndex(Vector3 screenPoint)
    {
        if(RectPoints.Length <= 0)
            return 0;

        int index = 0;
        float dictanceMin = float.MaxValue;

        for(int i = 0; i < RectPoints.Length; i++)
        {
            float currentDic = DistancePaw(RectPoints[i].position, screenPoint);
            if (currentDic < dictanceMin)
            {
                dictanceMin = currentDic;
                index = i;
            }
        }

        return index;
    }

    bool IsPointInsideHandle(Vector2 screenPoint)
    {    
        Vector2 HandleSenter = SenterRT.transform.position;

        return Vector2.Distance(screenPoint, HandleSenter) < SenterRadius;
    }

    void ResetHandle()
    {
        IsInsideFlag = false;
        SenterRT.up = Vector3.zero;
        currentPoint = 0;
        NextPoint = 1;
    }

    void InputPosCheck(Vector3 downInput)
    {
        IsInsideFlag = IsPointInsideHandle(new Vector2(downInput.x, downInput.y));
    }

    void RotateHandle(Vector3 dragInput)
    {
        if (!IsInsideFlag)
            return;

        HasHandleEvent.Invoke();

        SenterRT.up = (SenterRT.transform.position - dragInput).normalized;
        SenterRT.rotation = SenterRT.rotation * Quaternion.Euler(0, 0, RotateFixValue);

        int newPoint = GetNearestIndex(dragInput);

        if (newPoint != currentPoint) // ポイントが変わった時だけ進める
        {
            if ((currentPoint + 1) % RectPoints.Length == newPoint)
            {
                NextPoint++;
                if (NextPoint >= RectPoints.Length)
                {
                    NextPoint = 0;
                    OneRotateEvent.Invoke();
                    Debug.Log("1Rotate");
                }
            }
            currentPoint = newPoint;
        }
    }

    void ReleaseHandle(Vector3 upInput)
    {
        ResetHandle();
    }
}
