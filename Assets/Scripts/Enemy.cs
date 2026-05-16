using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable 
{
    public static event System.Action OnEnemyKilled;
    [Header("Movement (Strateji Deseni)")]
    public Transform targetCore;
    public float speed = 3f;
    public enum MovementType { Direct, ZigZag }
    public MovementType currentMovementType;
    private IEnemyMovement movementStrategy;

    [Header("Health (Can Sistemi)")]
    public float health = 50f;
    public bool IsDead => health <= 0;

    [Header("Animation")]
    public Animator animator; 
    private bool isStopped = false; 
    
    void Start()
    {
        SetMovementStrategy(currentMovementType);
    }

   
    private void OnEnable()
    {
        health = 50f; 
        isStopped = false;
        
       
        if (animator != null) animator.CrossFade("Walk", 0.1f);
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
   
        if (isStopped) return;

        if (movementStrategy != null && targetCore != null)
        {
            movementStrategy.Move(this.transform, targetCore, speed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isStopped) return;

        Core core = other.GetComponent<Core>();
        if (core != null)
        {
         
            StartCoroutine(AttackRoutine(core));
        }
    }

    IEnumerator AttackRoutine(Core core)
    {
        isStopped = true; 
        if (animator != null) animator.CrossFade("BasicAttack", 0.1f);
        
     
        yield return new WaitForSeconds(0.5f);
        
        core.TakeDamage(25f);
        gameObject.SetActive(false); 
    }

    public void TakeDamage(float damageAmount)
    {
        if (isStopped) return;

        health -= damageAmount;
        if (IsDead)
        {
            OnEnemyKilled?.Invoke();
            StartCoroutine(DeathRoutine());
        }
    }

    IEnumerator DeathRoutine()
    {
        isStopped = true; 
        if (animator != null) animator.CrossFade("Death", 0.1f);
        
      
        yield return new WaitForSeconds(1.5f);
        
        gameObject.SetActive(false); 
    }
}