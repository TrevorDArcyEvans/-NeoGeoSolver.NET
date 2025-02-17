using NeoGeoSolver.NET.Solver;

namespace NeoGeoSolver.NET.Constraints;

public abstract class Value : Constraint
{
  protected readonly Param value = new("value");

  public double GetValue()
  {
    return value.Value;
  }

  public void SetValue(double v)
  {
    value.Value = v;
  }

  public Param GetValueParam()
  {
    return value;
  }

  protected virtual bool OnSatisfy()
  {
    var sys = new EquationSystem
    {
      revertWhenNotConverged = false
    };
    sys.AddParameter(value);
    sys.AddEquations(Equations);
    return sys.Solve() == EquationSystem.SolveResult.Okay;
  }

  public bool Satisfy()
  {
    var result = OnSatisfy();

    return result;
  }
}
