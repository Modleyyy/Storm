namespace Storm.Physics;

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
        const int substeps = 2;
        deltaTime /= substeps;
        for (int i = 0; i < substeps; i++)
        {
            // Movement
            Parallel.For(0, bodies.Count, i =>
            {
                PhysicsBody body = bodies[i];
                if (!body.isStatic)
                {
                    body.velocity += gravity;
                    body.boundObject.transform.position += body.velocity * (float)deltaTime;
                }
            });

            // Collision detection and response
            Parallel.For(0, bodies.Count, i =>
            {
                PhysicsBody bodyA = bodies[i];
                Parallel.For(0, bodies.Count, y =>
                {
                    if (i == y)
                    {
                        // Return since the two bodies are the same
                        return;
                    }

                    PhysicsBody bodyB = bodies[y];
                    
                    if (bodyA is RectangleBody rA)
                    {
                        if (bodyB is RectangleBody rB)
                        {
                            // TODO: Implement AABB vs AABB collision detection and response

                            bool isColliding = rA.right >= rB.left && rA.left <= rB.right && rA.bottom >= rB.top && rA.top <= rB.bottom;
                            if (isColliding)
                            {
                                if (!rA.isStatic)
                                {
                                    if (!rB.isStatic)
                                    {
                                        float overlapX = Math.Min(rA.right - rB.left, rB.right - rA.left);
                                        float overlapY = Math.Min(rA.bottom - rB.top, rB.bottom - rA.top);

                                        if (overlapX < overlapY)
                                        {
                                            float penetrationDepth = overlapX;
                                            float separation = penetrationDepth / 2f;

                                            rA.boundObject.transform.position = rA.center - new Vector2(separation, 0);
                                            rB.boundObject.transform.position = rB.center + new Vector2(separation, 0);

                                            rA.velocity.x *= -1;
                                            rB.velocity.x *= -1;
                                        }
                                        else
                                        {
                                            float penetrationDepth = overlapY;
                                            float separation = penetrationDepth / 2f;

                                            rA.boundObject.transform.position = rA.center - new Vector2(0, separation);
                                            rB.boundObject.transform.position = rB.center + new Vector2(0, separation);

                                            rA.velocity.y *= -1;
                                            rB.velocity.y *= -1;
                                        }
                                    }
                                    else if (rB.isStatic)
                                    {
                                        float overlapX = Math.Min(rA.right - rB.left, rB.right - rA.left);
                                        float overlapY = Math.Min(rA.bottom - rB.top, rB.bottom - rA.top);

                                        if (overlapX < overlapY)
                                        {
                                            float penetrationDepth = overlapX;

                                            rA.boundObject.transform.position = rA.center - new Vector2(penetrationDepth, 0);
                                            rA.velocity.x *= -1;
                                        }
                                        else
                                        {
                                            float penetrationDepth = overlapY;

                                            rA.boundObject.transform.position = rA.center - new Vector2(0, penetrationDepth);
                                            rA.velocity.y *= -1;
                                        }
                                    }
                                }
                                else if (rA.isStatic)
                                {
                                    if (!rB.isStatic)
                                    {
                                        float overlapX = Math.Min(rA.right - rB.left, rB.right - rA.left);
                                        float overlapY = Math.Min(rA.bottom - rB.top, rB.bottom - rA.top);

                                        if (overlapX < overlapY)
                                        {
                                            float penetrationDepth = overlapX;

                                            rB.boundObject.transform.position = rB.center + new Vector2(penetrationDepth, 0);
                                            rB.velocity.x *= -1;
                                        }
                                        else
                                        {
                                            float penetrationDepth = overlapY;

                                            rB.boundObject.transform.position = rB.center + new Vector2(0, penetrationDepth);
                                            rB.velocity.y *= -1;
                                        }
                                    }
                                    else if (rB.isStatic)
                                    {
                                        // Both bodies are static, no need for collision response in this case.
                                    }
                                }
                            }

                            // Continue the loop once the collision stuff is done
                            return;
                        }
        
                        if (bodyB is CircleBody cB)
                        {
                            // TODO: Implement AABBs vs Circle collision detection and response

                            float closestX = Math.Max(rA.left, Math.Min(cB.center.x, rA.right));
                            float closestY = Math.Max(rA.top, Math.Min(cB.center.y, rA.bottom));
                            Vector2 closestPoint = new(closestX, closestY);

                            float distanceSquared = cB.center.DistanceSquared(closestPoint);
                            bool isColliding = distanceSquared <= cB.radius * cB.radius;

                            if (isColliding)
                            {
                                Vector2 collisionNormal = cB.center - closestPoint;
                                collisionNormal.Normalize();

                                float penetrationDepth = cB.radius - MathF.Sqrt(distanceSquared);

                                if (!rA.isStatic)
                                {
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
                                else if (rA.isStatic)
                                {
                                    if (!cB.isStatic)
                                    {
                                        cB.boundObject.transform.position = cB.center + collisionNormal * penetrationDepth;
                                    }
                                    else if (cB.isStatic)
                                    {
                                        // Nothing
                                    }
                                }
                            }
                            
                            // Continue the loop once the collision stuff is done
                            return;
                        }
                    }

                    else if (bodyA is CircleBody cA)
                    {
                        if (bodyB is CircleBody cB)
                        {
                            // TODO: Implement Circle vs Circle collision detection and response

                            float distanceSquared = cA.center.DistanceSquared(cB.center);
                            float combinedRadius = cA.radius + cB.radius;

                            bool isColliding = distanceSquared <= combinedRadius * combinedRadius;

                            if (isColliding)
                            {
                                if (!cA.isStatic)
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
                                else if (cA.isStatic)
                                {
                                    if (!cB.isStatic)
                                    {
                                        Vector2 collisionNormal = cB.center - cA.center;
                                        collisionNormal.Normalize();

                                        float penetrationDepth = combinedRadius - cA.center.Distance(cB.center);

                                        cB.boundObject.transform.position = cB.center - collisionNormal * penetrationDepth;
                                    }
                                    else if (cB.isStatic)
                                    {
                                        // Empty since there's no need for Static vs Static collisions
                                    }
                                }
                            }

                            return;
                        }

                        else if (bodyB is RectangleBody rB)
                        {
                            // TODO: Implement Circle vs AABB collision detection and response

                            float closestX = Math.Max(rB.left, Math.Min(cA.center.x, rB.right));
                            float closestY = Math.Max(rB.top, Math.Min(cA.center.y, rB.bottom));
                            Vector2 closestPoint = new(closestX, closestY);

                            float distanceSquared = cA.center.DistanceSquared(closestPoint);
                            bool isColliding = distanceSquared <= cA.radius * cA.radius;

                            if (isColliding)
                            {
                                Vector2 collisionNormal = cA.center - closestPoint;
                                collisionNormal.Normalize();

                                float penetrationDepth = cA.radius - MathF.Sqrt(distanceSquared);

                                if (!rB.isStatic)
                                {
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
                                else if (rB.isStatic)
                                {
                                    if (!cA.isStatic)
                                    {
                                        cA.boundObject.transform.position = cA.center + collisionNormal * penetrationDepth;
                                    }
                                    else if (cA.isStatic)
                                    {
                                        // Nothing
                                    }
                                }
                            }

                            // Continue the loop once the collision stuff is done
                            return;
                        }
                    }
                });
            });
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
                float closestX = Math.Max(rOtherBody.left, Math.Min(cBody.center.x, rOtherBody.right));
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
                float closestX = Math.Max(rOtherBody.left, Math.Min(cBody.center.x, rOtherBody.right));
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
