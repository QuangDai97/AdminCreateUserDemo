﻿using AdminCreateUserDemo.Services.NewFolder.Implementations;
using AdminCreateUserDemo.Services.NewFolder.Interfaces;

namespace AdminCreateUserDemo.Services
{
    public static class ServiceRegistration
    {
        public static void AddServices(this IServiceCollection services)
        {
            // Add services
            services.AddScoped<IEmailSender, EmailSender>();
        }
    }
}
