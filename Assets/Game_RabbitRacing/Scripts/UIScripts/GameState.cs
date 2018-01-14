public abstract class GameState {

	protected GameManager m_gm;
	public GameState(GameManager gm) {
		m_gm = gm;
	}

	public abstract void Enter(); 
	public abstract void Execute();
	public abstract void Exit();

}

