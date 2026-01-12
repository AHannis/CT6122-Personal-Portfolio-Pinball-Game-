using UnityEngine;

public class Triple : PinballInteractable
{
    [SerializeField] private Transform spawnPosition1;
    [SerializeField] private Transform spawnPosition2;

    private bool hasTriggered = false;

    private void Awake()
    {
        if (spawnPosition1 == null || spawnPosition2 == null)
        {
            Debug.LogError("Spawn positions not assigned on Triple.", this);
            enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasTriggered)
        {
            return;
        }

        if (!IsBall(collision.gameObject))
        {
            return;
        }

        if (!TryGetGame(out Game game))
        {
            return;
        }

        if (game.PinBall1 == null)
        {
            Debug.LogWarning("PinBall prefab missing on Game.", this);
            return;
        }

        hasTriggered = true;

        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");

        if (balls.Length < 3)
        {
            Instantiate(game.PinBall1, spawnPosition1.position, Quaternion.identity);
            Instantiate(game.PinBall1, spawnPosition2.position, Quaternion.identity);
        }
    }
}
