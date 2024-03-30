using UnityEngine;

public class PlayerRaycastInteract : MonoBehaviour
{
    [SerializeField] private float raycastRange;
    [SerializeField] private LayerMask layerMask;

    void Update()
    {
        
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        if (Physics.Raycast(ray, out hit, raycastRange, layerMask))
        {
            var interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {

                if (Input.GetButtonDown("Interact"))
                {
                    interactable.Interact();
                }
            }
        }
    }
}
