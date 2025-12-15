using System;
using InventoryV2.Data.DbContexts;
using InventoryV2.Interfaces.IServices;
using InventoryV2.Migrations;
using InventoryV2.Models;

namespace InventoryV2.Services;

public class AuditableEntityService
{
      readonly SqlDbContext _context;
      readonly ICurrentUserService _currentUserService;
        public AuditableEntityService(SqlDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task SoftDeleteAsync<T>(T entity,CancellationToken cancellationToken = default) where T : AuditableEntity
        {
            entity.SoftDelete(_currentUserService.UserId, _currentUserService.UserIp);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task RestoreAsync<T>(T entity,CancellationToken cancellationToken = default) where T : AuditableEntity
        {
            entity.Restore();
            await _context.SaveChangesAsync(cancellationToken);
        }

}
