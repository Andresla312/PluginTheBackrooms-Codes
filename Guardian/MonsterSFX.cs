using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSFX : MonoBehaviour
{
    public Transform player;    // Referencia al jugador.
    public AudioClip mute;    // Música de fondo.
    public AudioClip approachMusic;    // Música cuando el monstruo se acerca al jugador. 
    public AudioClip chaseMusic;    // Música cuando el monstruo detecta y persigue al jugador.

    private AudioSource audioSource;    // Fuente de audio para reproducir la música.
    private GuardianScript persecucion;    // Script del guardián que controla la persecución.
    private GameObject availableGuardians;    // Objeto que contiene a los guardianes disponibles.
    private CameraChange cameraFx;    // Efectos de cámara.

    private bool detected;     // Variable para controlar si el jugador ha sido detectado.

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();    // Obtiene el componente AudioSource del objeto de juego.
        persecucion = GetComponent<GuardianScript>();    // Obtiene el componente GuardianScript del objeto de juego.
        audioSource.clip = mute;    // Asigna la música de fondo al AudioSource.
        audioSource.Play();    // Comienza a reproducir la música de fondo.
        availableGuardians = GameObject.Find("GuardianBox");    // Busca el objeto de juego con el nombre "GuardianBox".

        cameraFx = GetComponent<CameraChange>();    // Obtiene el componente CameraChange del objeto de juego.
        if (cameraFx == null)
        {
            Debug.LogError("CameraChange component not found on this GameObject.");
        }

        detected = false;    // Inicializa detected a false.

        if (cameraFx != null)
        {
            cameraFx.deactivateEffect();    // Desactiva los efectos de cámara.
        }
    }

    IEnumerator CheckPlayerDistance()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);    // Calcula la distancia al jugador.

        if (distanceToPlayer < 12f && distanceToPlayer > 1f && !persecucion.following)    // Si el jugador está a una distancia entre 1 y 12 unidades y el monstruo no está persiguiendo al jugador.
        {
            ChangeMusic(approachMusic);    // Cambia la música a la música de aproximación.
        }
        else if (distanceToPlayer >= 12f)    // Si el jugador está a una distancia mayor o igual a 12 unidades.
        {
            ChangeMusic(mute);    // Cambia la música a la música de fondo.
        }
        else if (persecucion.following)    // Si el monstruo está siguiendo al jugador.
        {
            persecucion.following = false;
            ChangeMusic(chaseMusic);    // Cambia la música a la música de persecución.
        }

        yield return null;
    }

    void ChangeMusic(AudioClip newClip)    // Método para cambiar la música.
    {
        if (audioSource.clip != newClip)    // Si la música actual es diferente de la nueva música.
        {
            audioSource.Stop();    // Detiene la música actual.
            audioSource.clip = newClip;    // Asigna la nueva música al AudioSource.
            audioSource.Play();    // Comienza a reproducir la nueva música.
        }
    }

    private void Update()
    {
        StartCoroutine(CheckPlayerDistance());    // Inicia una coroutina para comprobar la distancia al jugador.
        if (Vector3.Distance(transform.position, player.position) < 12f && Vector3.Distance(transform.position, player.position) > 1f)    // Si el jugador está a una distancia entre 1 y 12 unidades.
        {
            detected = true; // Marca que el jugador ha sido detectado.
            if (cameraFx != null)
            {
                cameraFx.activateEffect(); // Activa los efectos de cámara.
            }
        }
        else if (detected)    // Si el jugador ha sido detectado.
        {
            detected = false; // Marca que el jugador ya no está detectado.
            if (cameraFx != null)
            {
                cameraFx.deactivateEffect(); // Desactiva los efectos de cámara.
            }
        }
    }

    private void OnDestroy()    // Método que se llama cuando el objeto de juego es destruido.
    {
        if (cameraFx != null)    // Si hay un componente CameraChange.
        {
            cameraFx.deactivateEffect();    // Desactiva los efectos de cámara.
        }
    }
}
