namespace SagaDB.Actor
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="Buff" />.
    /// </summary>
    public class Buff
    {
        /// <summary>
        /// Defines the buffs.
        /// </summary>
        private BitMask[] buffs = new BitMask[7]
        {
      new BitMask(),
      new BitMask(),
      new BitMask(),
      new BitMask(),
      new BitMask(),
      new BitMask(),
      new BitMask()
        };

        /// <summary>
        /// Gets or sets a value indicating whether 武器の無属性上昇.
        /// </summary>
        public bool 武器の無属性上昇
        {
            get
            {
                return this.buffs[2].Test(1);
            }
            set
            {
                this.buffs[2].SetValue(1, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 武器の火属性上昇.
        /// </summary>
        public bool 武器の火属性上昇
        {
            get
            {
                return this.buffs[2].Test(2);
            }
            set
            {
                this.buffs[2].SetValue(2, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 武器の水属性上昇.
        /// </summary>
        public bool 武器の水属性上昇
        {
            get
            {
                return this.buffs[2].Test(4);
            }
            set
            {
                this.buffs[2].SetValue(4, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 武器の風属性上昇.
        /// </summary>
        public bool 武器の風属性上昇
        {
            get
            {
                return this.buffs[2].Test(8);
            }
            set
            {
                this.buffs[2].SetValue(8, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 武器の土属性上昇.
        /// </summary>
        public bool 武器の土属性上昇
        {
            get
            {
                return this.buffs[2].Test(16);
            }
            set
            {
                this.buffs[2].SetValue(16, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 武器の光属性上昇.
        /// </summary>
        public bool 武器の光属性上昇
        {
            get
            {
                return this.buffs[2].Test(32);
            }
            set
            {
                this.buffs[2].SetValue(32, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 武器の闇属性上昇.
        /// </summary>
        public bool 武器の闇属性上昇
        {
            get
            {
                return this.buffs[2].Test(64);
            }
            set
            {
                this.buffs[2].SetValue(64, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 武器の無属性減少.
        /// </summary>
        public bool 武器の無属性減少
        {
            get
            {
                return this.buffs[2].Test(128);
            }
            set
            {
                this.buffs[2].SetValue(128, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 武器の火属性減少.
        /// </summary>
        public bool 武器の火属性減少
        {
            get
            {
                return this.buffs[2].Test(256);
            }
            set
            {
                this.buffs[2].SetValue(256, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 武器の水属性減少.
        /// </summary>
        public bool 武器の水属性減少
        {
            get
            {
                return this.buffs[2].Test(512);
            }
            set
            {
                this.buffs[2].SetValue(512, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 武器の風属性減少.
        /// </summary>
        public bool 武器の風属性減少
        {
            get
            {
                return this.buffs[2].Test(1024);
            }
            set
            {
                this.buffs[2].SetValue(1024, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 武器の土属性減少.
        /// </summary>
        public bool 武器の土属性減少
        {
            get
            {
                return this.buffs[2].Test(2048);
            }
            set
            {
                this.buffs[2].SetValue(2048, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 武器の光属性減少.
        /// </summary>
        public bool 武器の光属性減少
        {
            get
            {
                return this.buffs[2].Test(4096);
            }
            set
            {
                this.buffs[2].SetValue(4096, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 武器の闇属性減少.
        /// </summary>
        public bool 武器の闇属性減少
        {
            get
            {
                return this.buffs[2].Test(8192);
            }
            set
            {
                this.buffs[2].SetValue(8192, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 体の無属性上昇.
        /// </summary>
        public bool 体の無属性上昇
        {
            get
            {
                return this.buffs[2].Test(16384);
            }
            set
            {
                this.buffs[2].SetValue(16384, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 体の火属性上昇.
        /// </summary>
        public bool 体の火属性上昇
        {
            get
            {
                return this.buffs[2].Test(32768);
            }
            set
            {
                this.buffs[2].SetValue(32768, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 体の水属性上昇.
        /// </summary>
        public bool 体の水属性上昇
        {
            get
            {
                return this.buffs[2].Test(65536);
            }
            set
            {
                this.buffs[2].SetValue(65536, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 体の風属性上昇.
        /// </summary>
        public bool 体の風属性上昇
        {
            get
            {
                return this.buffs[2].Test(131072);
            }
            set
            {
                this.buffs[2].SetValue(131072, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 体の土属性上昇.
        /// </summary>
        public bool 体の土属性上昇
        {
            get
            {
                return this.buffs[2].Test(262144);
            }
            set
            {
                this.buffs[2].SetValue(262144, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 体の光属性上昇.
        /// </summary>
        public bool 体の光属性上昇
        {
            get
            {
                return this.buffs[2].Test(524288);
            }
            set
            {
                this.buffs[2].SetValue(524288, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 体の闇属性上昇.
        /// </summary>
        public bool 体の闇属性上昇
        {
            get
            {
                return this.buffs[2].Test(1048576);
            }
            set
            {
                this.buffs[2].SetValue(1048576, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 体の無属性減少.
        /// </summary>
        public bool 体の無属性減少
        {
            get
            {
                return this.buffs[2].Test(2097152);
            }
            set
            {
                this.buffs[2].SetValue(2097152, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 体の火属性減少.
        /// </summary>
        public bool 体の火属性減少
        {
            get
            {
                return this.buffs[2].Test(4194304);
            }
            set
            {
                this.buffs[2].SetValue(4194304, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 体の水属性減少.
        /// </summary>
        public bool 体の水属性減少
        {
            get
            {
                return this.buffs[2].Test(1048576);
            }
            set
            {
                this.buffs[2].SetValue(1048576, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 体の風属性減少.
        /// </summary>
        public bool 体の風属性減少
        {
            get
            {
                return this.buffs[2].Test(16777216);
            }
            set
            {
                this.buffs[2].SetValue(16777216, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 体の土属性減少.
        /// </summary>
        public bool 体の土属性減少
        {
            get
            {
                return this.buffs[2].Test(33554432);
            }
            set
            {
                this.buffs[2].SetValue(33554432, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 体の光属性減少.
        /// </summary>
        public bool 体の光属性減少
        {
            get
            {
                return this.buffs[2].Test(67108864);
            }
            set
            {
                this.buffs[2].SetValue(67108864, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 体の闇属性減少.
        /// </summary>
        public bool 体の闇属性減少
        {
            get
            {
                return this.buffs[2].Test(134217728);
            }
            set
            {
                this.buffs[2].SetValue(134217728, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 狂戦士.
        /// </summary>
        public bool 狂戦士
        {
            get
            {
                return this.buffs[1].Test(1);
            }
            set
            {
                this.buffs[1].SetValue(1, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Curse.
        /// </summary>
        public bool Curse
        {
            get
            {
                return this.buffs[1].Test(2);
            }
            set
            {
                this.buffs[1].SetValue(2, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 透視.
        /// </summary>
        public bool 透視
        {
            get
            {
                return this.buffs[1].Test(4);
            }
            set
            {
                this.buffs[1].SetValue(4, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 浮遊.
        /// </summary>
        public bool 浮遊
        {
            get
            {
                return this.buffs[1].Test(8);
            }
            set
            {
                this.buffs[1].SetValue(8, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 水中呼吸.
        /// </summary>
        public bool 水中呼吸
        {
            get
            {
                return this.buffs[1].Test(16);
            }
            set
            {
                this.buffs[1].SetValue(16, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Transparent.
        /// </summary>
        public bool Transparent
        {
            get
            {
                return this.buffs[1].Test(32);
            }
            set
            {
                this.buffs[1].SetValue(32, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Undead.
        /// </summary>
        public bool Undead
        {
            get
            {
                return this.buffs[1].Test(64);
            }
            set
            {
                this.buffs[1].SetValue(64, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Mushroom.
        /// </summary>
        public bool Mushroom
        {
            get
            {
                return this.buffs[1].Test(128);
            }
            set
            {
                this.buffs[1].SetValue(128, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 硬直.
        /// </summary>
        public bool 硬直
        {
            get
            {
                return this.buffs[1].Test(256);
            }
            set
            {
                this.buffs[1].SetValue(256, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 呪縛.
        /// </summary>
        public bool 呪縛
        {
            get
            {
                return this.buffs[1].Test(512);
            }
            set
            {
                this.buffs[1].SetValue(512, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 封印.
        /// </summary>
        public bool 封印
        {
            get
            {
                return this.buffs[1].Test(1024);
            }
            set
            {
                this.buffs[1].SetValue(1024, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 封魔.
        /// </summary>
        public bool 封魔
        {
            get
            {
                return this.buffs[1].Test(2048);
            }
            set
            {
                this.buffs[1].SetValue(2048, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 憑依準備.
        /// </summary>
        public bool 憑依準備
        {
            get
            {
                return this.buffs[1].Test(4096);
            }
            set
            {
                this.buffs[1].SetValue(4096, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 熱波防御.
        /// </summary>
        public bool 熱波防御
        {
            get
            {
                return this.buffs[1].Test(8192);
            }
            set
            {
                this.buffs[1].SetValue(8192, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 寒波防御.
        /// </summary>
        public bool 寒波防御
        {
            get
            {
                return this.buffs[1].Test(16384);
            }
            set
            {
                this.buffs[1].SetValue(16384, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 真空防御.
        /// </summary>
        public bool 真空防御
        {
            get
            {
                return this.buffs[1].Test(32768);
            }
            set
            {
                this.buffs[1].SetValue(32768, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 猛毒.
        /// </summary>
        public bool 猛毒
        {
            get
            {
                return this.buffs[1].Test(65536);
            }
            set
            {
                this.buffs[1].SetValue(65536, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether HolyFeather.
        /// </summary>
        public bool HolyFeather
        {
            get
            {
                return this.buffs[1].Test(131072);
            }
            set
            {
                this.buffs[1].SetValue(131072, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 亀の構え.
        /// </summary>
        public bool 亀の構え
        {
            get
            {
                return this.buffs[1].Test(262144);
            }
            set
            {
                this.buffs[1].SetValue(262144, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 必中陣.
        /// </summary>
        public bool 必中陣
        {
            get
            {
                return this.buffs[1].Test(524288);
            }
            set
            {
                this.buffs[1].SetValue(524288, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether ShortSwordDelayCancel.
        /// </summary>
        public bool ShortSwordDelayCancel
        {
            get
            {
                return this.buffs[1].Test(1048576);
            }
            set
            {
                this.buffs[1].SetValue(1048576, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether DelayCancel.
        /// </summary>
        public bool DelayCancel
        {
            get
            {
                return this.buffs[1].Test(2097152);
            }
            set
            {
                this.buffs[1].SetValue(2097152, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether AxeDelayCancel.
        /// </summary>
        public bool AxeDelayCancel
        {
            get
            {
                return this.buffs[1].Test(4194304);
            }
            set
            {
                this.buffs[1].SetValue(4194304, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether SpearDelayCancel.
        /// </summary>
        public bool SpearDelayCancel
        {
            get
            {
                return this.buffs[1].Test(8388608);
            }
            set
            {
                this.buffs[1].SetValue(8388608, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether BowDelayCancel.
        /// </summary>
        public bool BowDelayCancel
        {
            get
            {
                return this.buffs[1].Test(16777216);
            }
            set
            {
                this.buffs[1].SetValue(16777216, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether DefenseSlash.
        /// </summary>
        public bool DefenseSlash
        {
            get
            {
                return this.buffs[1].Test(33554432);
            }
            set
            {
                this.buffs[1].SetValue(33554432, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether DefenseStub.
        /// </summary>
        public bool DefenseStub
        {
            get
            {
                return this.buffs[1].Test(67108864);
            }
            set
            {
                this.buffs[1].SetValue(67108864, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether DefenseBlow.
        /// </summary>
        public bool DefenseBlow
        {
            get
            {
                return this.buffs[1].Test(134217728);
            }
            set
            {
                this.buffs[1].SetValue(134217728, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Revive.
        /// </summary>
        public bool Revive
        {
            get
            {
                return this.buffs[1].Test(268435456);
            }
            set
            {
                this.buffs[1].SetValue(268435456, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether PetUp.
        /// </summary>
        public bool PetUp
        {
            get
            {
                return this.buffs[1].Test(536870912);
            }
            set
            {
                this.buffs[1].SetValue(536870912, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 点火.
        /// </summary>
        public bool 点火
        {
            get
            {
                return this.buffs[1].Test(1073741824);
            }
            set
            {
                this.buffs[1].SetValue(1073741824, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Poison.
        /// </summary>
        public bool Poison
        {
            get
            {
                return this.buffs[0].Test(1);
            }
            set
            {
                this.buffs[0].SetValue(1, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Stone.
        /// </summary>
        public bool Stone
        {
            get
            {
                return this.buffs[0].Test(2);
            }
            set
            {
                this.buffs[0].SetValue(2, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Paralysis.
        /// </summary>
        public bool Paralysis
        {
            get
            {
                return this.buffs[0].Test(4);
            }
            set
            {
                this.buffs[0].SetValue(4, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Sleep.
        /// </summary>
        public bool Sleep
        {
            get
            {
                return this.buffs[0].Test(8);
            }
            set
            {
                this.buffs[0].SetValue(8, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Silence.
        /// </summary>
        public bool Silence
        {
            get
            {
                return this.buffs[0].Test(16);
            }
            set
            {
                this.buffs[0].SetValue(16, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether SpeedDown.
        /// </summary>
        public bool SpeedDown
        {
            get
            {
                return this.buffs[0].Test(32);
            }
            set
            {
                this.buffs[0].SetValue(32, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Confused.
        /// </summary>
        public bool Confused
        {
            get
            {
                return this.buffs[0].Test(64);
            }
            set
            {
                this.buffs[0].SetValue(64, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Frosen.
        /// </summary>
        public bool Frosen
        {
            get
            {
                return this.buffs[0].Test(128);
            }
            set
            {
                this.buffs[0].SetValue(128, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Stun.
        /// </summary>
        public bool Stun
        {
            get
            {
                return this.buffs[0].Test(256);
            }
            set
            {
                this.buffs[0].SetValue(256, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Dead.
        /// </summary>
        public bool Dead
        {
            get
            {
                return this.buffs[0].Test(512);
            }
            set
            {
                this.buffs[0].SetValue(512, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether CannotMove.
        /// </summary>
        public bool CannotMove
        {
            get
            {
                return this.buffs[0].Test(1024);
            }
            set
            {
                this.buffs[0].SetValue(1024, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether PoisonResist.
        /// </summary>
        public bool PoisonResist
        {
            get
            {
                return this.buffs[0].Test(2048);
            }
            set
            {
                this.buffs[0].SetValue(2048, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether StoneResist.
        /// </summary>
        public bool StoneResist
        {
            get
            {
                return this.buffs[0].Test(4096);
            }
            set
            {
                this.buffs[0].SetValue(4096, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether ParalysisResist.
        /// </summary>
        public bool ParalysisResist
        {
            get
            {
                return this.buffs[0].Test(8192);
            }
            set
            {
                this.buffs[0].SetValue(8192, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether SleepResist.
        /// </summary>
        public bool SleepResist
        {
            get
            {
                return this.buffs[0].Test(16384);
            }
            set
            {
                this.buffs[0].SetValue(16384, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether SilenceResist.
        /// </summary>
        public bool SilenceResist
        {
            get
            {
                return this.buffs[0].Test(32768);
            }
            set
            {
                this.buffs[0].SetValue(32768, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether SpeedDownResist.
        /// </summary>
        public bool SpeedDownResist
        {
            get
            {
                return this.buffs[0].Test(65536);
            }
            set
            {
                this.buffs[0].SetValue(65536, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether ConfuseResist.
        /// </summary>
        public bool ConfuseResist
        {
            get
            {
                return this.buffs[0].Test(131072);
            }
            set
            {
                this.buffs[0].SetValue(131072, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether FrosenResist.
        /// </summary>
        public bool FrosenResist
        {
            get
            {
                return this.buffs[0].Test(262144);
            }
            set
            {
                this.buffs[0].SetValue(262144, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether FaintResist.
        /// </summary>
        public bool FaintResist
        {
            get
            {
                return this.buffs[0].Test(524288);
            }
            set
            {
                this.buffs[0].SetValue(524288, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Sit.
        /// </summary>
        public bool Sit
        {
            get
            {
                return this.buffs[0].Test(1048576);
            }
            set
            {
                this.buffs[0].SetValue(1048576, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Spirit.
        /// </summary>
        public bool Spirit
        {
            get
            {
                return this.buffs[0].Test(2097152);
            }
            set
            {
                this.buffs[0].SetValue(2097152, value);
            }
        }

        /// <summary>
        /// Gets or sets the Buffs.
        /// </summary>
        public BitMask[] Buffs
        {
            get
            {
                return this.buffs;
            }
            set
            {
                this.buffs = value;
            }
        }

        /// <summary>
        /// The Clear.
        /// </summary>
        public void Clear()
        {
            this.buffs[0].Value = 0;
            this.buffs[1].Value = 0;
            this.buffs[2].Value = 0;
            this.buffs[3].Value = 0;
            this.buffs[4].Value = 0;
            this.buffs[5].Value = 0;
            this.buffs[6].Value = 0;
        }

        /// <summary>
        /// Gets or sets a value indicating whether state190.
        /// </summary>
        public bool state190
        {
            get
            {
                return this.buffs[6].Test(1);
            }
            set
            {
                this.buffs[6].SetValue(1, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether オーバーワーク.
        /// </summary>
        public bool オーバーワーク
        {
            get
            {
                return this.buffs[6].Test(2);
            }
            set
            {
                this.buffs[6].SetValue(2, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether ディレイキャンセル.
        /// </summary>
        public bool ディレイキャンセル
        {
            get
            {
                return this.buffs[6].Test(4);
            }
            set
            {
                this.buffs[6].SetValue(4, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 赤くなる.
        /// </summary>
        public bool 赤くなる
        {
            get
            {
                return this.buffs[6].Test(8);
            }
            set
            {
                this.buffs[6].SetValue(8, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether フェニックス.
        /// </summary>
        public bool フェニックス
        {
            get
            {
                return this.buffs[6].Test(16);
            }
            set
            {
                this.buffs[6].SetValue(16, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether スタミナテイク.
        /// </summary>
        public bool スタミナテイク
        {
            get
            {
                return this.buffs[6].Test(32);
            }
            set
            {
                this.buffs[6].SetValue(32, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether マナの守護.
        /// </summary>
        public bool マナの守護
        {
            get
            {
                return this.buffs[6].Test(128);
            }
            set
            {
                this.buffs[6].SetValue(128, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether チャンプモンスターキラー状態.
        /// </summary>
        public bool チャンプモンスターキラー状態
        {
            get
            {
                return this.buffs[6].Test(256);
            }
            set
            {
                this.buffs[6].SetValue(256, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 竜眼開放.
        /// </summary>
        public bool 竜眼開放
        {
            get
            {
                return this.buffs[6].Test(512);
            }
            set
            {
                this.buffs[6].SetValue(512, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 温泉効果.
        /// </summary>
        public bool 温泉効果
        {
            get
            {
                return this.buffs[6].Test(1024);
            }
            set
            {
                this.buffs[6].SetValue(1024, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 武器属性無効化.
        /// </summary>
        public bool 武器属性無効化
        {
            get
            {
                return this.buffs[6].Test(2048);
            }
            set
            {
                this.buffs[6].SetValue(2048, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 防御属性無効化.
        /// </summary>
        public bool 防御属性無効化
        {
            get
            {
                return this.buffs[6].Test(4096);
            }
            set
            {
                this.buffs[6].SetValue(4096, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether ロケットブースター点火.
        /// </summary>
        public bool ロケットブースター点火
        {
            get
            {
                return this.buffs[6].Test(8192);
            }
            set
            {
                this.buffs[6].SetValue(8192, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether ゾンビ.
        /// </summary>
        public bool ゾンビ
        {
            get
            {
                return this.buffs[5].Test(1);
            }
            set
            {
                this.buffs[5].SetValue(1, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether リボーン.
        /// </summary>
        public bool リボーン
        {
            get
            {
                return this.buffs[5].Test(2);
            }
            set
            {
                this.buffs[5].SetValue(2, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 演奏中.
        /// </summary>
        public bool 演奏中
        {
            get
            {
                return this.buffs[5].Test(4);
            }
            set
            {
                this.buffs[5].SetValue(4, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 羽交い絞め.
        /// </summary>
        public bool 羽交い絞め
        {
            get
            {
                return this.buffs[5].Test(8);
            }
            set
            {
                this.buffs[5].SetValue(8, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 光魔法封印.
        /// </summary>
        public bool 光魔法封印
        {
            get
            {
                return this.buffs[5].Test(16);
            }
            set
            {
                this.buffs[5].SetValue(16, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether オーバーレンジ.
        /// </summary>
        public bool オーバーレンジ
        {
            get
            {
                return this.buffs[5].Test(32);
            }
            set
            {
                this.buffs[5].SetValue(32, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether ライフテイク.
        /// </summary>
        public bool ライフテイク
        {
            get
            {
                return this.buffs[5].Test(64);
            }
            set
            {
                this.buffs[5].SetValue(64, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 恐怖.
        /// </summary>
        public bool 恐怖
        {
            get
            {
                return this.buffs[5].Test(128);
            }
            set
            {
                this.buffs[5].SetValue(128, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 経験値上昇.
        /// </summary>
        public bool 経験値上昇
        {
            get
            {
                return this.buffs[5].Test(256);
            }
            set
            {
                this.buffs[5].SetValue(256, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether パッシング.
        /// </summary>
        public bool パッシング
        {
            get
            {
                return this.buffs[5].Test(512);
            }
            set
            {
                this.buffs[5].SetValue(512, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 回復不可能.
        /// </summary>
        public bool 回復不可能
        {
            get
            {
                return this.buffs[5].Test(1024);
            }
            set
            {
                this.buffs[5].SetValue(1024, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether エンチャントブロック.
        /// </summary>
        public bool エンチャントブロック
        {
            get
            {
                return this.buffs[5].Test(2048);
            }
            set
            {
                this.buffs[5].SetValue(2048, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether ソリッドボディ.
        /// </summary>
        public bool ソリッドボディ
        {
            get
            {
                return this.buffs[5].Test(4096);
            }
            set
            {
                this.buffs[5].SetValue(4096, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether ブラッディウエポン.
        /// </summary>
        public bool ブラッディウエポン
        {
            get
            {
                return this.buffs[5].Test(8192);
            }
            set
            {
                this.buffs[5].SetValue(8192, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether フレア.
        /// </summary>
        public bool フレア
        {
            get
            {
                return this.buffs[5].Test(16384);
            }
            set
            {
                this.buffs[5].SetValue(16384, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether ガンディレイキャンセル.
        /// </summary>
        public bool ガンディレイキャンセル
        {
            get
            {
                return this.buffs[5].Test(32768);
            }
            set
            {
                this.buffs[5].SetValue(32768, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether ダブルアップ.
        /// </summary>
        public bool ダブルアップ
        {
            get
            {
                return this.buffs[5].Test(65536);
            }
            set
            {
                this.buffs[5].SetValue(65536, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether ATフィールド.
        /// </summary>
        public bool ATフィールド
        {
            get
            {
                return this.buffs[5].Test(131072);
            }
            set
            {
                this.buffs[5].SetValue(131072, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 根性.
        /// </summary>
        public bool 根性
        {
            get
            {
                return this.buffs[5].Test(262144);
            }
            set
            {
                this.buffs[5].SetValue(262144, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 物理攻撃付加.
        /// </summary>
        public bool 物理攻撃付加
        {
            get
            {
                return this.buffs[5].Test(524288);
            }
            set
            {
                this.buffs[5].SetValue(524288, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 死んだふり.
        /// </summary>
        public bool 死んだふり
        {
            get
            {
                return this.buffs[5].Test(1048576);
            }
            set
            {
                this.buffs[5].SetValue(1048576, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether パパ点火.
        /// </summary>
        public bool パパ点火
        {
            get
            {
                return this.buffs[5].Test(2097152);
            }
            set
            {
                this.buffs[5].SetValue(2097152, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 紫になる.
        /// </summary>
        public bool 紫になる
        {
            get
            {
                return this.buffs[5].Test(4194304);
            }
            set
            {
                this.buffs[5].SetValue(4194304, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 精密射撃.
        /// </summary>
        public bool 精密射撃
        {
            get
            {
                return this.buffs[5].Test(8388608);
            }
            set
            {
                this.buffs[5].SetValue(8388608, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether オーバーチューン.
        /// </summary>
        public bool オーバーチューン
        {
            get
            {
                return this.buffs[5].Test(16777216);
            }
            set
            {
                this.buffs[5].SetValue(16777216, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 警戒.
        /// </summary>
        public bool 警戒
        {
            get
            {
                return this.buffs[5].Test(33554432);
            }
            set
            {
                this.buffs[5].SetValue(33554432, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether リフレクション.
        /// </summary>
        public bool リフレクション
        {
            get
            {
                return this.buffs[5].Test(67108864);
            }
            set
            {
                this.buffs[5].SetValue(67108864, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether エンチャントウエポン.
        /// </summary>
        public bool エンチャントウエポン
        {
            get
            {
                return this.buffs[5].Test(134217728);
            }
            set
            {
                this.buffs[5].SetValue(134217728, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether オラトリオ.
        /// </summary>
        public bool オラトリオ
        {
            get
            {
                return this.buffs[5].Test(268435456);
            }
            set
            {
                this.buffs[5].SetValue(268435456, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether イビルソウル.
        /// </summary>
        public bool イビルソウル
        {
            get
            {
                return this.buffs[5].Test(536870912);
            }
            set
            {
                this.buffs[5].SetValue(536870912, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether フレイムハート.
        /// </summary>
        public bool フレイムハート
        {
            get
            {
                return this.buffs[5].Test(1073741824);
            }
            set
            {
                this.buffs[5].SetValue(1073741824, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether アトラクトマーチ.
        /// </summary>
        public bool アトラクトマーチ
        {
            get
            {
                return this.buffs[5].Test((object)2147483648U);
            }
            set
            {
                this.buffs[5].SetValue((object)2147483648U, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 最大HP減少.
        /// </summary>
        public bool 最大HP減少
        {
            get
            {
                return this.buffs[4].Test(1);
            }
            set
            {
                this.buffs[4].SetValue(1, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 最大MP減少.
        /// </summary>
        public bool 最大MP減少
        {
            get
            {
                return this.buffs[4].Test(2);
            }
            set
            {
                this.buffs[4].SetValue(2, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 最大SP減少.
        /// </summary>
        public bool 最大SP減少
        {
            get
            {
                return this.buffs[4].Test(4);
            }
            set
            {
                this.buffs[4].SetValue(4, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 最小攻撃力減少.
        /// </summary>
        public bool 最小攻撃力減少
        {
            get
            {
                return this.buffs[4].Test(16);
            }
            set
            {
                this.buffs[4].SetValue(16, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 最大攻撃力減少.
        /// </summary>
        public bool 最大攻撃力減少
        {
            get
            {
                return this.buffs[4].Test(32);
            }
            set
            {
                this.buffs[4].SetValue(32, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 最小魔法攻撃力減少.
        /// </summary>
        public bool 最小魔法攻撃力減少
        {
            get
            {
                return this.buffs[4].Test(64);
            }
            set
            {
                this.buffs[4].SetValue(64, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 最大魔法攻撃力減少.
        /// </summary>
        public bool 最大魔法攻撃力減少
        {
            get
            {
                return this.buffs[4].Test(128);
            }
            set
            {
                this.buffs[4].SetValue(128, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 防御率減少.
        /// </summary>
        public bool 防御率減少
        {
            get
            {
                return this.buffs[4].Test(256);
            }
            set
            {
                this.buffs[4].SetValue(256, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 防御力減少.
        /// </summary>
        public bool 防御力減少
        {
            get
            {
                return this.buffs[4].Test(512);
            }
            set
            {
                this.buffs[4].SetValue(512, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 魔法防御率減少.
        /// </summary>
        public bool 魔法防御率減少
        {
            get
            {
                return this.buffs[4].Test(1024);
            }
            set
            {
                this.buffs[4].SetValue(1024, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 魔法防御力減少.
        /// </summary>
        public bool 魔法防御力減少
        {
            get
            {
                return this.buffs[4].Test(2048);
            }
            set
            {
                this.buffs[4].SetValue(2048, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 近距離命中率減少.
        /// </summary>
        public bool 近距離命中率減少
        {
            get
            {
                return this.buffs[4].Test(4096);
            }
            set
            {
                this.buffs[4].SetValue(4096, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 遠距離命中率減少.
        /// </summary>
        public bool 遠距離命中率減少
        {
            get
            {
                return this.buffs[4].Test(8192);
            }
            set
            {
                this.buffs[4].SetValue(8192, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 魔法命中率減少.
        /// </summary>
        public bool 魔法命中率減少
        {
            get
            {
                return this.buffs[4].Test(16384);
            }
            set
            {
                this.buffs[4].SetValue(16384, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 近距離回避率減少.
        /// </summary>
        public bool 近距離回避率減少
        {
            get
            {
                return this.buffs[4].Test(32768);
            }
            set
            {
                this.buffs[4].SetValue(32768, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 遠距離回避率減少.
        /// </summary>
        public bool 遠距離回避率減少
        {
            get
            {
                return this.buffs[4].Test(65536);
            }
            set
            {
                this.buffs[4].SetValue(65536, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 魔法抵抗率減少.
        /// </summary>
        public bool 魔法抵抗率減少
        {
            get
            {
                return this.buffs[4].Test(131072);
            }
            set
            {
                this.buffs[4].SetValue(131072, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether クリティカル率減少.
        /// </summary>
        public bool クリティカル率減少
        {
            get
            {
                return this.buffs[4].Test(262144);
            }
            set
            {
                this.buffs[4].SetValue(262144, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether クリティカル回避率減少.
        /// </summary>
        public bool クリティカル回避率減少
        {
            get
            {
                return this.buffs[4].Test(524288);
            }
            set
            {
                this.buffs[4].SetValue(524288, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether HP回復率減少.
        /// </summary>
        public bool HP回復率減少
        {
            get
            {
                return this.buffs[4].Test(1048576);
            }
            set
            {
                this.buffs[4].SetValue(1048576, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether MP回復率減少.
        /// </summary>
        public bool MP回復率減少
        {
            get
            {
                return this.buffs[4].Test(2097152);
            }
            set
            {
                this.buffs[4].SetValue(2097152, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether SP回復率減少.
        /// </summary>
        public bool SP回復率減少
        {
            get
            {
                return this.buffs[4].Test(4194304);
            }
            set
            {
                this.buffs[4].SetValue(4194304, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 攻撃スピード減少.
        /// </summary>
        public bool 攻撃スピード減少
        {
            get
            {
                return this.buffs[4].Test(8388608);
            }
            set
            {
                this.buffs[4].SetValue(8388608, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 詠唱スピード減少.
        /// </summary>
        public bool 詠唱スピード減少
        {
            get
            {
                return this.buffs[4].Test(16777216);
            }
            set
            {
                this.buffs[4].SetValue(16777216, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether STR減少.
        /// </summary>
        public bool STR減少
        {
            get
            {
                return this.buffs[4].Test(33554432);
            }
            set
            {
                this.buffs[4].SetValue(33554432, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether DEX減少.
        /// </summary>
        public bool DEX減少
        {
            get
            {
                return this.buffs[4].Test(67108864);
            }
            set
            {
                this.buffs[4].SetValue(67108864, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether INT減少.
        /// </summary>
        public bool INT減少
        {
            get
            {
                return this.buffs[4].Test(134217728);
            }
            set
            {
                this.buffs[4].SetValue(134217728, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether VIT減少.
        /// </summary>
        public bool VIT減少
        {
            get
            {
                return this.buffs[4].Test(268435456);
            }
            set
            {
                this.buffs[4].SetValue(268435456, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether AGI減少.
        /// </summary>
        public bool AGI減少
        {
            get
            {
                return this.buffs[4].Test(536870912);
            }
            set
            {
                this.buffs[4].SetValue(536870912, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether MAG減少.
        /// </summary>
        public bool MAG減少
        {
            get
            {
                return this.buffs[4].Test(1073741824);
            }
            set
            {
                this.buffs[4].SetValue(1073741824, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 最大HP上昇.
        /// </summary>
        public bool 最大HP上昇
        {
            get
            {
                return this.buffs[3].Test(1);
            }
            set
            {
                this.buffs[3].SetValue(1, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 最大MP上昇.
        /// </summary>
        public bool 最大MP上昇
        {
            get
            {
                return this.buffs[3].Test(2);
            }
            set
            {
                this.buffs[3].SetValue(2, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 最大SP上昇.
        /// </summary>
        public bool 最大SP上昇
        {
            get
            {
                return this.buffs[3].Test(4);
            }
            set
            {
                this.buffs[3].SetValue(4, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 移動力上昇.
        /// </summary>
        public bool 移動力上昇
        {
            get
            {
                return this.buffs[3].Test(8);
            }
            set
            {
                this.buffs[3].SetValue(8, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 最小攻撃力上昇.
        /// </summary>
        public bool 最小攻撃力上昇
        {
            get
            {
                return this.buffs[3].Test(16);
            }
            set
            {
                this.buffs[3].SetValue(16, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 最大攻撃力上昇.
        /// </summary>
        public bool 最大攻撃力上昇
        {
            get
            {
                return this.buffs[3].Test(32);
            }
            set
            {
                this.buffs[3].SetValue(32, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 最小魔法攻撃力上昇.
        /// </summary>
        public bool 最小魔法攻撃力上昇
        {
            get
            {
                return this.buffs[3].Test(64);
            }
            set
            {
                this.buffs[3].SetValue(64, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 最大魔法攻撃力上昇.
        /// </summary>
        public bool 最大魔法攻撃力上昇
        {
            get
            {
                return this.buffs[3].Test(128);
            }
            set
            {
                this.buffs[3].SetValue(128, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 防御率上昇.
        /// </summary>
        public bool 防御率上昇
        {
            get
            {
                return this.buffs[3].Test(256);
            }
            set
            {
                this.buffs[3].SetValue(256, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 防御力上昇.
        /// </summary>
        public bool 防御力上昇
        {
            get
            {
                return this.buffs[3].Test(512);
            }
            set
            {
                this.buffs[3].SetValue(512, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 魔法防御率上昇.
        /// </summary>
        public bool 魔法防御率上昇
        {
            get
            {
                return this.buffs[3].Test(1024);
            }
            set
            {
                this.buffs[3].SetValue(1024, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 魔法防御力上昇.
        /// </summary>
        public bool 魔法防御力上昇
        {
            get
            {
                return this.buffs[3].Test(2048);
            }
            set
            {
                this.buffs[3].SetValue(2048, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 近距離命中率上昇.
        /// </summary>
        public bool 近距離命中率上昇
        {
            get
            {
                return this.buffs[3].Test(4096);
            }
            set
            {
                this.buffs[3].SetValue(4096, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 遠距離命中率上昇.
        /// </summary>
        public bool 遠距離命中率上昇
        {
            get
            {
                return this.buffs[3].Test(8192);
            }
            set
            {
                this.buffs[3].SetValue(8192, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 魔法命中率上昇.
        /// </summary>
        public bool 魔法命中率上昇
        {
            get
            {
                return this.buffs[3].Test(16384);
            }
            set
            {
                this.buffs[3].SetValue(16384, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 近距離回避率上昇.
        /// </summary>
        public bool 近距離回避率上昇
        {
            get
            {
                return this.buffs[3].Test(32768);
            }
            set
            {
                this.buffs[3].SetValue(32768, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 遠距離回避上昇.
        /// </summary>
        public bool 遠距離回避上昇
        {
            get
            {
                return this.buffs[3].Test(65536);
            }
            set
            {
                this.buffs[3].SetValue(65536, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 魔法抵抗上昇.
        /// </summary>
        public bool 魔法抵抗上昇
        {
            get
            {
                return this.buffs[3].Test(131072);
            }
            set
            {
                this.buffs[3].SetValue(131072, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether クリティカル率上昇.
        /// </summary>
        public bool クリティカル率上昇
        {
            get
            {
                return this.buffs[3].Test(262144);
            }
            set
            {
                this.buffs[3].SetValue(262144, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether クリティカル回避率上昇.
        /// </summary>
        public bool クリティカル回避率上昇
        {
            get
            {
                return this.buffs[3].Test(524288);
            }
            set
            {
                this.buffs[3].SetValue(524288, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether HP回復率上昇.
        /// </summary>
        public bool HP回復率上昇
        {
            get
            {
                return this.buffs[3].Test(1048576);
            }
            set
            {
                this.buffs[3].SetValue(1048576, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether MP回復率上昇.
        /// </summary>
        public bool MP回復率上昇
        {
            get
            {
                return this.buffs[3].Test(2097152);
            }
            set
            {
                this.buffs[3].SetValue(2097152, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether SP回復率上昇.
        /// </summary>
        public bool SP回復率上昇
        {
            get
            {
                return this.buffs[3].Test(4194304);
            }
            set
            {
                this.buffs[3].SetValue(4194304, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 攻撃スピード上昇.
        /// </summary>
        public bool 攻撃スピード上昇
        {
            get
            {
                return this.buffs[3].Test(8388608);
            }
            set
            {
                this.buffs[3].SetValue(8388608, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 詠唱スピード上昇.
        /// </summary>
        public bool 詠唱スピード上昇
        {
            get
            {
                return this.buffs[3].Test(16777216);
            }
            set
            {
                this.buffs[3].SetValue(16777216, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether STR上昇.
        /// </summary>
        public bool STR上昇
        {
            get
            {
                return this.buffs[3].Test(33554432);
            }
            set
            {
                this.buffs[3].SetValue(33554432, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether DEX上昇.
        /// </summary>
        public bool DEX上昇
        {
            get
            {
                return this.buffs[3].Test(67108864);
            }
            set
            {
                this.buffs[3].SetValue(67108864, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether INT上昇.
        /// </summary>
        public bool INT上昇
        {
            get
            {
                return this.buffs[3].Test(134217728);
            }
            set
            {
                this.buffs[3].SetValue(134217728, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether VIT上昇.
        /// </summary>
        public bool VIT上昇
        {
            get
            {
                return this.buffs[3].Test(268435456);
            }
            set
            {
                this.buffs[3].SetValue(268435456, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether AGI上昇.
        /// </summary>
        public bool AGI上昇
        {
            get
            {
                return this.buffs[3].Test(536870912);
            }
            set
            {
                this.buffs[3].SetValue(536870912, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether MAG上昇.
        /// </summary>
        public bool MAG上昇
        {
            get
            {
                return this.buffs[3].Test(1073741824);
            }
            set
            {
                this.buffs[3].SetValue(1073741824, value);
            }
        }
    }
}
