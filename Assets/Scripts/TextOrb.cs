using UnityEngine;
using TMPro; // Obligatorio
using UnityEngine.SceneManagement;
public class TextOrb : MonoBehaviour
{
    // Usar TMP_Text funciona tanto para objetos 3D como para UI
    [SerializeField] private TMP_Text textDisplay;

    [SerializeField] private string message;
    [SerializeField] private float duration = 3f;
    [SerializeField] bool destroy = false;
    [SerializeField] bool mortal = false;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopAllCoroutines(); // Evita que se solapen si entras varias veces
            StartCoroutine(ShowAndHideRoutine());
            if (destroy)
            {
                //gameObject.SetActive(false);
                EnemyMovement movement = GetComponent<EnemyMovement>();
                movement.stop = true;
            }
                
        }
    }

    System.Collections.IEnumerator ShowAndHideRoutine()
    {
        textDisplay.text = message;
        yield return new WaitForSeconds(duration);

        if (textDisplay.text == message)
        {
            textDisplay.text = "";
        }

        if (destroy) 
        { 
            Destroy(gameObject);
        }
        //reset scene
        if (mortal)
        {
                SceneManager.LoadScene("MainMenu");
        }
    }
}