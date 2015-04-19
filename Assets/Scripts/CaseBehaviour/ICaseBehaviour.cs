using UnityEngine;

public interface ICaseBehaviour
{
    int PositionX { get; }
    int PositionY { get; }
    bool HasStone { get; }
    bool IsObstacle { get; }

    void onEnter(GameObject player);
    void onLeave(GameObject player);
    void putStone();
    void setPosition(int x, int y);
}