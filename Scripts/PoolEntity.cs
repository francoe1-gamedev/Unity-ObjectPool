using UnityEngine;

namespace UFR.ObjectPool
{
    [AddComponentMenu("")]
    public class PoolEntity : MonoBehaviour
    {
        public int ReferenceInstanceId { get; set; }
        public System.Type ReferenceType { get; set; }

        public delegate void DepawnDelegate(bool silent);

        public DepawnDelegate DepawnHandle { get; set; }

        public void Create(bool silent)
        {
            foreach (IPoolEntity entity in gameObject.GetComponents<IPoolEntity>())
                entity.Spawn(silent);
        }

        public void Recycle(bool silent)
        {
            foreach (IPoolEntity entity in gameObject.GetComponents<IPoolEntity>())
                entity.Depawn(silent);
        }
    }
}