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

    public Stats Stats
    {
        get
        {
            return GenericNotImplementedError<Stats>.TryGet(_stats, transform.parent.name);
        }
        private set
        {
            _stats = value;
        }
    }

    private Movement _movement;
    private CollisionSenses _collisionSenses;
    private Combat _combat;
    private Stats _stats;

    private List<ILogicUpdate> _components = new List<ILogicUpdate>();

    private void Awake()
    {
        Movement = GetComponentInChildren<Movement>();
        CollisionSenses = GetComponentInChildren<CollisionSenses>();
        Combat = GetComponentInChildren<Combat>();
        Stats = GetComponentInChildren<Stats>();
    }

    public void UpdateLogic()
    {
        foreach (ILogicUpdate component in _components)
        {
            component.UpdateLogic();
        }
    }

    public void AddComponent(ILogicUpdate component)
    {
        if (!_components.Contains(component))
        {
            _components.Add(component);
        } 
    }
}
