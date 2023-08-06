# Howdy_Challenge

[Original assigment]([https://www.google.com](https://github.com/WorklifeBarometer/HowdyChallenge)https://github.com/WorklifeBarometer/HowdyChallenge)

This application is a console application written in C#. It is capable of reading .json files containing new data. Upon pressing 'x', all objects (Groups, Individuals, and Evaluations) are serialized into JSON. Upon subsequent runs, the application reloads this data. The application can perform an evaluation for a user-inputted group in a specific month and year. If there are multiple records from the same time, the latest one is used. Most exceptions are handled, particularly those related to duplicates and incorrect user inputs. The evaluation proceeds even if not all Individuals responded in a given month, but the application notifies us of the IDs of the non-responding individuals. The serialized data is saved to the "groups.json" file. If you want to insert new data, after selecting the option, type the path to the file.
