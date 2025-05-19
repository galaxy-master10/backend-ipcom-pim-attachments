namespace dotnet_ipcom_pim_domain.Entities;

public class ProductCharacteristic
{
    public Guid Id { get; set; }
    public string? Width { get; set; }
    public string? Length { get; set; }
    public string? Thickness { get; set; }
    public string? Diameter { get; set; }
    public Guid Product_Id { get; set; }
    public Guid ProductCode_Id { get; set; }
    public string? Lambda { get; set; }
    public string? R { get; set; }
    public string? FireClass { get; set; }
    public string? EdgeFinish { get; set; }
    public string? PressureStrength { get; set; }
    public string? Coating { get; set; }
    public string? Density { get; set; }
    public string? Volume { get; set; }
    public string? Weight { get; set; }
    public string? CoolingCapacity { get; set; }
    public string? HeatingCapacity { get; set; }
    public string? PipeDiameter { get; set; }
    public string? RefrigerantType { get; set; }
    public string? DimensionsInnerUnit { get; set; }
    public string? DimensionsOuterUnit { get; set; }
    public string? EnergyEfficiency { get; set; }
    
    public virtual Product Product { get; set; } = null!;
    public virtual ProductCode ProductCode { get; set; } = null!;
    
}