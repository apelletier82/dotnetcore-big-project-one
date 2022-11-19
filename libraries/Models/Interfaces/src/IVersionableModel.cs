namespace BigProjectOne.Libraries.Models.Interfaces
{
    public interface IVersionableModel : IModel
    {
        byte[] RowVersion { get; set; }
    }
}