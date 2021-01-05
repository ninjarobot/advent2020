module FsAdvent2020.PasswordPolicy

open System

type PolicyErrors = InvalidPolicyStart | InvalidPolicyEnd | InvalidPolicyFormat | InvalidPolicySegments of string array

type PasswordPolicy =
    | RangePolicy of RangeStart:int * RangeEnd:int *  Letter:char * Password : string
    | PositionPolicy of FirstPos:int * SecondPos:int *  Letter:char * Password : string
module PasswordPolicy =
    let IsValid = function
        | RangePolicy (rangeStart, rangeEnd, letter, password) ->
            let numInstances = password |> Seq.where (fun c -> c = letter) |> Seq.length
            numInstances >= rangeStart && numInstances <= rangeEnd
        | PositionPolicy (position1, position2, letter, password) ->
            if password.Length >= position2 then
                let char1 = password.[position1 - 1]
                let char2 = password.[position2 - 1]
                if char1 = letter && char2 <> letter || char1 <> letter && char2 = letter then
                    true
                else false
            else false

let parsePolicy (policy:string) =
    match policy.Split([|'-'; ' '; ':'|], (StringSplitOptions.TrimEntries ||| StringSplitOptions.RemoveEmptyEntries)) with
    | [|rangeStart; rangeEnd; letter; password|] when letter.Length = 1 ->
        match Int32.TryParse rangeStart with
        | true, rangeStart ->
            match Int32.TryParse rangeEnd with
            | true, rangeEnd ->
                (rangeStart, rangeEnd, letter.Chars 0, password) |> Result.Ok
            | _ -> Result.Error InvalidPolicyEnd
        | _ -> Result.Error InvalidPolicyStart
    | segments -> Result.Error (InvalidPolicySegments segments)

let parseAndValidate policyType (policy:string) =
    policy
    |> parsePolicy
    |> Result.map (policyType >> PasswordPolicy.IsValid)
    |> function
    | Result.Ok isValid -> isValid
    | Result.Error _ -> false

let numValid policyType (entries:string seq) =
    entries
    |> Seq.where (parseAndValidate policyType)
    |> Seq.length
