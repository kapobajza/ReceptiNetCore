using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Recepti
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var directory = Directory.GetParent(Directory.GetCurrentDirectory());
                    config.AddJsonFile($@"{directory}/Secrets/secrets.json", false, true);
                })
                .UseKestrel(x => x.AddServerHeader = false)
                .UseStartup<Startup>();
    }
}
