namespace SagaMap.Skill.SkillDefinations.Enchanter
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// 大地結界（アースパワーサークル）
    /// 神風結界（ウィンドパワーサークル）
    /// 火焰結界（ファイアパワーサークル）
    /// 寒冰結界（ウォーターパワーサークル）.
    /// </summary>
    public class ElementCircle : ISkill
    {
        /// <summary>
        /// Defines the MapElement.
        /// </summary>
        private Elements MapElement = Elements.Neutral;

        /// <summary>
        /// Defines the MobUse.
        /// </summary>
        private bool MobUse;

        /// <summary>
        /// Initializes a new instance of the <see cref="ElementCircle"/> class.
        /// </summary>
        /// <param name="e">The e<see cref="Elements"/>.</param>
        public ElementCircle(Elements e)
        {
            this.MapElement = e;
            this.MobUse = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ElementCircle"/> class.
        /// </summary>
        /// <param name="e">The e<see cref="Elements"/>.</param>
        /// <param name="MobUse">The MobUse<see cref="bool"/>.</param>
        public ElementCircle(Elements e, bool MobUse)
        {
            this.MapElement = e;
            this.MobUse = MobUse;
        }

        /// <summary>
        /// The TryCast.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        /// <param name="dActor">The dActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="args">The args<see cref="SkillArg"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int TryCast(ActorPC pc, SagaDB.Actor.Actor dActor, SkillArg args)
        {
            Map map = Singleton<MapManager>.Instance.GetMap(pc.MapID);
            return map.CheckActorSkillInRange(SagaLib.Global.PosX8to16(args.x, map.Width), SagaLib.Global.PosY8to16(args.y, map.Height), (short)100) ? -17 : 0;
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
            if (this.MobUse)
                level = (byte)5;
            int lifetime = 25 + 5 * (int)level;
            ElementCircle.ElementCircleBuff elementCircleBuff = new ElementCircle.ElementCircleBuff(args.skill, sActor, this.MapElement.ToString() + nameof(ElementCircle), lifetime, this.MapElement, args.x, args.y);
            SkillHandler.ApplyAddition(sActor, (Addition)elementCircleBuff);
        }

        /// <summary>
        /// Defines the <see cref="ElementCircleBuff" />.
        /// </summary>
        public class ElementCircleBuff : DefaultBuff
        {
            /// <summary>
            /// Defines the map.
            /// </summary>
            private Map map;

            /// <summary>
            /// Defines the centerX.
            /// </summary>
            private byte centerX;

            /// <summary>
            /// Defines the centerY.
            /// </summary>
            private byte centerY;

            /// <summary>
            /// Defines the prefix.
            /// </summary>
            private string prefix;

            /// <summary>
            /// Defines the MapElement.
            /// </summary>
            private Elements MapElement;

            /// <summary>
            /// Initializes a new instance of the <see cref="ElementCircleBuff"/> class.
            /// </summary>
            /// <param name="skill">The skill<see cref="SagaDB.Skill.Skill"/>.</param>
            /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="AdditionName">The AdditionName<see cref="string"/>.</param>
            /// <param name="lifetime">The lifetime<see cref="int"/>.</param>
            /// <param name="e">The e<see cref="Elements"/>.</param>
            /// <param name="x">The x<see cref="byte"/>.</param>
            /// <param name="y">The y<see cref="byte"/>.</param>
            public ElementCircleBuff(SagaDB.Skill.Skill skill, SagaDB.Actor.Actor actor, string AdditionName, int lifetime, Elements e, byte x, byte y)
        : base(skill, actor, AdditionName, lifetime)
            {
                this.OnAdditionStart += new DefaultBuff.StartEventHandler(this.StartEvent);
                this.OnAdditionEnd += new DefaultBuff.EndEventHandler(this.EndEvent);
                this.MapElement = e;
                this.centerX = x;
                this.centerY = y;
                this.prefix = this.MapElement.ToString() + nameof(ElementCircle);
                this.map = Singleton<MapManager>.Instance.GetMap(actor.MapID);
            }

            /// <summary>
            /// The StartEvent.
            /// </summary>
            /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
            private void StartEvent(SagaDB.Actor.Actor actor, DefaultBuff skill)
            {
                switch (this.MapElement)
                {
                    case Elements.Fire:
                        for (int x = (int)this.centerX - 1; x <= (int)this.centerX + 1; ++x)
                        {
                            for (int y = (int)this.centerY - 1; y <= (int)this.centerY + 1; ++y)
                            {
                                if (x >= 0 && x <= (int)byte.MaxValue && (y >= 0 && y <= (int)byte.MaxValue))
                                {
                                    if (skill.Variable.ContainsKey(this.getVariableKey(x, y)))
                                        skill.Variable.Remove(this.getVariableKey(x, y));
                                    skill.Variable.Add(this.getVariableKey(x, y), (int)this.map.Info.fire[x, y]);
                                    this.map.Info.fire[x, y] = (byte)50;
                                }
                            }
                        }
                        break;
                    case Elements.Water:
                        for (int x = (int)this.centerX - 1; x <= (int)this.centerX + 1; ++x)
                        {
                            for (int y = (int)this.centerY - 1; y <= (int)this.centerY + 1; ++y)
                            {
                                if (x >= 0 && x <= (int)byte.MaxValue && (y >= 0 && y <= (int)byte.MaxValue))
                                {
                                    if (skill.Variable.ContainsKey(this.getVariableKey(x, y)))
                                        skill.Variable.Remove(this.getVariableKey(x, y));
                                    skill.Variable.Add(this.getVariableKey(x, y), (int)this.map.Info.water[x, y]);
                                    this.map.Info.water[x, y] = (byte)50;
                                }
                            }
                        }
                        break;
                    case Elements.Wind:
                        for (int x = (int)this.centerX - 1; x <= (int)this.centerX + 1; ++x)
                        {
                            for (int y = (int)this.centerY - 1; y <= (int)this.centerY + 1; ++y)
                            {
                                if (x >= 0 && x <= (int)byte.MaxValue && (y >= 0 && y <= (int)byte.MaxValue))
                                {
                                    if (skill.Variable.ContainsKey(this.getVariableKey(x, y)))
                                        skill.Variable.Remove(this.getVariableKey(x, y));
                                    skill.Variable.Add(this.getVariableKey(x, y), (int)this.map.Info.wind[x, y]);
                                    this.map.Info.wind[x, y] = (byte)50;
                                }
                            }
                        }
                        break;
                    case Elements.Earth:
                        for (int x = (int)this.centerX - 1; x <= (int)this.centerX + 1; ++x)
                        {
                            for (int y = (int)this.centerY - 1; y <= (int)this.centerY + 1; ++y)
                            {
                                if (x >= 0 && x <= (int)byte.MaxValue && (y >= 0 && y <= (int)byte.MaxValue))
                                {
                                    if (skill.Variable.ContainsKey(this.getVariableKey(x, y)))
                                        skill.Variable.Remove(this.getVariableKey(x, y));
                                    skill.Variable.Add(this.getVariableKey(x, y), (int)this.map.Info.earth[x, y]);
                                    this.map.Info.earth[x, y] = (byte)50;
                                }
                            }
                        }
                        break;
                    case Elements.Holy:
                        for (int x = (int)this.centerX - 1; x <= (int)this.centerX + 1; ++x)
                        {
                            for (int y = (int)this.centerY - 1; y <= (int)this.centerY + 1; ++y)
                            {
                                if (x >= 0 && x <= (int)byte.MaxValue && (y >= 0 && y <= (int)byte.MaxValue))
                                {
                                    if (skill.Variable.ContainsKey(this.getVariableKey(x, y)))
                                        skill.Variable.Remove(this.getVariableKey(x, y));
                                    skill.Variable.Add(this.getVariableKey(x, y), (int)this.map.Info.holy[x, y]);
                                    this.map.Info.holy[x, y] = (byte)50;
                                }
                            }
                        }
                        break;
                    case Elements.Dark:
                        for (int x = (int)this.centerX - 1; x <= (int)this.centerX + 1; ++x)
                        {
                            for (int y = (int)this.centerY - 1; y <= (int)this.centerY + 1; ++y)
                            {
                                if (x >= 0 && x <= (int)byte.MaxValue && (y >= 0 && y <= (int)byte.MaxValue))
                                {
                                    if (skill.Variable.ContainsKey(this.getVariableKey(x, y)))
                                        skill.Variable.Remove(this.getVariableKey(x, y));
                                    skill.Variable.Add(this.getVariableKey(x, y), (int)this.map.Info.dark[x, y]);
                                    this.map.Info.dark[x, y] = (byte)50;
                                }
                            }
                        }
                        break;
                }
            }

            /// <summary>
            /// The EndEvent.
            /// </summary>
            /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
            private void EndEvent(SagaDB.Actor.Actor actor, DefaultBuff skill)
            {
                switch (this.MapElement)
                {
                    case Elements.Fire:
                        for (int x = (int)this.centerX - 1; x <= (int)this.centerX + 1; ++x)
                        {
                            for (int y = (int)this.centerY - 1; y <= (int)this.centerY + 1; ++y)
                            {
                                if (x >= 0 && x <= (int)byte.MaxValue && (y >= 0 && y <= (int)byte.MaxValue))
                                    this.map.Info.fire[x, y] = (byte)skill.Variable[this.getVariableKey(x, y)];
                            }
                        }
                        break;
                    case Elements.Water:
                        for (int x = (int)this.centerX - 1; x <= (int)this.centerX + 1; ++x)
                        {
                            for (int y = (int)this.centerY - 1; y <= (int)this.centerY + 1; ++y)
                            {
                                if (x >= 0 && x <= (int)byte.MaxValue && (y >= 0 && y <= (int)byte.MaxValue))
                                    this.map.Info.water[x, y] = (byte)skill.Variable[this.getVariableKey(x, y)];
                            }
                        }
                        break;
                    case Elements.Wind:
                        for (int x = (int)this.centerX - 1; x <= (int)this.centerX + 1; ++x)
                        {
                            for (int y = (int)this.centerY - 1; y <= (int)this.centerY + 1; ++y)
                            {
                                if (x >= 0 && x <= (int)byte.MaxValue && (y >= 0 && y <= (int)byte.MaxValue))
                                    this.map.Info.wind[x, y] = (byte)skill.Variable[this.getVariableKey(x, y)];
                            }
                        }
                        break;
                    case Elements.Earth:
                        for (int x = (int)this.centerX - 1; x <= (int)this.centerX + 1; ++x)
                        {
                            for (int y = (int)this.centerY - 1; y <= (int)this.centerY + 1; ++y)
                            {
                                if (x >= 0 && x <= (int)byte.MaxValue && (y >= 0 && y <= (int)byte.MaxValue))
                                    this.map.Info.earth[x, y] = (byte)skill.Variable[this.getVariableKey(x, y)];
                            }
                        }
                        break;
                    case Elements.Holy:
                        for (int x = (int)this.centerX - 1; x <= (int)this.centerX + 1; ++x)
                        {
                            for (int y = (int)this.centerY - 1; y <= (int)this.centerY + 1; ++y)
                            {
                                if (x >= 0 && x <= (int)byte.MaxValue && (y >= 0 && y <= (int)byte.MaxValue))
                                    this.map.Info.holy[x, y] = (byte)skill.Variable[this.getVariableKey(x, y)];
                            }
                        }
                        break;
                    case Elements.Dark:
                        for (int x = (int)this.centerX - 1; x <= (int)this.centerX + 1; ++x)
                        {
                            for (int y = (int)this.centerY - 1; y <= (int)this.centerY + 1; ++y)
                            {
                                if (x >= 0 && x <= (int)byte.MaxValue && (y >= 0 && y <= (int)byte.MaxValue))
                                    this.map.Info.dark[x, y] = (byte)skill.Variable[this.getVariableKey(x, y)];
                            }
                        }
                        break;
                }
            }

            /// <summary>
            /// The getVariableKey.
            /// </summary>
            /// <param name="x">The x<see cref="int"/>.</param>
            /// <param name="y">The y<see cref="int"/>.</param>
            /// <returns>The <see cref="string"/>.</returns>
            private string getVariableKey(int x, int y)
            {
                return this.prefix + string.Format("{0:000}", (object)x) + string.Format("{0:000}", (object)y);
            }
        }
    }
}
