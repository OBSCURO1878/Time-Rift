using UnityEngine;

public class Coletaveis : MonoBehaviour
{
    private SpriteRenderer sr;
    private CircleCollider2D cicle;

    [Header("Configurações do Coletável")]
    public GameObject collected;
    public int scoreValue = 1; // Já começa com 1 por padrão

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        cicle = GetComponent<CircleCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        // Corrigido para CompareTag com C maiúsculo
        if (collider.gameObject.CompareTag("Player"))
        {
            if (sr != null) sr.enabled = false;
            if (cicle != null) cicle.enabled = false;

            if (collected != null)
            {
                collected.SetActive(true);
            }

            // Envia os pontos para o GameController
            if (GameController.instance != null)
            {
                GameController.instance.AdicionarPontos(scoreValue);
            }

            Destroy(gameObject, 0.25f);
        }
    }
}
