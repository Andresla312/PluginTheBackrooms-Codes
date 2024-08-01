using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable2 : MonoBehaviour
{
    public Material glowMaterial; // Material brilloso de la caja
    public float glowIntensity = 5f; // Intensidad del brillo que emite el color de la caja
    public Color defaultEmissionColor; // Color que emite la caja cuando no ha sido activa
    public Color interactedEmissionColor; // Color que emite la caja cuando ya fue activada
    public Animator animator; // Tiene el control de las animaciones de la caja
    public KeyCode interactionKey; //Tecla con la que se activa la caja
    public HealthBar healthBar; // Referencia a la barra de vida del personaje

    [Header("----------Audio Source-----------")]
    [SerializeField]
    AudioSource effectSource; // Fuente de sonido para el clip

    [Header("----------Audio Clip-----------")]
    public AudioClip openCrate; // Efecto de sonido para abrir la caja

    private bool hasBeenOpened = false; // Variable para rastrear si la caja ya ha sido abierta
    private bool isPlayerInside = false; // Variable para rastrear si el jugador est� tocando de la caja

    private void Start()
    {
        glowMaterial.SetColor("_EmissionColor", defaultEmissionColor * glowIntensity); // Establece el color inicial de la caja
        effectSource = GetComponent<AudioSource>(); // Obtiene el AudioSource
        healthBar = FindObjectOfType<HealthBar>(); // Obtiene la barra de vida del jugador
        effectSource.clip = openCrate; // Iguala el clip que va a reproducir la fuente de audio a openCrate
    }

    private void Update()
    {
        if (Input.GetKeyDown(interactionKey) && !hasBeenOpened && isPlayerInside) // Si se presiona la tecla indicada y todav�a no se abre la caja y el jugador est� tocando la caja...
        {
            glowMaterial.SetColor("_EmissionColor", interactedEmissionColor * glowIntensity); // Establece el color de la caja activada 
            animator.SetTrigger("Open"); // Activa la animaci�n de abrir 
            effectSource.Play(); // Reproduce el efecto de sonido 

            if(healthBar.currentHealth < 100) // Si la vida actual del jugador en menor a 100 puntos 
            {
                healthBar.currentHealth += 10; // Recupera 10 puntos de vida
            }
            

            hasBeenOpened = true; // Pone el estado de la caja en activada
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("JUGADOR")) 
        {
            isPlayerInside = true; 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("JUGADOR")) // 
        {
            isPlayerInside = false; 
        }
    }
}
