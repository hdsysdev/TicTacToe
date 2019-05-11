module TicTacToe.Program

open System
open Elmish
open Elmish.WPF

type Model =
    {   Count: int
        StepSize: int 
        Shape: string}

let init () =
    {   Count = 0
        StepSize = 1 
        Shape = "circle.jpg"}

type Msg =
    | ChangeShape
    | Increment
    | Decrement
    | SetStepSize of int
    | Reset

let update msg m =
    match msg with
        | ChangeShape -> {m with Shape = "cross.png"}
        | Increment -> { m with Count = m.Count + m.StepSize }
        | Decrement -> { m with Count = m.Count - m.StepSize }
        | SetStepSize x -> { m with StepSize = x }
        | Reset -> init ()

open Elmish.WPF

let bindings model dispatch =
    [
        "ChangeShape" |> Binding.cmd (fun m -> ChangeShape)
        "Shape" |> Binding.oneWay (fun m -> m.Shape)
        "CounterValue" |> Binding.oneWay (fun m -> m.Count)
        "Increment" |> Binding.cmd (fun m -> Increment)
        "Decrement" |> Binding.cmd (fun m -> Decrement)
        "StepSize" |> Binding.twoWay
          (fun m -> float m.StepSize)
          (fun newVal m -> int newVal |> SetStepSize)
    ]


[<EntryPoint; STAThread>]
let main argv = 
    Program.mkSimple init update bindings
    |> Program.withConsoleTrace
    |> Program.runWindowWithConfig
        { ElmConfig.Default with LogConsole = true }
        (MainWindow())