using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    public bool IsJump { get; private set; }

    void Update()
    {
        SetMove();
    }

    void SetMove()
    {
        IsJump = false;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetMouseButtonDown(0))
        {
            IsJump = true;
        }
    }
}
