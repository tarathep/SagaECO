namespace SagaMap.Manager
{
    using SagaDB.Actor;
    using SagaDB.ODWar;
    using SagaLib;
    using SagaMap.ActorEventHandlers;
    using SagaMap.Network.Client;
    using SagaMap.Packets.Server;
    using SagaMap.Scripting;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="ODWarManager" />.
    /// </summary>
    public class ODWarManager : Singleton<ODWarManager>
    {
        /// <summary>
        /// The StartODWar.
        /// </summary>
        /// <param name="mapID">The mapID<see cref="uint"/>.</param>
        public void StartODWar(uint mapID)
        {
            if (this.IsDefence(mapID))
                this.spawnSymbol(mapID);
            else
                this.spawnSymbolTrash(mapID);
        }

        /// <summary>
        /// The IsDefence.
        /// </summary>
        /// <param name="mapID">The mapID<see cref="uint"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsDefence(uint mapID)
        {
            return Singleton<ScriptManager>.Instance.VariableHolder.AInt["ODWar" + mapID.ToString() + "Captured"] == 0;
        }

        /// <summary>
        /// The spawnSymbol.
        /// </summary>
        /// <param name="mapID">The mapID<see cref="uint"/>.</param>
        private void spawnSymbol(uint mapID)
        {
            SagaDB.ODWar.ODWar odWar = Factory<ODWarFactory, SagaDB.ODWar.ODWar>.Instance.Items[mapID];
            Map map = Singleton<MapManager>.Instance.GetMap(mapID);
            foreach (SagaDB.ODWar.ODWar.Symbol symbol in odWar.Symbols.Values)
            {
                short x = Global.PosX8to16(symbol.x, map.Width);
                short y = Global.PosY8to16(symbol.y, map.Height);
                symbol.actorID = map.SpawnMob(symbol.mobID, x, y, (short)2000, (SagaDB.Actor.Actor)null).ActorID;
                symbol.broken = false;
            }
        }

        /// <summary>
        /// The spawnSymbolTrash.
        /// </summary>
        /// <param name="mapID">The mapID<see cref="uint"/>.</param>
        private void spawnSymbolTrash(uint mapID)
        {
            SagaDB.ODWar.ODWar odWar = Factory<ODWarFactory, SagaDB.ODWar.ODWar>.Instance.Items[mapID];
            Map map = Singleton<MapManager>.Instance.GetMap(mapID);
            foreach (SagaDB.ODWar.ODWar.Symbol symbol in odWar.Symbols.Values)
            {
                short x = Global.PosX8to16(symbol.x, map.Width);
                short y = Global.PosY8to16(symbol.y, map.Height);
                symbol.actorID = map.SpawnMob(odWar.SymbolTrash, x, y, (short)2000, (SagaDB.Actor.Actor)null).ActorID;
                symbol.broken = true;
            }
        }

        /// <summary>
        /// The SymbolDown.
        /// </summary>
        /// <param name="mapID">The mapID<see cref="uint"/>.</param>
        /// <param name="mob">The mob<see cref="ActorMob"/>.</param>
        public void SymbolDown(uint mapID, ActorMob mob)
        {
            SagaDB.ODWar.ODWar odWar = Factory<ODWarFactory, SagaDB.ODWar.ODWar>.Instance.Items[mapID];
            Map map = Singleton<MapManager>.Instance.GetMap(mapID);
            bool flag = true;
            foreach (int key in odWar.Symbols.Keys)
            {
                SagaDB.ODWar.ODWar.Symbol symbol = odWar.Symbols[key];
                if ((int)symbol.actorID == (int)mob.ActorID)
                {
                    if ((int)mob.MobID == (int)odWar.SymbolTrash)
                        symbol.actorID = 0U;
                    else if ((int)mob.MobID == (int)symbol.mobID)
                    {
                        symbol.actorID = map.SpawnMob(odWar.SymbolTrash, mob.X, mob.Y, (short)10, (SagaDB.Actor.Actor)null).ActorID;
                        symbol.broken = true;
                        map.Announce(string.Format(Singleton<LocalManager>.Instance.Strings.ODWAR_SYMBOL_DOWN, (object)key));
                    }
                }
                if (!symbol.broken)
                    flag = false;
            }
            if (!this.IsDefence(mapID) || !flag)
                return;
            this.EndODWar(mapID, false);
        }

        /// <summary>
        /// The UpdateScore.
        /// </summary>
        /// <param name="mapID">The mapID<see cref="uint"/>.</param>
        /// <param name="actorID">The actorID<see cref="uint"/>.</param>
        /// <param name="delta">The delta<see cref="int"/>.</param>
        public void UpdateScore(uint mapID, uint actorID, int delta)
        {
            if (!Factory<ODWarFactory, SagaDB.ODWar.ODWar>.Instance.Items.ContainsKey(mapID))
                return;
            SagaDB.ODWar.ODWar odWar = Factory<ODWarFactory, SagaDB.ODWar.ODWar>.Instance.Items[mapID];
            if (!odWar.Score.ContainsKey(actorID))
                odWar.Score.Add(actorID, 0);
            Dictionary<uint, int> score;
            uint index;
            (score = odWar.Score)[index = actorID] = score[index] + delta;
            if (odWar.Score[actorID] < 0)
                odWar.Score[actorID] = 0;
        }

        /// <summary>
        /// The ReviveSymbol.
        /// </summary>
        /// <param name="mapID">The mapID<see cref="uint"/>.</param>
        /// <param name="number">The number<see cref="int"/>.</param>
        /// <returns>The <see cref="SymbolReviveResult"/>.</returns>
        public SymbolReviveResult ReviveSymbol(uint mapID, int number)
        {
            SagaDB.ODWar.ODWar odWar = Factory<ODWarFactory, SagaDB.ODWar.ODWar>.Instance.Items[mapID];
            Map map = Singleton<MapManager>.Instance.GetMap(mapID);
            if (!odWar.Symbols.ContainsKey(number))
                return SymbolReviveResult.Faild;
            if (!odWar.Symbols[number].broken)
                return SymbolReviveResult.NotDown;
            if (odWar.Symbols[number].actorID != 0U)
                return SymbolReviveResult.StillTrash;
            short x = Global.PosX8to16(odWar.Symbols[number].x, map.Width);
            short y = Global.PosY8to16(odWar.Symbols[number].y, map.Height);
            SagaDB.Actor.Actor sActor = (SagaDB.Actor.Actor)map.SpawnMob(odWar.Symbols[number].mobID, x, y, (short)10, (SagaDB.Actor.Actor)null);
            sActor.HP = sActor.MaxHP / 2U;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, (MapEventArgs)null, sActor, false);
            odWar.Symbols[number].actorID = sActor.ActorID;
            odWar.Symbols[number].broken = false;
            map.Announce(string.Format(Singleton<LocalManager>.Instance.Strings.ODWAR_SYMBOL_ACTIVATE, (object)number));
            if (!this.IsDefence(mapID))
            {
                bool flag = true;
                foreach (SagaDB.ODWar.ODWar.Symbol symbol in odWar.Symbols.Values)
                {
                    if (symbol.broken)
                        flag = false;
                }
                if (flag)
                    this.EndODWar(mapID, true);
            }
            return SymbolReviveResult.Success;
        }

        /// <summary>
        /// The SpawnBoss.
        /// </summary>
        /// <param name="mapID">The mapID<see cref="uint"/>.</param>
        public void SpawnBoss(uint mapID)
        {
            SagaDB.ODWar.ODWar odWar = Factory<ODWarFactory, SagaDB.ODWar.ODWar>.Instance.Items[mapID];
            Map map = Singleton<MapManager>.Instance.GetMap(mapID);
            foreach (SagaDB.ODWar.ODWar.Symbol symbol in odWar.Symbols.Values)
            {
                short x = Global.PosX8to16(symbol.x, map.Width);
                short y = Global.PosY8to16(symbol.y, map.Height);
                uint mobID = odWar.Boss[Global.Random.Next(0, odWar.Boss.Count - 1)];
                short[] randomPosAroundPos = map.GetRandomPosAroundPos(x, y, 1500);
                map.SpawnMob(mobID, randomPosAroundPos[0], randomPosAroundPos[1], (short)2000, (SagaDB.Actor.Actor)null);
            }
        }

        /// <summary>
        /// The SpawnMob.
        /// </summary>
        /// <param name="mapID">The mapID<see cref="uint"/>.</param>
        /// <param name="strong">The strong<see cref="bool"/>.</param>
        public void SpawnMob(uint mapID, bool strong)
        {
            SagaDB.ODWar.ODWar odWar = Factory<ODWarFactory, SagaDB.ODWar.ODWar>.Instance.Items[mapID];
            Map map = Singleton<MapManager>.Instance.GetMap(mapID);
            foreach (SagaDB.ODWar.ODWar.Symbol symbol in odWar.Symbols.Values)
            {
                short x = Global.PosX8to16(symbol.x, map.Width);
                short y = Global.PosY8to16(symbol.y, map.Height);
                if (strong)
                {
                    for (int index = 0; index < odWar.WaveStrong.DEMChamp; ++index)
                    {
                        uint mobID = odWar.DEMChamp[Global.Random.Next(0, odWar.DEMChamp.Count - 1)];
                        short[] randomPosAroundPos = map.GetRandomPosAroundPos(x, y, 1500);
                        map.SpawnMob(mobID, randomPosAroundPos[0], randomPosAroundPos[1], (short)2000, (SagaDB.Actor.Actor)null);
                    }
                    for (int index = 0; index < odWar.WaveStrong.DEMNormal; ++index)
                    {
                        uint mobID = odWar.DEMNormal[Global.Random.Next(0, odWar.DEMNormal.Count - 1)];
                        short[] randomPosAroundPos = map.GetRandomPosAroundPos(x, y, 1500);
                        map.SpawnMob(mobID, randomPosAroundPos[0], randomPosAroundPos[1], (short)2000, (SagaDB.Actor.Actor)null);
                    }
                }
                else
                {
                    for (int index = 0; index < odWar.WaveWeak.DEMChamp; ++index)
                    {
                        uint mobID = odWar.DEMChamp[Global.Random.Next(0, odWar.DEMChamp.Count - 1)];
                        short[] randomPosAroundPos = map.GetRandomPosAroundPos(x, y, 1500);
                        map.SpawnMob(mobID, randomPosAroundPos[0], randomPosAroundPos[1], (short)2000, (SagaDB.Actor.Actor)null);
                    }
                    for (int index = 0; index < odWar.WaveWeak.DEMNormal; ++index)
                    {
                        uint mobID = odWar.DEMNormal[Global.Random.Next(0, odWar.DEMNormal.Count - 1)];
                        short[] randomPosAroundPos = map.GetRandomPosAroundPos(x, y, 1500);
                        map.SpawnMob(mobID, randomPosAroundPos[0], randomPosAroundPos[1], (short)2000, (SagaDB.Actor.Actor)null);
                    }
                }
            }
        }

        /// <summary>
        /// The CanApply.
        /// </summary>
        /// <param name="mapID">.</param>
        /// <returns>.</returns>
        public bool CanApply(uint mapID)
        {
            if (!this.IsDefence(mapID))
                return false;
            SagaDB.ODWar.ODWar odWar = Factory<ODWarFactory, SagaDB.ODWar.ODWar>.Instance.Items[mapID];
            Dictionary<int, int> startTime1 = odWar.StartTime;
            DateTime dateTime = DateTime.Today;
            int dayOfWeek1 = (int)dateTime.DayOfWeek;
            if (!startTime1.ContainsKey(dayOfWeek1))
                return false;
            dateTime = DateTime.Now;
            int hour = dateTime.Hour;
            Dictionary<int, int> startTime2 = odWar.StartTime;
            dateTime = DateTime.Today;
            int dayOfWeek2 = (int)dateTime.DayOfWeek;
            int num = startTime2[dayOfWeek2];
            if (hour < num)
                return true;
            dateTime = DateTime.Now;
            return dateTime.Minute < 15;
        }

        /// <summary>
        /// The EndODWar.
        /// </summary>
        /// <param name="mapID">.</param>
        /// <param name="win">The win<see cref="bool"/>.</param>
        public void EndODWar(uint mapID, bool win)
        {
            SagaDB.ODWar.ODWar odWar = Factory<ODWarFactory, SagaDB.ODWar.ODWar>.Instance.Items[mapID];
            Map map = Singleton<MapManager>.Instance.GetMap(mapID);
            if (this.IsDefence(mapID))
            {
                if (!win)
                {
                    Singleton<ScriptManager>.Instance.VariableHolder.AInt["ODWar" + mapID.ToString() + "Captured"] = 1;
                    MapClientManager.Instance.Announce(Singleton<LocalManager>.Instance.Strings.ODWAR_LOSE);
                    foreach (SagaDB.Actor.Actor actor in map.Actors.Values.ToList<SagaDB.Actor.Actor>())
                    {
                        if (actor.type == ActorType.MOB)
                        {
                            MobEventHandler e = (MobEventHandler)actor.e;
                            if (!e.AI.Mode.Symbol && !e.AI.Mode.SymbolTrash)
                                e.OnDie();
                        }
                    }
                }
                else
                {
                    MapClientManager.Instance.Announce(Singleton<LocalManager>.Instance.Strings.ODWAR_WIN);
                    MapClientManager.Instance.Announce(Singleton<LocalManager>.Instance.Strings.ODWAR_WIN2);
                    MapClientManager.Instance.Announce(Singleton<LocalManager>.Instance.Strings.ODWAR_WIN3);
                    MapClientManager.Instance.Announce(Singleton<LocalManager>.Instance.Strings.ODWAR_WIN4);
                    foreach (SagaDB.Actor.Actor actor in map.Actors.Values.ToList<SagaDB.Actor.Actor>())
                    {
                        if (actor.type == ActorType.MOB)
                        {
                            MobEventHandler e = (MobEventHandler)actor.e;
                            if (!e.AI.Mode.Symbol && !e.AI.Mode.SymbolTrash)
                                e.OnDie();
                        }
                    }
                }
                this.SendResult(mapID, win);
            }
            else if (win)
            {
                Singleton<ScriptManager>.Instance.VariableHolder.AInt["ODWar" + mapID.ToString() + "Captured"] = 0;
                MapClientManager.Instance.Announce(Singleton<LocalManager>.Instance.Strings.ODWAR_CAPTURE);
                foreach (SagaDB.Actor.Actor actor in map.Actors.Values.ToList<SagaDB.Actor.Actor>())
                {
                    if (actor.type == ActorType.MOB)
                    {
                        MobEventHandler e = (MobEventHandler)actor.e;
                        if (!e.AI.Mode.Symbol && !e.AI.Mode.SymbolTrash)
                            e.OnDie();
                    }
                }
            }
            foreach (SagaDB.Actor.Actor actor in map.Actors.Values.ToList<SagaDB.Actor.Actor>())
            {
                if (actor.type == ActorType.PC && ((ActorPC)actor).Online)
                {
                    MapClient.FromActorPC((ActorPC)actor).netIO.SendPacket((Packet)new SSMG_NPC_SET_EVENT_AREA()
                    {
                        StartX = 6U,
                        EndX = 6U,
                        StartY = (uint)sbyte.MaxValue,
                        EndY = (uint)sbyte.MaxValue,
                        EventID = 4043309056U,
                        EffectID = 9005U
                    });
                    MapClient.FromActorPC((ActorPC)actor).netIO.SendPacket((Packet)new SSMG_NPC_SET_EVENT_AREA()
                    {
                        StartX = 245U,
                        EndX = 245U,
                        StartY = (uint)sbyte.MaxValue,
                        EndY = (uint)sbyte.MaxValue,
                        EventID = 4043309057U,
                        EffectID = 9005U
                    });
                }
            }
            odWar.Score.Clear();
            odWar.Started = false;
        }

        /// <summary>
        /// The SendResult.
        /// </summary>
        /// <param name="mapID">The mapID<see cref="uint"/>.</param>
        /// <param name="win">The win<see cref="bool"/>.</param>
        private void SendResult(uint mapID, bool win)
        {
            SagaDB.ODWar.ODWar odWar = Factory<ODWarFactory, SagaDB.ODWar.ODWar>.Instance.Items[mapID];
            Map map = Singleton<MapManager>.Instance.GetMap(mapID);
            foreach (uint key in odWar.Score.Keys)
            {
                SagaDB.Actor.Actor actor = map.GetActor(key);
                if (actor != null && actor.type == ActorType.PC)
                {
                    uint num1 = (uint)odWar.Score[key];
                    ActorPC actorPc = (ActorPC)actor;
                    if (actorPc.Online)
                    {
                        if (num1 > 3000U)
                            num1 = 3000U;
                        if (actorPc.WRPRanking <= 10U)
                            num1 = (uint)((double)num1 * 1.5);
                        if (!win)
                            num1 = (uint)((double)num1 * 0.75);
                        if (win && num1 < 200U)
                            num1 = 200U;
                        uint num2 = (uint)((double)num1 * 0.600000023841858);
                        actorPc.CP += num1;
                        Singleton<ExperienceManager>.Instance.ApplyExp(actorPc, num2, num2, 1f);
                        MapClient.FromActorPC(actorPc).netIO.SendPacket((Packet)new SSMG_ODWAR_RESULT()
                        {
                            Win = win,
                            EXP = num2,
                            JEXP = num2,
                            CP = num1
                        });
                    }
                }
            }
        }
    }
}
