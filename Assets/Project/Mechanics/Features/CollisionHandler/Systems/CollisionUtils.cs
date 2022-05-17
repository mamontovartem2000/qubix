using ME.ECS;
using Project.Common.Components;
using UnityEngine;

public static class CollisionUtils
{
	public static bool CircleCircleCollision(Entity circ1, Entity circ2)
	{
		ref readonly var rad1 = ref circ1.Get<CircleRect>().Radius;
		ref readonly var rad2 = ref circ2.Get<CircleRect>().Radius;
		
		var pos1 = circ1.GetPosition();
		var pos2 = circ2.GetPosition();

		if ((pos1 - pos2).sqrMagnitude <= (rad1 * rad1) + (rad2 * rad2))
		{
			return true;
		}
		return false;
	}

	public static bool CircleSquareCollision(Entity circle, Entity square)
	{
		ref readonly var circleRadius = ref circle.Get<CircleRect>().Radius;
		ref readonly var squareWidth = ref square.Get<SquareRect>().Width;
		ref readonly var squareHeight = ref square.Get<SquareRect>().Width;
		
		var circlePosition = circle.GetPosition();
		var squarePosition = square.GetPosition();
		
		var collision = new Vector3(Mathf.Max(squarePosition.x - squareWidth, Mathf.Min(squarePosition.x + squareWidth, circlePosition.x)), 
				0, Mathf.Max(squarePosition.z - squareHeight, Mathf.Min(squarePosition.z + squareHeight, circlePosition.z)));
            
			if ((collision - circlePosition).sqrMagnitude <= circleRadius * circleRadius)
			{
				Debug.Log($"circle:{circle.ToStringNoVersion()}, square: {square.ToStringNoVersion()}");
				return true;
			}

			return false;
	}

	public static bool CircleTriangleCollision()
	{
		return true;
	}

	public static bool SquareSquareCollision()
	{
		return true;
	}
	public static bool SquareTriangleCollision()
	{
		return true;
	}

	public static bool TriangleTriangleCollision()
	{
		return true;
	}
}
