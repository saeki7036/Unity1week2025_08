using System.Threading;
using UnityEngine;

public class RoadObjectGeneretor : MonoBehaviour
{
    [SerializeField]
    string GeneretObjectName;

    [SerializeField]
    TimeCounter timeCounterClass;

    [SerializeField]
    RoadSystem roadSystem;

    [SerializeField]
    GameObject[] GeneretObjects;

    [SerializeField]
    float SpawnRandomPosMin = -10, SpawnRandomPosMax = 10;

    [SerializeField]
    float SpawnPos_Y = -5f;

    [SerializeField]
    float timeCountValue = 0.02f;

    [SerializeField]
    float GeneretTime = 5f;
    
    [SerializeField]
    float timeCounter = -100f;

    [SerializeField]
    float AddValueRankRimit = 20f;

    float ResetCounter;

    // Start is called before the first frame update
    void Start()
    {
        if (roadSystem == null)
            Debug.LogError("シリアライズ漏れ");

        ResetCounter = timeCounter;
    }

    SR_System system => SR_System.instance;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (system.IsMainGamePlay() == false)
        {
            timeCounter = ResetCounter;
            return;
        }

        if (roadSystem == null)
            return;

        timeCounter += timeCountValue * (1 + (int)(timeCounterClass.GetTravelCount() / AddValueRankRimit));
        
        if(timeCounter > GeneretTime)
        {
            timeCounter = 0;
            ObjectGeneret();
        }
    }

    void ObjectGeneret()
    {
        if (GeneretObjects.Length == 0) return;

        Vector3 pos = new()
        {
            x = UnityEngine.Random.Range(SpawnRandomPosMin, SpawnRandomPosMax),
            y = SpawnPos_Y,
            z = 0,
        };

        GameObject SpawnObject = Instantiate(
            GeneretObjects[Random.Range(0, GeneretObjects.Length)], pos, Quaternion.identity);

        var RoadBaseClass = SpawnObject.GetComponent<RoadObject>();

        roadSystem.AddRoadValue(RoadBaseClass);
    }
}
