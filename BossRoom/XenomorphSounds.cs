using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XenomorphSounds : MonoBehaviour
{
    public AudioClip[] sounds; // Los sonidos que puede emitir el enemigo.
    public AudioClip deathSound; // Sonido que emite el enemigo cuando muere.
    public float minTime = 1f; // El tiempo manimo entre sonidos.
    public float maxTime = 5f; // El tiempo maximo entre sonidos.
    public BossHealthBar bossHealthBar; // Referencia a la barra de vida del Boss

    private AudioSource audioSource; // El AudioSource del enemigo.

    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Obtiene el AudioSource del enemigo.
        StartCoroutine(PlaySounds()); // Inicia la corutina...
    }

    IEnumerator PlaySounds() // Corutina 
    {
        while (bossHealthBar.currentBossHealth > 0) // Mientras la vida actual del boss sea mayor a cero
        {
            float delay = Random.Range(minTime, maxTime); // Se establece una variable que lleva el tiempo en un intervalo random
            yield return new WaitForSeconds(delay); // Espera seg�n el intervalo random  

            if (bossHealthBar.currentBossHealth == 0) // Si la vida del boss es igual a 0... 
            {
                break; // Salimos del bucle si la vida del enemigo es 0.
            }

            int soundIndex = Random.Range(0, sounds.Length); // Variable random que tiene un intervalo entre 0 y el n�mero de sonidos en el arreglo.
            audioSource.clip = sounds[soundIndex]; // La fuente de audio se prepara para reproducir el sonido random que salga de soundIndex.
            audioSource.Play(); // La fuente de audio reproduce el sonido.
        }
        
        audioSource.Stop(); // Detiene la reproducci�n de sonidos aleatorios.
        audioSource.PlayOneShot(deathSound); // Reproduce el sonido de muerte inmediatamente.
    }
}
