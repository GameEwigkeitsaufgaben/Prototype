# Prototype

First prototype of a learning game to seniblilize children older than 15 for the tasks based on coal mining. 

Prototype is based on https://miro.com/app/board/o9J_kmQ5v0g=/

### Targetplatform: WebGL Desktop
### Developmentplatform: Window 10, Unity 2020.3.18f1, Visual Studio Community 2019

Das Spiel teilt sich auf in 3 Hauptbereiche: 
Einführung, Grubenwasser, Ewigkeitsaufgabe. Das Spiel sollte auf 3 Einheiten aufgeteilt werden. Jedes Kapitel kann durch eine Code freigeschalten werden. Der Code wird am Ende es Kapitels bzw. durch die Betreuungsperson mitgeteilt. Es können zusätzliche Unterlagen zum Notieren von Infos runtergeladen werden, sowie wird es einen Guide für die Betreuungsperson gegen.  Spielstände werden nicht gespeichert. 

Insta Menü:
Das Menü ist so aufgebaut, dass es einzelne Posts gibt und diese als Kacheln (mit einem Bild) angezeigt werden. Drückt man auf die Kachel erscheint ein Overlay. Dieser besteht aus einem Bildbereich und einem Textbereich. Der Bildbereich kann unterschiedliche Typen haben, Bild, Quiz, Interaction und Video. Interaktive Overlays wie Interaktion, Video und Quiz sind durch ein Icon gekennzeichnet. Ein Postoverlay kann durch das Betätigen des Schließenbuttons geschlossen werden. Der Textbereich Ist unterteilt in Beschreibung und Tags. Durch die Kacheln wird einerseits die Story aufgebaut, als auch ein Weg durch das Spiel vorgegeben. 

Video: Das Video wird Gestartet durch das Betätigen des Spielberiechs. Ein Icon zeigt an, dass es sich um ein Video handelt. Wurde das Video das Video abgespielt, wird ein Wiederholen Incon anstatt des Play Icons angezeigt. Das Video kann nicht pausiert aber gestoppt werden und fängt immer von vorne an.

MainMenü:
3 Kapitel mit pwd Prüfung, Adminpwd für ch1, ein startpwd zum Spielen. Wird ins input field eingegeben. Falls richtig wir in GameData.progressWithAdmin auf true gesetzt und in Post.cs wird dies geprüft. Wenn mit Admin eingestiegen wird alles im Kapitel freigeschalten. Wird gemacht 1) zum zusammenwarten, zur Sicherstellung was angesehen wurde. So kann auch jederzeit jeder mit dem Adminpwd rein (Schüler haben gefehlt, etc. ), Das Pwd soll von den Schülern gemerkt werden und beim nächsten Mal eingegeben werden. 

InstaMenü ch1:
vom Instamenü kommt man ins Main retour, wenn man wieder einsteigt muss kein pwd mehr eingegeben werden. Hat man das AdminPwd ist alles freigeschalten.  

