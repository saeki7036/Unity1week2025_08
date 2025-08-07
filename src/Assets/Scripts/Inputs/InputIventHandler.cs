using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputIventHandler : MonoBehaviour
{
    [SerializeField]
    InputManager manager;

    [SerializeField]
    HandleController handleController;
    
    void Start()
    {
        handleController.InputRegister(manager);
    }
}
