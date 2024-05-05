namespace Duccsoft;

public sealed class FreecamTest : Component
{
	protected override void OnAwake()
	{
		Freecam.OnFreecamStart += f => Log.Info( $"Freecam Started: {f.GameObject.Name}" );
		Freecam.OnFreecamEnd += f => Log.Info( $"Freecam Ended: {f.GameObject.Name}" );
	}

	protected override void OnUpdate()
	{
		if ( Input.Pressed( "slot1" ) )
		{
			Freecam.Toggle();
		}
	}
}
