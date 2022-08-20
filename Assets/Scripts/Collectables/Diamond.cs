using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : Collectable
{
    private void Start()
    {
        if (!GameManager.Instance.LoadingSave)
            _levelObjects.maxCountDiamonds = FindObjectsOfType<Diamond>();
    }

    [SerializeField] private SO_LevelObjects _levelObjects;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.DiamondCount += _value;
            _levelObjects.collectedDiamonds.Add(this.gameObject.name);
            Destroy(this.gameObject);
        }
    }
}
