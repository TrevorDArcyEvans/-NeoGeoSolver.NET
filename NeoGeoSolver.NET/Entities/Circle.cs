﻿using System.Numerics;
using NeoGeoSolver.NET.Solver;

namespace NeoGeoSolver.NET.Entities;

public class Circle : Entity {

	public Point c;
	public Param radius = new("r");

	public override IEntityType type { get { return IEntityType.Circle; } }

	public Circle()
	{
		c = AddChild(new Point());
	}

	public override IEnumerable<Point> points {
		get {
			yield return c;
		}
	}

	public override IEnumerable<Param> parameters {
		get {
			yield return radius;
		}
	}

	public Point center { get { return c; } }

	public override ExpressionVector PointOn(Expression t) {
		var angle = t * 2.0 * Math.PI;
		return c.exp + new ExpressionVector(Expression.Cos(angle), Expression.Sin(angle), 0.0) * Radius();
	}

	public override ExpressionVector TangentAt(Expression t) {
		var angle = t * 2.0 * Math.PI;
		return new ExpressionVector(-Expression.Sin(angle), Expression.Cos(angle), 0.0);
	}

	public override Expression Length() {
		return new Expression(2.0) * Math.PI * Radius();
	}

	public override Expression Radius() {
		return Expression.Abs(radius);
	}

	public override ExpressionVector Center() {
		return center.exp;
	}
}
