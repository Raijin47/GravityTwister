using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    private Collider _collider;

    private void Awake() => _collider = GetComponent<Collider>();

    private void OnEnable()
    {
        if(_collider != null)
            _collider.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        _collider.enabled = false;
        Game.Action.SendLose();
    }
}