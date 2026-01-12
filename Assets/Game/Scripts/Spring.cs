using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    [Header("Plunger Settings")]
    public float maxPull = 2f;        
    public float pullSpeed = 2.5f;      
    public float maxForce = 200f;        
    public float detectionRadius = 2.5f;

    float currentPull = 0f;
    Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        // pull plunger down
        if (Input.GetKey(KeyCode.Space))
        {
            currentPull += Time.deltaTime * pullSpeed;
            currentPull = Mathf.Clamp(currentPull, 0f, maxPull);

            UpdatePlungerVisual();
        }

        // on release space fire
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Fire();
            ResetPlunger();
        }
    }

    void UpdatePlungerVisual()
    {
        Vector3 pos = originalPosition;
        pos.y -= currentPull;   
        transform.localPosition = pos;
    }

    void Fire()
    {
        float force = (currentPull / maxPull) * maxForce;

        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);

        foreach (Collider col in colliders)
        {
            Ball ball = col.GetComponentInParent<Ball>();
            if (ball != null)
            {
                ball.Shoot(force);
            }
        }
    }

    void ResetPlunger()
    {
        currentPull = 0f;
        transform.localPosition = originalPosition;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, 1.5f);
    }
}


