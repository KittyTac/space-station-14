﻿using Content.Server.Station.Systems;
using Robust.Shared.Map;

namespace Content.Server.Station.Components;

/// <summary>
/// Controls spawning on the given station, tracking spawners present on it.
/// </summary>
[RegisterComponent, Friend(typeof(StationSpawningSystem))]
public sealed class StationSpawningComponent : Component
{
}
