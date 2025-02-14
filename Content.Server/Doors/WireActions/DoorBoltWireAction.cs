using Content.Server.Doors.Components;
using Content.Server.Wires;
using Content.Shared.Doors;
using Content.Shared.Wires;

namespace Content.Server.Doors;

[DataDefinition]
public class DoorBoltWireAction : BaseWireAction
{
    [DataField("color")]
    private Color _statusColor = Color.Red;

    [DataField("name")]
    private string _text = "BOLT";

    public override StatusLightData? GetStatusLightData(Wire wire)
    {
        StatusLightState lightState = StatusLightState.Off;
        if (IsPowered(wire.Owner)
            && EntityManager.TryGetComponent<AirlockComponent>(wire.Owner, out var door))
        {
            if (door.BoltsDown)
            {
                lightState = StatusLightState.On;
            }
        }

        return new StatusLightData(
            _statusColor,
            lightState,
            _text);
    }

    public override object StatusKey { get; } = AirlockWireStatus.BoltIndicator;

    public override bool Cut(EntityUid user, Wire wire)
    {
        if (EntityManager.TryGetComponent<AirlockComponent>(wire.Owner, out var door))
        {
            if (!door.BoltsDown)
            {
                door.SetBoltsWithAudio(true);
            }
        }

        return true;
    }

    // does nothing
    public override bool Mend(EntityUid user, Wire wire)
    {
        return true;
    }

    public override bool Pulse(EntityUid user, Wire wire)
    {
        if (EntityManager.TryGetComponent<AirlockComponent>(wire.Owner, out var door))
        {
            if (IsPowered(wire.Owner))
            {
                door.SetBoltsWithAudio(!door.BoltsDown);
            }
            else if (!door.BoltsDown)
            {
                door.SetBoltsWithAudio(true);
            }

        }

        return true;
    }
}
