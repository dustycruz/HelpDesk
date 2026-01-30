using HelpDesk.BLL;
using HelpDesk.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HelpDesk.UI
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            var host = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration(cfg =>
                cfg.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true))
            .ConfigureServices((ctx, services) =>
            {
                services.AddDbContext<HelpDeskDbContext>(opt =>
                    opt.UseSqlServer(ctx.Configuration.GetConnectionString("DefaultConnection")));

                // DAL
                services.AddScoped<ITicketRepository, TicketRepository>();
                services.AddScoped<IEmployeeRepository, EmployeeRepository>();
                services.AddScoped<ITicketCategoryRepository, TicketCategoryRepository>();

                // BLL
                services.AddScoped<ITicketService, TicketService>();

                // Forms
                services.AddTransient<Form1>();
            })
            .Build();

            using var scope = host.Services.CreateScope();
            Application.Run(scope.ServiceProvider.GetRequiredService<Form1>());

        }
    }
}