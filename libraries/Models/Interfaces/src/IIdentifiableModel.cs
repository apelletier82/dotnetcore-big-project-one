namespace BigProjectOne.Libraries.Models.Interfaces
{
    public interface IIdentifiableModel : IModel
    {
        long ID { get; set; }
    }
}