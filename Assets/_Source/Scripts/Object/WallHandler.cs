using UnityEngine;

public class WallHandler : MonoBehaviour
{
    [SerializeField] private Transform[] _walls;

    private readonly Vector3[] AngleRot = new Vector3[4]
    {
        new Vector3(0, 0, 90),
        new Vector3(0, 0, -90),
        new Vector3(0, 0, 180),
        Vector3.zero
    };

    private void OnEnable()
    {
        for (int i = 0; i < _walls.Length; i++)      
            _walls[i].localRotation = Quaternion.Euler(AngleRot[Random.Range(0, AngleRot.Length)]);       
    }
}