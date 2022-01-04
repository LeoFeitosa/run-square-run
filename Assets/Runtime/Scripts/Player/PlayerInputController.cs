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
        if (!enabled)
        {
            return;
        }

        IsJump = false;

        if (Input.GetKeyDown(KeyCode.Space)
        || Input.GetKeyDown(KeyCode.UpArrow)
        || Input.GetMouseButtonDown(0)
        || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            IsJump = true;
        }
    }
}
