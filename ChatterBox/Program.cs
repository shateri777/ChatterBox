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
            builder.Services.AddDbContext<ChatterBoxDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            
            // Register services
            builder.Services.AddScoped<IAiService, AiService>();
            builder.Services.AddScoped<SendMessageHandler>();
            builder.Services.AddScoped<GetHistoryHandler>();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
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
