﻿using NeoGeoSolver.NET.Constraints;
using NeoGeoSolver.NET.Solver;

namespace NeoGeoSolver.NET.Entities;

public class EllipticArc : Entity
{
  public Point p0 = new();
  public Point p1 = new();
  public Point c = new();

  public override IEnumerable<Expression> Equations
  {
    get
    {
      if (!p0.IsCoincidentWith(p1))
      {
        yield return (p0.exp - c.exp).Magnitude() - (p1.exp - c.exp).Magnitude();
      }
    }
  }

  public Expression GetAngleExp()
  {
    if (!p0.IsCoincidentWith(p1))
    {
      var d0 = p0.exp - c.exp;
      var d1 = p1.exp - c.exp;
      return ConstraintExp.Angle2d(d0, d1, angle360: true);
    }

    return Math.PI * 2.0;
  }

  public override ExpressionVector PointOn(Expression t)
  {
    var angle = GetAngleExp();
    var cos = Expression.Cos(angle * t);
    var sin = Expression.Sin(angle * t);
    var rv = p0.exp - c.exp;

    return c.exp + new ExpressionVector(
      cos * rv.x - sin * rv.y,
      sin * rv.x + cos * rv.y,
      0.0
    );
  }

  public override ExpressionVector TangentAt(Expression t)
  {
    var angle = GetAngleExp();
    var cos = Expression.Cos(angle * t + Math.PI / 2);
    var sin = Expression.Sin(angle * t + Math.PI / 2);
    var rv = p0.exp - c.exp;

    return new ExpressionVector(
      cos * rv.x - sin * rv.y,
      sin * rv.x + cos * rv.y,
      0.0
    );
  }

  public Expression LengthExpr()
  {
    return GetAngleExp() * RadiusExpr();
  }

  public Expression RadiusExpr()
  {
    return (p0.exp - c.exp).Magnitude();
  }

  public ExpressionVector CentreExpr()
  {
    return c.exp;
  }
}
