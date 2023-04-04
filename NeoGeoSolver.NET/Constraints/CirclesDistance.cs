﻿using NeoGeoSolver.NET.Entities;
using NeoGeoSolver.NET.Solver;

namespace NeoGeoSolver.NET.Constraints;

public class CirclesDistance : Value
{
  public enum Option
  {
    Outside,
    FirstInside,
    SecondInside,
  }

  public Option option { get; set; }

  protected override Enum optionInternal
  {
    get
    {
      return option;
    }
    set
    {
      option = (Option) value;
    }
  }

  public CirclesDistance(IEntity c0, IEntity c1)
  {
    AddEntity(c0);
    AddEntity(c1);
    value.value = 1;
    ChooseBestOption();
    Satisfy();
  }

  private Point GetCenterPoint(IEntity e)
  {
    if (e is Circle) return (e as Circle).center;
    if (e is Arc) return (e as Arc).center;
    return null;
  }

  private bool IsCentersCoincident(IEntity c0, IEntity c1)
  {
    var cp0 = GetCenterPoint(c0);
    var cp1 = GetCenterPoint(c1);
    return cp0 != null && cp1 != null && cp0.IsCoincidentWith(cp1);
  }

  public override IEnumerable<Expression> equations
  {
    get
    {
      var c0 = GetEntity(0);
      var c1 = GetEntity(1);
      var r0 = c0.Radius();
      var r1 = c1.Radius();
      if (IsCentersCoincident(c0, c1))
      {
        if (option == Option.FirstInside)
        {
          yield return r0 - r1 - value.exp;
        }
        else
        {
          yield return r1 - r0 - value.exp;
        }
      }
      else
      {
        var dist = (c0.Center() - c1.Center()).Magnitude();
        switch (option)
        {
          case Option.Outside:
            yield return (dist - r0 - r1) - value.exp;
            break;
          case Option.FirstInside:
            yield return (r1 - r0 - dist) - value.exp;
            break;
          case Option.SecondInside:
            yield return (r0 - r1 - dist) - value.exp;
            break;
        }
      }
    }
  }
}
