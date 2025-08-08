using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeBagGeneretor : MonoBehaviour
{
    [SerializeField]
    GameObject CoffeeBagObject;

    [SerializeField]
    Transform LeftGeneretePos;

    [SerializeField]
    Transform RightGeneretePos;

    [SerializeField]
    float RandomPowerMin = 5.0f;

    [SerializeField]
    float RandomPowerMax = 10.0f;

    [SerializeField]
    float RandomTargetMin_Y = -2f;

    [SerializeField]
    float RandomTargetMax_Y = 4f;

    [HideInInspector]
    // インスタンス（シングルトン）
    public static CoffeeBagGeneretor Instance { get; private set; }

    private void Awake()
    {
        // インスタンスを設定（複数防止）
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーンを跨いでも保持
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 外部から呼び出したい関数
    public void GenereteCoffeeBag(bool IsLeft)
    {
        if (this == null) return;

        Vector3 generetePos = IsLeft ? LeftGeneretePos.position : RightGeneretePos.position;

        float Random_z = UnityEngine.Random.Range(0, 360);

        GameObject CoffeeBag = Instantiate(CoffeeBagObject, generetePos, Quaternion.Euler(0, 0, Random_z));

        CoffeeBag.transform.parent = transform;

        Rigidbody2D rigidbody2D = CoffeeBag.GetComponent<Rigidbody2D>();

        Vector2 target = new Vector2((IsLeft ? 1 : -1), UnityEngine.Random.Range(RandomTargetMin_Y, RandomTargetMax_Y));

        rigidbody2D.AddForce(target.normalized * UnityEngine.Random.Range(RandomPowerMin, RandomPowerMax), ForceMode2D.Impulse);
    }

    public Transform GetInSideTransform(Vector2 PicPos, float PicRadius)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (Vector2.Distance(transform.GetChild(i).position, PicPos) < PicRadius)
                return transform.GetChild(i);
        }

        return null;
    }
}
