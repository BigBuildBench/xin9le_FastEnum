﻿using System;
using BenchmarkDotNet.Attributes;
using EnumsNET;
using FastEnumUtility.Benchmarks.Models;
using _FastEnum = FastEnumUtility.FastEnum;

namespace FastEnumUtility.Benchmarks.Scenarios;



public class GetName
{
    private const Fruits Value = Fruits.Pineapple;


    [GlobalSetup]
    public void Setup()
    {
        _ = Enum.GetNames<Fruits>();
        _ = Enums.GetNames<Fruits>();
        _ = _FastEnum.GetNames<Fruits>();
    }


    [Benchmark(Baseline = true)]
    public string? NetCore()
        => Enum.GetName(Value);


    [Benchmark]
    public string? EnumsNet()
        => Enums.GetName(Value);


    [Benchmark]
    public string? FastEnum()
        => _FastEnum.GetName<Fruits, FruitsBooster>(Value);
}
