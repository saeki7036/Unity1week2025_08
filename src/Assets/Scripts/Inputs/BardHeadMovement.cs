using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BardHeadMovement : MonoBehaviour
{
    [SerializeField]
    RectTransform PointerRT;

    [SerializeField]
    Image HeadImage;

    [SerializeField]
    Sprite OpenSprite;

    [SerializeField]
    Sprite CloseSprite;

    [SerializeField]
    float UIwidthMax = Screen.width;

    [SerializeField]
    float UIheightMax = Screen.height;

    public void InputRegister(InputManager input)
    {
        input.LeftDownEvent += CloseMouth;
        input.LeftUpEvent += OpenMouth;
    }

    void OnEnable()
    {
        UIwidthMax = Screen.width;
        UIheightMax = Screen.height;
    }

    Vector3 GetTouchClamp()
    {
        Vector3 ClampPosition = Input.mousePosition;

        ClampPosition.y = Mathf.Clamp(ClampPosition.y, 0, UIheightMax);
        ClampPosition.x = Mathf.Clamp(ClampPosition.x, 0, UIwidthMax);

        return ClampPosition;
    }

    void FixedUpdate()
    {
        PointerRT.position = GetTouchClamp();
    }

    void CloseMouth(Vector3 vector3)
    {
        HeadImage.sprite = CloseSprite;
    }

    void OpenMouth(Vector3 vector3)
    {
        HeadImage.sprite = OpenSprite;
    }
}
