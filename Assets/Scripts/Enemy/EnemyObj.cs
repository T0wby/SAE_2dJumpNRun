using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObj : MonoBehaviour
{
    [SerializeField] private int attackDamage;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Spike collided with " + collision.name);
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
        }
    }
}
