﻿using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rental.Core.Common.Configuration;
using Rental.DataAccess.Context;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
[assembly: InternalsVisibleTo("Rental.DataAccess.Tests")]

namespace Rental.DataAccess
{
    public static class EntityFrameworkModule
    {
        public static IServiceCollection AddDataAccessModule(this IServiceCollection services,
            ConnectionStrings connectionStrings)
        {
            services.AddDbContext<RentalContext>(options => options.UseSqlite(connectionStrings.SqLite,
                migrationsOptions =>
                    migrationsOptions.MigrationsAssembly(typeof(RentalContext).GetTypeInfo().Assembly
                        .GetName()
                        .Name)));

            return services;
        }
    }
}