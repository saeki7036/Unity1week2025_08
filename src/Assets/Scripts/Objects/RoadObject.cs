using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadObject : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer spriteRenderer;

    public bool IsLayerFlont() => spriteRenderer.sortingOrder == 0;

    public void SetLayerBack() => spriteRenderer.sortingOrder = -12;

    public void SetLayerFront() => spriteRenderer.sortingOrder = 0;

    public Transform GetTransform() => this.transform;

    public void Destroy()=> Destroy(this.gameObject);
}
