namespace Util
{
    public interface IStateMachine
    {
        void StateEnter<T>(T component);
        void StateUpdate();
        void StateExit();
        void StatePause();
        void StateResum();
    }
}