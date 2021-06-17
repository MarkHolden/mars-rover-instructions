using System;
using MarsRovers.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MarsRovers
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceProvider serviceProvider = new ServiceCollection()
                .AddScoped<IMovementService, MovementService>()
                .AddScoped<IRoverService, RoverService>()
                .AddScoped<IProcessor, Processor>()
                .BuildServiceProvider();

            Console.WriteLine("Starting application");

            IProcessor processor = serviceProvider.GetRequiredService<IProcessor>();

            try
            {
                processor.Process();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Rover Commands failed. {ex.Message}");
            }

            Console.WriteLine("Stopping Application");
        }
    }
}
