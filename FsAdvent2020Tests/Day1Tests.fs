module Day1Tests

open System
open Xunit
open FsAdvent2020

[<Fact>]
let ``Cover DU cases ()`` =
    Assert.NotEqual (ExpenseReport.No2020PairFound, ExpenseReport.No2020TripleFound)

[<Theory>]
[<InlineData("1721,979,366,299,675,1456")>]
let ``Product of two entries that sum to 2020`` (sample:string) =
    let entries = sample.Split ',' |> Array.map Int32.Parse |> List.ofArray
    match ExpenseReport.productOf2020Sum2 entries with
    | Result.Ok product ->
        Assert.Equal(514579, product)
    | Result.Error err ->
        failwithf "%A" err

[<Fact>]
let ``Ensure empty list for sum2 returns Error rather than exception`` () =
    match ExpenseReport.productOf2020Sum2 [] with
    | Result.Ok _ ->
        failwithf "Should have returned error on empty list"
    | Result.Error err ->
        ()

[<Fact>]
let ``Ensure list with no pair for sum2 returns Error rather than exception`` () =
    match ExpenseReport.productOf2020Sum2 [1; 2; 3] with
    | Result.Ok _ ->
        failwithf "Should have returned error when no pair found"
    | Result.Error err ->
        ()

[<Theory>]
[<InlineData("1721,979,366,299,675,1456")>]
let ``Product of three entries that sum to 2020`` (sample:string) =
    let entries = sample.Split ',' |> Array.map Int32.Parse |> List.ofArray
    match ExpenseReport.productOf2020Sum3 entries with
    | Result.Ok product ->
        Assert.Equal(241861950, product)
    | Result.Error err ->
        failwithf "%A" err

[<Fact>]
let ``Ensure empty list for sum3 returns Error rather than exception`` () =
    match ExpenseReport.productOf2020Sum3 [] with
    | Result.Ok _ ->
        failwithf "Should have returned error on empty list"
    | Result.Error err ->
        ()
