using System.Threading.Tasks;
using UnityEngine;

public class CarObject : RoadObject
{
    [SerializeField]
    float HitTime = 1.5f;

    [SerializeField]
    float RemoveTime = 2f;

    [SerializeField]
    float RemoveSpeed = 2.5f;

    Transform cameraTransform;

    bool isLeftMove;

    SR_System system => SR_System.instance;

    void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    protected override void HitAction()
    {
        system.gameMode = SR_System.GameMode.After;

        Destroy(gameObject, HitTime);
    }

    protected override void RemoveAction()
    {
        AsyncStartRemove();

        Destroy(gameObject, RemoveTime);
    }

    async void AsyncStartRemove()
    {
        await RemoveAsync(RemoveTime);
    }

    async Task RemoveAsync(float duration)
    {
        isLeftMove = cameraTransform.position.x <= transform.position.x;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            float deltaTime = Time.deltaTime;

            if (this == null) return;

            Vector3 moveVector = new Vector3()
            {
                x = (isLeftMove ? 1 : -1),
                y = -1 
            };

            transform.position += moveVector.normalized * RemoveSpeed * deltaTime;

            elapsed += deltaTime;

            // 1フレーム待機
            await Task.Yield();
        }
    }
}
