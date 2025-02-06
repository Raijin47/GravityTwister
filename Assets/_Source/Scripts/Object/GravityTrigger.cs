using UnityEngine;

public class GravityTrigger : MonoBehaviour
{
    public GravityHook Gravity { get; set; }
   
    [SerializeField] private ElectroFloor _floor;

    private Collider _collider;

    private readonly Vector3[] Pos = new Vector3[3]
    {
        new Vector3(-1.5f, 0, 0),
        new Vector3(1.5f, 0, 0),
        Vector3.zero
    };

    private void Awake() => _collider = GetComponent<Collider>();

    private void OnTriggerEnter(Collider other)
    {
        _collider.enabled = false;
        _floor.Disable();
      
        int r = Random.Range(0, 2);
        var gravity = Game.Locator.Gravity;

        switch (Gravity)
        {
            case GravityHook.Left:
                if (r == 0) gravity.ApplyUp();
                if (r == 1) gravity.ApplyDown();
                if (r == 2) gravity.ApplyRight();
                break;

            case GravityHook.Right:
                if (r == 0) gravity.ApplyUp();
                if (r == 1) gravity.ApplyDown();
                if (r == 2) gravity.ApplyLeft();
                break;

            case GravityHook.Up:
                if (r == 0) gravity.ApplyLeft();
                if (r == 1) gravity.ApplyDown();
                if (r == 2) gravity.ApplyRight();
                break;

            case GravityHook.Down:
                if (r == 0) gravity.ApplyUp();
                if (r == 1) gravity.ApplyLeft();
                if (r == 2) gravity.ApplyRight();
                break;
        }
    }

    private void OnEnable()
    {
        transform.localPosition = Pos[Random.Range(0, Pos.Length)];

        if (_collider != null)
            _collider.enabled = true;
    }
}

public enum GravityHook
{
    Left, Right, Up, Down
}