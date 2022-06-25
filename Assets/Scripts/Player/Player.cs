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
    private CapsuleCollider2D _playerCapsule;

    private void Awake()
    {
        _playerCapsule = GetComponent<CapsuleCollider2D>();
    }
    public void TakeDamage(int damage)
    {
        if (hp>0)
        {
            hp -=damage;
            if (hp <= 0)
            {
                OpenGameOverPrompt();
            }
        }
    }

    private void OpenGameOverPrompt()
    {
        Debug.Log("GameOVER");
    }
}
