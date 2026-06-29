# Exercise07 Agent-Workflow

Diese Datei dient der Middleware als strukturierte Grundlage fuer die automatisch erzeugten Issues von Maria. Die fachlichen Hinweise und Beispiele stehen in `README.md`.

# Part 1 -- CI Workflow vorbereiten

<!-- agent-assignment-part:{"kind":"ci","required_checks":["workflow:.github/workflows/ci.yml"]} -->

- Der LiaScript-Link im `README.md` muss auf das Repository der Studierenden geändert werden.
- Ein Badge für den Status der Tests muss in die `README.md` eingebunden werden. Hilfen stehen in der `README.md`.
- Das Testprojekt soll unter `csharp/tests/TruthTableTests` angelegt werden.
- Eine Solution muss im Ordner `csharp/` angelegt werden und sowohl das Produktionsprojekt als auch das Testprojekt enthalten.
- Das Testprojekt muss das Produktionsprojekt referenzieren.
- Die CI-Aktion in `.github/workflows/ci.yml` soll so angepasst werden, dass sie die Tests bei jedem Push auf jeden Branch ausführt.
- Die CI muss über den Workflow-Pfad `.github/workflows/ci.yml` erfolgreich laufen. Der Jobname darf geändert werden.

# Part 2 -- Automatische Tests ergänzen

<!-- agent-assignment-part:{"kind":"ci","required_checks":["workflow:.github/workflows/ci.yml"]} -->

Schreibe folgende Tests und stelle sicher, dass sie lokal und in `.github/workflows/ci.yml` ausgeführt werden:

1. Integrationstest Wahrheitstabelle End-to-End
Eingabe eines Terms wie A AND (B OR NOT C) über den normalen Ablauf (Input -> Parse -> Matrix -> Writer). Bonus: Prüfung, dass Anzahl Zeilen, Spaltenreihenfolge und Ergebnisse konsistent sind. Diesen Test als Commandline-Integrationstest implementieren, der den gesamten Ablauf prüft.

2. Unit-Test AND korrekt
Mehrere Belegungen für A AND B prüfen, insbesondere dass nur 1 AND 1 zu 1 wird.

3. Unit-Test OR korrekt
Belegungen für A OR B prüfen, insbesondere dass nur 0 OR 0 zu 0 wird.

4. Unit-Test NOT korrekt
Prüfen, dass NOT A den Wert zuverlässig invertiert (0 -> 1, 1 -> 0).

5. Unit-Test Klammern/Priorität korrekt
Vergleich von A AND (B OR C) mit A AND B OR C, um sicherzustellen, dass Klammern die Auswertungsreihenfolge wirklich verändern.

6. Leere Eingaben im Wahrheitstabellenmodul müssen einen Fehler verursachen
Falls der Nutzer nichts eingibt, muss der Parser mit einer klaren Fehlermeldung fehlschlagen.

7. Parser-Fehlertest fehlende schließende Klammer
Eingabe wie A AND (B OR C muss mit klarer Fehlermeldung fehlschlagen.

8. Parser-Fehlertest unerwartetes Token/ungültiges Zeichen
Eingaben mit verbotenen Zeichen oder Token (z. B. A & B) müssen sauber abgewiesen werden.

9. Nicht-implementierte Operatoren
Für XOR, NAND, NOR jeweils prüfen, dass gezielt NotImplementedException geworfen wird.

10. Variablenextraktion und Matrixgröße
Für einen Term mit n unterschiedlichen Variablen prüfen, dass exakt 2^n Zeilen erzeugt werden und jede Variablenbelegung genau einmal vorkommt.

# Part 3 -- Codequalität in der CI absichern

<!-- agent-assignment-part:{"kind":"ci","required_checks":["workflow:.github/workflows/ci.yml"]} -->

Maria möchte die Codequalität im Projekt verbessern.

- Binde einen statischen Code-Analyzer oder eine Formatprüfung in das Projekt ein.
- Erweitere `.github/workflows/ci.yml`, sodass diese Prüfung automatisch in der CI ausgeführt wird.
- Behebe die dadurch sichtbar werdenden Qualitätsprobleme, ohne die vorhandene Funktionalität zu verschlechtern.
- Hinweise stehen in Abschnitt 2 der `README.md`.

# Part 4 -- Dokumentation als CI-Artefakt erzeugen

<!-- agent-assignment-part:{"kind":"ci","required_checks":["workflow:.github/workflows/ci.yml"]} -->

Jürgen möchte eine Dokumentation immer verfügbar haben.

- Erzeuge die Dokumentation mit Doxygen.
- Erweitere `.github/workflows/ci.yml`, sodass die Doxygen-Dokumentation in der CI generiert wird.
- Lade die generierte RTF-Dokumentation als GitHub-Actions-Artefakt hoch.
- Hinweise stehen in Abschnitt 3 der `README.md`.

# Part 5 -- Bonusaufgabe: Issue- und PR-Templates

<!-- agent-assignment-part:{"kind":"bonus","required_checks":["workflow:.github/workflows/ci.yml"],"labels":["bonus"]} -->

Diese Bonusaufgabe wird nach den Pflichtteilen automatisch erzeugt. Sie ist freiwillig und dient dazu, den GitHub-Projektworkflow weiter zu verbessern.

Issue-Template erstellen
====================

Erstelle ein Issue-Template für dein Projekt, das folgende Punkte abdeckt:

- Titel
- Beschreibung
- Akzeptanzkriterien
- Labels
- Priorität

Erstelle ein PR-Template, das folgende Punkte enthält:

- Titel
- Beschreibung
- Verknüpfte Issues
- Änderungen
- Tests
- Checkliste

Teste die Templates, indem du ein neues Issue und einen neuen Pull Request erstellst.
