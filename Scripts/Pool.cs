using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UFR.ObjectPool
{
    public class Pool<T> where T : Object
    {
        private HashSet<T> _used { get; } = new HashSet<T>();
        private HashSet<T> _free { get; } = new HashSet<T>();
        private T _reference { get; }
        private int _count { get; set; }

        public Pool(T reference)
        {
            _reference = reference;
        }

        public T Spawn(bool silent = false)
        {
            T item;
            if (_free.Count > 0)
            {
                item = _free.FirstOrDefault();
                PoolEntity entity = item.ToGameObject().GetComponent<PoolEntity>();
                entity.Create(silent);
                _free.Remove(item);
            }
            else
            {
                _count++;
                item = Object.Instantiate(_reference);
                item.name = $" ==> {_reference.name.Substring(0, Mathf.Min(15, _reference.name.Length))}_{_count.ToString().PadLeft(4, '0')}";
                PoolEntity entity = item.ToGameObject().AddComponent<PoolEntity>();
                entity.Create(silent);
                entity.hideFlags = HideFlags.HideInInspector;
                entity.ReferenceType = _reference.GetType();
                entity.ReferenceInstanceId = _reference.GetInstanceID();
                entity.DespawnHandle = (x) =>
                {
                    entity.Recycle(x);
                    Despawn(item);
                };
            }
            _used.Add(item);
            return item;
        }

        public void Despawn(T obj)
        {
            if (_used.Remove(obj)) _free.Add(obj);
        }

        public void Clear()
        {
            while (_used.Count > 0)
            {
                T item = _used.FirstOrDefault();
                PoolEntity entity = item.ToGameObject().GetComponent<PoolEntity>();
                if (entity != null) entity.DespawnHandle(true);
                else Despawn(item);
            }
        }
    }
}