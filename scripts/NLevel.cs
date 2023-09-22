using Godot;
using System;
using System.Collections.Generic;

public partial class NLevel : TileMap
{
	[Export] private NPlayer _player;

	public readonly Dictionary<Vector2I, Tile> Tiles = new();
	private Rect2I _bounds;
	private Vector2I _offset;

	public void Setup(Vector2I startPosition)
	{
		Game.Level = this;
		_bounds = GetUsedRect();
		_offset = _bounds.Position;
		
		foreach (var c in GetUsedCells(0)) Tiles.Add(c, new(c));
		foreach (var c in GetUsedCells(1)) Tiles.Remove(c);

		_player.Setup(startPosition);
	}

	public Vector2I GetTileFromGlobal(Vector2 globalPosition) =>
		LocalToMap(ToLocal(globalPosition));
	
	public Vector2 GetGlobalFromTile(Vector2I tile) =>
		ToGlobal(MapToLocal(tile)) + new Vector2(-12, -12);

	public int GetDistance(Vector2I startTile, Vector2I endTile) =>
		Math.Abs(endTile.X - startTile.X) + Math.Abs(endTile.Y - startTile.Y);
}

public class Tile
{
	public Vector2I Position;
	public Node2D Character;

	public Tile(Vector2I position)
	{
		Position = position;
	}
}