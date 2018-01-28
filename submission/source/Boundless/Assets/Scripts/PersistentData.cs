public static class PersistentData {

	private static bool gameStarted = false;

	public static bool GameStarted {
		get {
			return gameStarted;
		}
		set {
			gameStarted = value;
		}
	}

}
