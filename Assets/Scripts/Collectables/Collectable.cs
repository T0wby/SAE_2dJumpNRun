using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] protected string _name;
    [SerializeField] protected string _description;
    [SerializeField] protected int _value;

    public string Name { get { return _name; } }
    public string Description { get { return _description; } }
    public int Value { get { return _value; } }
}
