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

namespace BadEcho.Extensibility;

/// <summary>
/// Defines a family of plugins that support the Bad Echo Extensibility framework's filterable plugin capabilities, indicating
/// that types belonging to this family can be isolated from types belonging to different families.
/// </summary>
public interface IFilterableFamily
{
    /// <summary>
    /// Gets the identity of this filterable family.
    /// </summary>
    Guid FamilyId { get; }

    /// <summary>
    /// Gets the name of this filterable family.
    /// </summary>
    string Name { get; }
}