using UnityEngine.EventSystems;
using UnityEngine;

/**
 * Author:          Sebastian Przyszlak
 * Collaborators:   
 */
public class OnMouseOver : MonoBehaviour, IPointerEnterHandler
{
    SoundManager soundManager;

    private void Start () {
        soundManager = SoundManager.instance;
    }
    public void OnPointerEnter(PointerEventData eventData) {
        soundManager.PlaySoundOnDefaultSource ("OnMouseOver");
    }
}
