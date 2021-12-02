﻿using Microsoft.Extensions.DependencyInjection;
using System;

namespace DomesticOrganizationGuru.Api.Kernel.RegisterMappers
{
    public static class Mappers
    {
        public static void RegisterMappers(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSingleton(AutoMapperConfiguration.Mapper);
        }
    }
}
