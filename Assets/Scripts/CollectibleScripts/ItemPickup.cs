using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item Item;
    public GameManager gm;

    public void Pickup()
    {
        InventoryManager.Instance.Add(Item);
        gm.items += 1;
        Destroy(transform.GetChild(0).GetChild(1).gameObject);
        //Destroy should be changed later, ideally the dog doesnt completely destroy anything it smells
        //Destroy(gameObject);
    }

    //this next part should also be changed, idk what we are using to smell though, what input at least
    private void OnMouseDown()
    {
        //Pickup();
    }
}
