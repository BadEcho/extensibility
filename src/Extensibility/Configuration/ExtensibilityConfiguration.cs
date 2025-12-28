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

using BadEcho.Extensibility.Properties;
using BadEcho.Extensions;

namespace BadEcho.Extensibility.Configuration;

/// <summary>
/// Provides configuration settings for the Bad Echo Extensibility framework.
/// </summary>
public sealed class ExtensibilityConfiguration
{
    /// <summary>
    /// The name of the configuration section Extensibility framework settings are typically found in.
    /// </summary>
    public static string SectionName
        => "Extensibility";

    /// <summary>
    /// Gets or sets a value indicating if plugins are scanned for and loaded from a configured plugin directory.
    /// </summary>
    /// <remarks>
    /// If this is set to false, only exports originating from extensibility points and calling assemblies already loaded
    /// into the current application context will be loaded. This defaults to <c>true</c>.
    /// </remarks>
    public bool LoadPlugins
    { get; set; } = true;

    /// <summary>
    /// Gets or sets the path relative from the current application context's base directory to the directory
    /// where all plugins are stored.
    /// </summary>
    /// <remarks>
    /// <para>
    /// If this setting is not set then the directory will default to <c>plugins</c>. Setting this to an empty string will result
    /// in plugins being loaded from the base directory of the current application context. This is a situation that should normally
    /// be avoided, as it will result in many assemblies which contain no exported parts being loaded and scanned. To disable plugin loading,
    /// set <see cref="LoadPlugins"/> to false.
    /// </para>
    /// <para> 
    /// Providing a value for this setting sets an expectation that the specified directory exists. If this setting is specified
    /// and the resulting full path does not refer to an existing directory, then an error will occur.
    /// </para>
    /// </remarks>
    public string? PluginDirectory 
    { get; set; }

    /// <summary>
    /// Gets the collection of contracts being segmented by call-routable plugins.
    /// </summary>
    public ICollection<ContractConfiguration> SegmentedContracts
    { get; init; } = [];
    
    /// <summary>
    /// Gets the full path to where plugins are stored based on this configuration.
    /// </summary>
    /// <returns>
    /// <para>
    /// The full path to where plugins are stored based on this configuration. This will be based on combining either the
    /// <see cref="PluginDirectory"/> setting (if that was set) or a default directory of <c>plugins</c> (if it was not)
    /// as a relative path to the base directory of the current application context.
    /// </para>
    /// <para>
    /// If <see cref="PluginDirectory"/> was not specified and the default directory does not exist, then plugins will be loaded
    /// from the base directory of the current application context instead.
    /// </para>
    /// </returns>
    /// <exception cref="DirectoryNotFoundException">
    /// <see cref="PluginDirectory"/> was specified, but the resulting full path does not refer to an existing directory.
    /// </exception>
    /// <seealso cref="PluginDirectory"/>
    public string GetFullPathToPlugins()
    {
        const string defaultPluginDirectory = "plugins";

        if (PluginDirectory == null)
        {
            string defaultPath = Path.GetFullPath(defaultPluginDirectory, AppContext.BaseDirectory);

            return !Directory.Exists(defaultPath) ? AppContext.BaseDirectory : defaultPath;
        }

        string path = Path.GetFullPath(PluginDirectory, AppContext.BaseDirectory);

        if (!Directory.Exists(path))
        {
            throw new DirectoryNotFoundException(Strings.ExtensibilityConfigurationDirectoryNotFound
                                                        .InvariantFormat(path));
        }

        return path;
    }
}