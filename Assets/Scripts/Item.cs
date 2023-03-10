using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum ItemState
{
    DROPPED,
    HELD,
    THROWN,
    CAUGHT,
    UNHOLDABLE,
    UNDROPABLE
}

public class Item : Entity
{
    private ItemState itemState;

    private bool onlyTargetPosition;
    private Entity target = null;
    private Vector3 targetedPosition = Vector3.zero;

    private void Update()
    {
        Aim();
    }

    public virtual void Aim()
    {
        return;
    }

    public virtual void PickedUp(Entity entity)
    {
        return;
    }

    public virtual void Drop()
    {
        return;
    }

    public void SetItemState(ItemState itemState)
    {
        this.itemState = itemState;
    }

    public ItemState GetItemState()
    {
        return this.itemState;
    }

    public bool HasTargetedPosition()
    {
        if (target.IsUnityNull())
        {
            return onlyTargetPosition;
        }
        else
        {
            return true;
        }
    }

    public Entity GetTarget()
    {
        return target;
    }

    public Vector3 GetTargetedPosition()
    {
        if (onlyTargetPosition)
        {
            return targetedPosition;
        }
        else
        {
            return target.transform.position;
        }
    }

    public void SetTarget(Entity target)
    {
        this.target = target;
    }

    public void SetTargetedPosition(Vector3 targetedPosition)
    {
        this.targetedPosition = targetedPosition;
    }

    public void OnlyTargetPosition(bool onlyTargetPosition)
    {
        this.onlyTargetPosition = onlyTargetPosition;
    }
}
