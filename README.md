# Unity-ObjectPool
Pool object system for Unity3D

Spawn
```csharp
using UFR.ObjectPool;
using UnityEngine;

namespace Tank
{
    public class TankFireHandler : MonoBehaviour
    {
        [SerializeField] private TankController _tankController;
        [SerializeField] private Transform _projectilePrefab;

        public KeyCode Key;

        private void Update()
        {
            if (Input.GetKeyDown(Key))
            {
                Transform projectile = _projectilePrefab.PoolSpawn();
                projectile.SetPositionAndRotation(_tankController.TurretFirePoint, _tankController.TurretRotation);
            }
        }
    }
}
```

Depawn
```csharp
using UFR.ObjectPool;
using UnityEngine;

public class Projectile2 : MonoBehaviour, IPoolEntity
{
    [SerializeField] private float _speed = 30;

    private void Update()
    {
        transform.position = transform.position + _speed * Time.deltaTime * (transform.forward + new Vector3(Random.Range(-1f, 1), Random.Range(-1f, 1), Random.Range(-1f, 1)));
        if (transform.position.magnitude > 100) gameObject.PoolDepawn();
    }

    void IPoolEntity.Spawn(bool silent)
    {
        gameObject.SetActive(true);
        _speed = Random.Range(20, 60);
    }

    void IPoolEntity.Depawn(bool silent)
    {
        gameObject.SetActive(false);
        gameObject.GetComponentInChildren<TrailRenderer>().Clear();
    }
}
```

Pre-spawn 
```csharp
[SerializeField] private Transform _projectilePrefab;

private void Start()
{
    _projectilePrefab.PoolBuffer(30);
}
```

Depawn All
```csharp
[SerializeField] private Transform _projectilePrefab;

private void Start()
{
    _projectilePrefab.PoolClear();
}
``` 

Depawn Silent
```csharp
[SerializeField] private Transform _projectilePrefab;

private void Start()
{
    Transform projectile = _projectilePrefab.PoolSpawn();
    projectile.PoolDepawn(true);
}
``` 

