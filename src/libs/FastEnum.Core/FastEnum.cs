﻿using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using FastEnumUtility.Internals;

namespace FastEnumUtility;



/// <summary>
/// Provides high performance utilities for <see cref="Enum"/> type.
/// </summary>
public static partial class FastEnum
{
    #region Type
    /// <summary>
    /// Returns the underlying type of the specified enumeration.
    /// </summary>
    /// <typeparam name="T"><see cref="Enum"/> type</typeparam>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Type GetUnderlyingType<T>()
        where T : struct, Enum
        => EnumInfo<T>.s_underlyingType;
    #endregion


    #region Member
    /// <summary>
    /// Retrieves an array of the values of the constants in a specified enumeration.
    /// </summary>
    /// <typeparam name="T"><see cref="Enum"/> type</typeparam>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ImmutableArray<T> GetValues<T>()
        where T : struct, Enum
        => ImmutableCollectionsMarshal.AsImmutableArray(EnumInfo<T>.s_values);


    /// <summary>
    /// Retrieves an array of the names of the constants in a specified enumeration.
    /// </summary>
    /// <typeparam name="T"><see cref="Enum"/> type</typeparam>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ImmutableArray<string> GetNames<T>()
        where T : struct, Enum
        => ImmutableCollectionsMarshal.AsImmutableArray(EnumInfo<T>.s_names);


    /// <summary>
    /// Retrieves the name of the constant in the specified enumeration type that has the specified value.
    /// </summary>
    /// <typeparam name="T"><see cref="Enum"/> type</typeparam>
    /// <param name="value"></param>
    /// <returns>A string containing the name of the enumerated constant in enumType whose value is value; or null if no such constant is found.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? GetName<T>(T value)
        where T : struct, Enum
        => UnderlyingOperation<T>.GetName(value);


    /// <summary>
    /// Retrieves an array of the member information of the constants in a specified enumeration.
    /// </summary>
    /// <typeparam name="T"><see cref="Enum"/> type</typeparam>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ImmutableArray<Member<T>> GetMembers<T>()
        where T : struct, Enum
        => ImmutableCollectionsMarshal.AsImmutableArray(EnumInfo<T>.s_members);


    /// <summary>
    /// Retrieves the member information of the constants in a specified enumeration.
    /// </summary>
    /// <typeparam name="T"><see cref="Enum"/> type</typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Member<T>? GetMember<T>(T value)
        where T : struct, Enum
        => UnderlyingOperation<T>.TryGetMember(value, out var member) ? member : null;
    #endregion


    #region Min / Max
    /// <summary>
    /// Returns the minimum value.
    /// </summary>
    /// <typeparam name="T"><see cref="Enum"/> type</typeparam>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T? GetMinValue<T>()
        where T : struct, Enum
        => EnumInfo<T>.s_isEmpty ? null : EnumInfo<T>.s_minValue;


    /// <summary>
    /// Returns the maximum value.
    /// </summary>
    /// <typeparam name="T"><see cref="Enum"/> type</typeparam>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T? GetMaxValue<T>()
        where T : struct, Enum
        => EnumInfo<T>.s_isEmpty ? null : EnumInfo<T>.s_maxValue;
    #endregion


    #region Condition
    /// <summary>
    /// Returns whether the values of the constants in a specified enumeration are continuous.
    /// </summary>
    /// <typeparam name="T"><see cref="Enum"/> type</typeparam>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsContinuous<T>()
        where T : struct, Enum
        => EnumInfo<T>.s_isContinuous;


    /// <summary>
    /// Returns an indication whether a constant with a specified value exists in a specified enumeration.
    /// </summary>
    /// <param name="value"></param>
    /// <typeparam name="T"><see cref="Enum"/> type</typeparam>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsDefined<T>(T value)
        where T : struct, Enum
        => UnderlyingOperation<T>.IsDefined(value);


    /// <summary>
    /// Returns an indication whether a constant with a specified name exists in a specified enumeration.
    /// </summary>
    /// <param name="name"></param>
    /// <typeparam name="T"><see cref="Enum"/> type</typeparam>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsDefined<T>(ReadOnlySpan<char> name)
        where T : struct, Enum
        => EnumInfo<T>.s_memberByNameCaseSensitive.ContainsKey(name);


    /// <summary>
    /// Returns whether no fields in a specified enumeration.
    /// </summary>
    /// <typeparam name="T"><see cref="Enum"/> type</typeparam>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEmpty<T>()
        where T : struct, Enum
        => EnumInfo<T>.s_isEmpty;


    /// <summary>
    /// Returns whether the <see cref="FlagsAttribute"/> is defined.
    /// </summary>
    /// <typeparam name="T"><see cref="Enum"/> type</typeparam>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsFlags<T>()
        where T : struct, Enum
        => EnumInfo<T>.s_isFlags;
    #endregion


    #region Parse
    /// <summary>
    /// Converts the string representation of the name or numeric value of enumerated constant to an equivalent enumerated object.
    /// </summary>
    /// <param name="value"></param>
    /// <typeparam name="T"><see cref="Enum"/> type</typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Parse<T>(ReadOnlySpan<char> value)
        where T : struct, Enum
        => Parse<T>(value, false);


    /// <summary>
    /// Converts the string representation of the name or numeric value of enumerated constant to an equivalent enumerated object.
    /// A parameter specifies whether the operation is case-insensitive.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="ignoreCase"></param>
    /// <typeparam name="T"><see cref="Enum"/> type</typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Parse<T>(ReadOnlySpan<char> value, bool ignoreCase)
        where T : struct, Enum
    {
        if (!TryParse<T>(value, ignoreCase, out var result))
            ThrowHelper.ThrowValueNotDefined(value, nameof(value));
        return result;
    }


    /// <summary>
    /// Converts the string representation of the name or numeric value of enumerated constant to an equivalent enumerated object.
    /// The return value indicates whether the conversion succeeded.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="result"></param>
    /// <typeparam name="T"><see cref="Enum"/> type</typeparam>
    /// <returns>true if the value parameter was converted successfully; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryParse<T>(ReadOnlySpan<char> value, out T result)
        where T : struct, Enum
        => TryParse(value, false, out result);


    /// <summary>
    /// Converts the string representation of the name or numeric value of enumerated constant to an equivalent enumerated object.
    /// A parameter specifies whether the operation is case-sensitive.
    /// The return value indicates whether the conversion succeeded.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="ignoreCase"></param>
    /// <param name="result"></param>
    /// <typeparam name="T"><see cref="Enum"/> type</typeparam>
    /// <returns>true if the value parameter was converted successfully; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryParse<T>(ReadOnlySpan<char> value, bool ignoreCase, out T result)
        where T : struct, Enum
    {
        if (value.IsEmpty)
        {
            result = default;
            return false;
        }

        if (StringHelper.IsNumeric(value.At(0)))
            return UnderlyingOperation<T>.TryParseValue(value, out result);

        if (ignoreCase)
            return tryParseNameCaseInsensitive(value, out result);

        return tryParseNameCaseSensitive(value, out result);


        #region Local Functions
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static bool tryParseNameCaseSensitive(ReadOnlySpan<char> name, out T result)
        {
            if (EnumInfo<T>.s_memberByNameCaseSensitive.TryGetValue(name, out var member))
            {
                result = member.Value;
                return true;
            }
            result = default;
            return false;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static bool tryParseNameCaseInsensitive(ReadOnlySpan<char> name, out T result)
        {
            if (EnumInfo<T>.s_memberByNameCaseInsensitive.TryGetValue(name, out var member))
            {
                result = member.Value;
                return true;
            }
            result = default;
            return false;
        }
        #endregion
    }
    #endregion


    #region ToString
    /// <summary>
    /// Converts the specified value to its equivalent string representation.
    /// </summary>
    /// <typeparam name="T"><see cref="Enum"/> type</typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToString<T>(T value)
        where T : struct, Enum
        => UnderlyingOperation<T>.GetName(value) ?? UnderlyingOperation<T>.ToNumberString(value);
    #endregion
}



/// <inheritdoc/>
partial class FastEnum
{
    // for IFastEnumBooster<T>

    #region Member
    /// <summary>
    /// Retrieves the name of the constant in the specified enumeration type that has the specified value.
    /// </summary>
    /// <typeparam name="TEnum"><see cref="Enum"/> type</typeparam>
    /// <typeparam name="TBooster">Custom implementation</typeparam>
    /// <param name="value"></param>
    /// <returns>A string containing the name of the enumerated constant in enumType whose value is value; or null if no such constant is found.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? GetName<TEnum, TBooster>(TEnum value)
        where TEnum : struct, Enum
        where TBooster : IFastEnumBooster<TEnum>
        => TBooster.GetName(value);
    #endregion


    #region IsDefined
    /// <summary>
    /// Returns an indication whether a constant with a specified value exists in a specified enumeration.
    /// </summary>
    /// <param name="value"></param>
    /// <typeparam name="TEnum"><see cref="Enum"/> type</typeparam>
    /// <typeparam name="TBooster">Custom implementation</typeparam>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsDefined<TEnum, TBooster>(TEnum value)
        where TEnum : struct, Enum
        where TBooster : IFastEnumBooster<TEnum>
        => TBooster.IsDefined(value);


    /// <summary>
    /// Returns an indication whether a constant with a specified name exists in a specified enumeration.
    /// </summary>
    /// <param name="name"></param>
    /// <typeparam name="TEnum"><see cref="Enum"/> type</typeparam>
    /// <typeparam name="TBooster">Custom implementation</typeparam>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsDefined<TEnum, TBooster>(ReadOnlySpan<char> name)
        where TEnum : struct, Enum
        where TBooster : IFastEnumBooster<TEnum>
        => TBooster.IsDefined(name);
    #endregion


    #region Parse
    /// <summary>
    /// Converts the string representation of the name or numeric value of enumerated constant to an equivalent enumerated object.
    /// </summary>
    /// <typeparam name="TEnum"><see cref="Enum"/> type</typeparam>
    /// <typeparam name="TBooster">Custom implementation</typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TEnum Parse<TEnum, TBooster>(ReadOnlySpan<char> value)
        where TEnum : struct, Enum
        where TBooster : IFastEnumBooster<TEnum>
        => Parse<TEnum, TBooster>(value, false);


    /// <summary>
    /// Converts the string representation of the name or numeric value of enumerated constant to an equivalent enumerated object.
    /// A parameter specifies whether the operation is case-insensitive.
    /// </summary>
    /// <typeparam name="TEnum"><see cref="Enum"/> type</typeparam>
    /// <typeparam name="TBooster">Custom implementation</typeparam>
    /// <param name="value"></param>
    /// <param name="ignoreCase"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TEnum Parse<TEnum, TBooster>(ReadOnlySpan<char> value, bool ignoreCase)
        where TEnum : struct, Enum
        where TBooster : IFastEnumBooster<TEnum>
    {
        if (!TryParse<TEnum, TBooster>(value, ignoreCase, out var result))
            ThrowHelper.ThrowValueNotDefined(value, nameof(value));
        return result;
    }


    /// <summary>
    /// Converts the string representation of the name or numeric value of enumerated constant to an equivalent enumerated object.
    /// The return value indicates whether the conversion succeeded.
    /// </summary>
    /// <typeparam name="TEnum"><see cref="Enum"/> type</typeparam>
    /// <typeparam name="TBooster">Custom implementation</typeparam>
    /// <param name="value"></param>
    /// <param name="result"></param>
    /// <returns>true if the value parameter was converted successfully; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryParse<TEnum, TBooster>(ReadOnlySpan<char> value, out TEnum result)
        where TEnum : struct, Enum
        where TBooster : IFastEnumBooster<TEnum>
        => TryParse<TEnum, TBooster>(value, false, out result);


    /// <summary>
    /// Converts the string representation of the name or numeric value of enumerated constant to an equivalent enumerated object.
    /// A parameter specifies whether the operation is case-sensitive.
    /// The return value indicates whether the conversion succeeded.
    /// </summary>
    /// <typeparam name="TEnum"><see cref="Enum"/> type</typeparam>
    /// <typeparam name="TBooster">Custom implementation</typeparam>
    /// <param name="value"></param>
    /// <param name="ignoreCase"></param>
    /// <param name="result"></param>
    /// <returns>true if the value parameter was converted successfully; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryParse<TEnum, TBooster>(ReadOnlySpan<char> value, bool ignoreCase, out TEnum result)
        where TEnum : struct, Enum
        where TBooster : IFastEnumBooster<TEnum>
    {
        if (value.IsEmpty)
        {
            result = default;
            return false;
        }

        if (StringHelper.IsNumeric(value.At(0)))
            return UnderlyingOperation<TEnum>.TryParseValue(value, out result);

        return TBooster.TryParseName(value, ignoreCase, out result);
    }
    #endregion


    #region ToString
    /// <summary>
    /// Converts the specified value to its equivalent string representation.
    /// </summary>
    /// <typeparam name="TEnum"><see cref="Enum"/> type</typeparam>
    /// <typeparam name="TBooster">Custom implementation</typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToString<TEnum, TBooster>(TEnum value)
        where TEnum : struct, Enum
        where TBooster : IFastEnumBooster<TEnum>
        => TBooster.GetName(value) ?? UnderlyingOperation<TEnum>.ToNumberString(value);
    #endregion
}
