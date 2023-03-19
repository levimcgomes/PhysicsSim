using Raylib_cs;
using PhysicsSim;
using PhysicsSimTester;
//using System.Numerics;

internal class Program
{
    private static void Main(string[] args) {
        // Set the resolution
        Visualization.Res = 800;

        // Initialize Raylib
        Raylib.InitWindow(Visualization.Res, Visualization.Res, "Physics Sim");
        Raylib.SetTraceLogLevel(TraceLogLevel.LOG_ALL);
        Raylib.SetTargetFPS(200);
        // Load a better font
        Visualization.font = Raylib.LoadFontEx("times.ttf", 256, null, 1024);

        // Physics variables, eventually will be moved to a physics world class
        bool timeSteps = false;
        Vector g = new Vector(0f, -9.81f);

        // Some test particles
        Particle obj1 = new Particle(new Vector(-.5f, 1f), new Vector(0f, 0f), g, .01f);
        Particle obj2 = new Particle(new Vector(  0f, 1f), new Vector(0f, 0f), g, 1f);
        Particle obj3 = new Particle(new Vector( .5f, 1f), new Vector(0f, 0f), g, 100f);

        // Core loop
        while(!Raylib.WindowShouldClose()) {
            // If SPACE is pressed, toggle time stepping on/off
            if(Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE)) timeSteps = !timeSteps;
            // If BACKSPACE is pressed, reset everything
            if(Raylib.IsKeyPressed(KeyboardKey.KEY_BACKSPACE)) {
                obj1 = new Particle(new Vector(-.5f, 1f), new Vector(0f, 0f), g, .01f);
                obj2 = new Particle(new Vector(0f, 1f), new Vector(0f, 0f), g, 1f);
                obj3 = new Particle(new Vector(.5f, 1f), new Vector(0f, 0f), g, 100f);
            }

            // Update the positions and velocities of all particles
            if(timeSteps) {
                obj1.Integrate(Raylib.GetFrameTime());
                obj2.Integrate(Raylib.GetFrameTime());
                obj3.Integrate(Raylib.GetFrameTime()); 
            }

            // Display the particles
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.BLACK);
            Visualization.DrawAll(obj1, obj2, obj3);
            Visualization.DrawDebugInterface(Raylib.GetFrameTime(), obj1, obj2, obj3);
            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}