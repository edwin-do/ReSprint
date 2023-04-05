using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using FinalSprint.Hubs;
using Syncfusion.Windows.Tools.Controls;
using System.Windows.Threading;
using System;
using System.Windows;
using System.Windows.Media;

namespace FinalSprint.src.Classes
{
    internal class RemoteAccess    {
        private readonly MainWindow _mainWindow;
        public RemoteAccess(MainWindow mainWindow) {
            _mainWindow = mainWindow;
        }

        public async void StartServer(IHost _host)
        {

                _host?.Dispose();
                _host = Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls("http://*:5100");
                    webBuilder.ConfigureServices(services =>
                    {
                        services.AddSingleton<ChatHub>(new ChatHub(_mainWindow));
                        services.AddCors(options =>
                        {
                            options.AddPolicy("CorsPolicy",
                                builder =>
                                {
                                    builder.WithOrigins("https://resprint.netlify.app", "http://192.168.0.119:45455", "http://localhost:3000", "null")
                                           .AllowAnyMethod()
                                           .AllowAnyHeader()
                                           .WithExposedHeaders("Content-Disposition")
                                           .WithHeaders("x-requested-with", "X-SignalR-User-Agent")
                                           .SetIsOriginAllowed((x) => true)
                                           .AllowCredentials();
                                });
                        });
                        services.AddSignalR();
                    });
                    webBuilder.Configure(app =>
                    {
                        app.UseWebSockets();

                        app.Use(async (context, next) =>
                        {
                            context.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "https://resprint.netlify.app", "http://localhost:3000", "null" });
                            context.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
                            await next();
                        });
                        app.UseCors("CorsPolicy");
                        app.UseRouting();
                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapHub<ChatHub>("/Hubs/chatHub");
                        });
                    });
                })
                .Build();


                await _host.StartAsync();
            }
    }
}
