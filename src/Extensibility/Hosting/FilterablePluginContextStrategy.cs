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
using System.Reflection;
using BadEcho.Extensions;

namespace BadEcho.Extensibility.Hosting;

/// <summary>
/// Provides a strategy that directs a <see cref="PluginContext"/> to make available only the exports that belong to a specific
/// filterable family of plugins.
/// </summary>
internal sealed class FilterablePluginContextStrategy : IPluginContextStrategy
{
    private readonly string _pluginDirectory;
    private readonly Guid _familyId;

    /// <summary>
    /// Initializes a new instance of the <see cref="FilterablePluginContextStrategy"/> class.
    /// </summary>
    /// <param name="pluginDirectory">Full path to the directory where plugins will be loaded from.</param>
    /// <param name="familyId">Identifies the filterable family of plugins to allow through the filter.</param>
    public FilterablePluginContextStrategy(string pluginDirectory, Guid familyId)
    {
        _pluginDirectory = pluginDirectory;
        _familyId = familyId;
    }

    /// <inheritdoc/>
    public CompositionHost CreateContainer()
    {
        var globalConfiguration = new ContainerConfiguration()
            .WithExtensibilityPoints();

        IEnumerable<Assembly> assemblies
            = globalConfiguration.LoadFromDirectory(_pluginDirectory);

        ConventionBuilder conventions
            = this.LoadConventions(globalConfiguration.WithAssemblies(assemblies));

        globalConfiguration.WithDefaultConventions(conventions);

        IEnumerable<Type> matchingPartTypes = FindMatchingPartTypes(globalConfiguration);
            
        return new ContainerConfiguration()
               .WithParts(matchingPartTypes)
               .WithDefaultConventions(conventions)
               .CreateContainer();
    }

    private IEnumerable<Type> FindMatchingPartTypes(ContainerConfiguration globalConfiguration)
    {
        using (var globalContainer = globalConfiguration.CreateContainer())
        {
            var filterableParts = globalContainer.GetExports<Lazy<IFilterable, FilterableMetadataView>>();
                
            return filterableParts
                   .Where(filterablePart =>
                              _familyId.Equals(filterablePart.Metadata.FamilyId))
                   .Select(matchingPart => matchingPart.Metadata.PartType)
                   .WhereNotNull();
        }
    }
}