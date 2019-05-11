module TicTacToe.Program

open System
open Elmish
open Elmish.WPF

//game design
module TicTacToeDesign =
    //players X and O
    type Player = PlayerX | PlayerO

    //board positions
    //board made of horizontal and vertical components
    type HorizontalPosition = Left | Center | Right 
    type VerticalPosition = Top | Center | Bottom
    type SpacePosition = HorizontalPosition * VerticalPosition

    //board spaces
    //Spaces either empty or used 
    //(used when an X or O is placed on them)
    //space made of its position and state
    type SpaceSate = Taken of Player | Empty

    type Space = {
        position : SpacePosition 
        state : SpaceSate
    }


    //player position
    //postion player puts a counter on their turn
    type PlayerXPosition = PlayerXPosition of SpacePosition
    type PlayerOPosition = PlayerOPosition of SpacePosition

    //vaild places player can put a counter on their turn
    //places to put a counter decided from making a list of the positions taken
    type PlayerXValidPlaces = PlayerXPosition list
    type PlayerOValidPlaces = PlayerOPosition list

    //turn 
    type TurnResult = 
        | PlayerXTurn of PlayerXValidPlaces
        | PlayerOTurn of PlayerOValidPlaces
        | WinGame of Player
    //When a player places their counter, this position is then taken.
    //GameState is updated to save all the taken positions by 
    //adding this position to the other taken 1s from previous turns
    //input -> output
    type PlayerXPlacesCounter<'GameState> = 'GameState * PlayerXPosition -> 'GameState * TurnResult
    type PlayerOPlacesCounter<'GameState> = 'GameState * PlayerOPosition -> 'GameState * TurnResult
    //New game made by resetting TurnResult and GameState
    type NewGame<'GameState> = 'GameState * TurnResult

//game logic 
module TicTacToeLogic =

    open TicTacToeDesign

    type Model =
        {   Count: int
            CurrentPlayer: Player 
            Shape: string}
    
    let init () =
        {   Count = 0
            //StepSize = 1 
            CurrentPlayer = PlayerX
            Shape = "cross.jpg"}

    let doMove p m : Model = m.CurrentPlayer = PlayerO 

    //GameState used to keep track of the game
    //saves which board spaces are taken 
    type GameState = {
        spaces : Space list
    }

    //TODO: List horizontal positions 


    //TODO: List vertial positions 


    //TODO: Line of SpacePostions list


    //TODO: Check for 3 in a row
        //3 in a row vertial lines 
        //3 in a row horizontal lines 
        //diagonal 
        //other diagonal 

    //TODO: Check lines for win



//game user interface
//module TicTacToeInterface =

open TicTacToeDesign



//if CurrentPlayer = PlayerX then "cross.jpg" else "circle.jpg"


open TicTacToeLogic

type Msg =
    | ChangeShape of string
    //| Increment
    //| SetStepSize of int
    | Reset 

let update msg m =
    match msg with
        //| ChangeShape -> {m with Shape = if m.CurrentPlayer = PlayerX then "cross.jpg" else "circle.jpg"}

        //ChangeShape message passes coordinates of pressed button as parameter p, turn is handled in doMove function
        //doMove takes then position and current model as paramenters
        | ChangeShape p -> doMove p m
        //| Increment -> { m with Count = m.Count + m.StepSize }
        //| SetStepSize x -> { m with StepSize = x }
        | Reset -> init ()

open Elmish.WPF

let bindings model dispatch =
    [
        //"ChangeShape" |> Binding.cmd (fun m -> ChangeShape)
        
        //ChangeShape calls function with coordinate parameter declared in the XAML
        //Pipes position parameter from XAML into ChangeShape then doMove 
        "ChangeShape" |> Binding.paramCmd (fun p m -> string p |> ChangeShape)
        "Shape" |> Binding.oneWay (fun m -> m.Shape)
        "CounterValue" |> Binding.oneWay (fun m -> m.Count)
        //"Increment" |> Binding.cmd (fun m -> Increment)
        //"StepSize" |> Binding.twoWay
        //  (fun m -> float m.StepSize)
        //  (fun newVal m -> int newVal |> SetStepSize)
    ]


[<EntryPoint; STAThread>]
let main argv = 
    Program.mkSimple init update bindings
    |> Program.withConsoleTrace
    |> Program.runWindowWithConfig
        { ElmConfig.Default with LogConsole = true }
        (MainWindow())