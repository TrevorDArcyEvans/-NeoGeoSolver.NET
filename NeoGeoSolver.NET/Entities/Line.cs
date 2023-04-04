﻿using System.Numerics;
using NeoGeoSolver.NET.Solver;

namespace NeoGeoSolver.NET.Entities;

public class Line : Entity {
	public Point p0;
	public Point p1;

	public Line()
	{
		p0 = new Point();
		p1 = new Point();
	}

	public override EntityType type { get { return EntityType.Line; } }

	public override ExpressionVector PointOn(Expression t) {
		var pt0 = p0.exp;
		var pt1 = p1.exp;
		return pt0 + (pt1 - pt0) * t;
	}

	public override ExpressionVector TangentAt(Expression t) {
		return p1.exp - p0.exp;
	}

	public override Expression Length() {
		return (p1.exp - p0.exp).Magnitude();
	}

	public override Expression Radius() {
		return null;
	}
}
