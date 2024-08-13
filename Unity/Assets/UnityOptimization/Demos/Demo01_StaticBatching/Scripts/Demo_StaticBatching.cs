using RMC.Optimizations.Shared;

namespace RMC.Optimizations.Demos.Demo_StaticBatching
{
    /// <summary>
    /// Demo: Static Batching
    ///
    /// Docs: https://docs.unity3d.com/Manual/static-batching.html
    ///
    /// About: Static batching is a draw call batching method that combines meshes that
    /// don’t move to reduce draw calls. It transforms the combined meshes into world
    /// space and builds one shared vertex and index buffer for them. Then Unity performs
    /// a single draw call that uses this combined mesh to draw all objects in the batch
    /// at once. Static batching can significantly reduce the number of draw calls.
    /// 
    /// Static batching is more efficient than dynamic batching
    /// because static batching doesn’t transform vertices on the CPU. For more
    /// information about the performance implications for static batching,
    /// see Performance implications.
    ///
    /// </summary>
    public class Demo_StaticBatching : BaseDemo
    {
        //  Events ----------------------------------------


        //  Properties ------------------------------------


        //  Fields ----------------------------------------


        //  Unity Methods ---------------------------------
        protected override void Awake()
        {
            base.Awake();
            logDemoInstructions ($"Welcome to {this.GetType().Name}"!);
        }

        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {
            base.Update();
        }


        //  Methods ---------------------------------------


        //  Event Handlers --------------------------------
    }
}