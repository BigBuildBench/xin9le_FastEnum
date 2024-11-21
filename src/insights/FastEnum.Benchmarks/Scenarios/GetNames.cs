﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using BenchmarkDotNet.Attributes;
using EnumsNET;
using FastEnumUtility.Benchmarks.Models;
using _FastEnum = FastEnumUtility.FastEnum;

namespace FastEnumUtility.Benchmarks.Scenarios;



public class GetNames
{
    [GlobalSetup]
    public void Setup()
    {
        _ = Enum.GetNames<Fruits>();
        _ = Enums.GetNames<Fruits>();
        _ = _FastEnum.GetNames<Fruits>();
    }


    [Benchmark(Baseline = true)]
    public IReadOnlyList<string> NetCore()
        => Enum.GetNames<Fruits>();


    [Benchmark]
    public IReadOnlyList<string> EnumsNet()
        => Enums.GetNames<Fruits>();


    [Benchmark]
    public string[] NetEscapades()
        => FruitsExtensions.GetNames();


    [Benchmark]
    public ImmutableArray<string> FastEnum()
        => _FastEnum.GetNames<Fruits>();
}
