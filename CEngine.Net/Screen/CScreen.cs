namespace SimpleX.CEngine
{
    public static class CScreen
    {
        public static int width { get; private set; } = Console.BufferWidth;
        public static int height { get; private set; } = Console.BufferHeight;
    }
}
