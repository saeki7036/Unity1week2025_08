using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class RoadObject : MonoBehaviour
{
    [SerializeField]
    protected SpriteRenderer spriteRenderer;

    [SerializeField]
    float ScaleMax =  1.5f;

    [SerializeField]
    float AddScaleValue = 0.01f;

    public float GetScaleMax() => ScaleMax;

    public float GetAddScaleValue() => AddScaleValue;

    public bool IsLayerFlont() => spriteRenderer.sortingOrder >= 0;

    public void SetLayerNumber(int index) => spriteRenderer.sortingOrder = index;

    public void SetLayerFront() => spriteRenderer.sortingOrder = 0;

    public Transform GetTransform() => this.transform;

    public void Destroy()=> Destroy(this.gameObject);

    public void ObjectHit() => HitAction();

    public void SideRemove() => RemoveAction();

    protected virtual void HitAction()
    {
        return;
    }

    protected virtual void RemoveAction()
    {
        return;
    }
}
