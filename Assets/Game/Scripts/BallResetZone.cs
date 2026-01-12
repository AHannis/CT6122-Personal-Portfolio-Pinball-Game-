using UnityEngine;

public class BallResetZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ball"))
        {
            return;
        }

        Destroy(other.gameObject);

        if (Game.Instance != null)
        {
            Game.Instance.RequestBallReset();
        }
    }
}
