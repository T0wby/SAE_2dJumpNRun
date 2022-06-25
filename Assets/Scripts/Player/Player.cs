using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int hp;
    [SerializeField] private LayerMask _enemyLayers;
    [SerializeField] private Sprite greenSprite;
    [SerializeField] private Sprite yellowSprite;
    [SerializeField] private Sprite purpleSprite;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void TakeDamage(int damage)
    {
        if (hp>0)
        {
            hp -= damage;
            switch (hp)
            {
                case <=0:
                    OpenGameOverPrompt();
                    break;
                case 1:
                    _spriteRenderer.sprite = purpleSprite;
                    break;
                case 2:
                    _spriteRenderer.sprite = yellowSprite;
                    break;
                case 3:
                    _spriteRenderer.sprite = greenSprite;
                    break;
                default:
                    break;
            }
        }
    }

    private void OpenGameOverPrompt()
    {
        Debug.Log("GameOVER");
    }
}
