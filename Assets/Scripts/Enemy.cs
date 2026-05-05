using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform targetCore;
    public float speed = 3f;
    public enum MovementType { Direct, ZigZag }
    public MovementType currentMovementType;
    private IEnemyMovement movementStrategy;

    void Start()
    {
        SetMovementStrategy(currentMovementType);
    }
    public void SetMovementStrategy(MovementType type)
    {
        if (type == MovementType.Direct)
            movementStrategy = new DirectMovement();
        else if (type == MovementType.ZigZag)
            movementStrategy = new ZigZagMovement();
    }

    void Update()
    {
        if (movementStrategy != null && targetCore != null)
        {
            movementStrategy.Move(this.transform, targetCore, speed);
        }
    }
}