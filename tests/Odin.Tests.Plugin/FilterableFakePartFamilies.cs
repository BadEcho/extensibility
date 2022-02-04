﻿//-----------------------------------------------------------------------
// <copyright>
//      Created by Matt Weber <matt@badecho.com>
//      Copyright @ 2022 Bad Echo LLC. All rights reserved.
//
//		Bad Echo Technologies are licensed under a
//		Creative Commons Attribution-NonCommercial 4.0 International License.
//
//		See accompanying file LICENSE.md or a copy at:
//		http://creativecommons.org/licenses/by-nc/4.0/
// </copyright>
//-----------------------------------------------------------------------

using BadEcho.Odin.Extensibility;
using BadEcho.Odin.Tests.Extensibility;

namespace BadEcho.Odin.Tests.Plugin;

[FilterableFamily(FamilyIdValue, NAME)]
public class AlphaFamily : IFilterableFamily
{
    internal const string FamilyIdValue = FakeFilterableIds.AlphaFakeIdValue;
    private const string NAME = "Alpha";

    public Guid FamilyId
        => new(FamilyIdValue);

    public string Name
        => NAME;
}

[FilterableFamily(FamilyIdValue, NAME)]
public class BetaFamily : IFilterableFamily
{
    internal const string FamilyIdValue = FakeFilterableIds.BetaFakeIdValue;
    private const string NAME = "Beta";

    public Guid FamilyId
        => new (FamilyIdValue);

    public string Name
        => NAME;
}

[FilterableFamily(FamilyIdValue, NAME)]
public class DeltaFamily : IFilterableFamily
{
    internal const string FamilyIdValue = FakeFilterableIds.DeltaFakeIdValue;
    private const string NAME = "Delta";

    public Guid FamilyId
        => new(FamilyIdValue);

    public string Name
        => NAME;
}

[FilterableFamily(FamilyIdValue, NAME)]
public class GammaFamily : IFilterableFamily
{
    internal const string FamilyIdValue = FakeFilterableIds.GammaFakeIdValue;
    private const string NAME = "Gamma";

    public Guid FamilyId
        => new(FamilyIdValue);

    public string Name
        => NAME;
}