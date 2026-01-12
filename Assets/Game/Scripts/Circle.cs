using UnityEngine;

public class Circle : PinballInteractable
{
    [SerializeField] public Light circleLight;
    [SerializeField] private float lightDuration = 0.3f;

    private float timer;

    void Start()
    {
        if (circleLight != null)
        {
            circleLight.enabled = false;
        }
    }

    void Update()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;

            if (timer <= 0f && circleLight != null)
            {
                circleLight.enabled = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsBall(other.gameObject))
            return;

        if (TryGetGame(out Game game))
        {
            game.IncreaseScore(300);
        }

        if (circleLight != null)
        {
            circleLight.enabled = true;
            timer = lightDuration;
        }
    }
}
