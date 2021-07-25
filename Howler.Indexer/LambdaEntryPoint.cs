// <copyright file="LambdaEntryPoint.cs" company="Howler Team">
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
    /// This class extends from APIGatewayProxyFunction which contains the
    /// method FunctionHandlerAsync which is the actual Lambda function entry
    /// point. The Lambda handler field should be set to
    ///
    /// Howler.Indexer::Howler.Indexer.LambdaEntryPoint::FunctionHandlerAsync.
    /// </summary>
    public class LambdaEntryPoint :
        Amazon.Lambda.AspNetCoreServer.APIGatewayProxyFunction
    {
        /// <summary>
        /// The builder has configuration, logging and Amazon API Gateway
        /// already configured. The startup class needs to be configured in
        /// this method using the UseStartup() method.
        /// </summary>
        /// <param name="builder">The web host builder.</param>
        protected override void Init(IWebHostBuilder builder)
        {
            builder.UseStartup<Startup>();
        }

        /// <summary>
        /// Use this override to customize the services registered with the
        /// IHostBuilder.
        ///
        /// It is recommended not to call ConfigureWebHostDefaults to configure
        /// the IWebHostBuilder inside this method. Instead customize the
        /// IWebHostBuilder in the Init(IWebHostBuilder) overload.
        /// </summary>
        /// <param name="builder">The host builder.</param>
        protected override void Init(IHostBuilder builder)
        {
        }
    }
}
