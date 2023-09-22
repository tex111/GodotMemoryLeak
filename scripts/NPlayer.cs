using Godot;
using System;

[Tool]
public partial class NPlayer : Node2D
{
	public Vector2I Tile;
	private bool _isBusy;
	
	public void Setup(Vector2I startPosition)
	{
		GlobalPosition = startPosition;
		Tile = Game.Level.GetTileFromGlobal(startPosition);
		Game.Level.Tiles[Tile].Character = this;
	}

	public override void _PhysicsProcess(double delta)
	{
		if (Engine.IsEditorHint()) return;
		if (_isBusy) _isBusy = false;
	}

	public override void _Input(InputEvent @event)
	{
		if (Engine.IsEditorHint() || _isBusy) return;
		if (Input.IsActionPressed("MoveLeft")) TryMove(Vector2I.Left);
		else if (Input.IsActionPressed("MoveRight")) TryMove(Vector2I.Right);
		else if (Input.IsActionPressed("MoveUp")) TryMove(Vector2I.Up);
		else if (Input.IsActionPressed("MoveDown")) TryMove(Vector2I.Down);
	}

	private void TryMove(Vector2I direction)
	{
		_isBusy = true;
		var target = Tile + direction;
		if (!Game.Level.Tiles.ContainsKey(target)) return;
		Move(target);
	}

	private void Move(Vector2I targetTile)
	{
		Game.Level.Tiles[Tile].Character = null;
		Game.Level.Tiles[targetTile].Character = this;
		GlobalPosition = Game.Level.GetGlobalFromTile(targetTile);
		Tile = targetTile;
	}
}
