namespace Duccsoft;

public sealed partial class Freecam
{
	/// <summary>
	/// Holds whatever instance of <see cref="Freecam"/> may have been created by invoking <see cref="Toggle"/>.
	/// </summary>
	private static Freecam _conCmdInstance;

	[ConCmd("freecam", Help = "Toggle a mode where the camera can be moved and rotated freely.")]
	public static void Toggle()
	{
		var camera = Game.ActiveScene?.Camera;
		if ( !camera.IsValid() )
			return;

		// If we were already using the freecam ConCmd...
		if ( _conCmdInstance?.Active == true )
		{
			// ...toggle it off by destroying its GameObject.
			_conCmdInstance?.GameObject?.Destroy();
			_conCmdInstance = null;
			return;
		}

		DisableActiveFreecams();

		// There were no active freecams, so create one.
		var freecamGo = new GameObject( true, "Freecam" );
		_conCmdInstance = freecamGo.Components.Create<Freecam>();
		// Assume that if someone uses a ConCmd to freecam, they also want to noclip.
		// Colliding with walls is more of an official "photo mode" or "spectator mode" kind of thing.
		_conCmdInstance.UseCollision = false;
	}

	private static bool DisableActiveFreecams()
	{
		var freecams = Game.ActiveScene.GetAllComponents<Freecam>();
		if ( !freecams.Any() )
			return false;

		foreach ( var freecam in freecams )
		{
			freecam.Enabled = false;
		}
		return true;
	}
}
