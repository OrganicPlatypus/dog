﻿using AutoMapper;
using DomesticOrganizationGuru.Api.Model.MappingProfiles;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DomesticOrganizationGuru.Api.Kernel.RegisterMappers
{
    public static class Mappers
    {
        public static void RegisterMappers(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            var profiles = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new NotesPackMapper());
                mc.AddProfile(new NoteMapper());
            });
            var mapper = profiles.CreateMapper();

            services.AddSingleton(mapper);
        }
    }
}
