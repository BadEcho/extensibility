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

using Xunit;
using Microsoft.Extensions.Configuration;
using BadEcho.Extensibility.Configuration;

namespace BadEcho.Extensibility.Tests;

public class ConfigurationProviderTests
{
    [Fact]
    public void GetExtensibilityConfiguration_Json_ReturnsValid()
    {
        var builder = new ConfigurationBuilder()
                      .SetBasePath(Directory.GetCurrentDirectory())
                      .AddJsonFile("test.json");

        var configuration = builder.Build();

        var extensibility = configuration.Get<ExtensibilityConfiguration>();
        Assert.NotNull(extensibility);

        ValidateConfiguration(extensibility);
    }

    [Fact]
    public void GetContractSection_Json_ReturnsValid()
    {
        var builder = new ConfigurationBuilder()
                      .SetBasePath(Directory.GetCurrentDirectory())
                      .AddJsonFile("test.json");

        var configuration = builder.Build();

        var contracts = configuration.GetSection("segmentedContracts")
                                     .Get<IEnumerable<ContractConfiguration>>();
        Assert.NotNull(contracts);
        Assert.Single(contracts);
    }

    private static void ValidateConfiguration(ExtensibilityConfiguration extensibility)
    {
        Assert.Equal("testPlugins", extensibility.PluginDirectory);
        Assert.NotEmpty(extensibility.SegmentedContracts);

        var contract = extensibility.SegmentedContracts.First();

        Assert.Equal("ISegmentedContract", contract.Name);
        Assert.NotEmpty(contract.RoutablePlugins);
        Assert.Equal(2, contract.RoutablePlugins.Count());

        var firstPlugin = contract.RoutablePlugins.First();
        var secondPlugin = contract.RoutablePlugins.Skip(1).First();

        Assert.True(firstPlugin.Primary);
        Assert.False(secondPlugin.Primary);

        Assert.Equal(new Guid("BE157C54-2BC0-48B8-9E39-0AE53D8A4E61"), firstPlugin.Id);
        Assert.Equal(new Guid("F544EC74-919E-4167-A421-FA74223F49C5"), secondPlugin.Id);

        Assert.NotEmpty(secondPlugin.MethodClaims);

        var methodClaim = secondPlugin.MethodClaims.First();

        Assert.Equal("SomeOtherMethod", methodClaim);
    }
}