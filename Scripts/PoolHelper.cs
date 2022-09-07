using UnityEngine;

namespace UFR.ObjectPool
{
    internal static class PoolHelper
    {
        public static GameObject ToGameObject(this Object obj)
        {
            GameObject go = obj as GameObject;
            if (go == null)
            {
                Transform transform = obj as Transform;
                if (transform != null) go = transform.gameObject;
            }

            if (go == null)
            {
                Component component = obj as Component;
                if (component != null) go = component.gameObject;
            }

            return go;
        }
    }
}