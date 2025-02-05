using UnityEngine;

public class CubeHandler : MonoBehaviour
{
    [SerializeField] private Transform[] _cubes;
    [SerializeField] private Transform[] _pivots;

    private readonly Vector3[] AngleRot = new Vector3[4]
    {
        new Vector3(0, 0, 90),
        new Vector3(0, 0, -90),
        new Vector3(0, 0, 180),
        Vector3.zero
    };

    private readonly Vector3[] Pos = new Vector3[3]
    {
        new Vector3(-1.5f, 0, 0),
        new Vector3(1.5f, 0, 0),
        Vector3.zero
    };

    private void OnEnable()
    {
        for(int i = 0; i < _cubes.Length; i++)
        {
            _cubes[i].localRotation = Quaternion.Euler(AngleRot[Random.Range(0, AngleRot.Length)]);
            _pivots[i].localPosition = Pos[Random.Range(0, Pos.Length)];
        }
    }
}