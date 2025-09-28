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
using BadEcho.Extensibility.Hosting;
using Microsoft.Extensions.Configuration;
using IConfigurationProvider = BadEcho.Configuration.IConfigurationProvider;

namespace BadEcho.Extensibility.Extensions;

/// <summary>
/// Provides extension methods for integrating the Bad Echo Extensibility framework with a hosted application's configuration.
/// </summary>
public static class ConfigurationExtensions
{
    /// <summary>
    /// Exposes the configuration for the host to the Bad Echo Extensibility framework.
    /// </summary>
    /// <param name="configuration">The <typeparamref name="TConfiguration"/> instance to expose.</param>
    /// <returns>The current <see cref="TConfiguration"/> instance so that additional calls can be chained.</returns>
    /// <typeparam name="TConfiguration">The type of <see cref="IConfiguration"/> being exposed.</typeparam>
    /// <remarks>
    /// This accepts a generic parameter so that, in the event <typeparam name="TConfiguration"/> also implements something
    /// like <see cref="IConfigurationBuilder"/>, it can be used as part of a method chain.
    /// </remarks>
    public static TConfiguration AddExtensibility<TConfiguration>(this TConfiguration configuration)
        where TConfiguration : IConfiguration
    {
        Require.NotNull(configuration, nameof(configuration));

        var pluginConfiguration = configuration.GetSection("Extensibility").Get<ExtensibilityConfiguration>()
                                  ?? new ExtensibilityConfiguration
                                     {
                                         LoadPlugins = false
                                     };
        
        PluginHost.UpdateConfiguration(pluginConfiguration);
        PluginHost.ArmedLoad<IConfigurationProvider, IConfiguration>(configuration);

        return configuration;
    }
}
