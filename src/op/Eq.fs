module fsharper.op.Eq

open System

/// 引用相等性
let inline refEq a b = Object.ReferenceEquals(a, b)

/// 值相等性
let inline valEq< ^t when ^t: struct and ^t: equality> (a: ^t) (b: ^t) = a = b
