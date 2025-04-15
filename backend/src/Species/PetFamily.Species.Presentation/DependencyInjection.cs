﻿using Microsoft.Extensions.DependencyInjection;
using PetFamily.Species.Contracts;

namespace PetFamily.Species.Presentation;
public static class DependencyInjection
{
    public static IServiceCollection AddSpeciesPresentation(this IServiceCollection services)
    {
        services.AddScoped<ISpeciesContract, SpeciesContract>();

        return services;
    }
}