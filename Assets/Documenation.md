# Documentation

## EventTypes

- dPoint (DialoguePoint)
````
    "1":{
        "character": "Which character
        "shownText": "Good Morning, everyone!", 
        "options":{
            "A": {
                "response": "Back to you!",
                "followEvents" : {
                    "dPoint" : "2"
                }
            },
            "B": {
                "response": "Don't talk to me!",
                "followEvents" : {
                    "dPoint" : "3"
                }
            }
        }
    },
````
- npcPoint (NPC says something)
````
    "2":{
        "character":"NameOfCharacter",
        "shownText":"textOfNpc"
        "followEvents":{}
    }
````

- soundeffect
````
    "3":{
        "name":"name",
        "volume":"volume",
        "fadeInTime":"fadeInTime",
        "fadeOutTime:"fadeOutTime"
    }
````