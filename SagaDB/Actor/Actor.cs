namespace SagaDB.Actor
{
    using SagaLib;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="Actor" />.
    /// </summary>
    [Serializable]
    public class Actor
    {
        /// <summary>
        /// Defines the buff.
        /// </summary>
        [NonSerialized]
        private Buff buff = new Buff();

        /// <summary>
        /// Defines the tasks.
        /// </summary>
        [NonSerialized]
        private Dictionary<string, MultiRunTask> tasks = new Dictionary<string, MultiRunTask>();

        /// <summary>
        /// Defines the elements.
        /// </summary>
        [NonSerialized]
        private Dictionary<SagaLib.Elements, int> elements = new Dictionary<SagaLib.Elements, int>();

        /// <summary>
        /// Defines the attackElements.
        /// </summary>
        [NonSerialized]
        private Dictionary<SagaLib.Elements, int> attackElements = new Dictionary<SagaLib.Elements, int>();

        /// <summary>
        /// Defines the abnormalStatus.
        /// </summary>
        [NonSerialized]
        private Dictionary<SagaLib.AbnormalStatus, short> abnormalStatus = new Dictionary<SagaLib.AbnormalStatus, short>();

        /// <summary>
        /// Defines the visibleActors.
        /// </summary>
        [NonSerialized]
        private List<uint> visibleActors = new List<uint>();

        /// <summary>
        /// Defines the slaves.
        /// </summary>
        [NonSerialized]
        private List<SagaDB.Actor.Actor> slaves = new List<SagaDB.Actor.Actor>();

        /// <summary>
        /// Defines the lastX.
        /// </summary>
        private short lastX;

        /// <summary>
        /// Defines the lastY.
        /// </summary>
        private short lastY;

        /// <summary>
        /// Defines the id.
        /// </summary>
        private uint id;

        /// <summary>
        /// Defines the type.
        /// </summary>
        public ActorType type;

        /// <summary>
        /// Defines the name.
        /// </summary>
        private string name;

        /// <summary>
        /// Defines the region.
        /// </summary>
        public uint region;

        /// <summary>
        /// Defines the invisble.
        /// </summary>
        public bool invisble;

        /// <summary>
        /// Defines the x.
        /// </summary>
        private short x;

        /// <summary>
        /// Defines the y.
        /// </summary>
        private short y;

        /// <summary>
        /// Defines the dir.
        /// </summary>
        private ushort dir;

        /// <summary>
        /// Defines the sightRange.
        /// </summary>
        public uint sightRange;

        /// <summary>
        /// Defines the mapID.
        /// </summary>
        private uint mapID;

        /// <summary>
        /// Defines the hp.
        /// </summary>
        private uint hp;

        /// <summary>
        /// Defines the mp.
        /// </summary>
        private uint mp;

        /// <summary>
        /// Defines the sp.
        /// </summary>
        private uint sp;

        /// <summary>
        /// Defines the max_hp.
        /// </summary>
        private uint max_hp;

        /// <summary>
        /// Defines the max_mp.
        /// </summary>
        private uint max_mp;

        /// <summary>
        /// Defines the max_sp.
        /// </summary>
        private uint max_sp;

        /// <summary>
        /// Defines the ep.
        /// </summary>
        private uint ep;

        /// <summary>
        /// Defines the max_ep.
        /// </summary>
        private uint max_ep;

        /// <summary>
        /// Defines the speed.
        /// </summary>
        private ushort speed;

        /// <summary>
        /// Defines the range.
        /// </summary>
        private uint range;

        /// <summary>
        /// Defines the status.
        /// </summary>
        [NonSerialized]
        private Status status;

        /// <summary>
        /// Defines the noNewStatus.
        /// </summary>
        private bool noNewStatus;

        /// <summary>
        /// Defines the e.
        /// </summary>
        public ActorEventHandler e;

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        /// <summary>
        /// Gets or sets the LastX.
        /// </summary>
        public short LastX
        {
            get
            {
                return this.lastX;
            }
            set
            {
                this.lastX = value;
            }
        }

        /// <summary>
        /// Gets or sets the LastY.
        /// </summary>
        public short LastY
        {
            get
            {
                return this.lastY;
            }
            set
            {
                this.lastY = value;
            }
        }

        /// <summary>
        /// Gets or sets the ActorID.
        /// </summary>
        public uint ActorID
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        /// <summary>
        /// Gets or sets the MapID.
        /// </summary>
        public uint MapID
        {
            get
            {
                return this.mapID;
            }
            set
            {
                this.mapID = value;
            }
        }

        /// <summary>
        /// Gets or sets the Level.
        /// </summary>
        public virtual byte Level
        {
            get
            {
                return 0;
            }
            set
            {
            }
        }

        /// <summary>
        /// Gets or sets the X.
        /// </summary>
        public short X
        {
            get
            {
                return this.x;
            }
            set
            {
                this.x = value;
            }
        }

        /// <summary>
        /// Gets or sets the Y.
        /// </summary>
        public short Y
        {
            get
            {
                return this.y;
            }
            set
            {
                this.y = value;
            }
        }

        /// <summary>
        /// Gets or sets the Dir.
        /// </summary>
        public ushort Dir
        {
            get
            {
                return this.dir;
            }
            set
            {
                this.dir = value;
            }
        }

        /// <summary>
        /// Gets or sets the Speed.
        /// </summary>
        public ushort Speed
        {
            get
            {
                return (ushort)((uint)this.speed + (uint)this.Status.speed_up);
            }
            set
            {
                int num = (int)value - this.Status.speed_up;
                this.speed = num < 0 ? (ushort)0 : (ushort)num;
                if (this.e == null)
                    return;
                this.e.PropertyUpdate(UpdateEvent.SPEED, 0);
            }
        }

        /// <summary>
        /// Gets or sets the HP.
        /// </summary>
        public uint HP
        {
            get
            {
                return this.hp;
            }
            set
            {
                this.hp = value;
            }
        }

        /// <summary>
        /// Gets or sets the MP.
        /// </summary>
        public uint MP
        {
            get
            {
                return this.mp;
            }
            set
            {
                this.mp = value;
            }
        }

        /// <summary>
        /// Gets or sets the SP.
        /// </summary>
        public uint SP
        {
            get
            {
                return this.sp;
            }
            set
            {
                this.sp = value;
            }
        }

        /// <summary>
        /// Gets or sets the MaxHP.
        /// </summary>
        public uint MaxHP
        {
            get
            {
                return this.max_hp;
            }
            set
            {
                this.max_hp = value;
            }
        }

        /// <summary>
        /// Gets or sets the MaxMP.
        /// </summary>
        public uint MaxMP
        {
            get
            {
                return this.max_mp;
            }
            set
            {
                this.max_mp = value;
            }
        }

        /// <summary>
        /// Gets or sets the MaxSP.
        /// </summary>
        public uint MaxSP
        {
            get
            {
                return this.max_sp;
            }
            set
            {
                this.max_sp = value;
            }
        }

        /// <summary>
        /// Gets or sets the EP.
        /// </summary>
        public uint EP
        {
            get
            {
                return this.ep;
            }
            set
            {
                if (this.max_ep != 0U && value > this.max_ep)
                    value = this.max_ep;
                if (value < 0U)
                    value = 0U;
                int para = (int)value - (int)this.ep;
                this.ep = value;
                if (this.e == null)
                    return;
                this.e.PropertyUpdate(UpdateEvent.EP, para);
            }
        }

        /// <summary>
        /// Gets or sets the MaxEP.
        /// </summary>
        public uint MaxEP
        {
            get
            {
                return this.max_ep;
            }
            set
            {
                this.max_ep = value;
            }
        }

        /// <summary>
        /// Gets or sets the Status.
        /// </summary>
        public Status Status
        {
            get
            {
                if (this.status == null && !this.noNewStatus)
                {
                    this.status = new Status(this);
                    this.noNewStatus = true;
                }
                return this.status;
            }
            set
            {
                this.status = value;
            }
        }

        /// <summary>
        /// Gets or sets the Range.
        /// </summary>
        public uint Range
        {
            get
            {
                return this.range;
            }
            set
            {
                this.range = value;
            }
        }

        /// <summary>
        /// Gets the Tasks.
        /// </summary>
        public Dictionary<string, MultiRunTask> Tasks
        {
            get
            {
                return this.tasks;
            }
        }

        /// <summary>
        /// Gets the Buff.
        /// </summary>
        public Buff Buff
        {
            get
            {
                return this.buff;
            }
        }

        /// <summary>
        /// Gets the VisibleActors.
        /// </summary>
        public List<uint> VisibleActors
        {
            get
            {
                return this.visibleActors;
            }
        }

        /// <summary>
        /// Gets the Elements.
        /// </summary>
        public Dictionary<SagaLib.Elements, int> Elements
        {
            get
            {
                if (this.elements.Count == 0)
                {
                    this.elements.Add(SagaLib.Elements.Neutral, 0);
                    this.elements.Add(SagaLib.Elements.Fire, 0);
                    this.elements.Add(SagaLib.Elements.Water, 0);
                    this.elements.Add(SagaLib.Elements.Wind, 0);
                    this.elements.Add(SagaLib.Elements.Earth, 0);
                    this.elements.Add(SagaLib.Elements.Holy, 0);
                    this.elements.Add(SagaLib.Elements.Dark, 0);
                }
                return this.elements;
            }
        }

        /// <summary>
        /// Gets the AttackElements.
        /// </summary>
        public Dictionary<SagaLib.Elements, int> AttackElements
        {
            get
            {
                if (this.attackElements.Count == 0)
                {
                    this.attackElements.Add(SagaLib.Elements.Neutral, 0);
                    this.attackElements.Add(SagaLib.Elements.Fire, 0);
                    this.attackElements.Add(SagaLib.Elements.Water, 0);
                    this.attackElements.Add(SagaLib.Elements.Wind, 0);
                    this.attackElements.Add(SagaLib.Elements.Earth, 0);
                    this.attackElements.Add(SagaLib.Elements.Holy, 0);
                    this.attackElements.Add(SagaLib.Elements.Dark, 0);
                }
                return this.attackElements;
            }
        }

        /// <summary>
        /// Gets the AbnormalStatus.
        /// </summary>
        public Dictionary<SagaLib.AbnormalStatus, short> AbnormalStatus
        {
            get
            {
                if (this.abnormalStatus.Count == 0)
                {
                    this.abnormalStatus.Add(SagaLib.AbnormalStatus.Confused, (short)0);
                    this.abnormalStatus.Add(SagaLib.AbnormalStatus.Frosen, (short)0);
                    this.abnormalStatus.Add(SagaLib.AbnormalStatus.Paralyse, (short)0);
                    this.abnormalStatus.Add(SagaLib.AbnormalStatus.Poisen, (short)0);
                    this.abnormalStatus.Add(SagaLib.AbnormalStatus.Silence, (short)0);
                    this.abnormalStatus.Add(SagaLib.AbnormalStatus.Sleep, (short)0);
                    this.abnormalStatus.Add(SagaLib.AbnormalStatus.Stone, (short)0);
                    this.abnormalStatus.Add(SagaLib.AbnormalStatus.Stun, (short)0);
                    this.abnormalStatus.Add(SagaLib.AbnormalStatus.鈍足, (short)0);
                }
                return this.abnormalStatus;
            }
        }

        /// <summary>
        /// Gets the Slave.
        /// </summary>
        public List<SagaDB.Actor.Actor> Slave
        {
            get
            {
                return this.slaves;
            }
        }

        /// <summary>
        /// The ClearTaskAddition.
        /// </summary>
        public void ClearTaskAddition()
        {
            foreach (MultiRunTask multiRunTask in this.Tasks.Values)
            {
                try
                {
                    multiRunTask.Deactivate();
                }
                catch (Exception ex)
                {
                }
            }
            Addition[] array = new Addition[this.Status.Additions.Count];
            this.Status.Additions.Values.CopyTo(array, 0);
            foreach (Addition addition in array)
            {
                try
                {
                    if (addition.Activated)
                        addition.AdditionEnd();
                }
                catch
                {
                }
            }
            this.Status.Additions.Clear();
            this.Tasks.Clear();
        }
    }
}
