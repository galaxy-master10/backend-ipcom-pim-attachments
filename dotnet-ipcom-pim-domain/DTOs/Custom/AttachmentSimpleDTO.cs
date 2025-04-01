﻿namespace dotnet_ipcom_pim_domain.DTOs.Custom;

public class AttachmentSimpleDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string LanguageCode { get; set; } = null!;
    public bool? Published { get; set; }
    public int? Index { get; set; }
    public bool NoResize { get; set; }
    public long Size { get; set; }
    public DateOnly? ExpiryDate { get; set; }
    public List<ProductSimpleDTO> Products { get; set; } = new();
    public List<string> CategoryNames { get; set; } = new();
}