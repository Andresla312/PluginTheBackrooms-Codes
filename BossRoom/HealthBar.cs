using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour
{
    public Slider healthBarSlider; // Referencia al componente Slider de la barra de vida en el UI.
    public TextMeshProUGUI healthValueText; // Referencia al componente TextMeshProUGUI para mostrar la vida como un n�mero.

    public int maxHealth; // Vida m�xima del jugador 
    public int currentHealth; // Vida actual del jugador
    private int minHealth = 0; // M�nimo de vida del jugador es 0

    private void Start()
    {
        currentHealth = maxHealth; // Iguala la vida actual del jugador con la vida m�xima disponible
    }

    private void Update()
    {
        healthValueText.text = currentHealth.ToString() + "/ " + maxHealth.ToString(); // Lo muestra en el canvas 
        healthBarSlider.value = currentHealth; // Actualiza la vida actual 
        healthBarSlider.maxValue = maxHealth; // Iguala el m�ximo de vida en el slider al valor de vida m�xima 

        if(currentHealth <= minHealth)
        {
            SceneManager.LoadScene(3); // Cambia de escena con �ndice 3
            Cursor.lockState = CursorLockMode.None; //Desactiva la funci�n que hace que el cursor se quede centrado en la pantalla
            Cursor.visible = true; //Hace al cursor visible
        }
    }
}

