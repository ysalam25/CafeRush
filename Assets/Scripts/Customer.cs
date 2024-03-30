using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour, IInteractable
{
    public string GetPrompt()
    {
        return "Take Order";
    }

    public void Interact()
    {
        Debug.Log("Whats Up");
    }
}
