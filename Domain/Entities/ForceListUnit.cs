
namespace Domain.Entities;
public class ForceListUnit  {
    public Guid Id { get; set; }
    public Guid ForceListId { get; set; }
    public ForceList ForceList { get; set; }

    public Guid UnitId { get; set; }
    public Unit Unit { get; set; }
}