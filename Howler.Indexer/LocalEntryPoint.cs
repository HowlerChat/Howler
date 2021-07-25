// <copyright file="LocalEntryPoint.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.Indexer
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    /// The Main function can be used to run the ASP.NET Core application
    /// locally using the Kestrel webserver.
    /// </summary>
    public class LocalEntryPoint
    {
        /// <summary>
        /// The main entry point of the local version of the application.
        /// </summary>
        /// <param name="args">The application args list.</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Creates a host builder to use for initialization.
        /// </summary>
        /// <param name="args">The application args list.</param>
        /// <returns>A constructed <see cref="IHostBuilder"/>.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
