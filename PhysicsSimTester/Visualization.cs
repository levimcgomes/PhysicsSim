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
        /// <summary>
        /// Units used for sizing and positioning of text.
        /// </summary>
        public static int TextUnits { get => Res / 100; }
        public static Font font;

        // Variables used for the debug interface
        private struct SmoothDebugFloat {
            private const int cacheMax = 64;
            private int currentIndex;
            private float[] cachedValues;
            private float sum;
            public float AverageValue { get => sum / cacheMax; }

            public SmoothDebugFloat() {
                currentIndex = 0;
                cachedValues = new float[cacheMax];
                sum = 0;
            }
            public void FeedValue(float value) {
                sum -= cachedValues[currentIndex];
                sum += value;
                cachedValues[currentIndex] = value;
                currentIndex = (currentIndex + 1) % cacheMax;
            }
        }
        private static SmoothDebugFloat totalEnergy;
        private static SmoothDebugFloat deltaTime;

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
        /// Initializes the visualization
        /// </summary>
        /// <param name="res">Resolution of the window</param>
        public static void Init(int res) {
            // Set the resolution
            Res = res;

            // Initialize Raylib
            Raylib.InitWindow(Res, Res, "Physics Sim");
            Raylib.SetTraceLogLevel(TraceLogLevel.LOG_ALL);
            Raylib.SetTargetFPS(200);
            // Load a better font
            font = Raylib.LoadFontEx("times.ttf", 256, null, 1024);

            totalEnergy = new SmoothDebugFloat();
            deltaTime = new SmoothDebugFloat();
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
            // Sum the energy of every object and feed it to the corresponding smooth value
            float frameEnergy = 0f;
            foreach(Particle particle in objects) frameEnergy += particle.Energy;
            totalEnergy.FeedValue(frameEnergy);

            // Feed the delta time to the corresponding smooth value
            deltaTime.FeedValue(dt);

            // Write info to the screen
            DrawDebugLine($"{objects.Length} objects @ {(1 / deltaTime.AverageValue).ToString("F0")} FPS", 2, 2);
            DrawDebugLine($"{(dt*1000).ToString("F4")}Δt (in ms)", 2, 4);
            DrawDebugLine($"E={totalEnergy.AverageValue.ToString("F3")}", 2, 6);

        }
        // Draw a text line
        private static void DrawDebugLine(string line, float x, float y) {
            Raylib.DrawTextEx(font, line, new System.Numerics.Vector2(x * TextUnits, y * TextUnits), (int)(2.6 * TextUnits), (int)(TextUnits / 3), Color.RAYWHITE);
        }
        private static void DrawDebugLine(string line, float x, float y, Color color) {
            Raylib.DrawTextEx(font, line, new System.Numerics.Vector2(x * TextUnits, y * TextUnits), (int)(2.6 * TextUnits), (int)(TextUnits / 3), color);
        }
    }
}
