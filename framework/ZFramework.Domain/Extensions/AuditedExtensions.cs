using ZFramework.Common.Exceptions;

namespace ZFramework.Domain.Entities.Auditing
{
    public static class AuditedExtensions
    {
        public static void CheckConcurrencyUpdate(this IModificationAudited source, IModificationAudited destination)
        {
            if (source.LastModificationTime != destination.LastModificationTime)
            {
                throw new ConcurrentUpdateException("Error. El registro que se intenta modificar fue modificado por otro proceso después de que se obtuviera el valor original. Operación abortada.");
            }
        }
    }
}