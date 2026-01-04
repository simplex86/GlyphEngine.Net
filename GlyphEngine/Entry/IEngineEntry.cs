using System;
using System.Threading.Tasks;

namespace GlyphEngine
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEngineEntry
    {
        /// <summary>
        /// 
        /// </summary>
        Task Start();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        void Update(float dt);
        /// <summary>
        /// 
        /// </summary>
        void Exit();
    }

    /// <summary>
    /// 
    /// </summary>
    public class CEngineEntryAttribute : Attribute
    {

    }
}
