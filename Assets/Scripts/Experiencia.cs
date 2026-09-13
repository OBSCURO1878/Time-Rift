using UnityEngine;
using TMPro;

public class Experiencia : MonoBehaviour
{
    [Header("Experiência")]
    [SerializeField] private int experienciaAtual = 0;

    [Header("Interface")]
    [SerializeField] private TMP_Text textoXP;

    private void Start()
    {
        AtualizarTexto();
    }

    public void AdicionarXP(int quantidade)
    {
        experienciaAtual += quantidade;

        Debug.Log(
            "XP ganho: +" +
            quantidade +
            " | XP atual: " +
            experienciaAtual
        );

        AtualizarTexto();
    }

    private void AtualizarTexto()
    {
        if (textoXP != null)
        {
            textoXP.text = "XP: " + experienciaAtual;
        }
    }

    public int GetExperiencia()
    {
        return experienciaAtual;
    }
}