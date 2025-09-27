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

using System.Composition;
using System.Composition.Convention;
using BadEcho.Extensibility.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using IConfigurationProvider = BadEcho.Configuration.IConfigurationProvider;
using ConfigurationProvider = BadEcho.Configuration.ConfigurationProvider;

namespace BadEcho.Extensibility.Extensions;

/// <summary>
/// Provides configuration data sourced from a hosted application's configuration.
/// </summary>
[Export(typeof(IConfigurationProvider))]
internal sealed class ServiceConfigurationProvider : ConfigurationProvider
{
    private const string DEPENDENCY_CONTRACT
        = nameof(ServiceConfigurationProvider) + nameof(LocalDependency);

    private readonly IConfiguration _configuration;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="ServiceConfigurationProvider"/> class.
    /// </summary>
    /// <param name="configuration">The hosted application's configuration to source.</param>
    [ImportingConstructor]
    public ServiceConfigurationProvider([Import(DEPENDENCY_CONTRACT)] IConfiguration configuration)
    {
        _configuration = configuration;

        ChangeToken.OnChange(_configuration.GetReloadToken, OnConfigurationChanged);
    }

    /// <inheritdoc/>
    public override T GetConfiguration<T>(string? sectionName)
    {
        IConfiguration section = 
            !string.IsNullOrEmpty(sectionName) ? _configuration.GetSection(sectionName) : _configuration;

        return section.Get<T>() ?? new T();
    }

    /// <summary>
    /// Provides a convention provider that allows for this configuration provider to be armed with an injected
    /// <see cref="IConfiguration"/> instance.
    /// </summary>
    /// <remarks>
    /// Because this configures the exported configuration provider to be a shared singleton, the configuration provider
    /// only needs to be armed with the injected <see cref="IConfiguration"/> instance once.
    /// </remarks>
    /// <suppressions>
    /// ReSharper disable ClassNeverInstantiated.Local
    /// </suppressions>
    [Export(typeof(IConventionProvider))]
    private sealed class LocalDependency : DependencyRegistry<IConfiguration>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalDependency"/> class with its unique contract name.
        /// </summary>
        public LocalDependency()
            : base(DEPENDENCY_CONTRACT)
        { }

        /// <inheritdoc/>
        public override IConfiguration Dependency
            => LoadDependency();

        /// <inheritdoc/>
        protected override void OnConfigureRules(PartConventionBuilder partsConventions)
        {
            base.OnConfigureRules(partsConventions);

            Require.NotNull(partsConventions, nameof(partsConventions));

            partsConventions.Shared();
        }
    }
}