using System.Net.NetworkInformation;
using UnityEngine;

public class Chica : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] AudioSource audio;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //mira la tag del player
        if (other.CompareTag("Player"))
        {
            print("kneel");
            animator.SetBool("Kneel", true);
            audio.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //mira la tag del player
        if (other.CompareTag("Player"))
        {
            print("No kneel");
            animator.SetBool("Kneel", false);
            audio.Stop();
        }
    }
}
