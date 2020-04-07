using UnityEngine.EventSystems;
using UnityEngine;

public class OnMouseOver : MonoBehaviour, IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        FindObjectOfType<AudioManager>().Play("ButtonHoverSound");
    }
}
