using System;
using System.Collections.Generic;
using CleverMath;
namespace GameSystem
{
    internal class PhysicsEngine
    {
        private IntersectionPool intersectionPool;
        private List<Intersection> intersections;

        public PhysicsEngine()
        {
            intersectionPool = new IntersectionPool();
            intersections = new List<Intersection>();
        }

        public IReadOnlyList<Intersection> Update(float deltaTime, IReadOnlyList<IPhysicalObject> physicalObjects)
        {
            // Update physical obj's position
            for (int i = 0; i < physicalObjects.Count; i++)
            {
                var physicalObject = physicalObjects[i];
                if (physicalObject.IsMovable)
                {
                    physicalObject.Position += physicalObject.Velocity * deltaTime;
                }
            }
            // Dectect collision
            for (int i = 0; i < intersections.Count; i++)
            {
                var intersection = intersections[i];
                intersectionPool.Release(intersection);

            }
            intersections.Clear();
            for (int i = 0; i < physicalObjects.Count; i++)
            {
                var physicalObject = physicalObjects[i];
                for (int j = i + 1; j < physicalObjects.Count; j++)
                {
                    var otherPhysicalObject = physicalObjects[j];
                    var intersection = (physicalObject.IsMovable || otherPhysicalObject.IsMovable) ? GetIntersection(physicalObject, otherPhysicalObject) : null;
                    if (intersection != null)
                    {
                        if (physicalObject.IsTrigger || otherPhysicalObject.IsTrigger)
                        {
                            // Do nothing
                        }
                        else
                        {
                            if (physicalObject.IsMovable && otherPhysicalObject.IsMovable)
                            {
                                physicalObject.Position -= intersection.Delta * 0.5f;
                                otherPhysicalObject.Position += intersection.Delta * 0.5f;
                                var relativeVelocity = otherPhysicalObject.Velocity - physicalObject.Velocity;
                                var rvMagnitude = Coord2.Dot(relativeVelocity, intersection.Normal);
                                var e = CalRestitution(physicalObject, otherPhysicalObject);
                                var impulse = (1 + e) * rvMagnitude / ((1 / physicalObject.Mass) + (1 / otherPhysicalObject.Mass)) * intersection.Normal;
                                physicalObject.Velocity += impulse / physicalObject.Mass;
                                otherPhysicalObject.Velocity -= impulse / otherPhysicalObject.Mass;
                            }
                            else if (physicalObject.IsMovable && !otherPhysicalObject.IsMovable)
                            {
                                physicalObject.Position -= intersection.Delta;
                                var rvMagnitude = Coord2.Dot(-physicalObject.Velocity, intersection.Normal);
                                var e = CalRestitution(physicalObject, otherPhysicalObject);
                                var impulse = (1 + e) * rvMagnitude / (1 / physicalObject.Mass) * intersection.Normal;
                                physicalObject.Velocity += impulse / physicalObject.Mass;
                            }
                            else if (!physicalObject.IsMovable && otherPhysicalObject.IsMovable)
                            {
                                otherPhysicalObject.Position += intersection.Delta;
                                var rvMagnitude = Coord2.Dot(otherPhysicalObject.Velocity, intersection.Normal);
                                var e = CalRestitution(physicalObject, otherPhysicalObject);
                                var impulse = (1 + e) * rvMagnitude / (1 / otherPhysicalObject.Mass) * intersection.Normal;
                                otherPhysicalObject.Velocity -= impulse / otherPhysicalObject.Mass;
                            }
                            else
                            {
                                // Do nothing
                            }
                        }
                        intersections.Add(intersection);
                    }
                }
            }
            return intersections;
        }

        private float CalRestitution(IPhysicalObject physicalObject, IPhysicalObject otherPhysicalObject)
        {
            return physicalObject.Restitution * 0.5f + otherPhysicalObject.Restitution * 0.5f;
        }

        private Intersection GetIntersection(IPhysicalObject physicalObject, IPhysicalObject otherPhysicalObject)
        {
            var dx = otherPhysicalObject.Position.x - physicalObject.Position.x;
            var dy = otherPhysicalObject.Position.y - physicalObject.Position.y;
            var px = 0.5f * (physicalObject.Length + otherPhysicalObject.Length) - Math.Abs(dx);
            var py = 0.5f * (physicalObject.Width + otherPhysicalObject.Width) - Math.Abs(dy);
            if (px <= 0 || py <= 0) { return null; }
            else
            {
                if (px < py)
                {
                    var sx = dx >= 0 ? 1 : -1;
                    var delta = new Coord2(px * sx, 0);
                    var normal = new Coord2(sx, 0);
                    return intersectionPool.Get(physicalObject, otherPhysicalObject, delta, normal);
                }
                else
                {
                    var sy = dy >= 0 ? 1 : -1;
                    var delta = new Coord2(0, py * sy);
                    var normal = new Coord2(0, sy);
                    return intersectionPool.Get(physicalObject, otherPhysicalObject, delta, normal);
                }
            }
        }
    }
}