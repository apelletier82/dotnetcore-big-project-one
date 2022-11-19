namespace BigProjectOne.Libraries.Models.Interfaces
{
    public interface IAuditableModel : IModel
    {
        Audit Creation { get; set; }
        Audit Change { get; set; }
    }
}