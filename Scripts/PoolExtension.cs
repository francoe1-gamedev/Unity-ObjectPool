using System.Collections.Generic;
using UnityEngine;

namespace UFR.ObjectPool
{
    public static class PoolExtension
    {
        private static Dictionary<int, object> _pools { get; } = new Dictionary<int, object>();

        private static Pool<T> FindPool<T>(this T obj) where T : Object
        {
            if (!_pools.ContainsKey(obj.GetInstanceID())) _pools.Add(obj.GetInstanceID(), new Pool<T>(obj));
            return (Pool<T>)_pools[obj.GetInstanceID()];
        }

        public static T PoolSpawn<T>(this T obj) where T : Object
        {
            return obj.FindPool().Spawn();
        }

        public static void PoolBuffer<T>(this T obj, int size) where T : Object
        {
            Pool<T> pool = obj.FindPool();
            Queue<T> items = new Queue<T>();
            while (items.Count < size) items.Enqueue(pool.Spawn(true));
            while (items.Count > 0) items.Dequeue().PoolDepawn();
        }

        public static void PoolClear<T>(this T obj) where T : Object
        {
            obj.FindPool().Clear();
        }

        public static IEnumerable<T> PoolSpawn<T>(this T obj, int copies) where T : Object
        {
            for (int i = 0; i < copies; i++)
                yield return obj.PoolSpawn();
        }

        public static void PoolDepawn<T>(this T obj, bool silent = false) where T : Object
        {
            PoolEntity entity = obj.ToGameObject().GetComponent<PoolEntity>();
            if (entity == null)
            {
                Object.Destroy(obj);
                return;
            }
            entity.DepawnHandle(silent);
        }
    }
}