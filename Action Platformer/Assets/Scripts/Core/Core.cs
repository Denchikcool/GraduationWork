using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    public Movement Movement
    {
        get
        {
            return GenericNotImplementedError<Movement>.TryGet(_movement, transform.parent.name);
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
            return GenericNotImplementedError<CollisionSenses>.TryGet(_collisionSenses, transform.parent.name);
        }
        set
        {
            _collisionSenses = value;
        }
    }
    public Combat Combat
    {
        get
        {
            return GenericNotImplementedError<Combat>.TryGet(_combat, transform.parent.name);
        }
        private set
        {
            _combat = value;
        }
    }

    private Movement _movement;
    private CollisionSenses _collisionSenses;
    private Combat _combat;

    private void Awake()
    {
        Movement = GetComponentInChildren<Movement>();
        CollisionSenses = GetComponentInChildren<CollisionSenses>();
        Combat = GetComponentInChildren<Combat>();
    }

    public void UpdateLogic()
    {
        Movement.UpdateLogic();
        Combat.UpdateLogic();
    }
}
