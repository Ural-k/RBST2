using UnityEngine;

[CreateAssetMenu(fileName = "ParticleCollection", menuName = "ScriptableObjects/Player/ParticleCollection")]
public class ParticleCollection : ScriptableObject
{
    [SerializeField] private ParticleSystem circle_;
    [SerializeField] private ParticleSystem debuff_;
    [SerializeField] private ParticleSystem squareBlue_;

    public static ParticleSystem GetParticle(SkillShape shape, SkillData data)
    {
        ParticleCollection particles = Resources.Load<ParticleCollection>("Particle/ParticleCollection");
        ParticleSystem particle = shape switch
        {
            SkillShape.Single => data.particle_,
            SkillShape.Circle => particles.circle_,
            SkillShape.Square => particles.squareBlue_,
            _                   => particles.circle_,
        };
        return particle;
    }
}
