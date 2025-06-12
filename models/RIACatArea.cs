using System.ComponentModel.DataAnnotations;
public class RIACatArea
{
    [Key]
    public int IDArea { get; set; }
    public string AreaName { get; set; }
    public int StatusArea { get; set; }
    public DateTime CreateDate{ get; set; }
}
