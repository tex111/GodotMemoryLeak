using Godot;
using System;

public partial class NWorld : Node2D
{
	[Export] private PackedScene _levelScene;
	
	private bool _firstUpdate = true;
	
	public override void _Ready()
	{
		Game.World = this;
	}
	
	public override void _Process(double delta)
	{
		if (_firstUpdate)
		{
			_firstUpdate = false;

			var instance = _levelScene.Instantiate<NLevel>();
			AddChild(instance);
			instance.Setup(new(96, 360));
		}
	}
}
