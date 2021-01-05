#load "FsAdvent2020/Day5.fs"

open System
open System.IO
open FsAdvent2020.BinaryBoarding

"Day5Data.txt" |> File.ReadAllLines
|> maxSeatID |> printfn "Max seat ID: %d"

"Day5Data.txt" |> File.ReadAllLines
|> missingSeatID |> printfn "My seat ID: %d"