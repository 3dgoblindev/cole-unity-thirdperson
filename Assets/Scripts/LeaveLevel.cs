using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaveLevel : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("WinScene");

        }
    }
}
