module Main exposing (main)

import Html as Html exposing (Html)


main : Program Never Model Msg
main =
    Html.program
        { init = init
        , subscriptions = always Sub.none
        , update = update
        , view = view
        }


init : ( Model, Cmd Msg )
init =
    Model () ! []


update : Msg -> Model -> ( Model, Cmd Msg )
update msg model =
    model ! []


view : Model -> Html Msg
view model =
    Html.h1 [] [ Html.text "Elm on Dropstack!" ]


type alias Model =
    { empty : ()
    }


type Msg
    = NoOp
