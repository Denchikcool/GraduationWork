using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : CoreComponent
{
    private Vector2 _workSpace;

    public Rigidbody2D Rigidbody { get; private set; }

    public int FacingDirection { get; private set; }
    public Vector2 CurrentVelocity { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        Rigidbody = GetComponentInParent<Rigidbody2D>();
        FacingDirection = 1;
    }

    public void UpdateLogic()
    {
        CurrentVelocity = Rigidbody.velocity;
    }

    #region Set Functions
    public void SetHorizontalVelocity(float velocity)
    {
        _workSpace.Set(velocity, CurrentVelocity.y);
        Rigidbody.velocity = _workSpace;
        CurrentVelocity = _workSpace;
    }

    public void SetVerticalVelocity(float velocity)
    {
        _workSpace.Set(CurrentVelocity.x, velocity);
        Rigidbody.velocity = _workSpace;
        CurrentVelocity = _workSpace;
    }

    public void SetVelocity(float velocity, Vector2 direction)
    {
        _workSpace = direction * velocity;
        Rigidbody.velocity = _workSpace;
        CurrentVelocity = _workSpace;
    }

    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        _workSpace.Set(angle.x * velocity * direction, angle.y * velocity);
        Rigidbody.velocity = _workSpace;
        CurrentVelocity = _workSpace;
    }

    public void SetVelocityZero()
    {
        Rigidbody.velocity = Vector2.zero;
        CurrentVelocity = Vector2.zero;
    }

    public void CheckShouldFlip(int horizontalInput)
    {
        if (horizontalInput != 0 && horizontalInput != FacingDirection)
        {
            Flip();
        }
    }

    private void Flip()
    {
        FacingDirection *= -1;
        Rigidbody.transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    #endregion
}
