using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSFX : MonoBehaviour
{
    [SerializeField] private AudioClip[] stoneFootstepsSFX;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Step()
    {
        audioSource.PlayOneShot(stoneFootstepsSFX[Random.Range(0, stoneFootstepsSFX.Length)]);
    }
}
