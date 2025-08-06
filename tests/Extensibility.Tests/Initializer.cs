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

using System.Runtime.CompilerServices;
using BadEcho.ExtensibilityPoint;

namespace BadEcho.Extensibility.Tests;

public static class Initializer
{
    /// <suppressions>
    /// ReSharper disable UnusedVariable
    /// </suppressions>
    [ModuleInitializer]
    public static void EnsureExtensibilityPointsLoaded()
    {
        var part = new ExtensibilityPart();
    }
}
