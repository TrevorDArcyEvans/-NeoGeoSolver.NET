﻿using NeoGeoSolver.NET.Entities;
using NeoGeoSolver.NET.Solver;

namespace NeoGeoSolver.NET.Constraints;

public abstract class Constraint
{
  public virtual IEnumerable<Expression> Equations
  {
    get
    {
      yield break;
    }
  }

  public abstract IEnumerable<Entity> Entities { get; }

  private enum Option
  {
    Default
  }

  protected virtual Enum optionInternal
  {
    get
    {
      return Option.Default;
    }
    set
    {
    }
  }

  public void ChooseBestOption()
  {
    var type = optionInternal.GetType();
    var names = Enum.GetNames(type);
    if (names.Length < 2)
    {
      return;
    }

    var minValue = -1.0;
    var bestOption = 0;

    for (var i = 0; i < names.Length; i++)
    {
      optionInternal = (Enum) Enum.Parse(type, names[i]);
      var exprs = Equations.ToList();

      var curValue = exprs.Sum(e => Math.Abs(e.Eval()));
      if (minValue < 0.0 || curValue < minValue)
      {
        minValue = curValue;
        bestOption = i;
      }
    }

    optionInternal = (Enum) Enum.Parse(type, names[bestOption]);
  }
}
