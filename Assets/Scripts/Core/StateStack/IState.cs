
namespace Core.StateStack
{
    public interface IState<T> where T : StateContext
    {
        void OnEnter(T context);
        void OnExit(T context);
        bool OnUpdate(float dt, T context);
        void OnInputHandle(T context);
    }
}