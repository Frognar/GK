using UnityEngine.EventSystems;
using UnityEngine;

/**
 * Author:          Sebastian Przyszlak
 * Collaborators:   
 */
[RequireComponent (typeof(SoundPlayer))]
public class OnMouseOver : MonoBehaviour, IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<SoundPlayer>().PlaySoundEvent("OnMouseOver");
    }
}
