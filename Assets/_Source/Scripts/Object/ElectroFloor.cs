using UnityEngine;

public class ElectroFloor : MonoBehaviour
{
    [SerializeField] private GravityTrigger _trigger;
    [SerializeField] private DeathTrigger _deathTrigger;
    private bool _active;


    private void OnEnable()
    {
        Game.Locator.Gravity.OnChangeCling += Gravity_OnChangeCling;
        _active = true;
    }

    private void OnDisable()
    {
        Game.Locator.Gravity.OnChangeCling -= Gravity_OnChangeCling;
        _active = false;
    }

    public void Disable()
    {
        _active = false;
        _deathTrigger.Collider.enabled = false;
    }

    private void Gravity_OnChangeCling(Vector3 arg1, Vector3 arg2)
    {
        if (!_active) return;

        transform.localRotation = Quaternion.Euler(arg2);

        _trigger.Gravity = arg2.z switch
        {
            0 => GravityHook.Down,
            90 => GravityHook.Right,
            180 => GravityHook.Up,
            -90 => GravityHook.Left,

            _ => throw new System.NotImplementedException(),
        };
    }
}