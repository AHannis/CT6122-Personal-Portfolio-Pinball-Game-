using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Vector2 previousPosition;

    [SerializeField] private float maxSpeed; // left untouched
    [SerializeField] private bool isMainBall = true;

    [Header("Stuck Detection")]
    [SerializeField] private float stuckSpeedThreshold = 0.05f;
    [SerializeField] private float timeBeforeReset = 2f;

    private Rigidbody rb;
    private bool hasRespawned = false;
    private float stuckTimer = 0f;

    void Awake()
    {
        if (!TryGetComponent(out rb))
        {
            Debug.LogError("Ball is missing a Rigidbody component.", this);
        }
    }

    void Start()
    {
        previousPosition = transform.position;
    }

    public void Shoot(float force)
    {
        if (rb == null)
        {
            return;
        }

        rb.AddForce(Vector3.up * force, ForceMode.Impulse);
    }

    void Update()
    {
        Vector2 position = transform.position;
        Vector2 speed = position - previousPosition;
        Vector2 rotationAxis = Vector2.Perpendicular(speed);

        transform.Rotate(
            new Vector3(rotationAxis.x, rotationAxis.y, 0),
            -speed.magnitude * 40f,
            Space.World
        );

        previousPosition = position;

        
        if (!hasRespawned && rb != null)
        {
            if (rb.linearVelocity.magnitude < stuckSpeedThreshold)
            {
                stuckTimer += Time.deltaTime;

                if (stuckTimer >= timeBeforeReset)
                {
                    ResetBall();
                }
            }
            else
            {
                stuckTimer = 0f;
            }
        }

        // Fell out of level
        if (transform.position.y < -7.11f && !hasRespawned)
        {
            ResetBall();
        }
    }

    private void ResetBall()
    {
        if (hasRespawned)
        {
            return;
        }

        hasRespawned = true;

        if (isMainBall)
        {
            if (Game.Instance != null)
            {
                Game.Instance.SpawnBall();
            }
            else
            {
                Debug.LogWarning("Game.Instance null when attempting to respawn ball.");
            }
        }

        Destroy(gameObject);
    }

    public void SetAsBonusBall()
    {
        isMainBall = false;
    }
}
