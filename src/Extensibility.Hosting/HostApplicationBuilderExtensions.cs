// -----------------------------------------------------------------------
// <copyright>
//      Created by Matt Weber <matt@badecho.com>
//      Copyright @ 2025 Bad Echo LLC. All rights reserved.
//
//      Bad Echo Technologies are licensed under the
//      GNU Affero General Public License v3.0.
//
//      See accompanying file LICENSE.md or a copy at:
//      https://www.gnu.org/licenses/agpl-3.0.html
// </copyright>
// -----------------------------------------------------------------------

using BadEcho.Extensibility.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using IConfigurationProvider = BadEcho.Configuration.IConfigurationProvider;

namespace BadEcho.Extensibility.Hosting;

/// <summary>
/// Provides extension methods for integrating the Bad Echo Extensibility framework with a hosted application.
/// </summary>
public static class HostApplicationBuilderExtensions
{
    /// <summary>
    /// Exposes services and features registered with the host to the Bad Echo Extensibility framework.
    /// </summary>
    /// <param name="builder">The <see cref="IHostApplicationBuilder"/> instance to configure.</param>
    /// <returns>The current <see cref="IHostApplicationBuilder"/> instance so that additional calls can be chained.</returns>
    public static IHostApplicationBuilder AddExtensibility(this IHostApplicationBuilder builder)
    {
        Require.NotNull(builder, nameof(builder));

        var configuration = builder.Configuration.GetSection("Extensibility").Get<ExtensibilityConfiguration>()
                            ?? new ExtensibilityConfiguration
                               {
                                   LoadPlugins = false
                               };
        
        PluginHost.UpdateConfiguration(configuration);
        PluginHost.ArmedLoad<IConfigurationProvider, IConfiguration>(builder.Configuration);

        return builder;
    }
}
