using FitnessTracker.Models;
using MaterialDesignThemes.Wpf;

namespace FitnessTracker.ViewModels;

/// <summary>One health metric tile (icon kind + accent colors for MVVM binding).</summary>
public sealed class HealthMetricItemVm
{
    public required string Name { get; init; }
    public required string Value { get; init; }
    public PackIconKind IconKind { get; init; }
    public required string IconColorHex { get; init; }
    public required string ValueColorHex { get; init; }
    public required string TileBackgroundHex { get; init; }

    public static HealthMetricItemVm FromModel(HealthMetricModel m)
    {
        var (kind, iconHex, valHex) = m.Name switch
        {
            "Heart Rate" => (PackIconKind.Heart, "#E53935", "#E53935"),
            "Blood Sugar" => (PackIconKind.WaterDrop, "#1E88E5", "#1E88E5"),
            "Blood Pressure" => (PackIconKind.HeartPulse, "#E53935", "#1E88E5"),
            "Water" => (PackIconKind.CupWater, "#1E88E5", "#1E88E5"),
            _ => (PackIconKind.ChartBellCurve, "#00A693", "#212121")
        };

        var bg = string.IsNullOrWhiteSpace(m.TileBg) ? "#F8FAFC" : m.TileBg;
        return new HealthMetricItemVm
        {
            Name = m.Name,
            Value = m.Value,
            IconKind = kind,
            IconColorHex = iconHex,
            ValueColorHex = valHex,
            TileBackgroundHex = bg
        };
    }
}
