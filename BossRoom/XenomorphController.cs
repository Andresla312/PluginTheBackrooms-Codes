using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class XenomorphController : MonoBehaviour
{
    public bool following = false;
    public GameObject player;
    private RaycastHit hit;
    public float viewAngleHorizontal = 90f; // �ngulo de visi�n horizontal.
    public float viewAngleVertical = 10f; // �ngulo de visi�n vertical.
    public float maxDistance = 80f; // La distancia m�xima de visi�n.
    public float attackDistance = 10f;


    private Vector3 initialPosition; // La posici�n inicial del enemigo.
    private NavMeshAgent navMeshAgent; // El NavMeshAgent del enemigo.

    public Animator animator; 

    private void Start()
    {
        animator = GetComponent<Animator>(); // Obtiene el animator del enemigo 
        navMeshAgent = GetComponent<NavMeshAgent>(); // Obtiene el NavMeshAgent del enemigo.
        initialPosition = transform.position; // Guarda la posici�n inicial del enemigo.
        animator.SetTrigger("Idle"); // Activa la animaci�n Idle
    }

    private void Update()
    {
        Chase(); // Llama al m�todo 
    }

    void Chase() 
    {
        navMeshAgent.speed = 30f; // Velocidad del boss.
        int numRaysHorizontal = 100; // N�mero de rayos horizontales.
        int numRaysVertical = 20; // N�mero de rayos verticales.
        float angleBetweenRaysHorizontal = viewAngleHorizontal / numRaysHorizontal; // �ngulo entre rayos de forma horizontal.
        float angleBetweenRaysVertical = viewAngleVertical / numRaysVertical; // �ngulo entre rayos de forma vertical.

        for (int i = 0; i < numRaysHorizontal; i++) // Loop que hace los rayos horizontale
        {
            for (int j = 0; j < numRaysVertical; j++) // Loop que hace los rayos verticales 
            {
                float rayAngleHorizontal = -viewAngleHorizontal / 2 + angleBetweenRaysHorizontal * i; // �ngulo entre rayos de forma horizontal.
                float rayAngleVertical = -viewAngleVertical / 2 + angleBetweenRaysVertical * j; // �ngulo entre rayos de forma vertical.
                Vector3 rayDirection = Quaternion.Euler(rayAngleVertical, rayAngleHorizontal, 0) * transform.forward; // Le da direcci�n a la visi�n del boss. 

                float distance = maxDistance; // Todos los rayos tienen la misma longitud.

                if (Physics.Raycast(navMeshAgent.transform.position, rayDirection, out hit, distance)) // Si el boss activa su rango de visi�n...
                {
                    if (hit.transform.gameObject.CompareTag("JUGADOR")) // Si el raycast choca con el jugador...
                    {
                        navMeshAgent.SetDestination(hit.transform.position);
                        following = true; // Activa la variable 
                        navMeshAgent.speed = 30f; // La velocidad del boss
                        navMeshAgent.acceleration = 10f; // La aceleraci�n del boss

                        // Si el jugador est� dentro del rango de ataque, activa la animaci�n de ataque.
                        if (Vector3.Distance(transform.position, player.transform.position) <= attackDistance)
                        {
                            animator.SetTrigger("Attack"); // Se activa la animaci�n de atacar.
                        }
                        else // Si no, activa la animaci�n "BossRun".
                        {
                            animator.SetTrigger("BossRun"); // Se activa la animaci�n de correr.
                        }

                        return; // Salimos de la funci�n tan pronto como encontramos al jugador.
                    }
                }
            }
        }

        // Si el jugador no est� en el rango de visi�n, el enemigo regresa a su posici�n inicial.
        if (following && Vector3.Distance(transform.position, player.transform.position) > maxDistance)
        {
            navMeshAgent.SetDestination(initialPosition); // El boss se dirige a su posici�n inicial.
            following = false; // Se cambia de valor a la variable.
            animator.SetTrigger("BossRun"); // Se activa la animaci�n de correr. 
        }

        // Si el enemigo ha llegado a su posici�n inicial, activa la animaci�n "Idle".
        if (Vector3.Distance(transform.position, initialPosition) <= 0.5f)
        {
            animator.SetTrigger("Idle"); // Se activa la animaci�n Idle
        }
    }

    /*void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        int numRaysHorizontal = 100; // N�mero de rayos horizontales.
        int numRaysVertical = 20; // N�mero de rayos verticales.
        float angleBetweenRaysHorizontal = viewAngleHorizontal / numRaysHorizontal;
        float angleBetweenRaysVertical = viewAngleVertical / numRaysVertical;

        for (int i = 0; i < numRaysHorizontal; i++)
        {
            for (int j = 0; j < numRaysVertical; j++)
            {
                float rayAngleHorizontal = -viewAngleHorizontal / 2 + angleBetweenRaysHorizontal * i;
                float rayAngleVertical = -viewAngleVertical / 2 + angleBetweenRaysVertical * j;
                Vector3 rayDirection = Quaternion.Euler(rayAngleVertical, rayAngleHorizontal, 0) * transform.forward;

                float distance = maxDistance; // Todos los rayos tienen la misma longitud.

                Gizmos.DrawRay(navMeshAgent.transform.position, rayDirection * distance);
            }
        }
    }*/
}