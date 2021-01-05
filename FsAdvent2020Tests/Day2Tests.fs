module Day2Tests

open System
open Xunit
open FsAdvent2020

[<Theory>]
[<InlineData("1-3 a: abcde")>]
[<InlineData("1-3 b: cdefg")>]
[<InlineData("2-9 c: ccccccccc")>]
let ``Parse policies`` (policy:string) =
    match PasswordPolicy.parsePolicy policy with
    | Result.Error err -> failwithf "%A" err
    | Result.Ok _ -> ()

[<Fact>]
let ``Number of valid passwords according to range policy`` () =
    let entries = [
            "1-3 a: abcde"
            "1-3 b: cdefg"
            "2-9 c: ccccccccc"
        ]
    Assert.Equal (2, (PasswordPolicy.numValid PasswordPolicy.RangePolicy entries))

[<Fact>]
let ``Number of valid passwords according to position policy`` () =
    let entries = [
            "1-3 a: abcde"
            "1-3 b: cdefg"
            "2-9 c: ccccccccc"
        ]
    Assert.Equal (1, (PasswordPolicy.numValid PasswordPolicy.PositionPolicy entries))

[<Fact>]
let ``Parse bad rangeStart`` () =
    PasswordPolicy.parsePolicy "a-3 a: abcde"
    |> Result.map (fun _ -> failwith "Should fail parsing due to bad rangeStart")
    |> ignore

[<Fact>]
let ``Parse bad rangeEnd`` () =
    PasswordPolicy.parsePolicy "1-g a: abcde"
    |> Result.map (fun _ -> failwith "Should fail parsing due to bad rangeEnd")
    |> ignore

[<Fact>]
let ``Parse bad policy line`` () =
    PasswordPolicy.parsePolicy "foobar"
    |> Result.map (fun _ -> failwith "Should fail parsing due to bad password policy line")
    |> ignore

[<Fact>]
let ``Parse and validate bad range policy line`` () =
    Assert.False (PasswordPolicy.parseAndValidate PasswordPolicy.RangePolicy "foobar")

[<Fact>]
let ``Parse and validate bad position policy line`` () =
    Assert.False (PasswordPolicy.parseAndValidate PasswordPolicy.PositionPolicy "foobar")

[<Fact>]
let ``Validate positional policy with password too short for positions`` () =
    let policy = PasswordPolicy.PositionPolicy(1, 7, 'a', "abc") // abc is 3 chars, too short for second position
    Assert.False (PasswordPolicy.PasswordPolicy.IsValid policy)
