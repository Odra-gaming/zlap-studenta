using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SimpleAnimation : MonoBehaviour
{
    public SpriteRenderer spriteRenderer { get; private set; }

    public Sprite[] sprites;
    public int animationIndex;

    public float animationTime = 0.25f;

    public bool loop = true; 

    private void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        InvokeRepeating(nameof(Rotate), this.animationTime, this.animationTime);
    }

    void OnDisable()
    {
        CancelInvoke();
    }

    private void Rotate()
    {
        if (!spriteRenderer.enabled)
        {
            return;
        }

        animationIndex++;

        if(animationIndex >= sprites.Length && loop)
        {
            animationIndex = 0;
        }

        if (animationIndex >= 0 && animationIndex < sprites.Length)
        {
            spriteRenderer.sprite = sprites[animationIndex];
        }
    }

    public void resetAnimation()
    {
        this.animationIndex = -1;
        Rotate();
       
    }
}
