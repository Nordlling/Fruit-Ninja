namespace Main.Scripts.Infrastructure.States
{
    public interface IState
    {
        void Enter();
        void Exit();
    }
}