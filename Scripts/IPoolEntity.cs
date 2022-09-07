namespace UFR.ObjectPool
{
    public interface IPoolEntity
    {
        void Spawn(bool silent);

        void Despawn(bool silent);
    }
}