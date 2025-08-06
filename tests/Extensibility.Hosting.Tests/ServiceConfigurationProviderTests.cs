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

using BadEcho.Configuration;

namespace BadEcho.Extensibility.Hosting.Tests;

public class ServiceConfigurationProviderTests
{
    [Fact]
    public void LoadConfiguration_InterceptedOptions_MatchesSettings()
    {
        var configuration = PluginHost.LoadRequirement<IConfigurationProvider>();

        InterceptedOptions options = 
            configuration.GetConfiguration<InterceptedOptions>("Intercepted");

        Assert.NotNull(options);
        Assert.Equal("AreBelongToUs", options.AllYourBase);
        Assert.Equal("BaseBase", options.AllYourBaseYourBase);
    }

    /// <suppressions>
    /// ReSharper disable UnusedAutoPropertyAccessor.Local
    /// </suppressions>
    private class InterceptedOptions
    {
        public string? AllYourBase { get; init; }

        public string? AllYourBaseYourBase { get; init; }
    }
}
