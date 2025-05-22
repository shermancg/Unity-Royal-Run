using UnityEngine;

public class PickupApple : Pickup
{
    protected override void OnPickup()
    {
        Debug.Log("Apple picked up!");
    }
}
