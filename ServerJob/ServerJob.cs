
namespace Popup.ServerJob
{
    public static class ServerJob
    {
        private static int UID = 0;

        public static int RequestNewUID => UID++;
    }
}
