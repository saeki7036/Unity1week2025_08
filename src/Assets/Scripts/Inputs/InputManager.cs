using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public event Action<Vector3> LeftDownEvent;
    public event Action<Vector3> RightDownEvent;

    public event Action<Vector3> LeftDlagEvent;
    public event Action<Vector3> RightClickEvent;

    public event Action<Vector3> LeftUpEvent;
    public event Action<Vector3> RightUpEvent;

    [SerializeField]
    float UIwidthMax = Screen.width;

    [SerializeField]
    float UIheightMax = Screen.height;

    struct MouseParameter
    {
        public Vector3 mouseUIPos;
        public Vector3 mouseUIDownPos;
        public Vector3 mouseWorldPos;
        public Vector3 mouseWorldDownPos;

        public void ResetMousePos()
        {
            mouseUIPos = Vector3.zero;
            mouseUIDownPos = Vector3.zero;
            mouseWorldPos = Vector3.zero;
            mouseWorldDownPos = Vector3.zero;
        }
    }

    MouseParameter LeftParameter;

    void OnEnable()
    {
        UIwidthMax = Screen.width;
        UIheightMax = Screen.height;
    }

    // Start is called before the first frame update
    void Start()
    {
        LeftParameter = new MouseParameter();
        LeftParameter.ResetMousePos();
    }

    // Update is called once per frame
    void Update()
    {
        MouseInputParameter(ref LeftParameter);
    }

    void MouseInputParameter(ref MouseParameter parameter)
    {
        if (Input.GetMouseButtonDown(0))
        {
            parameter.mouseUIDownPos = GetTouchClamp();
            LeftDownEvent?.Invoke(parameter.mouseUIDownPos);
        }

        if (Input.GetMouseButton(0))
        {
            parameter.mouseUIPos = GetTouchClamp();           
            LeftDlagEvent?.Invoke(parameter.mouseUIPos);
        }

        if (Input.GetMouseButtonUp(0))
        {
            LeftUpEvent?.Invoke(parameter.mouseUIPos);
            parameter.ResetMousePos();
        }
    }

    Vector3 GetTouchClamp()
    {
        Vector3 ClampPosition = Input.mousePosition;

        ClampPosition.y = Mathf.Clamp(ClampPosition.y, 0, UIheightMax);
        ClampPosition.x = Mathf.Clamp(ClampPosition.x, 0, UIwidthMax);

        return ClampPosition;
    }
}
