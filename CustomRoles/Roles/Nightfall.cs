﻿using SnivysCustomRolesAbilities.Abilities;

namespace CustomRoles.Roles;

using System.Collections.Generic;
using CustomRoles.API;

using Exiled.API.Enums;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomRoles.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.Handlers;
using PlayerRoles;

[CustomRole(RoleTypeId.ChaosConscript)]
public class Nightfall : CustomRole, ICustomRole
{
    public int Chance { get; set; } = 5;

    public StartTeam StartTeam { get; set; } = StartTeam.Chaos;

    public override uint Id { get; set; } = 35;

    public override RoleTypeId Role { get; set; } = RoleTypeId.ChaosConscript;

    public override int MaxHealth { get; set; } = 200;

    public override string Name { get; set; } = "Nightfall";

    public override string Description { get; set; } =
        "A Chaos Insurgent that is special. You figure out the rest.";

    public override string CustomInfo { get; set; } = "Nightfall";

    public override SpawnProperties SpawnProperties { get; set; } = new()
    {
        Limit = 1,
        RoleSpawnPoints = new List<RoleSpawnPoint>
        {
            new()
            {
                Role = RoleTypeId.ChaosConscript,
                Chance = 100,
            },
        },
    };

    public override List<string> Inventory { get; set; } = new()
    {
        $"{ItemType.GunRevolver}",
        $"{ItemType.KeycardO5}",
        $"{ItemType.SCP500}",
        $"{ItemType.ArmorCombat}",
    };

    public override Dictionary<AmmoType, ushort> Ammo { get; set; } = new()
    {
        {
            AmmoType.Ammo44Cal, 20
        },
    };

    public override List<CustomAbility>? CustomAbilities { get; set; } = new()
    {
        new NightfallAbility(),
    };
}