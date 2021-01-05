module FsAdvent2020.PassportProcessing

open System

type ParseErrors =
    | KeyValueParseError of BadData:string
    | KeyParseError of BadData:string

let splitKeyVal (s:string) =
    match s.Split ':' with
    | [|key; value|] -> (key, value) |> Result.Ok
    | _ -> s |> KeyValueParseError |> Result.Error

type PassportField =
    | BirthYear
    | IssueYear
    | Expiration
    | Height
    | HairColor
    | EyeColor
    | PassportID
    | CountryID

let parseKey (key:string, value:string) =
    match key with
    | "byr" -> (BirthYear, value) |> Ok
    | "iyr" -> (IssueYear, value) |> Ok
    | "eyr" -> (Expiration, value) |> Ok
    | "hgt" -> (Height, value) |> Ok
    | "hcl" -> (HairColor, value) |> Ok
    | "ecl" -> (EyeColor, value) |> Ok
    | "pid" -> (PassportID, value) |> Ok
    | "cid" -> (CountryID, value) |> Ok
    | _ -> key |> KeyParseError |> Error

let parseField (s:string) =
    s |> splitKeyVal |> Result.bind parseKey

type Passport = Passport of Fields:(PassportField * string) list
module Passport =
    let parse (s:string) =
        let fields =
            s.Split null // split on whitespace
            |> Seq.map (parseField)
        let rec unwrapErrors (accFields:(PassportField * string) list) (accErrors:ParseErrors list) parsedFieldResults =
            match parsedFieldResults with
            | [] when accErrors = List.empty ->
                Result.Ok accFields
            | [] -> // Had some errors
                Result.Error accErrors
            | (Result.Ok (field, value)) :: tail ->
                unwrapErrors ((field, value) :: accFields) accErrors tail
            | (Result.Error err) :: tail ->
                unwrapErrors accFields (err :: accErrors) tail
        fields
        |> List.ofSeq
        |> unwrapErrors [] []
        |> Result.map Passport
    let isValid (Passport(fields)) =
        if fields.Length < 7 then false // not enough fields
        else // Has all required fields
            let fieldMap = fields |> dict
            fieldMap.ContainsKey BirthYear
            && fieldMap.ContainsKey IssueYear
            && fieldMap.ContainsKey Expiration
            && fieldMap.ContainsKey Height
            && fieldMap.ContainsKey HairColor
            && fieldMap.ContainsKey EyeColor
            && fieldMap.ContainsKey PassportID
    let isExtraValid (Passport(fields)) =
        if fields.Length < 7 then false // not enough fields
        else // Has all required fields
            let hairColorValidChars = "abcdef0123456789".ToCharArray ()
            let fieldMap = fields |> dict
            fieldMap.ContainsKey BirthYear
            && fieldMap.ContainsKey IssueYear
            && fieldMap.ContainsKey Expiration
            && fieldMap.ContainsKey Height
            && fieldMap.ContainsKey HairColor
            && fieldMap.ContainsKey EyeColor
            && fieldMap.ContainsKey PassportID
            &&
                match Int32.TryParse fieldMap.[BirthYear] with
                | true, year when year >= 1920 && year <= 2002 -> true
                | _ -> false
            &&
                match Int32.TryParse fieldMap.[IssueYear] with
                | true, year when year >= 2010 && year <= 2020 -> true
                | _ -> false
            &&
                match Int32.TryParse fieldMap.[Expiration] with
                | true, year when year >= 2020 && year <= 2030 -> true
                | _ -> false
            &&
                match fieldMap.[Height] with
                | s when s.EndsWith "in" ->
                    match s.TrimEnd("in".ToCharArray()) |> Int32.TryParse with
                    | true, height when height >= 59 && height <= 76 -> true
                    | _ -> false
                | s when s.EndsWith "cm" ->
                    match s.TrimEnd("cm".ToCharArray()) |> Int32.TryParse with
                    | true, height when height >= 150 && height <= 193 -> true
                    | _ -> false
                | _ -> false
            &&
                match fieldMap.[HairColor] with
                | s when s.StartsWith "#" && s.Substring(1,6).Trim(hairColorValidChars).Length = 0 -> true
                | _ -> printfn "Invalid hair color: %s" fieldMap.[HairColor]; false
            &&
                match fieldMap.[EyeColor] with
                | "amb" | "blu" | "brn" | "gry" | "grn" | "hzl" | "oth" -> true
                | _ -> printfn "Invalid eye color: %s" fieldMap.[EyeColor]; false
            &&
                match fieldMap.[PassportID] with
                | s when s.Length = 9 ->
                    Int32.TryParse s |> fst
                | _ -> false

let countPassport = function
    | Result.Ok passport when passport |> Passport.isValid -> 1
    | _ -> 0

let countValid (parsedPassports: Result<Passport, ParseErrors list> seq) =
    parsedPassports |> Seq.map countPassport |> Seq.sum

let countReallyValidPassport = function
    | Result.Ok passport when passport |> Passport.isExtraValid -> 1
    | _ -> 0

let countReallyValid (parsedPassports: Result<Passport, ParseErrors list> seq) =
    parsedPassports |> Seq.map countReallyValidPassport |> Seq.sum
