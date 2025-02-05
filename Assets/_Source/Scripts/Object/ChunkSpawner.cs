using System.Collections.Generic;
using UnityEngine;

public class ChunkSpawner : MonoBehaviour
{
    [SerializeField] private PoolMember _chunk;
    private Pool _pool;
    private Vector3 _distance = new(0, 0, 35);
    private Vector3 _position;
    private readonly List<PoolMember> Chunks = new();

    private void Start()
    {
        _pool = new(_chunk);
        Game.Action.OnEnter += Action_OnEnter;
        Game.Action.OnExit += Action_OnExit;
    }

    private void Action_OnEnter()
    {
        _position = Vector3.zero;

        for (int i = 0; i < 4; i++)
            CreateNewChunk();
    }

    private void Spawn(Vector3 position)
    {
        var obj = _pool.Spawn(position);

        Chunks.Add(obj);
        obj.Die += Obj_Die;
    }

    public void CreateNewChunk()
    {
        _position += _distance;
        Spawn(_position);
    }

    private void Obj_Die(PoolMember obj) => Chunks.Remove(obj);

    private void Action_OnExit()
    {
        for(int i = Chunks.Count - 1; i >= 0; i--)       
            Chunks[i].ReturnToPool();
        
        _position = Vector3.zero;
    }
}