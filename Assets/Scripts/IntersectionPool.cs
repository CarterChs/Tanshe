using System;
using System.Collections.Generic;
using CleverMath;
namespace GameSystem
{
    internal class IntersectionPool
    {
        private Queue<Intersection> queue;

        public IntersectionPool()
        {
            queue = new Queue<Intersection>();
        }

        internal Intersection Get(IPhysicalObject physicalObject, IPhysicalObject otherPhysicalObject, Coord2 delta, Coord2 normal)
        {
            if (queue.Count > 0)
            {
                var intersection = queue.Dequeue();
                intersection.Initialize(physicalObject, otherPhysicalObject, delta, normal);
                return intersection;
            }
            else
            {
                return new Intersection(physicalObject, otherPhysicalObject, delta, normal);
            }
        }

        internal void Release(Intersection intersection)
        {
            queue.Enqueue(intersection);
        }
    }
}