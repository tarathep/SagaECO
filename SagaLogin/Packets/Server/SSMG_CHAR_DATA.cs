namespace SagaLogin.Packets.Server
{
    using SagaDB.Actor;
    using SagaLib;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Defines the <see cref="SSMG_CHAR_DATA" />.
    /// </summary>
    public class SSMG_CHAR_DATA : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_CHAR_DATA"/> class.
        /// </summary>
        public SSMG_CHAR_DATA()
        {
            if (Singleton<Configuration>.Instance.Version >= SagaLib.Version.Saga10)
                this.data = new byte[113];
            else
                this.data = new byte[86];
            this.offset = (ushort)2;
            this.ID = (ushort)40;
        }

        /// <summary>
        /// The SetName.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <param name="index">The index<see cref="int"/>.</param>
        private void SetName(string name, int index)
        {
            int num1 = 0;
            int num2 = 3;
            if (Singleton<Configuration>.Instance.Version >= SagaLib.Version.Saga10)
                this.PutByte((byte)4, (ushort)2);
            else
                this.PutByte((byte)3, (ushort)2);
            for (; num1 < index; ++num1)
            {
                byte num3 = this.GetByte((ushort)num2);
                num2 = num2 + (int)num3 + 1;
            }
            byte[] bytes = Encoding.UTF8.GetBytes(name);
            this.PutByte((byte)bytes.Length, (ushort)num2);
            int num4 = num2 + 1;
            byte[] numArray = new byte[this.data.Length + bytes.Length];
            Array.Copy((Array)this.data, 0, (Array)numArray, 0, num4);
            Array.Copy((Array)this.data, num4, (Array)numArray, num4 + bytes.Length, this.data.Length - num4);
            this.data = numArray;
            this.PutBytes(bytes, (ushort)num4);
            this.SetUnkown();
        }

        /// <summary>
        /// The GetDataOffset.
        /// </summary>
        /// <returns>The <see cref="ushort"/>.</returns>
        private ushort GetDataOffset()
        {
            ushort index1 = 3;
            int num1 = Singleton<Configuration>.Instance.Version < SagaLib.Version.Saga10 ? 3 : 4;
            for (int index2 = 0; index2 < num1; ++index2)
            {
                byte num2 = this.GetByte(index1);
                index1 = (ushort)((int)index1 + (int)num2 + 1);
            }
            return index1;
        }

        /// <summary>
        /// The SetRace.
        /// </summary>
        /// <param name="race">The race<see cref="byte"/>.</param>
        /// <param name="index">The index<see cref="ushort"/>.</param>
        private void SetRace(byte race, ushort index)
        {
            ushort dataOffset = this.GetDataOffset();
            if (Singleton<Configuration>.Instance.Version >= SagaLib.Version.Saga10)
                this.PutByte((byte)4, dataOffset);
            else
                this.PutByte((byte)3, dataOffset);
            this.PutByte(race, (ushort)((int)dataOffset + (int)index + 1));
        }

        /// <summary>
        /// The SetForm.
        /// </summary>
        /// <param name="form">The form<see cref="byte"/>.</param>
        /// <param name="index">The index<see cref="ushort"/>.</param>
        private void SetForm(byte form, ushort index)
        {
            if (Singleton<Configuration>.Instance.Version < SagaLib.Version.Saga10)
                return;
            ushort index1 = (ushort)((uint)this.GetDataOffset() + 5U);
            this.PutByte((byte)4, index1);
            this.PutByte(form, (ushort)((int)index1 + (int)index + 1));
        }

        /// <summary>
        /// The SetGender.
        /// </summary>
        /// <param name="gender">The gender<see cref="byte"/>.</param>
        /// <param name="index">The index<see cref="ushort"/>.</param>
        private void SetGender(byte gender, ushort index)
        {
            if (Singleton<Configuration>.Instance.Version >= SagaLib.Version.Saga10)
            {
                ushort index1 = (ushort)((uint)this.GetDataOffset() + 10U);
                this.PutByte((byte)4, index1);
                this.PutByte(gender, (ushort)((int)index1 + (int)index + 1));
            }
            else
            {
                ushort index1 = (ushort)((uint)this.GetDataOffset() + 4U);
                this.PutByte((byte)3, index1);
                this.PutByte(gender, (ushort)((int)index1 + (int)index + 1));
            }
        }

        /// <summary>
        /// The SetHairStyle.
        /// </summary>
        /// <param name="hair">The hair<see cref="byte"/>.</param>
        /// <param name="index">The index<see cref="ushort"/>.</param>
        private void SetHairStyle(byte hair, ushort index)
        {
            if (Singleton<Configuration>.Instance.Version >= SagaLib.Version.Saga10)
            {
                ushort index1 = (ushort)((uint)this.GetDataOffset() + 15U);
                this.PutByte((byte)4, index1);
                this.PutByte(hair, (ushort)((int)index1 + (int)index + 1));
            }
            else
            {
                ushort index1 = (ushort)((uint)this.GetDataOffset() + 8U);
                this.PutByte((byte)3, index1);
                this.PutByte(hair, (ushort)((int)index1 + (int)index + 1));
            }
        }

        /// <summary>
        /// The SetHairColor.
        /// </summary>
        /// <param name="color">The color<see cref="byte"/>.</param>
        /// <param name="index">The index<see cref="ushort"/>.</param>
        private void SetHairColor(byte color, ushort index)
        {
            if (Singleton<Configuration>.Instance.Version >= SagaLib.Version.Saga10)
            {
                ushort index1 = (ushort)((uint)this.GetDataOffset() + 20U);
                this.PutByte((byte)4, index1);
                this.PutByte(color, (ushort)((int)index1 + (int)index + 1));
            }
            else
            {
                ushort index1 = (ushort)((uint)this.GetDataOffset() + 12U);
                this.PutByte((byte)3, index1);
                this.PutByte(color, (ushort)((int)index1 + (int)index + 1));
            }
        }

        /// <summary>
        /// The SetWig.
        /// </summary>
        /// <param name="wig">The wig<see cref="byte"/>.</param>
        /// <param name="index">The index<see cref="ushort"/>.</param>
        private void SetWig(byte wig, ushort index)
        {
            if (Singleton<Configuration>.Instance.Version >= SagaLib.Version.Saga10)
            {
                ushort index1 = (ushort)((uint)this.GetDataOffset() + 25U);
                this.PutByte((byte)4, index1);
                this.PutByte(wig, (ushort)((int)index1 + (int)index + 1));
            }
            else
            {
                ushort index1 = (ushort)((uint)this.GetDataOffset() + 16U);
                this.PutByte((byte)3, index1);
                this.PutByte(wig, (ushort)((int)index1 + (int)index + 1));
            }
        }

        /// <summary>
        /// The SetIfExist.
        /// </summary>
        /// <param name="exist">The exist<see cref="bool"/>.</param>
        /// <param name="index">The index<see cref="ushort"/>.</param>
        private void SetIfExist(bool exist, ushort index)
        {
            if (Singleton<Configuration>.Instance.Version >= SagaLib.Version.Saga10)
            {
                ushort index1 = (ushort)((uint)this.GetDataOffset() + 30U);
                this.PutByte((byte)4, index1);
                if (exist)
                    this.PutByte(byte.MaxValue, (ushort)((int)index1 + (int)index + 1));
                else
                    this.PutByte((byte)0, (ushort)((int)index1 + (int)index + 1));
            }
            else
            {
                ushort index1 = (ushort)((uint)this.GetDataOffset() + 20U);
                this.PutByte((byte)3, index1);
                if (exist)
                    this.PutByte(byte.MaxValue, (ushort)((int)index1 + (int)index + 1));
                else
                    this.PutByte((byte)0, (ushort)((int)index1 + (int)index + 1));
            }
        }

        /// <summary>
        /// The SetFace.
        /// </summary>
        /// <param name="face">The face<see cref="byte"/>.</param>
        /// <param name="index">The index<see cref="ushort"/>.</param>
        private void SetFace(byte face, ushort index)
        {
            if (Singleton<Configuration>.Instance.Version >= SagaLib.Version.Saga10)
            {
                ushort index1 = (ushort)((uint)this.GetDataOffset() + 35U);
                this.PutByte((byte)4, index1);
                this.PutByte(face, (ushort)((int)index1 + (int)index + 1));
            }
            else
            {
                ushort index1 = (ushort)((uint)this.GetDataOffset() + 24U);
                this.PutByte((byte)3, index1);
                this.PutByte(face, (ushort)((int)index1 + (int)index + 1));
            }
        }

        /// <summary>
        /// The SetUnkown.
        /// </summary>
        private void SetUnkown()
        {
            if (Singleton<Configuration>.Instance.Version >= SagaLib.Version.Saga10)
            {
                ushort index = (ushort)((uint)this.GetDataOffset() + 40U);
                this.PutUInt(67108864U, index);
                this.PutUInt(67108864U, (ushort)((uint)index + 5U));
                this.PutUInt(67108864U, (ushort)((uint)index + 10U));
            }
            else
            {
                ushort index = (ushort)((uint)this.GetDataOffset() + 28U);
                this.PutUInt(50331648U, index);
                this.PutUInt(50331648U, (ushort)((uint)index + 4U));
                this.PutUInt(50331648U, (ushort)((uint)index + 8U));
            }
        }

        /// <summary>
        /// The SetJob.
        /// </summary>
        /// <param name="job">The job<see cref="byte"/>.</param>
        /// <param name="index">The index<see cref="ushort"/>.</param>
        private void SetJob(byte job, ushort index)
        {
            if (Singleton<Configuration>.Instance.Version >= SagaLib.Version.Saga10)
            {
                ushort index1 = (ushort)((uint)this.GetDataOffset() + 55U);
                this.PutByte((byte)4, index1);
                this.PutByte(job, (ushort)((int)index1 + (int)index + 1));
            }
            else
            {
                ushort index1 = (ushort)((uint)this.GetDataOffset() + 40U);
                this.PutByte((byte)3, index1);
                this.PutByte(job, (ushort)((int)index1 + (int)index + 1));
            }
        }

        /// <summary>
        /// The SetMap.
        /// </summary>
        /// <param name="map">The map<see cref="uint"/>.</param>
        /// <param name="index">The index<see cref="ushort"/>.</param>
        private void SetMap(uint map, ushort index)
        {
            if (Singleton<Configuration>.Instance.Version >= SagaLib.Version.Saga10)
            {
                ushort index1 = (ushort)((uint)this.GetDataOffset() + 60U);
                this.PutByte((byte)4, index1);
                this.PutUInt(map, (ushort)((int)index1 + (int)index * 4 + 1));
            }
            else
            {
                ushort index1 = (ushort)((uint)this.GetDataOffset() + 44U);
                this.PutByte((byte)3, index1);
                this.PutUInt(map, (ushort)((int)index1 + (int)index * 4 + 1));
            }
        }

        /// <summary>
        /// The SetLv.
        /// </summary>
        /// <param name="lv">The lv<see cref="byte"/>.</param>
        /// <param name="index">The index<see cref="ushort"/>.</param>
        private void SetLv(byte lv, ushort index)
        {
            if (Singleton<Configuration>.Instance.Version >= SagaLib.Version.Saga10)
            {
                ushort index1 = (ushort)((uint)this.GetDataOffset() + 77U);
                this.PutByte((byte)4, index1);
                this.PutByte(lv, (ushort)((int)index1 + (int)index + 1));
            }
            else
            {
                ushort index1 = (ushort)((uint)this.GetDataOffset() + 57U);
                this.PutByte((byte)3, index1);
                this.PutByte(lv, (ushort)((int)index1 + (int)index + 1));
            }
        }

        /// <summary>
        /// The SetJob1.
        /// </summary>
        /// <param name="job1">The job1<see cref="byte"/>.</param>
        /// <param name="index">The index<see cref="ushort"/>.</param>
        private void SetJob1(byte job1, ushort index)
        {
            if (Singleton<Configuration>.Instance.Version >= SagaLib.Version.Saga10)
            {
                ushort index1 = (ushort)((uint)this.GetDataOffset() + 82U);
                this.PutByte((byte)4, index1);
                this.PutByte(job1, (ushort)((int)index1 + (int)index + 1));
            }
            else
            {
                ushort index1 = (ushort)((uint)this.GetDataOffset() + 61U);
                this.PutByte((byte)3, index1);
                this.PutByte(job1, (ushort)((int)index1 + (int)index + 1));
            }
        }

        /// <summary>
        /// The SetQuestRemaining.
        /// </summary>
        /// <param name="quest">The quest<see cref="ushort"/>.</param>
        /// <param name="index">The index<see cref="ushort"/>.</param>
        private void SetQuestRemaining(ushort quest, ushort index)
        {
            if (Singleton<Configuration>.Instance.Version == SagaLib.Version.Saga10)
            {
                ushort index1 = (ushort)((uint)this.GetDataOffset() + 87U);
                this.PutByte((byte)4, index1);
                this.PutUShort(quest, (ushort)((int)index1 + (int)index * 2 + 1));
            }
            else
            {
                ushort index1 = (ushort)((uint)this.GetDataOffset() + 65U);
                this.PutByte((byte)3, index1);
                this.PutUShort(quest, (ushort)((int)index1 + (int)index * 2 + 1));
            }
        }

        /// <summary>
        /// The SetJob2X.
        /// </summary>
        /// <param name="job2x">The job2x<see cref="byte"/>.</param>
        /// <param name="index">The index<see cref="ushort"/>.</param>
        private void SetJob2X(byte job2x, ushort index)
        {
            if (Singleton<Configuration>.Instance.Version >= SagaLib.Version.Saga10)
            {
                ushort index1 = (ushort)((uint)this.GetDataOffset() + 96U);
                this.PutByte((byte)4, index1);
                this.PutByte(job2x, (ushort)((int)index1 + (int)index + 1));
            }
            else
            {
                ushort index1 = (ushort)((uint)this.GetDataOffset() + 72U);
                this.PutByte((byte)3, index1);
                this.PutByte(job2x, (ushort)((int)index1 + (int)index + 1));
            }
        }

        /// <summary>
        /// The SetJob2T.
        /// </summary>
        /// <param name="job2t">The job2t<see cref="byte"/>.</param>
        /// <param name="index">The index<see cref="ushort"/>.</param>
        private void SetJob2T(byte job2t, ushort index)
        {
            if (Singleton<Configuration>.Instance.Version >= SagaLib.Version.Saga10)
            {
                ushort index1 = (ushort)((uint)this.GetDataOffset() + 101U);
                this.PutByte((byte)4, index1);
                this.PutByte(job2t, (ushort)((int)index1 + (int)index + 1));
            }
            else
            {
                ushort index1 = (ushort)((uint)this.GetDataOffset() + 76U);
                this.PutByte((byte)3, index1);
                this.PutByte(job2t, (ushort)((int)index1 + (int)index + 1));
            }
        }

        /// <summary>
        /// Sets the Chars.
        /// </summary>
        public List<ActorPC> Chars
        {
            set
            {
                if (value.Count == 0)
                {
                    this.SetName("", 0);
                    this.SetRace((byte)0, (ushort)0);
                    if (Singleton<Configuration>.Instance.Version >= SagaLib.Version.Saga10)
                        this.SetForm((byte)0, (ushort)0);
                    this.SetGender((byte)0, (ushort)0);
                    this.SetHairStyle((byte)0, (ushort)0);
                    this.SetHairColor((byte)0, (ushort)0);
                    this.SetWig((byte)0, (ushort)0);
                    this.SetIfExist(false, (ushort)0);
                    this.SetFace((byte)0, (ushort)0);
                    this.SetJob((byte)0, (ushort)0);
                    this.SetMap(0U, (ushort)0);
                    this.SetLv((byte)0, (ushort)0);
                    this.SetJob1((byte)0, (ushort)0);
                    this.SetQuestRemaining((ushort)0, (ushort)0);
                    this.SetJob2X((byte)0, (ushort)0);
                    this.SetJob2T((byte)0, (ushort)0);
                }
                int num = Singleton<Configuration>.Instance.Version < SagaLib.Version.Saga10 ? 3 : 4;
                for (int i = 0; i < num; ++i)
                {
                    IEnumerable<ActorPC> source = value.Where<ActorPC>((Func<ActorPC, bool>)(p => (int)p.Slot == i));
                    if (source.Count<ActorPC>() != 0)
                    {
                        ActorPC actorPc = source.First<ActorPC>();
                        this.SetName(actorPc.Name, (int)(ushort)i);
                        this.SetRace((byte)actorPc.Race, (ushort)i);
                        if (Singleton<Configuration>.Instance.Version >= SagaLib.Version.Saga10)
                            this.SetForm((byte)actorPc.Form, (ushort)i);
                        this.SetGender((byte)actorPc.Gender, (ushort)i);
                        this.SetHairStyle(actorPc.HairStyle, (ushort)i);
                        this.SetHairColor(actorPc.HairColor, (ushort)i);
                        this.SetWig(actorPc.Wig, (ushort)i);
                        this.SetIfExist(true, (ushort)i);
                        this.SetFace(actorPc.Face, (ushort)i);
                        this.SetJob((byte)actorPc.Job, (ushort)i);
                        this.SetMap(actorPc.MapID, (ushort)i);
                        this.SetLv(actorPc.Level, (ushort)i);
                        this.SetJob1(actorPc.JobLevel1, (ushort)i);
                        this.SetQuestRemaining(actorPc.QuestRemaining, (ushort)i);
                        this.SetJob2X(actorPc.JobLevel2X, (ushort)i);
                        this.SetJob2T(actorPc.JobLevel2T, (ushort)i);
                    }
                }
            }
        }
    }
}
