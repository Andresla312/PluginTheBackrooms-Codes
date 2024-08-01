using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timer;    // Declaramos una variable pública de tipo TextMeshProUGUI
    private float tiempoTranscurrido = 0;
    private bool isGameRunning = true;    // Esta variable se utiliza para controlar si el juego está en ejecución o no.


    // Update is called once per frame
    void Update()
    {
        if (isGameRunning)    // Si el juego está en ejecución.
        {
            tiempoTranscurrido += Time.deltaTime;    // Incrementamos tiempoTranscurrido por el tiempo que ha pasado desde el último frame.
            timer.text = Mathf.FloorToInt(tiempoTranscurrido).ToString();    // Convertimos tiempoTranscurrido a un entero y luego a una cadena, y lo asignamos al texto del timer.
        }
        
    }

    public void GameFinished()    // Método que se llama en GameController cuando el juego ha terminado.
    {
        isGameRunning = false;    // Marcamos que el juego ya no está en ejecución.
    }
}
