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

namespace BadEcho.Extensibility.Configuration;

/// <summary>
/// Provides configuration settings for a contract being segmented by one or more call-routable plugins.
/// </summary>
public sealed class ContractConfiguration
{
    /// <summary>
    /// Gets or sets the type name of the contract being segmented.
    /// </summary>
    public string Name 
    { get; set; } = string.Empty;

    /// <summary>
    /// Gets the collection of call-routable plugins that segment the represented contract.
    /// </summary>
    public ICollection<RoutablePluginConfiguration> RoutablePlugins
    { get; init; } = [];
}