using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : Collectable
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player enter");
            GameManager.Instance.DiamondCount += _value;
            this.gameObject.SetActive(false);
        }
    }
}
