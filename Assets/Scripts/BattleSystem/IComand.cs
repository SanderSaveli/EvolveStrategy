namespace BattleSystem
{
    public interface IComand
    {
        public float progress {get;}

        public delegate void ComandEnd(IComand comand);
        public event ComandEnd OnComandEnd;
    }
}
