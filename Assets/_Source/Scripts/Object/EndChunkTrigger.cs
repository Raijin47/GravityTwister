using UnityEngine;

public class EndChunkTrigger : MonoBehaviour
{
    [SerializeField] private Chunk _chunk;

    private void OnTriggerEnter(Collider other)
    {
        Game.Locator.Spawner.CreateNewChunk();
        _chunk.ReturnToPool();
    }
}