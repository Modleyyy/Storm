namespace Storm.Particles;

using Components;

public class ParticleSystem : Component
{
    public new bool isActive = true;
    private readonly List<Particle> particles = new();
    private readonly Queue<Particle> particlePool = new();
    private readonly ParticleData data;

    private double currentInterval = 0;
    public Vector2 offset;

    public ParticleSystem(ParticleData data, Vector2? offset = null)
    {
        this.data = data;
        this.offset = offset ?? Vector2.Zero;

        for (int i = 0; i < data.maxParticles; i++)
        {
            particlePool.Enqueue(new Particle(data, Vector2.Zero));
        }
    }

    private Particle GetOrCreateParticle(Vector2 position)
    {
        if (particlePool.Count > 0)
        {
            Particle pooledParticle = particlePool.Dequeue();
            pooledParticle.Reset(position);
            return pooledParticle;
        }
        else
        {
            return new(data, position);
        }
    }

    public override void OnUpdate(double deltaTime)
    {
        currentInterval += deltaTime;

        if (currentInterval >= data.interval)
        {
            currentInterval = 0;
            Vector2 center = boundObject.transform.position + offset;
            for(short i = 0; i < data.count; i++)
            {
                Particle p = GetOrCreateParticle(center);
                particles.Add(p);
            }
        }

        particles.RemoveAll(p =>
        {
            if (p.isDead && particlePool.Count < data.maxParticles)
            {
                particlePool.Enqueue(p);
            }

            return p.isDead;
        });

        for(short i = 0; i < particles.Count; i++)
        {
            Particle p = particles[i];
            p.Update(deltaTime);
        }
    }

    public void Draw(Graphics graphics)
    {
        foreach (Particle p in particles)
        {
            p.Draw(graphics);
        }
    }
}
