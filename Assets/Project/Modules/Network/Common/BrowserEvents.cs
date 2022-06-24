using System.Runtime.InteropServices;

namespace Project.Modules.Network
{
	public static class BrowserEvents
	{
		[DllImport("__Internal")]
		public static extern void ReadyToStart();

		[DllImport("__Internal")]
		public static extern void GameCancelled();

		[DllImport("__Internal")]
		public static extern void GameIsOver();

		[DllImport("__Internal")]
		public static extern void GameError();

		[DllImport("__Internal")]
		public static extern void WorldDestroyed();
	}
}