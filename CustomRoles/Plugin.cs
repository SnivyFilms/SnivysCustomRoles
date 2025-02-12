using Exiled.API.Enums;
using PluginAPI.Enums;
using SnivyCustomRoles.Configs;

namespace CustomRoles;

using System;
using System.Collections.Generic;

using CustomRoles.API;

using Exiled.API.Features;
using Exiled.CustomRoles.API;
using Exiled.CustomRoles.API.Features;

using PlayerRoles;

using Config = Config;
using CustomRole = Exiled.CustomRoles.API.Features.CustomRole;
using PlayerEvents = Exiled.Events.Handlers.Player;
using Scp049Events = Exiled.Events.Handlers.Scp049;
using ServerEvents = Exiled.Events.Handlers.Server;

public class Plugin : Plugin<Config>
{
    public static Plugin Singleton { get; private set; } = null!;
    
    public Dictionary<StartTeam, List<ICustomRole>> Roles { get; } = new();

    public List<Player> StopRagdollList { get; } = new ();

    public override string Author { get; } = "Vicious Vikki";

    public override string Name { get; } = "SFSCustomRoles";

    public override string Prefix { get; } = "SFSCustomRoles";

    public override Version RequiredExiledVersion { get; } = new (8, 12, 2);
    public Methods Methods { get; private set; } = null!;

    public EventHandlers EventHandlers { get; private set; } = null!;

    public override void OnEnabled()
    {
        Singleton = this;
        EventHandlers = new EventHandlers(this);
        Methods = new Methods(this);

        Config.LoadConfigs();

        Config.RoleConfigs.ContainmentScientists.Register();
        Config.RoleConfigs.LightGuards.Register();
        Config.RoleConfigs.Biochemists.Register();
        Config.RoleConfigs.ContainmentGuards.Register();
        Config.RoleConfigs.BorderPatrols.Register();
        Config.RoleConfigs.Nightfalls.Register();
        Config.RoleConfigs.A7Chaoss.Register();
        Config.RoleConfigs.Flippeds.Register();
        Config.RoleConfigs.TelepathicChaos.Register();
        Config.RoleConfigs.JuggernautChaos.Register();
        Config.RoleConfigs.CISpies.Register();
        Config.RoleConfigs.MtfWisps.Register();

        foreach (CustomRole role in CustomRole.Registered)
        {
            /*if (role.CustomAbilities is not null)
            {
                foreach (CustomAbility ability in role.CustomAbilities)
                {
                    ability.Register();
                }
            }*/

            if (role is ICustomRole custom)
            {
                Log.Debug($"Adding {role.Name} to dictionary..");
                StartTeam team;
                if (custom.StartTeam.HasFlag(StartTeam.Chaos))
                    team = StartTeam.Chaos;
                else if (custom.StartTeam.HasFlag(StartTeam.Guard))
                    team = StartTeam.Guard;
                else if (custom.StartTeam.HasFlag(StartTeam.Ntf))
                    team = StartTeam.Ntf;
                else if (custom.StartTeam.HasFlag(StartTeam.Scientist))
                    team = StartTeam.Scientist;
                else if (custom.StartTeam.HasFlag(StartTeam.ClassD))
                    team = StartTeam.ClassD;
                else if (custom.StartTeam.HasFlag(StartTeam.Scp))
                    team = StartTeam.Scp;
                else
                    team = StartTeam.Other;

                if (!Roles.ContainsKey(team))
                    Roles.Add(team, new());

                for (int i = 0; i < role.SpawnProperties.Limit; i++)
                    Roles[team].Add(custom);
                Log.Debug($"Roles {team} now has {Roles[team].Count} elements.");
            }
        }

        ServerEvents.RoundStarted += EventHandlers.OnRoundStarted;
        ServerEvents.RespawningTeam += EventHandlers.OnRespawningTeam;
        ServerEvents.ReloadedConfigs += EventHandlers.OnReloadedConfigs;
        Scp049Events.FinishingRecall += EventHandlers.FinishingRecall;
        PlayerEvents.SpawningRagdoll += EventHandlers.OnSpawningRagdoll;
        base.OnEnabled();
    }

    public override void OnDisabled()
    {
        CustomRole.UnregisterRoles();

        ServerEvents.RoundStarted -= EventHandlers.OnRoundStarted;
        ServerEvents.RespawningTeam -= EventHandlers.OnRespawningTeam;
        ServerEvents.ReloadedConfigs -= EventHandlers.OnReloadedConfigs;

        base.OnDisabled();
    }
}