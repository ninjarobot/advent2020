#load "FsAdvent2020/Day4.fs"

open System
open System.IO
open FsAdvent2020.PassportProcessing

let data = "Day4Data.txt" |> File.ReadAllText
let passportData = data.Split (sprintf "%s%s" Environment.NewLine Environment.NewLine)

passportData
|> Seq.map Passport.parse
|> countValid
|> printfn "Valid passports: %i"

passportData
|> Seq.map Passport.parse
|> countReallyValid
|> printfn "Really Valid passports: %i"
