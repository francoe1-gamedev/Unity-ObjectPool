namespace UFR.ObjectPool
{
    public interface IPoolEntity
    {
        void Spawn(bool silent);

        void Depawn(bool silent);
    }
}