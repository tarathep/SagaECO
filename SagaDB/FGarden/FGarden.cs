namespace SagaDB.FGarden
{
    using SagaDB.Actor;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="FGarden" />.
    /// </summary>
    public class FGarden
    {
        /// <summary>
        /// Defines the equips.
        /// </summary>
        private Dictionary<FGardenSlot, uint> equips = new Dictionary<FGardenSlot, uint>();

        /// <summary>
        /// Defines the furnitures.
        /// </summary>
        private Dictionary<FurniturePlace, List<ActorFurniture>> furnitures = new Dictionary<FurniturePlace, List<ActorFurniture>>();

        /// <summary>
        /// Defines the owner.
        /// </summary>
        private ActorPC owner;

        /// <summary>
        /// Defines the ropeActor.
        /// </summary>
        private ActorEvent ropeActor;

        /// <summary>
        /// Defines the mapID.
        /// </summary>
        private uint mapID;

        /// <summary>
        /// Defines the roomMapID.
        /// </summary>
        private uint roomMapID;

        /// <summary>
        /// Defines the id.
        /// </summary>
        private uint id;

        /// <summary>
        /// Initializes a new instance of the <see cref="FGarden"/> class.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        public FGarden(ActorPC pc)
        {
            this.owner = pc;
            this.equips.Add(FGardenSlot.FLYING_BASE, 0U);
            this.equips.Add(FGardenSlot.FLYING_SAIL, 0U);
            this.equips.Add(FGardenSlot.GARDEN_FLOOR, 0U);
            this.equips.Add(FGardenSlot.GARDEN_MODELHOUSE, 0U);
            this.equips.Add(FGardenSlot.HouseOutSideWall, 0U);
            this.equips.Add(FGardenSlot.HouseRoof, 0U);
            this.equips.Add(FGardenSlot.ROOM_FLOOR, 0U);
            this.equips.Add(FGardenSlot.ROOM_WALL, 0U);
            this.furnitures.Add(FurniturePlace.GARDEN, new List<ActorFurniture>());
            this.furnitures.Add(FurniturePlace.ROOM, new List<ActorFurniture>());
        }

        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public uint ID
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
        /// Gets or sets the Owner.
        /// </summary>
        public ActorPC Owner
        {
            get
            {
                return this.owner;
            }
            set
            {
                this.owner = value;
            }
        }

        /// <summary>
        /// Gets or sets the RopeActor.
        /// </summary>
        public ActorEvent RopeActor
        {
            get
            {
                return this.ropeActor;
            }
            set
            {
                this.ropeActor = value;
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
        /// Gets or sets the RoomMapID.
        /// </summary>
        public uint RoomMapID
        {
            get
            {
                return this.roomMapID;
            }
            set
            {
                this.roomMapID = value;
            }
        }

        /// <summary>
        /// Gets the FGardenEquipments.
        /// </summary>
        public Dictionary<FGardenSlot, uint> FGardenEquipments
        {
            get
            {
                return this.equips;
            }
        }

        /// <summary>
        /// Gets the Furnitures.
        /// </summary>
        public Dictionary<FurniturePlace, List<ActorFurniture>> Furnitures
        {
            get
            {
                return this.furnitures;
            }
        }
    }
}
