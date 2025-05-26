using UnityEngine;

public class PickupApple : Pickup
{
    LevelGenerator levelGenerator;

    public void Init(LevelGenerator levelGenerator)
    {
        this.levelGenerator = levelGenerator;
    }

    protected override void OnPickup()
    {
        levelGenerator.ChangeChunkMoveSpeed(levelGenerator.increaseSpeed); // Increase the chunk move speed on pickup
        Debug.Log("Apple picked up!");
    }

}
