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
using BadEcho.Extensions;
using System.Composition.Convention;
using System.Composition.Hosting;

namespace BadEcho.Extensibility.Hosting;

/// <summary>
/// Provides a strategy that directs a <see cref="PluginContext"/> to make available only the exports that belong to a specific
/// filterable family of plugins.
/// </summary>
internal sealed class FilterablePluginContextStrategy : IPluginContextStrategy
{
    private readonly ExtensibilityConfiguration _configuration;
    private readonly Guid _familyId;

    /// <summary>
    /// Initializes a new instance of the <see cref="FilterablePluginContextStrategy"/> class.
    /// </summary>
    /// <param name="configuration">
    /// Configuration for the Extensibility framework, used to determine where plugins will be loaded from.
    /// </param>
    /// <param name="familyId">Identifies the filterable family of plugins to allow through the filter.</param>
    public FilterablePluginContextStrategy(ExtensibilityConfiguration configuration, Guid familyId)
    {
        _configuration = configuration;
        _familyId = familyId;
    }

    /// <inheritdoc/>
    public CompositionHost CreateContainer()
    {   // We first build a global container, filtering from it all matching part types into a family-specific container
        // that the generated context will use to compose its objects.
        var globalBuilder = new ContainerConfiguration()
            .WithExtensibilityPoints();

        if (_configuration.LoadPlugins)
            globalBuilder.WithDirectory(_configuration.GetFullPathToPlugins());

        ConventionBuilder conventions = this.LoadConventions(globalBuilder);

        globalBuilder.WithDefaultConventions(conventions);

        IEnumerable<Type> matchingPartTypes = FindMatchingPartTypes(globalBuilder);
            
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