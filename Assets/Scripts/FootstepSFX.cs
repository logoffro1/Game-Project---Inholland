using UnityEngine;

public class FootstepSFX : MonoBehaviour
{
    [SerializeField] private AudioClip[] stoneFootstepsSFX;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Step() //called from the animator
    {
        //play a random footstep sound
        audioSource.PlayOneShot(stoneFootstepsSFX[Random.Range(0, stoneFootstepsSFX.Length)]);
    }
}
