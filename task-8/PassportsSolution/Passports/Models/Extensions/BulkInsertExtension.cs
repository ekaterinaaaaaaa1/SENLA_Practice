using EFCore.BulkExtensions;
using Passports.Database;

namespace Passports.Models.Extensions
{
    public static class BulkInsertExtension<T> where T : class
    {
        private const int BATCH_SIZE = 100000;

        public static void BulkInsertByBatchesAsync(ApplicationContext applicationContext, IEnumerable<T> collection)
        {
            int collectionSize = collection.Count();

            for (int i = 0; i < collectionSize; i += BATCH_SIZE)
            {
                applicationContext.BulkInsert(collection.Skip(i).Take(BATCH_SIZE));
            }
        }
    }
}
