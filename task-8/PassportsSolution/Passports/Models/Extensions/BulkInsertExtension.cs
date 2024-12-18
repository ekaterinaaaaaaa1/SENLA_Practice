﻿using EFCore.BulkExtensions;
using Passports.Database;

namespace Passports.Models.Extensions
{
    public static class BulkInsertExtension<T> where T : class
    {
        private const int BATCH_SIZE = 100000;

        public static async Task BulkInsertByBatchesAsync(ApplicationContext applicationContext, IEnumerable<T> collection, int batchSize = BATCH_SIZE)
        {
            await applicationContext.BulkInsertAsync(collection, new BulkConfig { BatchSize = batchSize });
        }
    }
}
