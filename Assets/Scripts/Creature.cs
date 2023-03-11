using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : Entity
{
    Item item;

    public bool HasItem()
    {
        return item != null;
    }

    public Item GetHeldItem()
    {
        return item;
    }

    public void SetHeldItem(Item item)
    {
        this.item = item;
    }

    public bool PickupItem()
    {
        if (HasItem())
        {
            return false;
        } else
        {
            item = GameManager.instance.GetClosestItemOfStates(EntitySubClass.WEAPON, new List<ItemState>() { ItemState.DROPPED }, this);
            if (HasItem())
            {
                item.PickedUp(this);
            }
        }
        return true;
    }

    public bool DropHeldItem()
    {
        if (HasItem())
        {
            if (item.GetItemState() == ItemState.UNDROPABLE)
            {
                return false;
            }
            item.Drop();
            item = null;
        } else
        {
            return false;
        }
        return true;
    }
}
