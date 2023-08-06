# Howdy_Challenge

[Original assigment]([https://www.google.com](https://github.com/WorklifeBarometer/HowdyChallenge)https://github.com/WorklifeBarometer/HowdyChallenge)

This application is a console application written in C#. It is capable of reading .json files containing new data. Upon pressing 'x', all objects (Groups, Individuals, and Evaluations) are serialized into JSON. Upon subsequent runs, the application reloads this data. The application can perform an evaluation for a user-inputted group in a specific month and year. If there are multiple records from the same time, the latest one is used. Most exceptions are handled, particularly those related to duplicates and incorrect user inputs. The evaluation proceeds even if not all Individuals responded in a given month, but the application notifies us of the IDs of the non-responding individuals. The serialized data is saved to the "groups.json" file. If you want to insert new data, after selecting the option, type the path to the file. 

After downloading the .zip file, you can run the application by clicking on the .exe file. Data will be automatically loaded from "groups.json". You can test this by pressing the "S" action for evaluation search and entering the group ID: 1, month: 2, year: 2021. You can test further data via the "A" action, and for evaluations, please enter "E".

[Link to .zip](Howdy_Challenge.zip)
