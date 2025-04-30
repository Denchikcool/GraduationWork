using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : CoreComponent
{
    private Vector2 _workSpace;

    public Rigidbody2D Rigidbody { get; private set; }

    public int FacingDirection { get; private set; }

    public Vector2 CurrentVelocity { get; private set; }

    public bool CanSetVelocity { get; set; }

    protected override void Awake()
    {
        base.Awake();

        Rigidbody = GetComponentInParent<Rigidbody2D>();
        FacingDirection = 1;
        CanSetVelocity = true;
    }

    public override void UpdateLogic()
    {
        CurrentVelocity = Rigidbody.velocity;
    }

    #region Set Functions
    public void SetHorizontalVelocity(float velocity)
    {
        _workSpace.Set(velocity, CurrentVelocity.y);
        SetFinalVelocity();
    }

    public void SetVerticalVelocity(float velocity)
    {
        _workSpace.Set(CurrentVelocity.x, velocity);
        SetFinalVelocity();
    }

    public void SetVelocity(float velocity, Vector2 direction)
    {
        _workSpace = direction * velocity;
        SetFinalVelocity();
    }

    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        _workSpace.Set(angle.x * velocity * direction, angle.y * velocity);
        SetFinalVelocity();
    }

    public void SetVelocityZero()
    {
        _workSpace = Vector2.zero;
        SetFinalVelocity();
    }

    private void SetFinalVelocity()
    {
        if (CanSetVelocity)
        {
            Rigidbody.velocity = _workSpace;
            CurrentVelocity = _workSpace;
        }
    }

    public void CheckShouldFlip(int horizontalInput)
    {
        if (horizontalInput != 0 && horizontalInput != FacingDirection)
        {
            Flip();
        }
    }

    public void Flip()
    {
        FacingDirection *= -1;
        Rigidbody.transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    #endregion
}
