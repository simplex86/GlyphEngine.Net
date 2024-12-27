namespace CEngine
{
    /// <summary>
    /// 
    /// </summary>
    public interface IApplication
    {
        /// <summary>
        /// 
        /// </summary>
        void Start();
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
    public class ApplicationEntryAttribute : Attribute
    {

    }
}
