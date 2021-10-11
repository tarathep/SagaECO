namespace SagaMap.Skill.SkillDefinations.Elementaler
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.ActorEventHandlers;
    using SagaMap.Manager;
    using SagaMap.Mob;
    using SagaMap.Skill.Additions.Global;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="AquaWave" />.
    /// </summary>
    public class AquaWave : ISkill
    {
        /// <summary>
        /// The TryCast.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="ActorPC"/>.</param>
        /// <param name="dActor">The dActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="args">The args<see cref="SkillArg"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int TryCast(ActorPC sActor, SagaDB.Actor.Actor dActor, SkillArg args)
        {
            return 0;
        }

        /// <summary>
        /// The Proc.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="dActor">The dActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="args">The args<see cref="SkillArg"/>.</param>
        /// <param name="level">The level<see cref="byte"/>.</param>
        public void Proc(SagaDB.Actor.Actor sActor, SagaDB.Actor.Actor dActor, SkillArg args, byte level)
        {
            ushort[] numArray1 = new ushort[4]
            {
        (ushort) 400,
        (ushort) 500,
        (ushort) 600,
        (ushort) 700
            };
            Map map = Singleton<MapManager>.Instance.GetMap(sActor.MapID);
            byte[] numArray2 = new byte[3];
            byte[] numArray3 = new byte[3];
            byte[] numArray4 = new byte[3];
            byte[] numArray5 = new byte[3];
            switch (Singleton<SkillHandler>.Instance.GetDirection(sActor))
            {
                case SkillHandler.ActorDirection.South:
                    Singleton<SkillHandler>.Instance.GetRelatedPos(sActor, -1, -1, out numArray2[0], out numArray3[0]);
                    Singleton<SkillHandler>.Instance.GetRelatedPos(sActor, 0, -1, out numArray2[1], out numArray3[1]);
                    Singleton<SkillHandler>.Instance.GetRelatedPos(sActor, 1, -1, out numArray2[2], out numArray3[2]);
                    Singleton<SkillHandler>.Instance.GetRelatedPos(sActor, 0, 13, numArray2[0], numArray3[0], out numArray4[0], out numArray5[0]);
                    Singleton<SkillHandler>.Instance.GetRelatedPos(sActor, 0, 13, numArray2[1], numArray3[1], out numArray4[1], out numArray5[1]);
                    Singleton<SkillHandler>.Instance.GetRelatedPos(sActor, 0, 13, numArray2[2], numArray3[2], out numArray4[2], out numArray5[2]);

                    break;
                case SkillHandler.ActorDirection.SouthWest:
                    Singleton<SkillHandler>.Instance.GetRelatedPos(sActor, 1, -1, out numArray2[0], out numArray3[0]);
                    Singleton<SkillHandler>.Instance.GetRelatedPos(sActor, 1, 0, out numArray2[1], out numArray3[1]);
                    Singleton<SkillHandler>.Instance.GetRelatedPos(sActor, 0, -1, out numArray2[2], out numArray3[2]);
                    Singleton<SkillHandler>.Instance.GetRelatedPos(sActor, -13, 13, numArray2[0], numArray3[0], out numArray4[0], out numArray5[0]);
                    Singleton<SkillHandler>.Instance.GetRelatedPos(sActor, -13, 13, numArray2[1], numArray3[1], out numArray4[1], out numArray5[1]);
                    Singleton<SkillHandler>.Instance.GetRelatedPos(sActor, -13, 13, numArray2[2], numArray3[2], out numArray4[2], out numArray5[2]);

                    break;
                case SkillHandler.ActorDirection.West:
                    Singleton<SkillHandler>.Instance.GetRelatedPos(sActor, 1, -1, out numArray2[0], out numArray3[0]);
                    Singleton<SkillHandler>.Instance.GetRelatedPos(sActor, 1, 0, out numArray2[1], out numArray3[1]);
                    Singleton<SkillHandler>.Instance.GetRelatedPos(sActor, 1, 1, out numArray2[2], out numArray3[2]);
                    Singleton<SkillHandler>.Instance.GetRelatedPos(sActor, -13, 0, numArray2[0], numArray3[0], out numArray4[0], out numArray5[0]);
                    Singleton<SkillHandler>.Instance.GetRelatedPos(sActor, -13, 0, numArray2[1], numArray3[1], out numArray4[1], out numArray5[1]);
                    Singleton<SkillHandler>.Instance.GetRelatedPos(sActor, -13, 0, numArray2[2], numArray3[2], out numArray4[2], out numArray5[2]);
                    break;
                case SkillHandler.ActorDirection.NorthWest:
                    Singleton<SkillHandler>.Instance.GetRelatedPos(sActor, 1, 1, out numArray2[0], out numArray3[0]);
                    Singleton<SkillHandler>.Instance.GetRelatedPos(sActor, 1, 0, out numArray2[1], out numArray3[1]);
                    Singleton<SkillHandler>.Instance.GetRelatedPos(sActor, 0, 1, out numArray2[2], out numArray3[2]);
                    Singleton<SkillHandler>.Instance.GetRelatedPos(sActor, -13, -13, numArray2[0], numArray3[0], out numArray4[0], out numArray5[0]);
                    Singleton<SkillHandler>.Instance.GetRelatedPos(sActor, -13, -13, numArray2[1], numArray3[1], out numArray4[1], out numArray5[1]);
                    Singleton<SkillHandler>.Instance.GetRelatedPos(sActor, -13, -13, numArray2[2], numArray3[2], out numArray4[2], out numArray5[2]);
                    break;
                case SkillHandler.ActorDirection.North:
                    //SWITCH NORTH & SOUTH
                    Singleton<SkillHandler>.Instance.GetRelatedPos(sActor, -1, 1, out numArray2[0], out numArray3[0]);
                    Singleton<SkillHandler>.Instance.GetRelatedPos(sActor, 0, 1, out numArray2[1], out numArray3[1]);
                    Singleton<SkillHandler>.Instance.GetRelatedPos(sActor, 1, 1, out numArray2[2], out numArray3[2]);
                    Singleton<SkillHandler>.Instance.GetRelatedPos(sActor, 0, -13, numArray2[0], numArray3[0], out numArray4[0], out numArray5[0]);
                    Singleton<SkillHandler>.Instance.GetRelatedPos(sActor, 0, -13, numArray2[1], numArray3[1], out numArray4[1], out numArray5[1]);
                    Singleton<SkillHandler>.Instance.GetRelatedPos(sActor, 0, -13, numArray2[2], numArray3[2], out numArray4[2], out numArray5[2]);
                    break;
                case SkillHandler.ActorDirection.NorthEast:
                    Singleton<SkillHandler>.Instance.GetRelatedPos(sActor, -1, 1, out numArray2[0], out numArray3[0]);
                    Singleton<SkillHandler>.Instance.GetRelatedPos(sActor, -1, 0, out numArray2[1], out numArray3[1]);
                    Singleton<SkillHandler>.Instance.GetRelatedPos(sActor, 0, 1, out numArray2[2], out numArray3[2]);
                    Singleton<SkillHandler>.Instance.GetRelatedPos(sActor, 13, -13, numArray2[0], numArray3[0], out numArray4[0], out numArray5[0]);
                    Singleton<SkillHandler>.Instance.GetRelatedPos(sActor, 13, -13, numArray2[1], numArray3[1], out numArray4[1], out numArray5[1]);
                    Singleton<SkillHandler>.Instance.GetRelatedPos(sActor, 13, -13, numArray2[2], numArray3[2], out numArray4[2], out numArray5[2]);
                    break;
                case SkillHandler.ActorDirection.East:
                    Singleton<SkillHandler>.Instance.GetRelatedPos(sActor, -1, -1, out numArray2[0], out numArray3[0]);
                    Singleton<SkillHandler>.Instance.GetRelatedPos(sActor, -1, 0, out numArray2[1], out numArray3[1]);
                    Singleton<SkillHandler>.Instance.GetRelatedPos(sActor, -1, 1, out numArray2[2], out numArray3[2]);
                    Singleton<SkillHandler>.Instance.GetRelatedPos(sActor, 13, 0, numArray2[0], numArray3[0], out numArray4[0], out numArray5[0]);
                    Singleton<SkillHandler>.Instance.GetRelatedPos(sActor, 13, 0, numArray2[1], numArray3[1], out numArray4[1], out numArray5[1]);
                    Singleton<SkillHandler>.Instance.GetRelatedPos(sActor, 13, 0, numArray2[2], numArray3[2], out numArray4[2], out numArray5[2]);
                    break;
                case SkillHandler.ActorDirection.SouthEast:
                    Singleton<SkillHandler>.Instance.GetRelatedPos(sActor, -1, -1, out numArray2[0], out numArray3[0]);
                    Singleton<SkillHandler>.Instance.GetRelatedPos(sActor, -1, 0, out numArray2[1], out numArray3[1]);
                    Singleton<SkillHandler>.Instance.GetRelatedPos(sActor, 0, -1, out numArray2[2], out numArray3[2]);
                    Singleton<SkillHandler>.Instance.GetRelatedPos(sActor, 13, 13, numArray2[0], numArray3[0], out numArray4[0], out numArray5[0]);
                    Singleton<SkillHandler>.Instance.GetRelatedPos(sActor, 13, 13, numArray2[1], numArray3[1], out numArray4[1], out numArray5[1]);
                    Singleton<SkillHandler>.Instance.GetRelatedPos(sActor, 13, 13, numArray2[2], numArray3[2], out numArray4[2], out numArray5[2]);


                    break;
            }
            for (int index = 0; index < 3; ++index)
            {
                ActorSkill actor = new ActorSkill(args.skill, sActor);
                actor.MapID = sActor.MapID;
                actor.X = SagaLib.Global.PosX8to16(numArray2[0], map.Width);
                actor.Y = SagaLib.Global.PosY8to16(numArray3[0], map.Height);
                actor.Speed = numArray1[(int)level];
                List<MapNode> path = new MobAI((SagaDB.Actor.Actor)actor, true).FindPath(numArray2[index], numArray3[index], numArray4[index], numArray5[index]);
                if (path.Count >= 2)
                {
                    int num1 = (int)path[path.Count - 1].x - (int)path[path.Count - 2].x;
                    int num2 = (int)path[path.Count - 1].y - (int)path[path.Count - 2].y;
                    int num3 = (int)path[path.Count - 1].x + num1;
                    int num4 = (int)path[path.Count - 1].y + num2;
                    path.Add(new MapNode()
                    {
                        x = (byte)num3,
                        y = (byte)num4
                    });
                }
                if (path.Count == 1)
                {
                    int num1 = (int)path[path.Count - 1].x - (int)SagaLib.Global.PosX16to8((short)numArray2[index], map.Width);
                    int num2 = (int)path[path.Count - 1].y - (int)SagaLib.Global.PosY16to8((short)numArray3[index], map.Height);
                    int num3 = (int)path[path.Count - 1].x + num1;
                    int num4 = (int)path[path.Count - 1].y + num2;
                    path.Add(new MapNode()
                    {
                        x = (byte)num3,
                        y = (byte)num4
                    });
                }
                actor.e = (ActorEventHandler)new NullEventHandler();
                map.RegisterActor((SagaDB.Actor.Actor)actor);
                actor.invisble = false;
                map.OnActorVisibilityChange((SagaDB.Actor.Actor)actor);
                new AquaWave.Activator(sActor, actor, args, path).Activate();
            }
        }

        /// <summary>
        /// Defines the <see cref="Activator" />.
        /// </summary>
        private class Activator : MultiRunTask
        {
            /// <summary>
            /// Defines the count.
            /// </summary>
            private int count = 0;

            /// <summary>
            /// Defines the factor.
            /// </summary>
            private float factor = 1f;

            /// <summary>
            /// Defines the stop.
            /// </summary>
            private bool stop = false;

            /// <summary>
            /// Defines the actor.
            /// </summary>
            private ActorSkill actor;

            /// <summary>
            /// Defines the caster.
            /// </summary>
            private SagaDB.Actor.Actor caster;

            /// <summary>
            /// Defines the skill.
            /// </summary>
            private SkillArg skill;

            /// <summary>
            /// Defines the map.
            /// </summary>
            private Map map;

            /// <summary>
            /// Defines the path.
            /// </summary>
            private List<MapNode> path;

            /// <summary>
            /// Defines the element.
            /// </summary>
            private Elements element;

            /// <summary>
            /// Initializes a new instance of the <see cref="Activator"/> class.
            /// </summary>
            /// <param name="caster">The caster<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="actor">The actor<see cref="ActorSkill"/>.</param>
            /// <param name="args">The args<see cref="SkillArg"/>.</param>
            /// <param name="path">The path<see cref="List{MapNode}"/>.</param>
            public Activator(SagaDB.Actor.Actor caster, ActorSkill actor, SkillArg args, List<MapNode> path)
            {
                this.actor = actor;
                this.caster = caster;
                this.skill = args.Clone();
                this.map = Singleton<MapManager>.Instance.GetMap(actor.MapID);
                this.period = 200;
                this.dueTime = 200;
                this.path = path;
                this.factor = this.CalcFactor(this.skill.skill.Level);
                this.element = Elements.Water;
            }

            /// <summary>
            /// The CalcFactor.
            /// </summary>
            /// <param name="level">技能等级.</param>
            /// <returns>伤害加成.</returns>
            private float CalcFactor(byte level)
            {
                return new float[4] { 0.0f, 1.5f, 1.75f, 2f }[(int)level];
            }

            /// <summary>
            /// The CallBack.
            /// </summary>
            /// <param name="o">The o<see cref="object"/>.</param>
            public override void CallBack(object o)
            {
                ClientManager.EnterCriticalArea();
                try
                {
                    if (this.path.Count <= this.count + 1)
                    {
                        this.Deactivate();
                        this.map.DeleteActor((SagaDB.Actor.Actor)this.actor);
                    }
                    else
                    {
                        try
                        {
                            short[] pos1 = new short[2];
                            short[] pos2 = new short[2];
                            pos1[0] = SagaLib.Global.PosX8to16(this.path[this.count].x, this.map.Width);
                            pos1[1] = SagaLib.Global.PosY8to16(this.path[this.count].y, this.map.Height);
                            pos2[0] = SagaLib.Global.PosX8to16(this.path[this.count + 1].x, this.map.Width);
                            pos2[1] = SagaLib.Global.PosY8to16(this.path[this.count + 1].y, this.map.Height);


                            this.map.MoveActor(Map.MOVE_TYPE.START, (SagaDB.Actor.Actor)this.actor, pos1, (ushort)0, this.actor.Speed);
                            List<SagaDB.Actor.Actor> actorsArea = this.map.GetActorsArea((SagaDB.Actor.Actor)this.actor, (short)50, false);
                            List<SagaDB.Actor.Actor> dActor1 = new List<SagaDB.Actor.Actor>();
                            foreach (SagaDB.Actor.Actor dActor2 in actorsArea)
                            {
                                if (Singleton<SkillHandler>.Instance.CheckValidAttackTarget(this.caster, dActor2))
                                    dActor1.Add(dActor2);
                            }
                            if (this.map.GetActorsArea(pos2[0], pos2[1], (short)50, new SagaDB.Actor.Actor[0]).Count != 0 || this.map.Info.walkable[(int)this.path[this.count + 1].x, (int)this.path[this.count + 1].y] != (byte)2)
                            {
                                if (this.stop)
                                {
                                    this.Deactivate();
                                    this.map.DeleteActor((SagaDB.Actor.Actor)this.actor);
                                    ClientManager.LeaveCriticalArea();
                                    return;
                                }
                                this.stop = true;
                            }
                            foreach (SagaDB.Actor.Actor actor in dActor1)
                            {
                                硬直 硬直 = new 硬直(this.skill.skill, actor, 400);
                                SkillHandler.ApplyAddition(actor, (Addition)硬直);
                                this.map.MoveActor(Map.MOVE_TYPE.START, actor, pos2, (ushort)500, actor.Speed, true);
                                if (actor.type == ActorType.MOB || actor.type == ActorType.PET || actor.type == ActorType.SHADOW)
                                    ((MobEventHandler)actor.e).AI.OnPathInterupt();
                            }
                            this.skill.affectedActors.Clear();
                            Singleton<SkillHandler>.Instance.MagicAttack(this.caster, dActor1, this.skill, this.element, this.factor);
                            this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, (MapEventArgs)this.skill, (SagaDB.Actor.Actor)this.actor, false);
                        }
                        catch
                        {
                        }
                        ++this.count;
                    }
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
                ClientManager.LeaveCriticalArea();
            }
        }
    }
}
