using GosteriZamani.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace GosteriZamani.API.Interceptors;

public class AuditInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        var context = eventData.Context;
        if (context is null)
        {
            return new ValueTask<InterceptionResult<int>>(result);
        }
        var entries = context.ChangeTracker.Entries<IAuditableEntity>();
        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.CreatedBy = "System"; // Replace with actual user identifier
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    entry.Entity.UpdatedBy = "System"; // Replace with actual user identifier
                    break;
                case EntityState.Deleted:
                    entry.State = EntityState.Modified; // Change to Modified to set DeletedAt and DeletedBy
                    entry.Entity.DeletedAt = DateTime.UtcNow;
                    entry.Entity.DeletedBy = "System"; // Replace with actual user identifier
                    break;
            }
        }
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
