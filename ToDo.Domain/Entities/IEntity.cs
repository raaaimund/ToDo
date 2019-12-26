namespace ToDo.Domain.Entities
{
    interface IEntity<TPk>
    {
        TPk Id { get; set; }
    }
}