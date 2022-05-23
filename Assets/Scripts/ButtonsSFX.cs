using UnityEngine;

public class ButtonsSFX : MonoBehaviour
{ // play audio sounds when interacting with buttons


    private AudioSource audioSource;
    [SerializeField] private AudioClip hoverFX;
    [SerializeField] private AudioClip pressedFX;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void HoverButton()
    {
        audioSource.PlayOneShot(hoverFX);
    }
    public void PressButton()
    {
        audioSource.PlayOneShot(pressedFX);
    }
}
