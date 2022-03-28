using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

abstract public class BellyEnemy : MonoBehaviour
{
    protected NavMeshAgent nav;
    protected GameObject playerObject;

    protected float distance;

    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        playerObject = GameObject.Find("Player");
        setSpeed();
    }

    void Update()
    {
        distance = Vector3.Distance(transform.position, playerObject.transform.position);
        updateAttack();
    }

    protected virtual void setSpeed()
    {
        nav.speed = 10f;
    }

    protected abstract void updateAttack();

    protected void OnDrawGizmos()
    {
        nav = GetComponent<NavMeshAgent>();
        playerObject = GameObject.Find("Player");

        Handles.color = Color.cyan;
        for (int i = 0; i < nav.path.corners.Length - 1; i++)
            Handles.DrawDottedLine(nav.path.corners[i], nav.path.corners[i + 1], 4.0f);

        Handles.color = Color.white;

        float distance = Vector3.Distance(transform.position, playerObject.transform.position);
        Vector3 direction = playerObject.transform.position - transform.position;

        Gizmos.DrawRay(transform.position, direction);

        Vector3 textPos = Vector3.Lerp(transform.position, playerObject.transform.position, 0.5f);

        textPos.y = textPos.y + 0.2f;
        Handles.Label(textPos, distance.ToString());

    }
}
