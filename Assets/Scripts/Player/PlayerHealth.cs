using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private LayerMask _enemyLayers;
    [SerializeField] private Sprite _greenSprite;
    [SerializeField] private Sprite _yellowSprite;
    [SerializeField] private Sprite _purpleSprite;
    [SerializeField] private SO_LevelObjects _LevelObjects;
    private SpriteRenderer _spriteRenderer;

    public int Health { get { return _health; } set { _health = value; onHealthChange.Invoke(_health); } }

    public UnityEvent onPlayerDeath;
    public UnityEvent<int> onHealthChange;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        onHealthChange.Invoke(_health);
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (_health > 0)
        {
            switch (_health)
            {
                case 1:
                    _spriteRenderer.sprite = _purpleSprite;
                    break;
                case 2:
                    _spriteRenderer.sprite = _yellowSprite;
                    break;
                case 3:
                    _spriteRenderer.sprite = _greenSprite;
                    break;
                default:
                    break;
            }
        }
        else
        {
            onPlayerDeath.Invoke();
        }
    }

    public void SaveHealth()
    {
        _LevelObjects.health = _health;
    }
}
