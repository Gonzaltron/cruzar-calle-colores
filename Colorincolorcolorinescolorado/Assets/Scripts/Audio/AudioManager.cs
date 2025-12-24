using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioMixer Mixer;
    public AudioSource MusicJuke;
    public AudioSource SFXJuke;
    private CharacterController controller;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (SFXJuke == null)
            SFXJuke = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W))
        {
            SFXPlay();

        }
  
    }
    public void SFXPlay()
    {
        SFXJuke.Play();
    }
}
