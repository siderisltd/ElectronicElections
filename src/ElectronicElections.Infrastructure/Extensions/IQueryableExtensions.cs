using AutoMapper.QueryableExtensions;
using ElectronicElections.Infrastructure.Mapping;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace ElectronicElections.Infrastructure.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<TDestination> To<TDestination>(this IQueryable source, params Expression<Func<TDestination, object>>[] membersToExpand)
        {
            return source.ProjectTo(AutoMapperConfig.Configuration, membersToExpand);
        }
    }
}
