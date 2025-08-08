using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class HumanObject : RoadObject
{
    [SerializeField]
    Sprite SurprisedSprite;

    [SerializeField] 
    float HitMoveSpeed = 1f;

    [SerializeField]
    float RemoveSpeed = 2.5f;

    [SerializeField] 
    float HitRotateSpeed = 90f;

    [SerializeField] 
    float HitTime = 3f;

    Transform cameraTransform;

    bool isLeftMove;

    void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    protected override void HitAction()
    {
        spriteRenderer.sprite = SurprisedSprite;

        AsyncStartHit();

        Destroy(gameObject, HitTime);
    }

    protected override void RemoveAction()
    {
        AsyncStartRemove();

        Destroy(gameObject, HitTime);
    }

    async void AsyncStartHit()
    {
        await MoveAndRotateAsync(HitTime);
    }

    async void AsyncStartRemove()
    {
        await SideRemoveAsync(HitTime);
    }

    async Task MoveAndRotateAsync(float duration)
    {
        isLeftMove = cameraTransform.position.x <= transform.position.x;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float deltaTime = Time.deltaTime;

            if (this == null) return;

            Vector3 HitMoveVector = new Vector3()
            {
                x = HitMoveSpeed * deltaTime * (isLeftMove ? 1 : -1),
                y = HitMoveSpeed * deltaTime,
                z = 0
            };

            transform.position += HitMoveVector;

            transform.Rotate(0f, 0f, HitRotateSpeed * deltaTime * (isLeftMove ? 1 : -1));

            elapsed += deltaTime;

            // 1フレーム待機
            await Task.Yield();
        }
    }

    async Task SideRemoveAsync(float duration)
    {
        isLeftMove = cameraTransform.position.x <= transform.position.x;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            float deltaTime = Time.deltaTime;

            if (this == null) return;

            transform.position += Vector3.right * RemoveSpeed * deltaTime * (isLeftMove ? 1 : -1);

            elapsed += deltaTime;

            // 1フレーム待機
            await Task.Yield();
        }
    }

    private void OnDestroy()
    {
        if (this == null) return;

        CoffeeBagGeneretor.Instance.GenereteCoffeeBag(isLeftMove);
    }
}
