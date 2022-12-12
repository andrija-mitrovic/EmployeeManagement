namespace Microsoft.EntityFrameworkCore.ChangeTracking
{
    internal static class EntityExtension
    {
        public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
            entry.References.Any(x =>
                x.TargetEntry != null &&
                x.TargetEntry.Metadata.IsOwned() &&
                (x.TargetEntry.State == EntityState.Added || x.TargetEntry.State == EntityState.Modified));
    }
}
