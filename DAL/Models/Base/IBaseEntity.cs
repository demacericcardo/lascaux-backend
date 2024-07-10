namespace DAL.Models.Base
{
    public interface IBaseEntity
    {
        DateTime CreatedDate { get; set; }
        DateTime? UpdatedDate { get; set; }
    }
}
