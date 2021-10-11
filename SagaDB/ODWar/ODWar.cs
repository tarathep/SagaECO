namespace SagaDB.ODWar
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="ODWar" />.
    /// </summary>
    public class ODWar
    {
        /// <summary>
        /// Defines the startTime.
        /// </summary>
        private Dictionary<int, int> startTime = new Dictionary<int, int>();

        /// <summary>
        /// Defines the symbols.
        /// </summary>
        private Dictionary<int, SagaDB.ODWar.ODWar.Symbol> symbols = new Dictionary<int, SagaDB.ODWar.ODWar.Symbol>();

        /// <summary>
        /// Defines the score.
        /// </summary>
        private Dictionary<uint, int> score = new Dictionary<uint, int>();

        /// <summary>
        /// Defines the demChamp.
        /// </summary>
        private List<uint> demChamp = new List<uint>();

        /// <summary>
        /// Defines the demNormal.
        /// </summary>
        private List<uint> demNormal = new List<uint>();

        /// <summary>
        /// Defines the boss.
        /// </summary>
        private List<uint> boss = new List<uint>();

        /// <summary>
        /// Defines the mapID.
        /// </summary>
        private uint mapID;

        /// <summary>
        /// Defines the symbolTrash.
        /// </summary>
        private uint symbolTrash;

        /// <summary>
        /// Defines the waveStrong.
        /// </summary>
        private SagaDB.ODWar.ODWar.Wave waveStrong;

        /// <summary>
        /// Defines the waveWeak.
        /// </summary>
        private SagaDB.ODWar.ODWar.Wave waveWeak;

        /// <summary>
        /// Defines the started.
        /// </summary>
        private bool started;

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
        /// Gets the StartTime.
        /// </summary>
        public Dictionary<int, int> StartTime
        {
            get
            {
                return this.startTime;
            }
        }

        /// <summary>
        /// Gets the Symbols.
        /// </summary>
        public Dictionary<int, SagaDB.ODWar.ODWar.Symbol> Symbols
        {
            get
            {
                return this.symbols;
            }
        }

        /// <summary>
        /// Gets or sets the SymbolTrash.
        /// </summary>
        public uint SymbolTrash
        {
            get
            {
                return this.symbolTrash;
            }
            set
            {
                this.symbolTrash = value;
            }
        }

        /// <summary>
        /// Gets the DEMChamp.
        /// </summary>
        public List<uint> DEMChamp
        {
            get
            {
                return this.demChamp;
            }
        }

        /// <summary>
        /// Gets the DEMNormal.
        /// </summary>
        public List<uint> DEMNormal
        {
            get
            {
                return this.demNormal;
            }
        }

        /// <summary>
        /// Gets the Boss.
        /// </summary>
        public List<uint> Boss
        {
            get
            {
                return this.boss;
            }
        }

        /// <summary>
        /// Gets or sets the WaveStrong.
        /// </summary>
        public SagaDB.ODWar.ODWar.Wave WaveStrong
        {
            get
            {
                return this.waveStrong;
            }
            set
            {
                this.waveStrong = value;
            }
        }

        /// <summary>
        /// Gets or sets the WaveWeak.
        /// </summary>
        public SagaDB.ODWar.ODWar.Wave WaveWeak
        {
            get
            {
                return this.waveWeak;
            }
            set
            {
                this.waveWeak = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Started.
        /// </summary>
        public bool Started
        {
            get
            {
                return this.started;
            }
            set
            {
                this.started = value;
            }
        }

        /// <summary>
        /// Gets the Score.
        /// </summary>
        public Dictionary<uint, int> Score
        {
            get
            {
                return this.score;
            }
        }

        /// <summary>
        /// Defines the <see cref="Symbol" />.
        /// </summary>
        public class Symbol
        {
            /// <summary>
            /// Defines the id.
            /// </summary>
            public int id;

            /// <summary>
            /// Defines the mobID.
            /// </summary>
            public uint mobID;

            /// <summary>
            /// Defines the x.
            /// </summary>
            public byte x;

            /// <summary>
            /// Defines the y.
            /// </summary>
            public byte y;

            /// <summary>
            /// Defines the actorID.
            /// </summary>
            public uint actorID;

            /// <summary>
            /// Defines the broken.
            /// </summary>
            public bool broken;
        }

        /// <summary>
        /// Defines the <see cref="Wave" />.
        /// </summary>
        public class Wave
        {
            /// <summary>
            /// Defines the DEMChamp.
            /// </summary>
            public int DEMChamp;

            /// <summary>
            /// Defines the DEMNormal.
            /// </summary>
            public int DEMNormal;
        }
    }
}
