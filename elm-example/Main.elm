module Main exposing (main)

import Html as Html exposing (Html)
import Html.Attributes as Attr
import Html.Events as Ev


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
    Model 0 ! []


update : Msg -> Model -> ( Model, Cmd Msg )
update msg model =
    case msg of
        ThumbUp ->
            { model | nrThumbsUp = model.nrThumbsUp + 1 } ! []


view : Model -> Html Msg
view model =
    Html.div
        [ Attr.class "container" ]
        [ Html.h1 [] [ Html.text "Elm on Dropstack!" ]
        , viewButtons
        , viewThumbs model.nrThumbsUp
        ]


viewThumbs : Int -> Html Msg
viewThumbs count =
    Html.h3 [] (List.repeat count viewThumb)


viewThumb : Html Msg
viewThumb =
    Html.span
        [ Attr.class "glyphicon glyphicon-thumbs-up"
        , Attr.attribute "aria-hidden" "true"
        ]
        []


viewButtons : Html Msg
viewButtons =
    Html.div
        [ Attr.class "btn-group"
        , Attr.attribute "role" "group"
        , Attr.attribute "aria-label" "..."
        ]
        [ Html.button
            [ Attr.type_ "button"
            , Attr.class "btn btn-primary"
            , Ev.onClick ThumbUp
            ]
            [ viewThumb ]
        ]


type alias Model =
    { nrThumbsUp : Int
    }


type Msg
    = ThumbUp
