namespace GlyphEngine
{
    public sealed partial class CSkin
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameobject"></param>
        /// <param name="skinname"></param>
        public static void Apply(CGameObject gameobject, string skinname)
        {
            if (gameobject is ISkinable skinable)
            {
                skinable.ApplySkin(skinname);
            }
        }
    }
}
