using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Moves : MonoBehaviour
{
    public TextMeshProUGUI moves;    // Declaramos una variable pública de tipo TextMeshProUGUI.
    public GameController gameController;    // Declaramos una variable pública de tipo GameController.

    void Update()
    {
        moves.text = gameController.countGuesses.ToString();    // Asignamos el número de intentos del GameController a la propiedad text del TextMeshProUGUI moves.
    }
}