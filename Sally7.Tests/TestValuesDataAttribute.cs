using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Xunit.Sdk;

namespace Sally7.Tests;

internal sealed class TestValuesDataAttribute : DataAttribute
{
    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        var paramType = testMethod.GetParameters()[0].ParameterType;
        var dataProperty = typeof(TestValues).GetProperty($"{paramType.Name}Data") ??
            throw new ArgumentException($"No data available for type {paramType}");

        var data = dataProperty.GetValue(null) as IEnumerable ??
            throw new NotSupportedException($"Data from {dataProperty} could not be converted to {nameof(IEnumerable)}.");

        foreach (var value in data) yield return [value];
    }
}