using UnityEngine;

public class Interactable : MonoBehaviour
{
    public GameObject interactionUI;

    public virtual void ShowUI()
    {
        interactionUI.SetActive(true);
    }

    public virtual void HideUI()
    {
        interactionUI.SetActive(false);
    }

    public virtual void Interact()
    {

    }
}