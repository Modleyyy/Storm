using System.Numerics;

namespace Storm.Particles;

public struct ParticleData
{
    // The lifetime of the particles
    public double lifetime = 1;
    // Every interval seconds, count particle(s) gets spawned
    public double interval = 0.1f;
    // Every interval seconds, count particle(s) gets spawned
    public int count = 1;
    // The max number of particles that get first created before getting pooled
    public int maxParticles = 20;

    public Color colorStart = Color.White;
    public Color colorEnd = Color.White;

    public float scaleStart = 1;
    public float scaleEnd = 1;

    public float linearVelocity = 0;
    public float angularVelocity = 0;

    public float angle = 0;
    public float angleVariance = 0;

    public Bitmap? texture = null;

    public ParticleData() { }
}
