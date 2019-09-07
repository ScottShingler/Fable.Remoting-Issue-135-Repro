module Client

open Elmish
open Elmish.React
open Fable.React
open Fable.React.Props
open Thoth.Json

open Shared

type Model = { Message: string }

type Msg =
    | DoSomething of int
    | DidSomething
    | Error of exn

module Server =
    open Shared
    open Fable.Remoting.Client

    /// A proxy you can use to talk to server directly
    let api : MyApi =
      Remoting.createApi()
      |> Remoting.withRouteBuilder Route.builder
      |> Remoting.buildProxy<MyApi>

let init () : Model * Cmd<Msg> =
    let initialModel = { Message = "Press the button" }
    initialModel, Cmd.none

let update (msg : Msg) (model : Model) : Model * Cmd<Msg> =
    match msg with
    | DoSomething x ->
        { Message = "Doing something..." },
        Cmd.OfAsync.either Server.api.doSomething x (fun _ -> DidSomething) Error

    | DidSomething ->
        { Message = "Success!"}, Cmd.none

    | Error e->
        { Message = sprintf "Error: %O" e}, Cmd.none


let safeComponents =
    let components =
        span [ ]
           [ a [ Href "https://github.com/SAFE-Stack/SAFE-template" ]
               [ str "SAFE  "
                 str Version.template ]
             str ", "
             a [ Href "https://saturnframework.github.io" ] [ str "Saturn" ]
             str ", "
             a [ Href "http://fable.io" ] [ str "Fable" ]
             str ", "
             a [ Href "https://elmish.github.io" ] [ str "Elmish" ]
             str ", "
             a [ Href "https://zaid-ajaj.github.io/Fable.Remoting/" ] [ str "Fable.Remoting" ]

           ]

    span [ ]
        [ str "Version "
          strong [ ] [ str Version.app ]
          str " powered by: "
          components ]

let view (model : Model) (dispatch : Msg -> unit) =
    div []
        [ h1 [] [ str "Fable.Remoting Repro for #135" ]
          p  [] [ str "Pressing the button should report a success message below" ]
          button [ OnClick (fun _ -> DoSomething 42 |> dispatch) ] [ str "Do something" ]
          p  [] [ str model.Message ]
          safeComponents ]

#if DEBUG
open Elmish.Debug
open Elmish.HMR
#endif

Program.mkProgram init update view
#if DEBUG
|> Program.withConsoleTrace
#endif
|> Program.withReactBatched "elmish-app"
#if DEBUG
|> Program.withDebugger
#endif
|> Program.run
