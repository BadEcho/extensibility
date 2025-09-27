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

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace BadEcho.Extensibility.Extensions.Tests;

public class Startup
{
    public void ConfigureHostApplicationBuilder(IHostApplicationBuilder builder)
    {
        builder.Configuration.AddJsonFile("appsettings.json", false, true);
        
        builder.AddExtensibility();
    }
}
