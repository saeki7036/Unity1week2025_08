using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSystem : MonoBehaviour
{
    List<RoadObject> RoadValue;

    [SerializeField]
    float UpMove = 0.002f;

    [SerializeField]
    float DownMove = 0.002f;

    [SerializeField]
    float Approach_Y = -2f;

    [SerializeField]
    float FadeOut_Y = -8f;
    
    public void AddRoadValue(RoadObject roadObject) => RoadValue.Add(roadObject);

    // Start is called before the first frame update
    void Start()
    {
        RoadValue = new List<RoadObject>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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

                if (roadTransform.position.y < FadeOut_Y)
                {
                    RoadValue[i].Destroy();
                    RoadValue.RemoveAt(i);
                }
            }
            else
            {
                Transform roadTransform = RoadValue[i].GetTransform();
                MoveTransForm(roadTransform, UpMove);
                //Debug.Log(roadTransform.position);
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
}
