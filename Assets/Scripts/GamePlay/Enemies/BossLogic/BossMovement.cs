using UnityEngine;

public abstract class BossMovement : MonoBehaviour
{
    [SerializeField] protected Transform[] positions;

    [SerializeField] protected PlayerBase player;

    public abstract void TeleportToAnotherPosition();
}
