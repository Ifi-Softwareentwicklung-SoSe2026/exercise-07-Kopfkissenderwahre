<!--

author:   Volker Göhler, Simon Hörtzsch
email:    volker.goehler@informatik.tu-freiberg.de
version:  0.0.2
language: de
narrator: Deutsch Female

edit: true
date: 2026-06-23

link:   https://raw.githubusercontent.com/vgoehler/LiaScript_CSS_Provider/refs/heads/main/dist/university.css

tags: [Sommersemester2026, Softwareentwicklung, Übung07]

-->

[![LiaScript Course](https://raw.githubusercontent.com/LiaScript/LiaScript/master/badges/course.svg)](https://liascript.github.io/course/?https://raw.githubusercontent.com/Ifi-Softwareentwicklung-SoSe2026/exercise_07/refs/heads/main/README.md)

# Aufgabe 07

Softwareentwicklung SoSe2026
============================

## 1. Continuous Integration (CI) Workflow

- Softwaretests sind ein wichtiger Bestandteil der Softwareentwicklung, um sicherzustellen, dass die implementierte Funktionalität korrekt arbeitet und keine Regressionen auftreten.
- Unit-Tests prüfen einzelne Komponenten oder Funktionen isoliert, während Integrationstests das Zusammenspiel mehrerer Komponenten testen.
- Wir nutzen GitHub Actions, um die Tests automatisch auszuführen.
- Um die Sichtbarkeit zu erhöhen werden oft Badges in der README.md eingebunden, die den aktuellen Status der Tests anzeigen.

### 1.1. Tests

Wir nutzen `xunit` als Testframework. https://xunit.net/?tabs=cs

Installation des Templates: 
======

```bash
dotnet new install xunit.v3.templates
```

Anlegen des UnitTest-Projekts (im `csharp` Ordner):
======

```bash
dotnet new xunit -n TruthTableTests -o tests/TruthTableTests
```

Solution File anlegen (im `csharp` Ordner):
======

Das Solution File dient dazu, mehrere Projekte in einer Lösung zu bündeln. In unserem Fall wollen wir das Testprojekt `TruthTableTests` in die Lösung `TruthTable` einbinden.

```bash
    dotnet new sln -n TruthTable
```

Hinzufügen des Testprojekts zur Solution:

```bash
    dotnet sln add tests/TruthTableTests/TruthTableTests.csproj
```

Hinzufügen des Produktionsprojekts zur Solution:

```bash
    dotnet sln add src/LogischeAusdrücke/LogischeAusdrücke.csproj  
```

Referenzieren des Produktionsprojekts im Testprojekt:

```bash
    dotnet add tests/TruthTableTests/TruthTableTests.csproj reference src/LogischeAusdrücke/LogischeAusdrücke.csproj
```

Starte Tests mit (im `csharp` Ordner):

```bash
    dotnet test
```

#### 1.1.1 Unit-Tests

- `[Fact]` kennzeichnet einen Unit-Test der immer Wahr ist.
- `Assert` wird verwendet, um Bedingungen zu prüfen.
Beispiel:
```csharp
public class MyAwesomeTestSuite
{
    [Fact]
    public void MyAwesomeTest()
    {
        // Arrange
        int a = 2;
        int b = 3;

        // Act
        int result = a + b;

        // Assert
        Assert.Equal(5, result);
    }
}
```
- `[Theory]` kennzeichnet einen parametrisierten Test, der mit verschiedenen Eingaben (`[InlineData(23)`) ausgeführt wird.
```csharp
public class MyAwesomeTestSuite
{
    [Theory]
    [InlineData(2, 3, 5)]
    [InlineData(10, 20, 30)]
    public void MyAwesomeTest(int a, int b, int expected)
    {
        // Act
        int result = a + b;

        // Assert
        Assert.Equal(expected, result);
    }
}
```

#### 1.1.2 Integrationstests

Integrationstests prüfen das Zusammenspiel mehrerer Komponenten. In unserem Fall werden wir die gesamte Pipeline von der Eingabe eines logischen Ausdrucks bis zur Ausgabe der Wahrheitstabelle testen.
Um Integrationstests zu implementieren, können wir ebenfalls `xunit` verwenden, aber die Tests werden komplexer, da sie mehrere Klassen und Methoden aufrufen.
Der Einfachheit halber nutzen wir die Ausführung auf der Kommandozeile, um die gesamte Pipeline zu testen. Wir können die Eingabe simulieren.

Mit `echo "A and B"` können wir eine Klausel über die Kommandozeile an unser Programm übergeben und die Ausgabe prüfen. Wir können dies in einem Integrationstest automatisieren, indem wir den Prozess starten, die Eingabe übergeben und die Ausgabe erfassen.

Mit dem Pipe Operator `|` können wir die Eingabe an unser Programm übergeben.

```bash
echo "A AND B" | dotnet run tabelle
```


> **Experten:**
> Für weitere Outputanalysen können wir die Ausgabe umleiten und mit `grep` oder `findstr` nach bestimmten Mustern suchen. `wc` kann verwendet werden, um die Anzahl der Zeilen, Wörter oder Zeichen in der Ausgabe zu zählen.

### 1.2. Badges

Badges in GitHub zeigen den Status von Builds, Tests und anderen Metriken an. Sie werden in der README.md eingebunden, um den aktuellen Zustand des Projekts auf einen Blick sichtbar zu machen.

Wir nutzen shields.io, um Badges zu generieren. Der Badge für den Status der Tests wird wie folgt eingebunden:

```markdown
![CI Status](https://img.shields.io/github/actions/workflow/status/Ifi-Softwareentwicklung-SoSe2026/exercise_07/ci.yml?branch=main&label=CI)
```

- Link: https://shields.io/badges/git-hub-actions-workflow-status
- `GET /github/actions/workflow/status/:user/:repo/:workflow`

### 1.3. CI Workflow

Der CI Workflow wird in der Datei `.github/workflows/ci.yml` definiert. Er beschreibt die Schritte, die GitHub Actions ausführt, um die Tests automatisch zu starten.

Diese Dateien sind yml Dateien (Mit Leerzeichen als Einrückung, keine Tabs).
Diese enthalten einen Header, der den Namen des Workflows (`name:`) und die Trigger (`on:`) definiert.

```yaml
name: CI
on:
  push:
    branches: [ main ]
  pull_request:
    branches: [ main ]
```

Das Beispiel zeigt, dass der Workflow bei jedem Push oder Pull Request auf den `main` Branch ausgelöst wird. `"**"` ist der Platzhalter für alle Branches. Wir können auch mehrere Branches angeben, z. B. `branches: [ main, develop ]`.

Danach folgt unter `jobs:` die Definition der Jobs, die ausgeführt werden sollen. Jeder Job hat einen Namen und eine Liste von Schritten (`steps:`), die nacheinander ausgeführt werden.

```yaml
jobs:
  build:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v5
      - name: Setup .NET
        uses: actions/setup-dotnet@v5
        with:
          dotnet-version: '10.0.x'
      - name: Restore dependencies
        run: dotnet restore
      - name: Build
        run: dotnet build --no-restore
      - name: Test
        run: dotnet test --no-build --verbosity normal
```

### 2 Linter

Ein Linter ist ein Werkzeug, das den Quellcode auf Stil- und Formatierungsfehler überprüft. Er hilft dabei, den Code konsistent und lesbar zu halten. In C# können wir `dotnet format` verwenden, um unseren Code automatisch zu formatieren.
`dotnet format --verify-no-changes` prüft, ob der Code den Formatierungsrichtlinien entspricht, ohne Änderungen vorzunehmen. Wenn der Code nicht formatiert ist, gibt der Befehl einen Fehler zurück.

Wir können auch den Meziantou Analyzer verwenden, um den Code auf Best Practices und potenzielle Probleme zu überprüfen. Dieser Analyzer kann in Visual Studio oder über die Kommandozeile integriert werden.

```bash
    dotnet add package Meziantou.Analyzer
```

Dabei läuft der Analyzer während des Build-Prozesses und gibt Warnungen oder Fehler aus, wenn bestimmte Regeln verletzt werden. Dies hilft, die Codequalität zu verbessern und potenzielle Probleme frühzeitig zu erkennen.

### 3 Documentation Generation

Doxygen ist ein Tool zur automatischen Generierung von Dokumentation aus dem Quellcode. Es unterstützt verschiedene Programmiersprachen, darunter C#. Mit Doxygen können wir HTML- oder PDF-Dokumentationen erstellen, die die Struktur und Funktionalität unseres Codes beschreiben.

- https://www.doxygen.nl/index.html
- lokal installieren oder nur in der CI/CD verwenden
- `sudo apt-get install -y doxygen graphviz` (Linux)

`Doxyfile` generieren `doxygen -g Doxyfile` und anpassen. Für unser Projekt bereits vorhanden. Die wichtigsten Einstellungen sind:

```text
OUTPUT_DIRECTORY       = docs
GENERATE_RTF = YES
```

Man kann auch HTML oder LaTeX (pdf) (etc.) generieren lassen.

Dann kann mit `doxygen` die Dokumentation generiert werden. Die generierte Dokumentation kann in der CI/CD Pipeline als Artefakt gespeichert werden, sodass sie immer verfügbar ist.
Bei RTF wird die Documentation nach `docs/rtf/refman.rtf` generiert. (mit den aktuellen Settings)

In GitHub Actions kann dann nach der Generierung der Dokumentation ein Artefakt erstellt werden, das heruntergeladen werden kann. Dies ermöglicht es dem Team, jederzeit auf die aktuelle Dokumentation zuzugreifen. Das findet sich auf der Aktionsseite unter "Artifacts" des jeweiligen Workflows.

```yaml

  - name: Upload RTF artifact
    uses: actions/upload-artifact@v5
    with:
      name: doxygen-rtf-documentation
      path: docs/rtf/refman.rtf
```

- https://github.com/actions/upload-artifact

### 4 PR und Issue Templates

Issue- und Pull-Request-Templates in GitHub dienen der Verbesserung der Zusammenarbeit in deinem Projekt.

Was sind Issue- und PR-Templates?
====================

Issues und Pull Requests (PRs) sind zentrale Elemente in der GitHub-Zusammenarbeit. Ein Issue dient dazu, Aufgaben, Fehler oder Verbesserungsvorschläge zu dokumentieren. Ein Pull Request wird genutzt, um Änderungen am Code vorzuschlagen und diese mit dem Team zu besprechen.

Templates helfen dir dabei, diese konsistent und vollständig zu gestalten. Das Team hat somit alle wichtigen Infos auf einen Blick und nichts wird vergessen.

Beispiel: Issue-Template für ein fiktives Café-Management-System
--------------------

Stell dir vor, du arbeitest an einem Projekt zur Verwaltung eines Cafés. Ein typisches Issue könnte die Bestellung eines neuen Kaffeeautomaten beschreiben. So könnte ein Issue-Template aussehen:

```markdown
---
name: Neue Ausrüstung bestellen
about: Verwende dieses Template, um die Bestellung neuer Ausrüstung zu beantragen.
title: "[Bestellung] "
labels: enhancement
assignees: ''

---

### Beschreibung
Beschreibe kurz, welche Ausrüstung bestellt werden soll und warum sie benötigt wird.

### Akzeptanzkriterien
- [ ] Ausrüstung wurde im Budget berücksichtigt.
- [ ] Lieferant wurde kontaktiert.
- [ ] Liefertermin wurde bestätigt.

### Zusätzliche Informationen
- **Priorität**: [high/medium/low]
- **Geschätzte Kosten**: [Betrag in €]
```

Diese Templates müssen im Ordner `.github/ISSUE_TEMPLATE/` gespeichert werden, damit GitHub sie automatisch erkennt und beim Erstellen eines neuen Issues anbietet.
Es werden nur Dateien mit der Endung `.md` unterstützt. Du kannst mehrere Templates erstellen, um unterschiedliche Arten von Issues abzudecken.

Beispiel: PR-Template für das Café-Management-System
--------------------

Ein Pull-Request-Template könnte so aussehen, wenn jemand eine neue Funktion zur Bestandsverwaltung hinzufügt:

```markdown
---
name: Pull Request
about: Beschreibe deine Änderungen und verknüpfe sie mit einem Issue.
title: ''
labels: ''
assignees: ''

---

### Beschreibung
Beschreibe kurz, welche Änderungen du vorgenommen hast und warum.

### Verknüpfte Issues
- Closes #

### Änderungen
- [ ] Neue Funktion zur Bestandsverwaltung hinzugefügt.
- [ ] Unit-Tests für die neue Funktion geschrieben.

### Tests
Beschreibe, wie du deine Änderungen getestet hast.

### Checkliste
- [ ] Code kompiliert ohne Fehler.
- [ ] Alle Tests wurden erfolgreich durchgeführt.
- [ ] Dokumentation wurde aktualisiert.
```

PR Templates müssen im Ordner `.github/PULL_REQUEST_TEMPLATE/` gespeichert werden, damit GitHub sie automatisch erkennt und beim Erstellen eines neuen Pull Requests anbietet. Auch hier werden nur Dateien mit der Endung `.md` unterstützt.
