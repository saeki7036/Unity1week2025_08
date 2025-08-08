using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeBag : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer spriteRenderer;

    [SerializeField]
    Sprite openSprite;

    [SerializeField]
    Rigidbody2D rb2D;

    [SerializeField]
    float OpenedGravityScale = 10f;

    [SerializeField]
    Transform BagTransform;
    public Rigidbody2D GetRB2D() => rb2D;

    public void PicBag() => BagTransform.localPosition = Vector2.left;

    public void RereaceBag() => BagTransform.localPosition = Vector2.zero;

    public void OpenBag()
    {
        spriteRenderer.sprite = openSprite;
        rb2D.gravityScale = OpenedGravityScale;
    }
}
