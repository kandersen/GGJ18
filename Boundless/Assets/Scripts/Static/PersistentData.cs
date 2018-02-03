public static class PersistentData {

	private static bool gameStarted = false;

	private static int level = 0;

	public static int Level {
		get {
			return level;
		}
		set {
			level = value;
		}
	}

	public static bool GameStarted {
		get {
			return gameStarted;
		}
		set {
			gameStarted = value;
		}
	}

}
