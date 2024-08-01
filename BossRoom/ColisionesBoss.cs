using UnityEngine;


public class ColisionesBoss : MonoBehaviour
{
    HealthBar healthBar;
    public AudioSource ugh;

    private void Start()
    {
        healthBar = FindObjectOfType<HealthBar>(); // Encuentra el objeto con el script HealthBar y lo asigna a la variable healthBar.
        ugh.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Xenomorph") //Si el otro gameobject con el que colisiona es el Xenomorph...
        {
            ugh.enabled = true;
            ugh.Play();
            healthBar.currentHealth -= 20;
            Score.contador -= 50;
        }
    }
}