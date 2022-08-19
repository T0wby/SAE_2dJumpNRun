using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/GameSettings", order = 0)]

public class SO_GameSettings : ScriptableObject
{
    public bool coyoteToggle = true;
    public bool doubleJumpToggle = true;
    public bool wallSlideToggle = true;
    public bool wallJumpToggle = true;
    public bool jumpBufferToggle = true;
    public int displayMode;
    public float volume;
}
