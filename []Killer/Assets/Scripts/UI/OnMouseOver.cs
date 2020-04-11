using UnityEngine.EventSystems;
using UnityEngine;

[RequireComponent(typeof(SoundPlayer))]
public class OnMouseOver : MonoBehaviour, IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<SoundPlayer>().PlaySoundEvent("OnMouseOver");
    }
}
