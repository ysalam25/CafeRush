using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject CustomerPrefab; 
    public Transform SpawnPoint; 

 
    public void CompleteOrderAndHandleNextCustomer(GameObject currentCustomer)
    {
        
        Destroy(currentCustomer);
        Instantiate(CustomerPrefab, SpawnPoint.position, SpawnPoint.rotation);
    }
}