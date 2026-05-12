using UnityEngine;
public class Enemy : MonoBehaviour, IDamageable 
{
    [Header("Movement (Strateji Deseni)")]
    public Transform targetCore;
    public float speed = 3f;
    public enum MovementType { Direct, ZigZag }
    public MovementType currentMovementType;
    private IEnemyMovement movementStrategy;

    [Header("Health (Can Sistemi)")]
    public float health = 50f;
    public bool IsDead => health <= 0;

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
    private void OnTriggerEnter(Collider other)
    {
        Core core = other.GetComponent<Core>();
        if (core != null)
        {
            core.TakeDamage(50f);
            gameObject.SetActive(false); 
        }
    }
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        if (IsDead)
        {
            gameObject.SetActive(false); 
        }
    }
} 