using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddButtons : MonoBehaviour
{
    
    [SerializeField]    // Atributo que permite que esta variable privada sea visible y editable desde el inspector de Unity.
    private Transform puzzleField;  // Declaramos una variable privada de tipo Transform.
    [SerializeField]
    private GameObject btn; // Declaramos una variable privada de tipo GameObject.
    private void Awake()    // Awake es una función de Unity que se llama cuando se carga una escena.
    {
        for (int i = 0; i < 18; i++)
        {
            GameObject button = Instantiate(btn);   // Creamos una nueva instancia del objeto de juego btn y lo asignamos a la variable button.
            button.name = "" + i; // Cambiamos el nombre del objeto de juego instanciado a la cadena de la iteración actual del bucle.

            // Establecemos el objeto puzzleField como el padre del objeto de juego instanciado.
            button.transform.SetParent(puzzleField, false);
        }
    }
}