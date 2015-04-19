using UnityEngine;

public interface ICaseBehaviour
{
    int PositionX { get; }
    int PositionY { get; }
    bool HasStone { get; set; }
    bool IsObstacle { get; }

    void OnEnter(PlayerController player);
    void OnLeave(PlayerController player);
    void PutStone(PlayerController player);
    void SetPosition(int x, int y);

    TBehaviour ChangeBehaviour<TBehaviour>()
        where TBehaviour : CaseBehaviour<TBehaviour>;
}