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
/// Provides configuration settings for a call-routable plugin.
/// </summary>
public sealed class RoutablePluginConfiguration
{
    /// <summary>
    /// Gets or sets the identifying <see cref="Guid"/> for the call-routable plugin.
    /// </summary>
    public Guid Id 
    { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the represented plugin is the primary call-routable plugin
    /// for a particular context.
    /// </summary>
    public bool Primary 
    { get; set; }

    /// <summary>
    /// Gets the collection of names for methods claimed by the represented call-routable plugin.
    /// </summary>
    public ICollection<string> MethodClaims
    { get; init; } = [];
}