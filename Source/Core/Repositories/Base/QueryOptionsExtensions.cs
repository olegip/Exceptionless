using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;

namespace Exceptionless.Core.Repositories {
    public static class FindOptionsExtensions {
        public static T WithId<T>(this T options, string id) where T: QueryOptions {
            options.Ids.Add(id);
            return options;
        }

        public static T WithIds<T>(this T options, params string[] ids) where T : QueryOptions {
            options.Ids.AddRange(ids.Distinct());
            return options;
        }

        public static T WithIds<T>(this T options, IEnumerable<string> ids) where T : QueryOptions {
            options.Ids.AddRange(ids.Distinct());
            return options;
        }

        public static T WithOrganizationId<T>(this T options, string id) where T : QueryOptions {
            options.OrganizationIds.Add(id);
            return options;
        }

        public static T WithOrganizationIds<T>(this T options, params string[] ids) where T : QueryOptions {
            options.OrganizationIds.AddRange(ids.Distinct());
            return options;
        }

        public static T WithOrganizationIds<T>(this T options, IEnumerable<string> ids) where T : QueryOptions {
            options.OrganizationIds.AddRange(ids.Distinct());
            return options;
        }

        public static T WithProjectId<T>(this T options, string id) where T : QueryOptions {
            options.ProjectIds.Add(id);
            return options;
        }

        public static T WithProjectIds<T>(this T options, params string[] ids) where T : QueryOptions {
            options.ProjectIds.AddRange(ids.Distinct());
            return options;
        }

        public static T WithProjectIds<T>(this T options, IEnumerable<string> ids) where T : QueryOptions {
            options.ProjectIds.AddRange(ids.Distinct());
            return options;
        }

        public static T WithStackId<T>(this T options, string id) where T : QueryOptions {
            options.StackIds.Add(id);
            return options;
        }

        public static T WithStackIds<T>(this T options, params string[] ids) where T : QueryOptions {
            options.StackIds.AddRange(ids.Distinct());
            return options;
        }

        public static T WithStackIds<T>(this T options, IEnumerable<string> ids) where T : QueryOptions {
            options.StackIds.AddRange(ids.Distinct());
            return options;
        }

        public static T WithQuery<T>(this T options, IMongoQuery query) where T : QueryOptions {
            options.Query = query;
            return options;
        }

        public static T WithReadPreference<T>(this T options, ReadPreference readPreference) where T : QueryOptions {
            options.ReadPreference = readPreference;
            return options;
        }
        
        public static T WithSort<T>(this T options, IMongoSortBy sort) where T : OneOptions {
            options.SortBy = sort;
            return options;
        }

        public static T WithCacheKey<T>(this T options, string cacheKey) where T: OneOptions {
            options.CacheKey = cacheKey;
            return options;
        }

        public static T WithLimit<T>(this T options, int? limit) where T : MultiOptions {
            options.Limit = limit;
            return options;
        }

        public static T WithExpiresAt<T>(this T options, DateTime? expiresAt) where T: OneOptions {
            options.ExpiresAt = expiresAt;
            return options;
        }

        public static T WithExpiresIn<T>(this T options, TimeSpan? expiresIn) where T: OneOptions {
            options.ExpiresIn = expiresIn;
            return options;
        }

        public static T WithBefore<T>(this T options, string before) where T : MultiOptions {
            options.BeforeValue = before;
            return options;
        }

        public static T WithAfter<T>(this T options, string after) where T : MultiOptions {
            options.AfterValue = after;
            return options;
        }

        public static T WithBeforeQuery<T>(this T options, IMongoQuery before) where T : MultiOptions {
            options.BeforeQuery = before;
            return options;
        }

        public static T WithAfterQuery<T>(this T options, IMongoQuery after) where T : MultiOptions {
            options.AfterQuery = after;
            return options;
        }

        public static T WithFields<T>(this T options, params string[] fields) where T : OneOptions {
            options.Fields.AddRange(fields);
            return options;
        }

        public static T WithFields<T>(this T options, IEnumerable<string> fields) where T : OneOptions {
            options.Fields.AddRange(fields);
            return options;
        }

        public static T WithPaging<T>(this T options, PagingOptions paging) where T : MultiOptions {
            if (paging == null)
                return options;

            var pagingWithSorting = paging as PagingWithSortingOptions;
            if (pagingWithSorting != null) {
                options.BeforeQuery = pagingWithSorting.BeforeQuery;
                options.AfterQuery = pagingWithSorting.AfterQuery;
                options.SortBy = pagingWithSorting.SortBy;
            }

            options.BeforeValue = paging.Before;
            options.AfterValue = paging.After;
            options.Page = paging.Page;
            options.Limit = paging.Limit;

            options.HasMore = false;
            options.HasMoreChanged += (source, args) => paging.HasMore = args.Value;
            return options;
        }
    }
}