using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Study.Application.Common.Behaviors;

namespace Study.Application;

public static class DependencyInjection
{
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
		// MediatR (scanna handlers i denna assembly)
		services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

		// AutoMapper (scanna profiler i denna assembly)
		services.AddAutoMapper(Assembly.GetExecutingAssembly());

		// FluentValidation (scanna validators i denna assembly)
		services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

		// Pipeline behaviors (ordning: logging -> validation)
		services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
		services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

		return services;
	}
}
