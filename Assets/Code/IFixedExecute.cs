namespace Code
{
    public interface IFixedExecute: IController
    {
        void FixedExecute(float deltaTime);
    }
}