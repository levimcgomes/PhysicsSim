using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsSim
{
    public class Particle
    {
        /// <summary>
        /// Universal damping applied to a moving particle. 0 means infinite damping, 1 means no damping.
        /// A value close to 1, but smaller, should always be used to prevent accuracy errors.
        /// Additionaly, a smaller value can be used as a cheap and easy way of simulating drag.
        /// </summary>
        public const float Damping = 0.995f;

        /// <summary>
        /// The position of the particle.
        /// </summary>
        public Vector Position { get; private set; }
        /// <summary>
        /// The velocity of the particle.
        /// </summary>
        public Vector Velocity { get; private set; }
        /// <summary>
        /// The acceleration of the particle.
        /// </summary>
        public Vector Acceleration { get; private set; }
        /// <summary>
        /// The inverse of the mass of the particle.
        /// </summary>
        public float InverseMass { get; private set; }
        /// <summary>
        /// Property for setting the mass in a more conventional way.
        /// </summary>
        public float Mass { get => 1 / InverseMass; set => InverseMass = 1/value; }
        /// <summary>
        /// Kinetic energy of the particle. Used for debugging purposes.
        /// </summary>
        public float Energy { get => .5f * Mass * Velocity.SquareMagnitude; }

        /// <summary>
        /// Creates a new particle with the given properties.
        /// </summary>
        public Particle(Vector position, Vector velocity, Vector acceleration, float mass) {
            Position = position;
            Velocity = velocity;
            Acceleration = acceleration;
            Mass = mass;
        }

        /// <summary>
        /// Updates the particles position and velocity according to it's acceleration.
        /// </summary>
        /// <param name="dt">Delta Time. Time elapsed since the function was last called.</param>
        public void Integrate(float dt) {
            Position += Velocity * dt + .5f * Acceleration * dt * dt;
            Velocity *= MathF.Pow(Damping, dt);
            Velocity += Acceleration * dt;
        }
    }
}
