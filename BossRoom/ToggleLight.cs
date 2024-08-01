using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleLight : MonoBehaviour
{
    public Light myLight; // La luz que quieres prender y apagar
    public KeyCode toggleKey; // La tecla que se usar� para prender y apagar la luz

    private void Update()
    {
        // Si el jugador presiona la tecla, cambiamos el estado de la luz
        if (Input.GetKeyDown(toggleKey))
        {
            myLight.enabled = !myLight.enabled;
        }
    }
}