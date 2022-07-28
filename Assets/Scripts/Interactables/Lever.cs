using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] private bool _leverPulled;
    [SerializeField] private Door _Door;
    [SerializeField] private Color _openDoorColor;
    [SerializeField] private Color _switchedLeverColor;
    private Transform _Pivot;

    public bool LeverPulled { get { return _leverPulled; }}

    private void Awake()
    {
        _Pivot = GetComponentInParent<Transform>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(collision.gameObject.GetComponent<PlayerInput>().InteractStatus == 1 && !_leverPulled)
            {
                _Pivot.Rotate(0,0,70);
                _leverPulled = true;
                GetComponent<SpriteRenderer>().color = _switchedLeverColor;
                _Door.GetComponent<SpriteRenderer>().color = _openDoorColor;
            }
        }
    }
}
