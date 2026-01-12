using UnityEngine;

public class Bumper : PinballInteractable
{
    [SerializeField] private new Light light;
    [SerializeField] private float bounceForce = 25f;
    [SerializeField] private float lightDuration = 0.2f;

    private float timeLeftLightShine;

    void Start()
    {
        if (light != null)
        {
            light.enabled = false;
        }
    }

    void Update()
    {
        if (timeLeftLightShine > 0f)
        {
            timeLeftLightShine -= Time.deltaTime;

            if (timeLeftLightShine <= 0f && light != null)
            {
                light.enabled = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!IsBall(collision.gameObject))
            return;

        if (TryGetGame(out Game game))
        {
            game.IncreaseScore(1500);
        }

        Rigidbody rb = collision.collider.attachedRigidbody;
        if (rb == null)
            return;

        Vector3 bounceDirection = collision.contacts[0].normal;
        rb.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);

        if (light != null)
        {
            light.enabled = true;
            timeLeftLightShine = lightDuration;
        }
    }
}
