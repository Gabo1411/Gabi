using UnityEngine;
using UnityEngine.AI; // Necesario para la IA

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyChase : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Buscar al jugador si no está asignado
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        // PARCHE DE SEGURIDAD:
        // Si el agente no está "pegado" al NavMesh, no intentamos moverlo
        // para evitar el error rojo.
        if (!agent.isOnNavMesh)
        {
            return;
        }

        agent.SetDestination(player.position);
    }
}