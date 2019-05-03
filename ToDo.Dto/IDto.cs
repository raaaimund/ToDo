namespace ToDo.Dto
{
    interface IDto<TPk>
    {
        TPk Id { get; set; }
    }
}