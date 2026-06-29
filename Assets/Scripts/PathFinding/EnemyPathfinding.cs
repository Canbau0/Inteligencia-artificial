using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [Header("Referencias")]
    public Transform player;
    
    [HideInInspector]
    public List<Node> path;

    //Se guardan todos los nodos una vez la iniico
    private Node[] allNodes;

    private void Start()
    {        
        allNodes = FindObjectsByType<Node>(FindObjectsSortMode.None);
    }

    public void CalculatePath()
    {        
        if (player == null || allNodes == null || allNodes.Length == 0) return;

        //el nodo más cercano al enemigo
        Node startNode = GetClosestNode(transform.position);

        //el nodo más cercano al jugador
        Node goalNode = GetClosestNode(player.position);

        if (startNode == null || goalNode == null) return;

        path = Dijkstra.Run(
            startNode,
            node => node == goalNode,
            node => node.neightbourds,
            (a, b) => Vector3.Distance(
                a.transform.position,
                b.transform.position)
        );
    }
    
    private Node GetClosestNode(Vector3 targetPosition)
    {
        Node closestNode = null;
        float minDistance = Mathf.Infinity;

        foreach (Node node in allNodes)
        {
            //la distancia entre el objetivo y el nodo actual
            float distance = Vector3.Distance(targetPosition, node.transform.position);

            //Si es la menor distancia registrada hasta ahora se guarda ekl nodo
            if (distance < minDistance)
            {
                minDistance = distance;
                closestNode = node;
            }
        }

        return closestNode;
    }
}