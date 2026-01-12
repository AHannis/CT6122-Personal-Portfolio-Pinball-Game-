using UnityEngine;

public abstract class PinballInteractable : MonoBehaviour
{
    protected bool IsBall(GameObject other)
    {
        return other.CompareTag("Ball");
    }

    protected bool TryGetGame(out Game game)
    {
        game = Game.Instance;

        if (game == null)
        {
            Debug.LogWarning("Game.Instance was null during interaction.", this);
            return false;
        }

        return true;
    }
}
