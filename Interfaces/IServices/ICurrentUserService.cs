using System;

namespace InventoryV2.Interfaces.IServices;

public interface ICurrentUserService
{
   string? UserId{ get; }
   string? UserRole{ get; }
   string? UserIp{ get; }
}


