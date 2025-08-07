using System.Threading;
using UnityEngine;

public class RoadObjectGeneretor : MonoBehaviour
{
    [SerializeField]
    string GeneretObjectName;

    [SerializeField]
    RoadSystem roadSystem;

    [SerializeField]
    GameObject GeneretObject;

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

        timeCounter += timeCountValue;
        
        if(timeCounter > GeneretTime)
        {
            timeCounter = 0;
            ObjectGeneret();
        }
    }

    void ObjectGeneret()
    {
        Vector3 pos = new()
        {
            x = UnityEngine.Random.Range(SpawnRandomPosMin, SpawnRandomPosMax),
            y = SpawnPos_Y,
            z = 0,
        };

        GameObject SpawnObject = Instantiate(GeneretObject, pos, Quaternion.identity);

        var RoadBaseClass = SpawnObject.GetComponent<RoadObject>();

        roadSystem.AddRoadValue(RoadBaseClass);
    }
}
