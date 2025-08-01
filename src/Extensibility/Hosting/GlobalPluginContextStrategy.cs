﻿// -----------------------------------------------------------------------
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

using System.Composition.Convention;
using System.Composition.Hosting;

namespace BadEcho.Extensibility.Hosting;

/// <summary>
/// Provides a strategy that directs a <see cref="PluginContext"/> to make available all plugins that are discoverable through Bad Echo's
/// Extensibility framework.
/// </summary>
/// <remarks>
/// This is the standard strategy used to load any plugins exported for consumption with no amount of discretion
/// at play. Any contracts loaded through contexts using this strategy will have all possible implementations of said
/// contracts returned.
/// </remarks>
internal sealed class GlobalPluginContextStrategy : IPluginContextStrategy
{
    private readonly string _pluginDirectory;

    /// <summary>
    /// Initializes a new instance of the <see cref="GlobalPluginContextStrategy"/> class.
    /// </summary>
    /// <param name="pluginDirectory">Full path to the directory where plugins will be loaded from.</param>
    public GlobalPluginContextStrategy(string pluginDirectory) 
        => _pluginDirectory = pluginDirectory;

    /// <inheritdoc/>
    public CompositionHost CreateContainer()
    {
        var configuration = new ContainerConfiguration()
                            .WithDirectory(_pluginDirectory)
                            .WithExtensibilityPoints();

        ConventionBuilder conventions = this.LoadConventions(configuration);

        configuration.WithDefaultConventions(conventions);

        return configuration.CreateContainer();
    }
}