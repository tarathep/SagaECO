namespace SagaMap.Skill.SkillDefinations.Shaman
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.ActorEventHandlers;
    using SagaMap.Manager;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="FireWall" />.
    /// </summary>
    internal class FireWall : ISkill
    {
        /// <summary>
        /// The TryCast.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        /// <param name="dActor">The dActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="args">The args<see cref="SkillArg"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int TryCast(ActorPC pc, SagaDB.Actor.Actor dActor, SkillArg args)
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
            Map map = Singleton<MapManager>.Instance.GetMap(sActor.MapID);
            List<ActorSkill> actorSkill1 = new List<ActorSkill>();
            int x = (int)args.x;
            int y = (int)args.y;
            int num1 = (int)SagaLib.Global.PosX16to8(sActor.X, map.Width);
            int num2 = (int)SagaLib.Global.PosY16to8(sActor.Y, map.Height);
            int num3 = x - num1;
            int num4 = y - num2;
            Dictionary<int, int[]> dictionary = new Dictionary<int, int[]>();
            int index = 0;
            for (int key = 0; key <= 4; ++key)
            {
                ActorSkill actorSkill2 = new ActorSkill(args.skill, sActor);
                actorSkill1.Add(actorSkill2);
                actorSkill1[key].e = (ActorEventHandler)new NullEventHandler();
                actorSkill1[key].MapID = sActor.MapID;
                int[] numArray = new int[2] { 0, 0 };
                dictionary.Add(key, numArray);
            }
            int flag;
            if (num3 != 0 || num4 != 0)
            {
                if (Math.Abs(num3) != Math.Abs(num4))
                {
                    actorSkill1.Remove(actorSkill1[actorSkill1.Count - 1]);
                    actorSkill1.Remove(actorSkill1[actorSkill1.Count - 1]);
                    if (Math.Abs(num4) > Math.Abs(num3))
                    {
                        dictionary[0][0] = x;
                        dictionary[0][1] = y;
                        dictionary[1][0] = x + 1;
                        dictionary[1][1] = y;
                        dictionary[2][0] = x - 1;
                        dictionary[2][1] = y;
                        flag = num4 <= 0 ? 2 : 1;
                    }
                    else
                    {
                        dictionary[0][0] = x;
                        dictionary[0][1] = y;
                        dictionary[1][0] = x;
                        dictionary[1][1] = y + 1;
                        dictionary[2][0] = x;
                        dictionary[2][1] = y - 1;
                        flag = num3 <= 0 ? 4 : 3;
                    }
                }
                else if (num3 * num4 < 0)
                {
                    dictionary[0][0] = x;
                    dictionary[0][1] = y;
                    dictionary[1][0] = x + 1;
                    dictionary[1][1] = y + 1;
                    dictionary[2][0] = x - 1;
                    dictionary[2][1] = y - 1;
                    if (x > 0)
                    {
                        dictionary[3][0] = x;
                        dictionary[3][1] = y + 1;
                        dictionary[4][0] = x - 1;
                        dictionary[4][1] = y;
                        flag = 5;
                    }
                    else
                    {
                        dictionary[3][0] = x;
                        dictionary[3][1] = y - 1;
                        dictionary[4][0] = x + 1;
                        dictionary[4][1] = y;
                        flag = 6;
                    }
                }
                else
                {
                    dictionary[0][0] = x;
                    dictionary[0][1] = y;
                    dictionary[1][0] = x + 1;
                    dictionary[1][1] = y - 1;
                    dictionary[2][0] = x - 1;
                    dictionary[2][1] = y + 1;
                    if (x > 0)
                    {
                        dictionary[3][0] = x;
                        dictionary[3][1] = y - 1;
                        dictionary[4][0] = x - 1;
                        dictionary[4][1] = y;
                        flag = 7;
                    }
                    else
                    {
                        dictionary[3][0] = x;
                        dictionary[3][1] = y + 1;
                        dictionary[4][0] = x + 1;
                        dictionary[4][1] = y;
                        flag = 8;
                    }
                }
            }
            else
            {
                dictionary[0][0] = x;
                dictionary[0][1] = y;
                dictionary[1][0] = x;
                dictionary[1][1] = y + 1;
                dictionary[2][0] = x;
                dictionary[2][1] = y - 1;
                flag = 9;
            }
            foreach (ActorSkill actorSkill2 in actorSkill1)
            {
                if (dictionary[index][0] != 0 && dictionary[index][1] != 0)
                {
                    actorSkill2.X = SagaLib.Global.PosX8to16((byte)dictionary[index][0], map.Width);
                    actorSkill2.Y = SagaLib.Global.PosY8to16((byte)dictionary[index][1], map.Height);
                    map.RegisterActor((SagaDB.Actor.Actor)actorSkill2);
                    actorSkill2.invisble = false;
                    map.OnActorVisibilityChange((SagaDB.Actor.Actor)actorSkill2);
                }
                ++index;
            }
            new FireWall.Activator(sActor, args, level, flag, actorSkill1).Activate();
        }

        /// <summary>
        /// Defines the <see cref="Activator" />.
        /// </summary>
        private class Activator : MultiRunTask
        {
            /// <summary>
            /// Defines the actorSkill.
            /// </summary>
            private List<ActorSkill> actorSkill = new List<ActorSkill>();

            /// <summary>
            /// Defines the timeMax.
            /// </summary>
            private DateTime timeMax = DateTime.Now + new TimeSpan(0, 0, 0, 0, 10000);

            /// <summary>
            /// Defines the countMax.
            /// </summary>
            private int countMax = 3;

            /// <summary>
            /// Defines the factor.
            /// </summary>
            private float factor = 1f;

            /// <summary>
            /// Defines the count.
            /// </summary>
            private int[] count = new int[5];

            /// <summary>
            /// Defines the destroyFlag.
            /// </summary>
            private bool[] destroyFlag = new bool[5];

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
            /// Defines the flag.
            /// </summary>
            private int flag;

            /// <summary>
            /// Initializes a new instance of the <see cref="Activator"/> class.
            /// </summary>
            /// <param name="caster">The caster<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="args">The args<see cref="SkillArg"/>.</param>
            /// <param name="level">The level<see cref="byte"/>.</param>
            /// <param name="flag">The flag<see cref="int"/>.</param>
            /// <param name="actorSkill">The actorSkill<see cref="List{ActorSkill}"/>.</param>
            public Activator(SagaDB.Actor.Actor caster, SkillArg args, byte level, int flag, List<ActorSkill> actorSkill)
            {
                this.caster = caster;
                this.actorSkill = actorSkill;
                this.skill = args.Clone();
                this.map = Singleton<MapManager>.Instance.GetMap(actorSkill[0].MapID);
                this.period = 100;
                this.dueTime = 0;
                this.flag = flag;
                switch (level)
                {
                    case 1:
                        this.factor = 0.25f;
                        this.countMax = 4;
                        break;
                    case 2:
                        this.factor = 0.3f;
                        this.countMax = 5;
                        break;
                    case 3:
                        this.factor = 0.35f;
                        this.countMax = 6;
                        break;
                    case 4:
                        this.factor = 0.4f;
                        this.countMax = 7;
                        break;
                    case 5:
                        this.factor = 0.45f;
                        this.countMax = 8;
                        break;
                }
            }

            /// <summary>
            /// The CallBack.
            /// </summary>
            /// <param name="o">The o<see cref="object"/>.</param>
            public override void CallBack(object o)
            {
                ClientManager.EnterCriticalArea();
                short[] pos = new short[2];
                int index = 0;
                List<SagaDB.Actor.Actor> actorList = new List<SagaDB.Actor.Actor>();
                List<SagaDB.Actor.Actor> dActor1 = new List<SagaDB.Actor.Actor>();
                try
                {
                    if (DateTime.Now <= this.timeMax)
                    {
                        foreach (ActorSkill actorSkill in this.actorSkill)
                        {
                            dActor1.Clear();
                            this.skill.affectedActors.Clear();
                            if (this.count[index] <= this.countMax - 1)
                            {
                                foreach (SagaDB.Actor.Actor mActor in this.map.GetActorsArea((SagaDB.Actor.Actor)actorSkill, (short)50, false))
                                {
                                    if (Singleton<SkillHandler>.Instance.CheckValidAttackTarget(this.caster, (SagaDB.Actor.Actor)actorSkill) && !dActor1.Contains(mActor))
                                    {
                                        dActor1.Add(mActor);
                                        this.count[index] = this.count[index] + 1;
                                        if (mActor.LastX != (short)0 && mActor.LastY != (short)0)
                                        {
                                            pos[0] = mActor.LastX;
                                            pos[1] = mActor.LastY;
                                            this.map.MoveActor(Map.MOVE_TYPE.START, mActor, pos, (ushort)500, (ushort)500);
                                            pos[0] = (short)(2 * (int)mActor.X - (int)mActor.LastX);
                                            pos[1] = (short)(2 * (int)mActor.Y - (int)mActor.LastY);
                                            this.map.MoveActor(Map.MOVE_TYPE.START, mActor, pos, (ushort)500, (ushort)500);
                                        }
                                    }
                                }
                                Singleton<SkillHandler>.Instance.MagicAttack(this.caster, dActor1, this.skill, Elements.Fire, this.factor);
                                this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, (MapEventArgs)this.skill, (SagaDB.Actor.Actor)this.actorSkill[0], false);
                            }
                            else
                            {
                                if (!this.destroyFlag[index])
                                    this.map.DeleteActor((SagaDB.Actor.Actor)actorSkill);
                                this.destroyFlag[index] = true;
                            }
                            ++index;
                        }
                    }
                    else
                    {
                        this.Deactivate();
                        foreach (SagaDB.Actor.Actor dActor2 in this.actorSkill)
                            this.map.DeleteActor(dActor2);
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
