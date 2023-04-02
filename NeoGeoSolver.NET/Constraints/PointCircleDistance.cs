﻿using NeoGeoSolver.NET.Entities;
using NeoGeoSolver.NET.Solver;

namespace NeoGeoSolver.NET.Constraints;

public class PointCircleDistance : Value {

	public PointCircleDistance(Sketch.Sketch sk) : base(sk) { }

	public PointCircleDistance(Sketch.Sketch sk, IEntity pt, IEntity c) : base(sk) {
		AddEntity(pt);
		AddEntity(c);
		Satisfy();
	}

	public override IEnumerable<Expression> equations {
		get {
			var point = GetEntity(0);
			var circle = GetEntity(1);
			var pPos = point.GetPointAtInPlane(0, getPlane());
			var cCen = circle.Center();
			var cRad = circle.Radius();

			yield return (pPos - cCen).Magnitude() - cRad - value.exp;
		}
	}
}
