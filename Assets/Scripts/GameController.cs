using UnityEngine;
using TMPro; // Necessário para controlar o texto do TextMeshPro

public class GameController : MonoBehaviour
{
    public static GameController instance;

    [Header("Pontuação / Experiência")]
    [SerializeField] private int totalScore = 0;

    [Header("Interface (UI)")]
    // Arraste o seu componente de texto (XP: 0) para cá no Inspector
    [SerializeField] private TMP_Text textoXP;

    private void Awake()
    {
        // Configuração padrão do Singleton
        if (instance == null)
        {
            instance = this;
            // Opcional: Garante que os pontos fiquem salvos se você mudar de fase
            // DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        AtualizarTextoUI();
    }

    // Método que qualquer coletável do jogo pode chamar
    public void AdicionarPontos(int quantidade)
    {
        totalScore += quantidade;

        Debug.Log("Pontos ganhos: +" + quantidade + " | Total: " + totalScore);

        AtualizarTextoUI();
    }

    // Atualiza o texto diretamente na tela do jogador
    private void AtualizarTextoUI()
    {
        if (textoXP != null)
        {
            textoXP.text = "XP: " + totalScore;
        }
    }

    // Caso algum outro script precise ler o valor atual dos pontos
    public int GetTotalScore()
    {
        return totalScore;
    }
}
