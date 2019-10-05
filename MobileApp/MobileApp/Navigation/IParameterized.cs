namespace MobileApp.Navigation
{
    public interface IParameterized<TParameter>
    {
        void HandleParameter(TParameter parameter);
    }
}
