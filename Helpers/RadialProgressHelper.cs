using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FitnessTracker.Helpers;

/// <summary>Builds an arc <see cref="PathGeometry"/> for circular progress (0..1).</summary>
public static class RadialProgressHelper
{
    private const double CenterX = 80;
    private const double CenterY = 80;
    private const double Radius = 76;

    /// <summary>Updates <paramref name="arcPath"/> to show a clockwise ring from 12 o'clock for <paramref name="percent"/> (0..1).</summary>
    public static void ApplyProgress(Path arcPath, double percent)
    {
        percent = Math.Clamp(percent, 0.001, 0.999);
        double sweepAngle = percent * 360.0;
        var start = PointOnCircle(CenterX, CenterY, Radius, -90);
        var end = PointOnCircle(CenterX, CenterY, Radius, -90 + sweepAngle);
        bool large = sweepAngle > 180;

        var arcSegment = new ArcSegment
        {
            Point = end,
            Size = new Size(Radius, Radius),
            SweepDirection = SweepDirection.Clockwise,
            IsLargeArc = large
        };

        var figure = new PathFigure { StartPoint = start, IsClosed = false };
        figure.Segments.Add(arcSegment);

        var geometry = new PathGeometry();
        geometry.Figures.Add(figure);
        arcPath.Data = geometry;
    }

    private static Point PointOnCircle(double cx, double cy, double r, double angleDeg)
    {
        double rad = angleDeg * Math.PI / 180.0;
        return new Point(cx + r * Math.Cos(rad), cy + r * Math.Sin(rad));
    }
}
