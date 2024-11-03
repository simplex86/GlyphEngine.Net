using System.Data;

namespace CExample
{
    /// <summary>
    /// 
    /// </summary>
    internal static class ProcedureManager
    {
        /// <summary>
        /// 
        /// </summary>
        private static Dictionary<Type, IProcedure> catches = new Dictionary<Type, IProcedure>();
        /// <summary>
        /// 
        /// </summary>
        private static IProcedure procedure = null;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TProcedure"></typeparam>
        public static void ChangeTo<TProcedure>() where TProcedure : IProcedure, new()
        {
            procedure?.Exit();

            if (!catches.TryGetValue(typeof(TProcedure), out procedure))
            {
                procedure = new TProcedure();
                catches.Add(typeof(TProcedure), procedure);
            }

            procedure.Enter();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        internal static void Update(float dt)
        {
            procedure?.Update(dt);
        }
    }
}
