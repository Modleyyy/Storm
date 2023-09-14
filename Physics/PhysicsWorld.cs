namespace Storm.Physics;

using System.Collections.Generic;

public class PhysicsWorld
{
    public readonly Vector2 gravity;
    private readonly List<PhysicsBody> bodies;

    public PhysicsWorld(Vector2 gravity)
    {
        this.bodies = new();
        this.gravity = gravity;
    }
    public PhysicsWorld()
    {
        this.bodies = new();
        this.gravity = new();
    }

    public void Update(double deltaTime)
    {
        const byte substeps = 2;
        deltaTime /= substeps;

        ReadOnlySpan<PhysicsBody> bodies = this.bodies.ToArray().AsSpan();

        for (byte x = 0; x < substeps; x++)
        {
            for (short i = 0; i < bodies.Length; i++)
            {
                PhysicsBody bodyA = bodies[i];

                if (bodyA.isStatic)
                    continue;

                bodyA.velocity += gravity;
                bodyA.boundObject.transform.position += bodyA.velocity * (float)deltaTime;

                for (short y = 0; y < bodies.Length; y++)
                {
                    if (i == y)
                        continue;

                    PhysicsBody bodyB = bodies[y];
                    
                    if (bodyA is RectangleBody rA)
                    {
                        if (bodyB is RectangleBody rB)
                        {
                            bool isColliding = rA.right >= rB.left && rA.left <= rB.right && rA.bottom >= rB.top && rA.top <= rB.bottom;
                            if (isColliding)
                            {
                                float overlapX = MathF.Min(rA.right - rB.left, rB.right - rA.left);
                                float overlapY = MathF.Min(rA.bottom - rB.top, rB.bottom - rA.top);

                                if (overlapX < overlapY)
                                {
                                    if (!rB.isStatic)
                                    {
                                        float seperation = overlapX/2;
                                        rA.boundObject.transform.xPos -= seperation;
                                        rB.boundObject.transform.xPos += seperation;
                                    }
                                    else
                                    {
                                        if (rA.boundObject.transform.xPos < rB.boundObject.transform.xPos)
                                            rA.boundObject.transform.xPos -= overlapX;
                                        else
                                            rA.boundObject.transform.xPos += overlapX;
                                    }
                                }
                                else
                                {
                                    if (!rB.isStatic)
                                    {
                                        float seperation = overlapY/2;
                                        rA.boundObject.transform.yPos -= seperation;
                                        rB.boundObject.transform.yPos += seperation;
                                    }
                                    else
                                    {
                                        if (rA.boundObject.transform.yPos < rB.boundObject.transform.yPos)
                                            rA.boundObject.transform.yPos -= overlapY;
                                        else
                                            rA.boundObject.transform.yPos += overlapY;
                                    }
                                }
                            }

                            // Continue the loop once the collision stuff is done
                            continue;
                        }
        
                        if (bodyB is CircleBody cB)
                        {
                            float closestX = MathF.Max(rA.left, MathF.Min(cB.center.x, rA.right));
                            float closestY = MathF.Max(rA.top, MathF.Min(cB.center.y, rA.bottom));
                            Vector2 closestPoint = new(closestX, closestY);

                            float distanceSquared = cB.center.DistanceSquared(closestPoint);
                            bool isColliding = distanceSquared <= cB.radius * cB.radius;

                            if (isColliding)
                            {
                                Vector2 collisionNormal = cB.center - closestPoint;
                                collisionNormal.Normalize();

                                float penetrationDepth = cB.radius - MathF.Sqrt(distanceSquared);

                                if (!cB.isStatic)
                                {
                                    float separation = penetrationDepth / 2f;
                                    rA.boundObject.transform.position = rA.center - collisionNormal * separation;
                                    cB.boundObject.transform.position = cB.center + collisionNormal * separation;
                                }
                                else if (cB.isStatic)
                                {
                                    rA.boundObject.transform.position = rA.center - collisionNormal * penetrationDepth;
                                }
                            }
                            
                            // Continue the loop once the collision stuff is done
                            continue;
                        }
                    }

                    else if (bodyA is CircleBody cA)
                    {
                        if (bodyB is CircleBody cB)
                        {
                            float distanceSquared = cA.center.DistanceSquared(cB.center);
                            float combinedRadius = cA.radius + cB.radius;

                            bool isColliding = distanceSquared <= combinedRadius * combinedRadius;

                            if (isColliding)
                            {
                                if (!cB.isStatic)
                                {
                                    Vector2 collisionNormal = cB.center - cA.center;
                                    collisionNormal.Normalize();

                                    float penetrationDepth = combinedRadius - cA.center.Distance(cB.center);

                                    float separation = penetrationDepth / 2f;
                                    cA.boundObject.transform.position = cA.center - collisionNormal * separation;
                                    cB.boundObject.transform.position = cB.center + collisionNormal * separation;
                                }
                                else if (cB.isStatic)
                                {
                                    Vector2 collisionNormal = cB.center - cA.center;
                                    collisionNormal.Normalize();

                                    float penetrationDepth = combinedRadius - cA.center.Distance(cB.center);

                                    cA.boundObject.transform.position = cA.center - collisionNormal * penetrationDepth;
                                }
                            }

                            continue;
                        }

                        else if (bodyB is RectangleBody rB)
                        {
                            float closestX = MathF.Max(rB.left, MathF.Min(cA.center.x, rB.right));
                            float closestY = MathF.Max(rB.top, MathF.Min(cA.center.y, rB.bottom));
                            Vector2 closestPoint = new(closestX, closestY);

                            float distanceSquared = cA.center.DistanceSquared(closestPoint);
                            bool isColliding = distanceSquared <= cA.radius * cA.radius;

                            if (isColliding)
                            {
                                Vector2 collisionNormal = cA.center - closestPoint;
                                collisionNormal.Normalize();

                                float penetrationDepth = cA.radius - MathF.Sqrt(distanceSquared);

                                if (!cA.isStatic)
                                {
                                    float separation = penetrationDepth / 2f;
                                    rB.boundObject.transform.position = rB.center - collisionNormal * separation;
                                    cA.boundObject.transform.position = cA.center + collisionNormal * separation;
                                }
                                else if (cA.isStatic)
                                {
                                    rB.boundObject.transform.position = rB.center - collisionNormal * penetrationDepth;
                                }
                            }

                            // Continue the loop once the collision stuff is done
                            continue;
                        }
                    }
                }
            }
        }
    }

    public void AddBody(PhysicsBody body)
    {
        body.world = this;
        bodies.Add(body);
    }

    public bool IsOnFloor(PhysicsBody body)
    {
        const float epsilon = 0.001f;

        foreach (PhysicsBody otherBody in bodies.Where(b => b != body))
        {
            if (body is RectangleBody rBody && otherBody is RectangleBody rOther)
            {
                if (rBody.right + epsilon >= rOther.left &&
                    rBody.left - epsilon <= rOther.right &&
                    rBody.bottom + epsilon >= rOther.top &&
                    rBody.bottom < rOther.top + epsilon)
                {
                    return true;
                }
            }
            else if (body is CircleBody cBody && otherBody is RectangleBody rOtherBody)
            {
                float closestX = MathF.Max(rOtherBody.left, MathF.Min(cBody.center.x, rOtherBody.right));
                float distanceSquared = cBody.center.DistanceSquared(new Vector2(closestX, rOtherBody.top));
                if (distanceSquared <= cBody.radius * cBody.radius && rOtherBody.top - epsilon <= cBody.center.y)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public bool IsOnWall(PhysicsBody body)
    {
        const float epsilon = 0.001f;

        foreach (PhysicsBody otherBody in bodies.Where(b => b != body))
        {
            if (body is RectangleBody rBody && otherBody is RectangleBody rOther)
            {
                if ((rBody.right + epsilon >= rOther.left && rBody.right <= rOther.left + epsilon) ||
                    (rBody.left - epsilon <= rOther.right && rBody.left >= rOther.right - epsilon))
                {
                    if (rBody.bottom < rOther.top && rBody.top > rOther.bottom)
                    {
                        return true;
                    }
                }
            }
            else if (body is CircleBody cBody && otherBody is RectangleBody rOtherBody)
            {
                if ((cBody.center.x + cBody.radius >= rOtherBody.left - epsilon &&
                    cBody.center.x + cBody.radius <= rOtherBody.left + epsilon) ||
                    (cBody.center.x - cBody.radius <= rOtherBody.right + epsilon &&
                    cBody.center.x - cBody.radius >= rOtherBody.right - epsilon))
                {
                    if (cBody.center.y < rOtherBody.bottom && cBody.center.y > rOtherBody.top)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    public bool IsOnCeiling(PhysicsBody body)
    {
        const float epsilon = 0.001f;

        foreach (PhysicsBody otherBody in bodies.Where(b => b != body))
        {
            if (body is RectangleBody rBody && otherBody is RectangleBody rOther)
            {
                if (rBody.right >= rOther.left &&
                    rBody.left <= rOther.right &&
                    rBody.top - epsilon <= rOther.bottom &&
                    rBody.top > rOther.bottom - epsilon)
                {
                    return true;
                }
            }
            else if (body is CircleBody cBody && otherBody is RectangleBody rOtherBody)
            {
                float closestX = MathF.Max(rOtherBody.left, MathF.Min(cBody.center.x, rOtherBody.right));
                float distanceSquared = cBody.center.DistanceSquared(new Vector2(closestX, rOtherBody.bottom));
                if (distanceSquared <= cBody.radius * cBody.radius && rOtherBody.bottom - epsilon <= cBody.center.y)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
