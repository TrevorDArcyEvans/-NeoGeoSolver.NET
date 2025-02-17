using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Excubo.Blazor.Canvas.Contexts;
using NeoGeoSolver.NET.Entities;
using NeoGeoSolver.NET.Solver;

namespace NeoGeoSolver.NET.UI.Web.Model;

public class CircleDrawer : EntityDrawer
{
  public Circle Circle { get; }
  public CentrePointDrawer Centre { get; }

  public CircleDrawer(Circle circle)
  {
    Circle = circle;
    Centre = new CentrePointDrawer(Circle.Centre);
  }

  public override IEnumerable<PointDrawer> SelectionPoints => new PointDrawer[] {Centre};
  public override object Entity => Circle;

  public override bool IsNear(System.Drawing.Point pt)
  {
    var arc = new Arc(Circle.Centre, Circle.Radius, new Param("StartAngle", 0), new Param("EndAngle", 2 * Math.PI));
    return pt.IsNear(arc);
  }

  protected override async Task DrawAsyncInternal(Batch2D batch)
  {
    await Centre.DrawAsync(batch);

    await batch.BeginPathAsync();
    await Initialise(batch);

    await batch.ArcAsync(Circle.Centre.X.Value, Circle.Centre.Y.Value, Circle.Radius.Value, 0d, 2 * Math.PI);
    await batch.StrokeAsync();
  }
}
