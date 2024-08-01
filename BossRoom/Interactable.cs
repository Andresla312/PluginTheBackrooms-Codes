using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    public Material glowMaterial; // Material brilloso de la caja
    public float glowIntensity = 5f; // Intensidad del brillo que emite el color de la caja
    public Color defaultEmissionColor; // Color que emite la caja cuando no ha sido activa
    public Color interactedEmissionColor; // Color que emite la caja cuando ya fue activada 
    public Animator animator; // Tiene el control de las animaciones de la caja
    public KeyCode interactionKey; //Tecla con la que se activa la caja
    public Gun gun; // Referencia al script Gun, para acceder al n�mero de balas 

    [Header("----------Audio Source-----------")]
    [SerializeField]
    AudioSource effectSource; // Fuente de sonido para el clip

    [Header("----------Audio Clip-----------")]
    public AudioClip openCrate; // Efecto de sonido para abrir la caja

    private bool hasBeenOpened = false; // Variable para rastrear si la caja ya ha sido abierta
    private bool isPlayerInside = false; // Variable para rastrear si el jugador est� dentro de la caja

    private void Start()
    {
        glowMaterial.SetColor("_EmissionColor", defaultEmissionColor * glowIntensity); // Establece el color inicial de la caja
        effectSource = GetComponent<AudioSource>(); // Obtiene el AudioSource
        effectSource.clip = openCrate; // Iguala el clip que va a reproducir la fuente de audio a openCrate
    }

    private void Update()
    {
        if (Input.GetKeyDown(interactionKey) && !hasBeenOpened && isPlayerInside) // Si se presiona la tecla indicada y todav�a no se abre la caja y el jugador est� tocando la caja...
        {
            glowMaterial.SetColor("_EmissionColor", interactedEmissionColor * glowIntensity); // Establece el color de la caja activada
            animator.SetTrigger("Open"); // Activa la animaci�n de abrir 
            effectSource.Play(); // Reproduce el efecto de sonido

            gun.AddAmmo(3); // Te suma 3 municiones

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
        if (other.gameObject.CompareTag("JUGADOR"))
        {
            isPlayerInside = false;
        }
    }
}