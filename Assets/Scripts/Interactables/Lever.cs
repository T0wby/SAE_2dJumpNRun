using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] private bool _leverPulled;
    [SerializeField] private Door _door;
    [SerializeField] private Color _openDoorColor;
    [SerializeField] private Color _switchedLeverColor;
    [SerializeField] private SO_LevelObjects _levelObjects;
    private Transform _pivot;

    public bool LeverPulled { get { return _leverPulled; }}

    private void Awake()
    {
        _pivot = GetComponentInParent<Transform>();
    }

    

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(collision.gameObject.GetComponent<PlayerInput>().InteractStatus == 1 && !_leverPulled)
            {
                _pivot.Rotate(0,0,70);
                _leverPulled = true;
                GetComponent<SpriteRenderer>().color = _switchedLeverColor;
                _door.GetComponent<SpriteRenderer>().color = _openDoorColor;
            }
        }
    }

    public void SetState(bool leverPulled)
    {
        if (leverPulled)
        {
            _pivot.Rotate(0, 0, 70);
            GetComponent<SpriteRenderer>().color = _switchedLeverColor;
            if (_door is null)
                return;
            _door.GetComponent<SpriteRenderer>().color = _openDoorColor;
        }
    }

    public void SaveState()
    {
        _levelObjects.leverPulled = _leverPulled;
    }
}
