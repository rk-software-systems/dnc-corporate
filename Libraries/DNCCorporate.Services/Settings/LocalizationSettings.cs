﻿using System.Collections.Immutable;

namespace DNCCorporate.Services;

public class LocalizationSettings
{
    public required string DefaultCulture { get; set; }

    public required string AvailableCulturesStr { get; set; }

    public ImmutableArray<string> AvailableCultures
    {
        get
        {
            if (!string.IsNullOrEmpty(AvailableCulturesStr))
            {
                return AvailableCulturesStr
                    .Split(';')
                    .ToImmutableArray();
            }
            return ImmutableArray<string>.Empty;
        }
    }
}