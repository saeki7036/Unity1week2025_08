using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputIventHandler : MonoBehaviour
{
    [SerializeField]
    InputManager manager;

    [SerializeField]
    HandleController handleController;

    [SerializeField]
    CoffeePiccer coffeePiccer;
    void Start()
    {
        handleController.InputRegister(manager);
        coffeePiccer.InputRegister(manager);
    }
}
