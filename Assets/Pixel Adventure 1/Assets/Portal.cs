using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            int proximaFase = SceneManager.GetActiveScene().buildIndex + 1;

            if (proximaFase < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(proximaFase);
            }
            else
            {
                // Quando tiver mais fases, descomente a linha abaixo e apague a de cima:
                // Debug.Log("Última fase! Aqui você pode ir pro menu principal.");
                // SceneManager.LoadScene(0); // volta ao menu principal

                // TEMPORÁRIO para teste — remove quando criar a fase 2:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}