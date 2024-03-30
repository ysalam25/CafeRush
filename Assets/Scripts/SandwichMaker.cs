using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandwichMaker : MonoBehaviour, IInteractable
{
    public bool isPicked = false;
    public string itemName;

    public void Interact()
    {
        //gameObject.SetActive(false);Component<Renderer>().enabled = false;
        // Loop through all children and disable their Renderer component
        foreach (Transform child in transform)
        {
            Renderer childRenderer = child.GetComponent<Renderer>();
            if (childRenderer != null)
            {
                childRenderer.enabled = false; // Hide the child object
            }
        }
        isPicked = true;
        Debug.Log("P");
    }

}
