namespace ToDo.Application.Common.Interfaces
{
    public interface IMap<TFrom, TTo>
    {
        TTo Map(TFrom from);
    }
}
