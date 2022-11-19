namespace BigProjectOne.Libraries.Models.Interfaces
{
    public interface ISoftDeletableModel : IModel
    {
        bool Deleted { get; set; }
        Audit Deletion { get; set; }
    }
}