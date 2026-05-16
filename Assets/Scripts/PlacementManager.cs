using UnityEngine;
using UnityEngine.InputSystem;

public class PlacementManager : MonoBehaviour
{
    [Header("Settings")]
    public GameObject towerPrefab;
    public LayerMask placementMask;

    [Header("Tower Connections (Kulelere verilecek hedefler)")]
    public Transform enemyTarget;
    public ProjectilePool bulletPool;
    
    void Update()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            PlaceTower();
        }
    }
    void PlaceTower()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f, placementMask))
        {
            GameObject newTowerObj = Instantiate(towerPrefab, hit.point, Quaternion.identity);
            Tower newTower = newTowerObj.GetComponent<Tower>();
            if (newTower != null)
            {
                newTower.target = enemyTarget;
                newTower.pool = bulletPool;
            }
            
            Debug.Log("Yeni kule eklendi!");
        }
    }
}