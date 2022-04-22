using System;
using CleverMath;
namespace GameSystem
{
    internal class Intersection
    {
        private IPhysicalObject physicalObject;
        private IPhysicalObject otherPhysicalObject;
        private Coord2 delta;
        private Coord2 normal;

        public Intersection(IPhysicalObject physicalObject, IPhysicalObject otherPhysicalObject, Coord2 delta, Coord2 normal)
        {
            Initialize(physicalObject, otherPhysicalObject, delta, normal);
        }

        public void Initialize(IPhysicalObject physicalObject, IPhysicalObject otherPhysicalObject, Coord2 delta, Coord2 normal)
        {
            this.physicalObject = physicalObject;
            this.otherPhysicalObject = otherPhysicalObject;
            this.delta = delta;
            this.normal = normal;
        }

        public Coord2 Delta { get { return delta; } }
        public Coord2 Normal { get { return normal; } }
        public IPhysicalObject PhysicalObject { get { return physicalObject; } }
        public IPhysicalObject OtherPhysicalObject { get { return otherPhysicalObject; } }
    }
}