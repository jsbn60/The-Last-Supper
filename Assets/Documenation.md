# Documentation

## EventTypes

- dPoint (DialoguePoint)
````
    "1":{
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