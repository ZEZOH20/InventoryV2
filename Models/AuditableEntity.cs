using System.ComponentModel.DataAnnotations;
using InventoryV2.Shares;

namespace InventoryV2.Models;

public abstract class AuditableEntity
{
    
    //Timestamps 
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; private set; }

    //Created / Updated By (optional but very common)
    public string? CreatedBy { get; private set; }           // UserId or username
    public string? UpdatedBy { get; private set; }

    //IP addresses (very useful for audit & security)
    public string? CreatedIP { get; private set; }
    public string? UpdatedIP { get; private set; }

    // Soft delete
    public bool IsDeleted { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    public string? DeletedBy { get; private set; }

    //Concurrency token (prevents two users editing same row) 
    [Timestamp]
    public byte[] RowVersion { get; private set; } = null!;

    // Public methods – call these from your services / middleware 
    public void SetCreated(string userId, string? ip = null)
    {
        CreatedBy = userId;
        CreatedIP = ip?.Truncate(45); // IPv6 can be long
        CreatedAt = DateTime.UtcNow;
    }

    public void SetUpdated(string userId, string? ip = null)
    {
        UpdatedBy = userId;
        UpdatedIP = ip?.Truncate(45);
        UpdatedAt = DateTime.UtcNow;
    }

    public void SoftDelete(string userId, string? ip = null)
    {
        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;
        DeletedBy = userId;
        SetUpdated(userId, ip);
    }

    public void Restore()
    {
        IsDeleted = false;
        DeletedAt = null;
        DeletedBy = null;
    }
}