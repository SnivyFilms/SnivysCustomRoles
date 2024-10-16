﻿using System.Collections.Generic;
using SnivysCustomRolesAbilities.Abilities;

namespace CustomRoles.Roles;

using CustomRoles.API;

using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomRoles.API.Features;

using MEC;
using PlayerRoles;
using UnityEngine;

[CustomRole(RoleTypeId.None)]
public class Flipped : CustomRole, ICustomRole
{
    public int Chance { get; set; } = 0;

    public StartTeam StartTeam { get; set; } = StartTeam.ClassD;

    public override uint Id { get; set; } = 37;

    public override RoleTypeId Role { get; set; } = RoleTypeId.None;

    public override int MaxHealth { get; set; } = 100;

    public override string Name { get; set; } = "Flipped";

    public override string Description { get; set; } =
        "For the people who complains that being small is boring";

    public override string CustomInfo { get; set; } = "Flipped";

    public override bool KeepInventoryOnSpawn { get; set; } = true;

    public override bool KeepRoleOnDeath { get; set; } = true;

    public override bool RemovalKillsPlayer { get; set; } = false;

    public override SpawnProperties SpawnProperties { get; set; } = new()
    {
        Limit = 1,
    };

     public override List<CustomAbility>? CustomAbilities { get; set; } = new()
    {
        new SnivysCustomRolesAbilities.Abilities.Flipped(),
    };
}