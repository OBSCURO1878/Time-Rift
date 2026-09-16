using UnityEngine;

public class ZonaMorte : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        VidaPersonagem vida = other.GetComponent<VidaPersonagem>();

        if (vida != null)
        {
            vida.MorrerInstantaneamente();
        }
    }
}