namespace BigProjectOne.Libraries.Models.Interfaces
{
    public interface ITenanciableModel : IModel
    {
        long TenantID { get; set; }
    }
}