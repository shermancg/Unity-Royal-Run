using UnityEngine;

public class PickupCoin : Pickup
{
    protected override void OnPickup()
    {
        Debug.Log("Coin picked up!");
    }
}
