namespace SagaMap.Packets.Server
{
    using SagaDB.Actor;
    using SagaDB.Skill;
    using SagaLib;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="SSMG_SKILL_LIST" />.
    /// </summary>
    public class SSMG_SKILL_LIST : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_SKILL_LIST"/> class.
        /// </summary>
        public SSMG_SKILL_LIST()
        {
            this.data = new byte[7];
            this.offset = (ushort)2;
            this.ID = (ushort)550;
        }

        /// <summary>
        /// The Skills.
        /// </summary>
        /// <param name="list">List.</param>
        /// <param name="job">0 for basic, 1 for expert, 2 for technical.</param>
        /// <param name="job2">The job2<see cref="PC_JOB"/>.</param>
        /// <param name="ifDominion">The ifDominion<see cref="bool"/>.</param>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        public void Skills(List<SagaDB.Skill.Skill> list, byte job, PC_JOB job2, bool ifDominion, ActorPC pc)
        {
            this.data = new byte[7 + list.Count * 2 + list.Count * 3];
            this.ID = (ushort)550;
            for (int index = 0; index < list.Count; ++index)
            {
                SagaDB.Skill.Skill skill = list[index];
                byte num = 1;
                if (Singleton<SkillFactory>.Instance.SkillList(job2).ContainsKey(skill.ID))
                    num = Singleton<SkillFactory>.Instance.SkillList(job2)[skill.ID];
                this.PutByte((byte)list.Count, (ushort)2);
                this.PutUShort((ushort)skill.ID, (ushort)(3 + index * 2));
                this.PutByte((byte)list.Count, (ushort)(3 + list.Count * 2));
                if ((int)pc.DominionJobLevel < (int)num && ifDominion)
                    this.PutByte((byte)0, (ushort)(4 + list.Count * 2 + index));
                else
                    this.PutByte(skill.Level, (ushort)(4 + list.Count * 2 + index));
                this.PutByte((byte)list.Count, (ushort)(4 + list.Count * 3));
                this.PutByte((byte)list.Count, (ushort)(5 + list.Count * 4));
                if (Singleton<SkillFactory>.Instance.SkillList(job2).ContainsKey(skill.ID))
                    this.PutByte(Singleton<SkillFactory>.Instance.SkillList(job2)[skill.ID], (ushort)(6 + list.Count * 4 + index));
                else
                    this.PutByte((byte)1, (ushort)(6 + list.Count * 4 + index));
            }
            this.PutByte(job, (ushort)(6 + list.Count * 5));
        }
    }
}
