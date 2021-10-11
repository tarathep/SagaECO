namespace SagaMap.Tasks.Skill
{
    using global::System;
    using global::System.Collections.Generic;
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.ActorEventHandlers;
    using SagaMap.Packets.Client;

    /// <summary>
    /// Defines the <see cref="AutoCast" />.
    /// </summary>
    public class AutoCast : MultiRunTask
    {
        /// <summary>
        /// Defines the caster.
        /// </summary>
        private SagaDB.Actor.Actor caster;

        /// <summary>
        /// Defines the arg.
        /// </summary>
        private SkillArg arg;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCast"/> class.
        /// </summary>
        /// <param name="pc">The pc<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="arg">The arg<see cref="SkillArg"/>.</param>
        public AutoCast(SagaDB.Actor.Actor pc, SkillArg arg)
        {
            this.period = 600000;
            this.dueTime = 0;
            this.caster = pc;
            this.arg = arg;
        }

        /// <summary>
        /// The CallBack.
        /// </summary>
        /// <param name="o">The o<see cref="object"/>.</param>
        public override void CallBack(object o)
        {
            try
            {
                this.Deactivate();
                AutoCastInfo autoCastInfo = (AutoCastInfo)null;
                using (List<AutoCastInfo>.Enumerator enumerator = this.arg.autoCast.GetEnumerator())
                {
                    if (enumerator.MoveNext())
                        autoCastInfo = enumerator.Current;
                }
                if (autoCastInfo != null)
                {
                    this.arg.x = autoCastInfo.x;
                    this.arg.y = autoCastInfo.y;
                    this.arg.autoCast.Remove(autoCastInfo);
                    switch (this.caster.type)
                    {
                        case ActorType.PC:
                            PCEventHandler e1 = (PCEventHandler)this.caster.e;
                            e1.Client.SkillDelay = DateTime.Now;
                            e1.Client.OnSkillCast(new CSMG_SKILL_CAST()
                            {
                                ActorID = this.arg.dActor,
                                SkillID = (ushort)autoCastInfo.skillID,
                                SkillLv = autoCastInfo.level,
                                X = this.arg.x,
                                Y = this.arg.y,
                                Random = (short)Global.Random.Next()
                            }, false, true);
                            break;
                        case ActorType.MOB:
                            MobEventHandler e2 = (MobEventHandler)this.caster.e;
                            e2.AI.CastSkill(autoCastInfo.skillID, autoCastInfo.level, this.arg.dActor, Global.PosX8to16(this.arg.x, e2.AI.map.Width), Global.PosY8to16(this.arg.y, e2.AI.map.Height));
                            break;
                    }
                    this.dueTime = autoCastInfo.delay;
                }
                else
                {
                    this.caster.Tasks.Remove(nameof(AutoCast));
                    this.caster.Buff.CannotMove = false;
                    if (this.caster.type == ActorType.PC)
                        ((PCEventHandler)this.caster.e).Client.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, this.caster, true);
                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
        }
    }
}
