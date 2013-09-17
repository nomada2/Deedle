﻿namespace FSharp.DataFrame 

open FSharp.DataFrame.Internal
open FSharp.DataFrame.Vectors
open FSharp.DataFrame.Vectors.ArrayVector

// ------------------------------------------------------------------------------------------------
// F# frienly operations for creating vectors
// ------------------------------------------------------------------------------------------------

[<AutoOpen>]
module FSharpVectorExtensions =
  open System

  /// Type that provides a simple access to creating vectors represented
  /// using the built-in `ArrayVector` type that stores the data in a 
  /// continuous block of memory.
  type Vector = 
    /// Creates a vector that stores the specified data in an array.
    /// Values such as `null` and `Double.NaN` are turned into missing values.
    static member inline ofValues<'T>(data:'T[]) = 
      ArrayVectorBuilder.Instance.Create(data)

    /// Creates a vector that stores the specified data in an array.
    /// Values such as `null` and `Double.NaN` are turned into missing values.
    static member inline ofValues<'T>(data:seq<'T>) = 
      ArrayVectorBuilder.Instance.Create(Array.ofSeq data)

    /// Creates a vector that stores the specified data in an array.
    /// Missing values can be specified explicitly as `None`, but other values 
    /// such as `null` and `Double.NaN` are turned into missing values too.
    static member inline ofOptionalValues<'T>(data:seq<option<'T>>) = 
      ArrayVectorBuilder.Instance.CreateMissing(data |> Seq.map OptionalValue.ofOption |> Array.ofSeq)

    /// Missing values can be specified explicitly as `OptionalValue.Missing`, but 
    /// other values such as `null` and `Double.NaN` are turned into missing values too.
    static member inline ofOptionalValues<'T>(data:seq<OptionalValue<'T>>) = 
      ArrayVectorBuilder.Instance.CreateMissing(data |> Array.ofSeq)

// ------------------------------------------------------------------------------------------------
// C# frienly operations for creating vectors
// ------------------------------------------------------------------------------------------------

/// Type that provides access to creating vectors (represented as arrays)
type Vector = 
  /// Creates a vector that stores the specified data in an array.
  /// Values such as `null` and `Double.NaN` are turned into missing values.
  [<CompilerMessage("This method is not intended for use from F#.", 10001, IsHidden=true, IsError=false)>]
  static member inline Create<'T>(data:'T[]) = 
    ArrayVectorBuilder.Instance.Create(data)

  /// Creates a vector that stores the specified data in an array.
  /// Values such as `null` and `Double.NaN` are turned into missing values.
  [<CompilerMessage("This method is not intended for use from F#.", 10001, IsHidden=true, IsError=false)>]
  static member inline Create<'T>(data:seq<'T>) = 
    ArrayVectorBuilder.Instance.Create(Array.ofSeq data)

  /// Creates a vector that stores the specified data in an array.
  /// Values such as `null` and `Double.NaN` are turned into missing values.
  [<CompilerMessage("This method is not intended for use from F#.", 10001, IsHidden=true, IsError=false)>]
  static member inline CreateMissing<'T>(data:seq<OptionalValue<'T>>) = 
    ArrayVectorBuilder.Instance.CreateMissing(data |> Array.ofSeq)

  /// Creates a vector that stores the specified data in an array.
  /// Values such as `null` and `Double.NaN` are turned into missing values.
  [<CompilerMessage("This method is not intended for use from F#.", 10001, IsHidden=true, IsError=false)>]
  static member inline CreateMissing(data:seq<System.Nullable<'T>>) = 
    ArrayVectorBuilder.Instance.CreateMissing(data |> Array.ofSeq |> Array.map OptionalValue.ofNullable)
