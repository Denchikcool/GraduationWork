using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    public Movement Movement
    {
        get
        {
            if (_movement)
            {
                return _movement;
            }
            Debug.LogError("No move core component on " + transform.parent.name);
            return null;
        }
        private set
        {
            _movement = value;
        }
    }
    public CollisionSenses CollisionSenses
    {
        get
        {
            if (_collisionSenses)
            {
                return _collisionSenses;
            }
            Debug.LogError("No collision core component on " + transform.parent.name);
            return null;
        }
        set
        {
            _collisionSenses = value;
        }
    }

    private Movement _movement;
    private CollisionSenses _collisionSenses;

    private void Awake()
    {
        Movement = GetComponentInChildren<Movement>();
        CollisionSenses = GetComponentInChildren<CollisionSenses>();
    }

    public void UpdateLogic()
    {
        Movement.UpdateLogic();
    }
}
