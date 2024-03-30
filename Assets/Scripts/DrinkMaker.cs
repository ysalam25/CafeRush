using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkMaker : MonoBehaviour, IInteractable
{
    public bool isPicked = false;
    public string itemName;

    public void Interact()
    {
        
        foreach (Transform child in transform)
        {
            Renderer childRenderer = child.GetComponent<Renderer>();
            if (childRenderer != null)
            {
                childRenderer.enabled = false; // Hide the child object
            }
        }
        isPicked = true;
        Debug.Log("L");
    }
}
