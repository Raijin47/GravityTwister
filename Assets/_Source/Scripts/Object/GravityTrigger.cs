using UnityEngine;

public class GravityTrigger : MonoBehaviour
{
    [SerializeField] private GravityHook _gravity;

    private void OnTriggerEnter(Collider other)
    {
        switch (_gravity)
        {
            case GravityHook.Left:
                Game.Locator.Gravity.ApplyLeft();
                break;
            case GravityHook.Right:
                Game.Locator.Gravity.ApplyRight();
                break;
            case GravityHook.Up:
                Game.Locator.Gravity.ApplyUp();
                break;
            case GravityHook.Down:
                Game.Locator.Gravity.ApplyDown();
                break;
        }
    }
}

public enum GravityHook
{
    Left, Right, Up, Down
}