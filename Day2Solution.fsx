#load "FsAdvent2020/Day2.fs"

open System
open System.IO
open FsAdvent2020.PasswordPolicy

let entries = "Day2Data.txt" |> File.ReadAllLines
entries |> numValid |> printfn "Num valid: %A"

entries |> numValid2  |> printfn "Num valid reinterpreted: %A"
