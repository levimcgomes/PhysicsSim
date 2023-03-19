using PhysicsSim;
using Raylib_cs;
using System.Data;

namespace PhysicsSimTester
{
    public static class Visualization
    {
        /// <summary>
        /// The window resolution, only one number because the window is always square
        /// </summary>
        public static int Res;
        public static int ScreenUnit { get => Res / 100; }
        public static Font font;

        // Variables used for the debug interface
        private static float totalEnergy;
        private const int deltaTimesMax = 64;
        private static int deltaTimesCurr = 0;
        private static float deltaTimesSum = 0;
        private static float[] deltaTimes = new float[deltaTimesMax];

        /// <summary>
        /// Utility function to convert a Vector from screen space to world space.
        /// </summary>
        /// <param name="vector">A Vector in screen space.</param>
        /// <returns>A Vector in world space.</returns>
        public static Vector ScreenToWorld(this Vector vector) {
            vector = (vector / Res * 2) - Vector.One;
            return new Vector(vector.X, -vector.Y);
        }
        /// <summary>
        /// Utility function to convert a Vector from world space to screen space.
        /// </summary>
        /// <param name="vector">A Vector in world space.</param>
        /// <returns>A Vector in screen space.</returns>
        public static Vector WorldToScreen(this Vector vector) {
            vector = new Vector(vector.X, -vector.Y);
            return (vector + Vector.One) / 2 * Res;
        }

        /// <summary>
        /// Draw a collection of particles.
        /// </summary>
        /// <param name="objects">Particles to be drawn.</param>
        public static void DrawAll(params Particle[] objects) {
            foreach (var obj in objects) {
                Draw(obj);
            }
        }
        /// <summary>
        /// Draw a single particle to the window.
        /// </summary>
        /// <param name="obj">The particle to be drawn.</param>
        public static void Draw(Particle obj) {
            Vector screenPos = obj.Position.WorldToScreen();
            Raylib.DrawCircle((int)screenPos.X, (int)screenPos.Y, 10, new Color(255, 255, 255, 255));
        }

        /// <summary>
        /// Draw a text overlay for debugging purposes.
        /// </summary>
        /// <param name="dt">The delta time.</param>
        /// <param name="objects">All the particles being processed.</param>
        public static void DrawDebugInterface(float dt, params Particle[] objects) {
            float avg = ProcessDeltaTime(dt);
            DrawDebugLine($"{objects.Length} objects @ {(1 / avg).ToString("F0")} FPS", 2, 2);
            DrawDebugLine($"{(dt*1000).ToString("F4")}Δt (in ms)", 2, 4);
            float energy = 0f;
            foreach(Particle particle in objects) energy += particle.Energy;
            DrawDebugLine($"E={energy}", 2, 6);
            DrawDebugLine($"ΔE={energy-totalEnergy}", 2, 8);
            totalEnergy = energy;

        }
        // Calculate the average of the last deltaTimesMax delta times.
        private static float ProcessDeltaTime(float dt) {
            deltaTimesSum -= deltaTimes[deltaTimesCurr];
            deltaTimesSum += dt;
            deltaTimes[deltaTimesCurr] = dt;
            deltaTimesCurr = (deltaTimesCurr + 1) % deltaTimesMax;
            return deltaTimesSum / deltaTimesMax;
        }
        // Draw a text line
        private static void DrawDebugLine(string line, float x, float y) {
            Raylib.DrawTextEx(font, line, new System.Numerics.Vector2(x * ScreenUnit, y * ScreenUnit), (int)(2.6 * ScreenUnit), (int)(ScreenUnit / 3), Color.RAYWHITE);
        }
        private static void DrawDebugLine(string line, float x, float y, Color color) {
            Raylib.DrawTextEx(font, line, new System.Numerics.Vector2(x * ScreenUnit, y * ScreenUnit), (int)(2.6 * ScreenUnit), (int)(ScreenUnit / 3), color);
        }
    }
}
