using ChatterBox.Data;
using ChatterBox.Features.Chat.GetHistory;
using ChatterBox.Features.Chat.SendMessage;
using ChatterBox.Infrastructure.Ai;
using Microsoft.EntityFrameworkCore;

namespace ChatterBox
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ChatterBoxDbContext>(options => 
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), 
                    sqlOptions => sqlOptions.EnableRetryOnFailure()));
            
            builder.Services.AddScoped<IAiService, AiService>();
            builder.Services.AddScoped<SendMessageHandler>();
            builder.Services.AddScoped<GetHistoryHandler>();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ChatterBoxDbContext>();
                try
                {
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while creating the database");
                }
            }

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Chat}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
