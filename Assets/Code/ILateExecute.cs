namespace Code
{
    public interface ILateExecute: IController
    {
        void LateExecute(float deltaTime);
    }
}