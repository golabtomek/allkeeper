# allkeeper
Handy assistant. 

It is available always on the top-left or bottom-left corner of your screen (depends on your taskbar position). You can set the position of window in the tray icon menu. 

Actual funcionality:
- [x] Quick Notes
- [x] Clipboard History (you can copy items by clicking on them)
- [ ] Personalization (temporary disabled)

Future funcionality:
- [ ] Files storing and management
- [ ] Integrated Web and PC search
- [ ] Automatizion of common actions (files copying, quick actions, smart apps launching)

It's made with WPF using MVVM Pattern. 
File names describe which file describe which object, so if you want to add some features for yourself, it's very easy to recognize where to put your code.

For example:
If you want to make window bigger, go to 
>allkeeper/ViewModel/window.cs

find method called
>setDimensions(bool toFullSize)

and variable 
>private double fullHeight = 450;

At selected method you can set width of the window. Current value is binded to 'SystemParameters.WorkArea'. 
To change height of window, simply change te 'fullHeight' variable. 



