using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSFX : MonoBehaviour
{

    private AudioSource audioSource;
    [SerializeField] private AudioClip hoverFX;
    [SerializeField] private AudioClip pressedFX;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
