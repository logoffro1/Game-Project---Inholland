using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    [SerializeField] private List<AudioClip> footstepSounds = new List<AudioClip>();
    private PlayerMovement playerMovement;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
/*        if (playerMovement.IsMoving() && !audioSource.isPlaying)
        {
            PlayFootStepAudio();
        }*/
    }
    private void PlayFootStepAudio()
    {
        if (!playerMovement.IsGrounded) return;

        //pick & play random footstep sound from list
        // excluding sound at index 0
        int n = Random.Range(1, footstepSounds.Count);
        audioSource.clip = footstepSounds[n];
        audioSource.PlayOneShot(audioSource.clip);

        //move picked sound to index 0 so its not picked next time
        footstepSounds[n] = footstepSounds[0];
        footstepSounds[0] = audioSource.clip;
    }

}
