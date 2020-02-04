﻿using Cosmonaut.Attributes;
namespace trifenix.agro.db {
    public abstract class DocumentBase {

        public abstract string Id { get; set; }

        [CosmosPartitionKey]
        public string CosmosEntityName { get; set; }

        protected DocumentBase() {
            CosmosEntityName = GetType().Name;
        }

    }
}