using System.Collections;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine;

public class ButtonsSFX : MonoBehaviour
{

    private AudioSource audioSource;
    [SerializeField] private AudioClip hoverFX;
    [SerializeField] private AudioClip pressedFX;
    // Start is called before the first frame update
    private void Awake()
    {

    }
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
