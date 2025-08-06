using System.Threading;
using UnityEngine;

public class RoadObjectGeneretor : MonoBehaviour
{
    [SerializeField]
    RoadSystem roadSystem;

    [SerializeField]
    GameObject HumanObject;

    [SerializeField]
    GameObject CarObject;

    [SerializeField]
    float SpawnPosmin_X = -10, SpawnPosmax_X = 10;

    [SerializeField]
    float SpawnPos_Y = -5f;

    [SerializeField]
    float timeCountValue = 0.02f;

    [SerializeField]
    float timelimit_Car = 5f;
    [SerializeField]
    float timelimit_Human = 2f;

    [SerializeField]
    float timeCount_Car = -100f;

    float timeCount_Human = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (roadSystem == null)
            Debug.LogError("シリアライズ漏れ");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (roadSystem == null)
            return;

        timeCount_Car += timeCountValue;
        timeCount_Human += timeCountValue;
        
        if(timeCount_Car > timelimit_Car)
        {
            timeCount_Car = 0;
            CreateObject(CarObject);
        }
        if (timeCount_Human > timelimit_Human)
        {
            timeCount_Human = 0;
            CreateObject(HumanObject);
        }
    }

    void CreateObject(GameObject gameobject)
    {
        Vector3 pos = new()
        {
            x = UnityEngine.Random.Range(SpawnPosmin_X, SpawnPosmax_X),
            y = SpawnPos_Y,
            z = 0,
        };

        GameObject SpawnObject = Instantiate(gameobject, pos, Quaternion.identity);

        var RoadBaseClass = SpawnObject.GetComponent<RoadObject>();

        roadSystem.AddRoadValue(RoadBaseClass);
    }
}
