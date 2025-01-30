using System.Collections.Generic;
using UnityEngine;

public class PlayerRagdoll : MonoBehaviour
{
    private const string _isRunningKey = "IsRunning";

    private BoxCollider _collider;

    private Animator _animator;
    private List<Rigidbody> _rigidbodies;
    private Coroutine _updateProcessCoroutine;


    private bool _isActive;

    public int Health { get; set; }

    public bool IsActive
    {
        get => _isActive;
        set
        {
            _isActive = value;

            _collider.enabled = value;
            foreach (Rigidbody rigidbody in _rigidbodies) rigidbody.isKinematic = value;
            _animator.enabled = value;
            _animator.SetBool(_isRunningKey, value);

            ReleaseCoroutine();
        }
    }

    public void Init()
    {
        _collider = GetComponent<BoxCollider>();
        _rigidbodies = new List<Rigidbody>(transform.GetChild(0).GetComponentsInChildren<Rigidbody>());
        _animator = GetComponent<Animator>();
    }

    private void ReleaseCoroutine()
    {
        if (_updateProcessCoroutine != null)
        {
            StopCoroutine(_updateProcessCoroutine);
            _updateProcessCoroutine = null;
        }
    }

    public void TakeDamage(Vector3 force, Vector3 hit, int damage)
    {
        Health -= damage;

        if(Health <= 0)
        {
            if(IsActive)
            {
                Game.Wallet.Add(5);
                IsActive = false;
            }

            _rigidbodies[Random.Range(0, _rigidbodies.Count^1)].AddForceAtPosition(force, hit, ForceMode.Impulse);
        }
    }
}
