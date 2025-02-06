using UnityEngine;

public class ObstacleHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] _obstacles;
    private int _current;

    private void OnEnable() => Show();

    private void OnDisable() => Hide();

    public void Show()
    {
        _current = Random.Range(0, _obstacles.Length);
        _obstacles[_current].SetActive(true);
    }

    public void Hide()
    {
        _obstacles[_current].SetActive(false);
    }
}