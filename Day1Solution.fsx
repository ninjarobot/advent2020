#load "FsAdvent2020/Day1.fs"

open System
open System.IO
open FsAdvent2020

let entries = "Day1Data.txt" |> File.ReadAllLines |> Array.map Int32.Parse |> List.ofArray
ExpenseReport.productOf2020Sum2 entries |> printfn "Product of 2: %A"

ExpenseReport.productOf2020Sum3 entries |> printfn "Product of 3: %A"
