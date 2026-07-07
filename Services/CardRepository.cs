using FiaeMemory.Models;

namespace FiaeMemory.Services;

public class CardRepository
{
    public static readonly List<string> Categories = new()
    {
        "Alle",
        "Code & Compiler",
        "Datentypen & Variablen",
        "Operatoren & Algorithmen",
        "OOP Grundlagen",
        "OOP Objekte & Klassen",
        "OOP Vererbung & Polymorphie",
        "Datenbanken Grundlagen",
        "SQL",
        "NoSQL & Transaktionen",
        "DB Modellierung"
    };

    private static int _idCounter = 1;

    private static (string Question, string Answer, string Category)[] AllPairs => new[]
    {
        // === CODE & COMPILER ===
        ("Quellcode vs. Maschinencode?", "Quellcode = menschenlesbar; Maschinencode = Binärformat, direkt ausführbar", "Code & Compiler"),
        ("Was ist ein Compiler?", "Übersetzt Quellcode komplett in Maschinencode vor der Ausführung", "Code & Compiler"),
        ("Compile time vs. Runtime?", "Compile time = Übersetzungsphase; Runtime = Ausführungsphase des Programms", "Code & Compiler"),
        ("Was ist Bytecode?", "Zwischenformat (z.B. Java/.NET IL), nicht maschinenspezifisch, wird von VM ausgeführt", "Code & Compiler"),
        ("Compiler vs. Interpreter?", "Compiler: vollständige Übersetzung vorab. Interpreter: zeilenweise Ausführung zur Laufzeit", "Code & Compiler"),
        ("Klammern in Programmiersprachen?", "() Parentheses, [] Brackets, {} Braces, <> Angle Brackets", "Code & Compiler"),
        ("Was ist ein Ausdruck (Expression)?", "Kombination aus Werten, Variablen, Operatoren — ergibt einen Wert", "Code & Compiler"),
        ("Was ist eine Anweisung (Statement)?", "Befehl, der eine Aktion ausführt; endet meist mit Semikolon", "Code & Compiler"),
        ("Was ist ein Block?", "Gruppe von Anweisungen in geschweiften Klammern {}", "Code & Compiler"),
        ("Was ist ein Kommentar?", "Nicht ausgeführter Text zur Dokumentation: // einzeilig, /* */ mehrzeilig", "Code & Compiler"),

        // === DATENTYPEN & VARIABLEN ===
        ("Was ist eine Variable?", "Benannter Speicherplatz, der einen Wert eines bestimmten Datentyps hält", "Datentypen & Variablen"),
        ("Was ist ein Datentyp?", "Definiert Art und Wertebereich der gespeicherten Daten (int, string, bool...)", "Datentypen & Variablen"),
        ("Was ist Deklaration?", "Bekanntmachung einer Variable mit Name und Typ ohne Wert", "Datentypen & Variablen"),
        ("Was ist Initialisierung?", "Erstmalige Wertzuweisung an eine Variable", "Datentypen & Variablen"),
        ("Was ist ein Literal?", "Direkter fester Wert im Code, z.B. 42, \"Hallo\", true", "Datentypen & Variablen"),
        ("Was ist der Scope (Gültigkeitsbereich)?", "Bereich im Code, in dem eine Variable sichtbar/zugänglich ist", "Datentypen & Variablen"),
        ("Was ist ein Cast?", "Explizite Typumwandlung: (int)3.7 → 3", "Datentypen & Variablen"),
        ("Was ist null?", "Fehlender/nicht gesetzter Referenzwert — zeigt auf kein Objekt", "Datentypen & Variablen"),
        ("Drei grundlegende Datenstrukturen?", "Array (fest), Liste (dynamisch), Dictionary/Map (Schlüssel-Wert)", "Datentypen & Variablen"),
        ("Listen vs. Arrays?", "Listen: dynamisch wachsend, einfaches Einfügen/Löschen. Arrays: feste Größe, schneller Zugriff", "Datentypen & Variablen"),
        ("Was ist ein Overflow?", "Wert überschreitet maximalen Wertebereich des Datentyps → Überlauf/Fehler", "Datentypen & Variablen"),
        ("Was ist ein Array?", "Geordnete Sammlung gleichartiger Elemente mit festem Index-Zugriff", "Datentypen & Variablen"),
        ("Statische vs. dynamische Typisierung?", "Statisch: Typen bei Kompilierung geprüft (C#, Java). Dynamisch: Typen zur Laufzeit (Python, JS)", "Datentypen & Variablen"),
        ("Was ist Typsicherheit?", "Garantie, dass Operationen nur auf typkorrekte Werte angewendet werden", "Datentypen & Variablen"),
        ("Starke vs. schwache Typisierung?", "Stark: keine impliziten Typumwandlungen. Schwach: implizite Konvertierungen erlaubt", "Datentypen & Variablen"),
        ("Was ist die Domäne eines Programms?", "Fachlicher Anwendungsbereich/Problemgebiet des Programms", "Datentypen & Variablen"),
        ("CamelCase, PascalCase, snake_case?", "camelCase: erste klein dann groß. PascalCase: alles groß. snake_case: Wörter mit Unterstrich", "Datentypen & Variablen"),
        ("Was bedeutet case-sensitive?", "Groß-/Kleinschreibung wird unterschieden: 'Name' ≠ 'name'", "Datentypen & Variablen"),

        // === OPERATOREN & ALGORITHMEN ===
        ("Was macht der Modulo-Operator (%)?", "Gibt den Rest einer ganzzahligen Division zurück: 10 % 3 = 1", "Operatoren & Algorithmen"),
        ("Was ist Konkatenation?", "Verknüpfung von Strings: \"Hallo\" + \" Welt\" = \"Hallo Welt\"", "Operatoren & Algorithmen"),
        ("Was sind unäre Operatoren?", "Operatoren mit nur einem Operanden: ++, --, !, - (negation)", "Operatoren & Algorithmen"),
        ("Was ist der ternäre Operator?", "Kurzform für if-else: Bedingung ? WertWenn­Wahr : WertWennFalsch", "Operatoren & Algorithmen"),
        ("Was ist ein Algorithmus?", "Endliche, eindeutige Schrittfolge zur Lösung eines Problems", "Operatoren & Algorithmen"),
        ("Drei Grundbestandteile eines Algorithmus?", "Sequenz (Folge), Selektion (Verzweigung/if), Iteration (Schleifen)", "Operatoren & Algorithmen"),
        ("Zwei grundlegende Schleifenformen?", "Kopfgesteuert (while: Bedingung zuerst) und fußgesteuert (do-while: Bedingung danach)", "Operatoren & Algorithmen"),
        ("Weitere Schleifenformen?", "for-Schleife (Zähler), foreach (Iteration über Sammlung)", "Operatoren & Algorithmen"),
        ("Was ist Rekursion?", "Funktion, die sich selbst aufruft — mit Basisfall zum Abbruch", "Operatoren & Algorithmen"),
        ("Vor-/Nachteile Rekursion?", "Vorteile: eleganter Code. Nachteile: Stack-Overflow-Risiko, oft langsamer als Iteration", "Operatoren & Algorithmen"),
        ("Muss man Rekursion einsetzen?", "Nein — jede Rekursion ist durch Iteration ersetzbar", "Operatoren & Algorithmen"),

        // === OOP GRUNDLAGEN ===
        ("Was ist Objektorientierung (OOP)?", "Programmierparadigma: Daten + Verhalten in Objekten gebündelt", "OOP Grundlagen"),
        ("Drei Konzepte der OOP?", "Kapselung, Vererbung, Polymorphie", "OOP Grundlagen"),
        ("Was ist ein Programmierparadigma?", "Grundlegendes Konzept/Stil der Programmierung (OOP, funktional, prozedural...)", "OOP Grundlagen"),
        ("Welche Paradigmen gibt es?", "Prozedural, Objektorientiert, Funktional, Deklarativ, Reaktiv", "OOP Grundlagen"),
        ("Vorteile der OOP?", "Wiederverwendbarkeit, Kapselung, Modularität, Wartbarkeit", "OOP Grundlagen"),
        ("Was ist idiomatische Programmierung?", "Code so schreiben, wie es die Sprache/Community als Best Practice vorsieht", "OOP Grundlagen"),
        ("Unterschied Prozedur vs. Funktion?", "Prozedur: kein Rückgabewert (void). Funktion: gibt Wert zurück", "OOP Grundlagen"),
        ("Was ist eine Methode?", "Funktion/Prozedur als Member einer Klasse", "OOP Grundlagen"),
        ("Wann schreibt man Methoden?", "DRY-Prinzip: bei Wiederholung, für Lesbarkeit, Testbarkeit und Kapselung", "OOP Grundlagen"),
        ("Was ist die Methodensignatur?", "Name + Parameterliste + Rückgabetyp einer Methode", "OOP Grundlagen"),
        ("Was muss an der Signatur eindeutig sein?", "Name + Parametertypen (für Überladen) — Rückgabetyp nicht eindeutig genug", "OOP Grundlagen"),
        ("Was ist Überladen von Methoden?", "Gleicher Methodenname, unterschiedliche Parameter — mehrere Varianten", "OOP Grundlagen"),
        ("Was macht return?", "Beendet Methode und gibt Optional einen Wert an den Aufrufer zurück", "OOP Grundlagen"),
        ("Was kennzeichnet void?", "Methode gibt keinen Wert zurück", "OOP Grundlagen"),
        ("Call by Value vs. Call by Reference?", "Value: Kopie des Wertes. Reference: Referenz auf Original — Änderungen wirken sich aus", "OOP Grundlagen"),

        // === OOP OBJEKTE & KLASSEN ===
        ("Was ist ein Objekt?", "Instanz einer Klasse mit eigenem Zustand (Felder) und Verhalten (Methoden)", "OOP Objekte & Klassen"),
        ("Was ist eine Klasse?", "Bauplan/Template für Objekte — definiert Felder und Methoden", "OOP Objekte & Klassen"),
        ("Unterschied Objekt vs. Klasse?", "Klasse = Bauplan. Objekt = konkrete Instanz des Bauplans", "OOP Objekte & Klassen"),
        ("Was sind Member einer Klasse?", "Felder (Daten) und Methoden (Verhalten) einer Klasse", "OOP Objekte & Klassen"),
        ("Was sind statische Member?", "Gehören zur Klasse, nicht zur Instanz — mit static markiert", "OOP Objekte & Klassen"),
        ("Vor-/Nachteile statischer Methoden?", "Vorteil: ohne Instanz nutzbar. Nachteil: kein polymorphes Verhalten, schlechter testbar", "OOP Objekte & Klassen"),
        ("Was bedeutet Instantiierung?", "Erzeugen eines konkreten Objekts aus einer Klasse via new", "OOP Objekte & Klassen"),
        ("Wie instantiiert man ein Objekt?", "Mit dem new-Operator: var obj = new MeineKlasse();", "OOP Objekte & Klassen"),
        ("Was ist eine Instanzvariable?", "Feld einer Klasse, das pro Objekt-Instanz separat existiert", "OOP Objekte & Klassen"),
        ("Was bedeutet Nachricht an Objekt senden?", "Methode auf einem Objekt aufrufen: objekt.Methode()", "OOP Objekte & Klassen"),
        ("Wie referenziert man das aktuelle Objekt?", "Mit dem Schlüsselwort this", "OOP Objekte & Klassen"),
        ("Was ist ein Konstruktor?", "Spezialmethode zur Initialisierung eines neuen Objekts — trägt Klassenname", "OOP Objekte & Klassen"),
        ("Was ist ein Default-Konstruktor?", "Parameterloser Konstruktor — wird automatisch erstellt, wenn kein eigener definiert", "OOP Objekte & Klassen"),
        ("Was ist Kapselung?", "Verbergen interner Details — nur definierte Schnittstelle nach außen sichtbar", "OOP Objekte & Klassen"),
        ("Was sind Getter und Setter?", "Getter: liest Instanzvariable. Setter: setzt Instanzvariable mit Validierungsmöglichkeit", "OOP Objekte & Klassen"),
        ("Vorteil Setter vs. public Attribut?", "Setter kann Validierung, Logging oder Events beinhalten vor dem Setzen des Wertes", "OOP Objekte & Klassen"),

        // === OOP VERERBUNG & POLYMORPHIE ===
        ("Was ist Vererbung?", "Klasse übernimmt Member einer anderen Klasse (Basisklasse/Superklasse)", "OOP Vererbung & Polymorphie"),
        ("Basisklasse vs. Subklasse?", "Basisklasse (Parent) vererbt an Subklasse (Child/Derived)", "OOP Vererbung & Polymorphie"),
        ("Was ist eine abstrakte Klasse?", "Kann nicht instantiiert werden — enthält abstrakte Methoden ohne Implementierung", "OOP Vererbung & Polymorphie"),
        ("Was ist eine abstrakte Methode?", "Methode ohne Body — muss in konkreter Subklasse implementiert werden", "OOP Vererbung & Polymorphie"),
        ("Was bedeutet Methode überschreiben (Override)?", "Subklasse stellt eigene Implementierung einer Basisklassenmethode bereit", "OOP Vererbung & Polymorphie"),
        ("Vererbung einschränken?", "Mit sealed (C#) oder final (Java) verhindert man weiteres Erben/Überschreiben", "OOP Vererbung & Polymorphie"),
        ("Wie greift man auf Basisklasse zu?", "Mit base (C#) oder super (Java): base.Methode()", "OOP Vererbung & Polymorphie"),
        ("Mehrfachvererbung möglich?", "Nicht in C#/Java für Klassen — aber über mehrere Interfaces möglich", "OOP Vererbung & Polymorphie"),
        ("Überladen vs. Überschreiben?", "Überladen: gleicher Name, andere Parameter. Überschreiben: gleiche Signatur, neue Implementierung", "OOP Vererbung & Polymorphie"),
        ("Was ist Polymorphie?", "Objekte verschiedener Typen werden über gemeinsame Schnittstelle gleich behandelt", "OOP Vererbung & Polymorphie"),
        ("Was ist ein Interface?", "Vertrag: definiert nur Methodensignaturen ohne Implementierung", "OOP Vererbung & Polymorphie"),
        ("Interface vs. abstrakte Basisklasse?", "Interface: reiner Vertrag, Mehrfach-Implementierung. ABC: Teilimplementierung, Hierarchie", "OOP Vererbung & Polymorphie"),
        ("Was sind Generics/parametr. Polymorphie?", "Typsichere Klassen/Methoden mit Typparametern: List<T>, Dictionary<K,V>", "OOP Vererbung & Polymorphie"),
        ("Was sind Exceptions?", "Laufzeitfehler-Mechanismus: throw wirft, try-catch fängt Ausnahmen", "OOP Vererbung & Polymorphie"),
        ("Was ist Refactoring?", "Code umstrukturieren ohne Verhalten zu ändern — für bessere Lesbarkeit/Wartbarkeit", "OOP Vererbung & Polymorphie"),
        ("Was ist Typinferenz?", "Compiler leitet Typ automatisch ab: var x = 42; → x ist int", "OOP Vererbung & Polymorphie"),
        ("Was sind Lambda-Ausdrücke?", "Anonyme Funktionen: x => x * 2 oder (x, y) => x + y", "OOP Vererbung & Polymorphie"),
        ("Was sind Design Patterns?", "Bewährte wiederverwendbare Lösungsmuster für häufige Entwurfsprobleme (Singleton, Factory...)", "OOP Vererbung & Polymorphie"),

        // === DATENBANKEN GRUNDLAGEN ===
        ("Was ist eine Datenbank?", "Strukturierte, persistente Sammlung von Daten mit Verwaltungssystem (DBMS)", "Datenbanken Grundlagen"),
        ("Vorteile Datenbank vs. Dateisystem?", "Redundanzvermeidung, Transaktionen, Mehrbenutzerzugriff, Abfragen, Integrität", "Datenbanken Grundlagen"),
        ("Welche DB-Formen gibt es?", "Relational (SQL), NoSQL (Dokument, Key-Value, Graph, Spalten), In-Memory", "Datenbanken Grundlagen"),
        ("Übliche DB-Datentypen?", "INTEGER, VARCHAR, BOOLEAN, DATE, DECIMAL, BLOB, TEXT", "Datenbanken Grundlagen"),
        ("Was ist eine In-Memory-Datenbank?", "Daten liegen im RAM statt auf Festplatte — extrem schnell, nicht persistent (Redis, H2)", "Datenbanken Grundlagen"),
        ("Was ist Redundanz (DB)?", "Gleiche Daten an mehreren Stellen gespeichert → Inkonsistenzrisiko", "Datenbanken Grundlagen"),
        ("Was ist Replikation?", "Kopien der DB auf mehreren Servern für Ausfallsicherheit und Lastverteilung", "Datenbanken Grundlagen"),
        ("Was ist ein Primärschlüssel?", "Eindeutiger Bezeichner einer Tabellenzeile — darf nicht NULL sein", "Datenbanken Grundlagen"),
        ("Was ist ein Fremdschlüssel?", "Verweis auf Primärschlüssel einer anderen Tabelle — stellt referentielle Integrität sicher", "Datenbanken Grundlagen"),
        ("Was sind zusammengesetzte Schlüssel?", "Primärschlüssel aus mehreren Spalten, die zusammen eindeutig sind", "Datenbanken Grundlagen"),
        ("Natürliche vs. künstliche Schlüssel?", "Natürlich: aus Fachlichkeit (Ausweisnummer). Künstlich: technisch generiert (AUTO_INCREMENT)", "Datenbanken Grundlagen"),
        ("Was sind anonyme Schlüssel?", "Surrogatschlüssel ohne fachliche Bedeutung, z.B. UUID oder Auto-ID", "Datenbanken Grundlagen"),

        // === SQL ===
        ("Was ist SQL?", "Structured Query Language — Abfragesprache für relationale Datenbanken", "SQL"),
        ("Was bedeutet CRUD?", "Create, Read, Update, Delete — vier Grundoperationen auf Daten", "SQL"),
        ("SQL-Untergruppen?", "DDL (CREATE/ALTER/DROP), DML (SELECT/INSERT/UPDATE/DELETE), DCL (GRANT/REVOKE), TCL (COMMIT/ROLLBACK)", "SQL"),
        ("Selektion vs. Projektion?", "Selektion: Zeilen filtern (WHERE). Projektion: Spalten auswählen (SELECT spalte)", "SQL"),
        ("Typisches SELECT-Statement?", "SELECT spalte FROM tabelle WHERE bedingung ORDER BY spalte LIMIT n", "SQL"),
        ("Was macht SELECT DISTINCT?", "Gibt nur eindeutige Zeilen zurück — doppelte Ergebnisse werden entfernt", "SQL"),
        ("Was macht LIKE?", "Mustervergleich: % = beliebige Zeichen, _ = genau ein Zeichen", "SQL"),
        ("Schnitt/Vereinigung/Differenz in SQL?", "INTERSECT (Schnitt), UNION (Vereinigung), EXCEPT/MINUS (Differenz)", "SQL"),
        ("Was ist ein Kreuzprodukt (SQL)?", "Alle Kombinationen zweier Tabellen: SELECT * FROM A, B ohne JOIN-Bedingung", "SQL"),
        ("Arten von JOINs?", "INNER JOIN, LEFT/RIGHT OUTER JOIN, FULL OUTER JOIN, CROSS JOIN, SELF JOIN", "SQL"),
        ("Möglichkeiten für JOINs in SQL?", "Explizit: JOIN ... ON. Implizit: FROM A, B WHERE A.id = B.fk", "SQL"),
        ("Was ist eine Aggregatsfunktion?", "Berechnet Wert über mehrere Zeilen: COUNT, SUM, AVG, MIN, MAX", "SQL"),
        ("Was macht GROUP BY?", "Fasst Zeilen mit gleichem Spaltenwert zusammen für Aggregation", "SQL"),
        ("Was macht HAVING?", "Filtert Gruppen nach Aggregat-Bedingung — wie WHERE aber nach GROUP BY", "SQL"),
        ("Was ist ein Subselect/Subquery?", "SELECT innerhalb eines anderen SQL-Statements als verschachtelte Abfrage", "SQL"),
        ("Was ist ein Full Table Scan?", "DB liest gesamte Tabelle zeilenweise — ineffizient, fehlendem Index geschuldet", "SQL"),
        ("Was ist ein Index (DB)?", "Datenstruktur zur schnellen Suche — wie Buchregister, vermeidet Full Table Scan", "SQL"),
        ("Was sind Stored Procedures?", "In DB gespeicherte, vorkompilierte SQL-Programme — wiederverwendbar, performant", "SQL"),
        ("Was sind Trigger?", "Automatisch ausgeführte DB-Prozedur bei INSERT/UPDATE/DELETE", "SQL"),
        ("Was sind Views?", "Gespeicherte SELECT-Abfragen als virtuelle Tabellen — keine eigenen Daten", "SQL"),
        ("Kann man Views updaten?", "Nur einfache Views — keine Aggregation, keine Joins mit mehreren Tabellen", "SQL"),
        ("Was ist ein Constraint?", "Einschränkung für Spalten: NOT NULL, UNIQUE, CHECK, PRIMARY KEY, FOREIGN KEY", "SQL"),
        ("Was ist eine Sequence?", "DB-Objekt zur automatischen Generierung eindeutiger aufsteigender Zahlen", "SQL"),
        ("Was ist SQL Injection?", "Angriff durch eingeschleuste SQL-Befehle in Eingabefelder — verhindert durch Prepared Statements", "SQL"),

        // === NOSQL & TRANSAKTIONEN ===
        ("Was ist NoSQL?", "Nicht-relationale DBs für große Datenmengen, flexible Schemata, horizontale Skalierung", "NoSQL & Transaktionen"),
        ("Arten von NoSQL-DBs?", "Key-Value, Dokumenten-DB, Spaltenorientiert, Graph-DB, Objekt-DB", "NoSQL & Transaktionen"),
        ("Wie funktionieren Key-Value-Stores?", "Einfaches Wörterbuch: Schlüssel → Wert. Sehr schnell. Beispiel: Redis, DynamoDB", "NoSQL & Transaktionen"),
        ("Wie funktionieren Dokumenten-DBs?", "Speichert JSON/BSON-Dokumente ohne festes Schema. Beispiel: MongoDB, CouchDB", "NoSQL & Transaktionen"),
        ("Wie funktionieren Graphen-DBs?", "Knoten (Entitäten) + Kanten (Beziehungen) — ideal für Netzwerke. Beispiel: Neo4j", "NoSQL & Transaktionen"),
        ("Wie funktionieren spaltenorientierte DBs?", "Speichert Daten spaltenweise statt zeilenweise — optimal für Analysen. Beispiel: Cassandra", "NoSQL & Transaktionen"),
        ("Bekannte NoSQL-Datenbanken?", "MongoDB, Redis, Cassandra (Apache), Neo4j, CouchDB, DynamoDB (AWS), HBase", "NoSQL & Transaktionen"),
        ("Was ist eine Transaktion?", "Folge von DB-Operationen als atomare Einheit — alles oder nichts", "NoSQL & Transaktionen"),
        ("Was sind ACID-Kriterien?", "Atomarität, Konsistenz, Isolation, Dauerhaftigkeit — Garantien für Transaktionen", "NoSQL & Transaktionen"),
        ("Was bedeutet Atomarität?", "Transaktion entweder vollständig oder gar nicht ausgeführt", "NoSQL & Transaktionen"),
        ("Was bedeutet Konsistenz (ACID)?", "DB-Zustand vor und nach Transaktion gültig gemäß Integritätsregeln", "NoSQL & Transaktionen"),
        ("Was bedeutet Isolation (ACID)?", "Parallele Transaktionen beeinflussen sich nicht gegenseitig", "NoSQL & Transaktionen"),
        ("Was bedeutet Dauerhaftigkeit (ACID)?", "Committed Daten bleiben dauerhaft — auch nach Systemausfall", "NoSQL & Transaktionen"),
        ("Was ist das BASE-Prinzip?", "Basically Available, Soft State, Eventual Consistency — Alternative zu ACID für NoSQL", "NoSQL & Transaktionen"),
        ("Was bedeutet Basically Available?", "System antwortet immer, auch bei Teilausfällen — ggf. mit veralteten Daten", "NoSQL & Transaktionen"),
        ("Was bedeutet Soft State?", "Zustand des Systems kann sich ohne Eingaben ändern (durch Replikation)", "NoSQL & Transaktionen"),
        ("Was bedeutet Eventual Consistency?", "System wird irgendwann konsistent — nicht sofort, aber letztlich", "NoSQL & Transaktionen"),
        ("Was ist das CAP-Theorem?", "Verteiltes System kann max. 2 von 3 garantieren: Consistency, Availability, Partition Tolerance", "NoSQL & Transaktionen"),
        ("Was ist ein Data Warehouse?", "Zentrale DB für analytische Auswertungen historischer Daten aus mehreren Quellen", "NoSQL & Transaktionen"),
        ("Was ist Big Data?", "Datenmengen, die klassische Tools überfordern — charakterisiert durch die 3 V", "NoSQL & Transaktionen"),
        ("Drei Dimensionen von Big Data?", "Volume (Menge), Velocity (Geschwindigkeit), Variety (Vielfalt)", "NoSQL & Transaktionen"),

        // === DB MODELLIERUNG ===
        ("Was sind Entität und Entitätstyp?", "Entitätstyp = Klasse (z.B. Kunde). Entität = konkretes Exemplar (Kunde Max Müller)", "DB Modellierung"),
        ("Was sind starke und schwache Entitäten?", "Stark: eigenständig identifizierbar. Schwach: nur über andere Entität identifizierbar", "DB Modellierung"),
        ("Was ist ein Entity-Relationship-Model?", "Grafisches Modell zur Darstellung von Entitäten, Attributen und Beziehungen", "DB Modellierung"),
        ("ERM-Bestandteile (Chen-Notation)?", "Rechtecke=Entitäten, Ellipsen=Attribute, Rauten=Beziehungen, Linien=Verbindungen", "DB Modellierung"),
        ("Wie bestimmt man die Kardinalität?", "Fragen: Wie viele X gehören zu einem Y? Beispiel: 1 Kunde → n Bestellungen", "DB Modellierung"),
        ("Was ist ein Tabellenmodell?", "Konkrete Darstellung der DB-Tabellen mit Spalten, Typen und Schlüsseln", "DB Modellierung"),
        ("Unterschied ERM vs. Tabellenmodell?", "ERM: konzeptuell/fachlich. Tabellenmodell: physisch/technisch umsetzbar", "DB Modellierung"),
        ("Was ist eine Relation (DB)?", "Tabelle in relationaler DB — Menge von Tupeln (Zeilen) mit gleichem Schema", "DB Modellierung"),
        ("Crow's Foot Notation?", "Symbolisiert Kardinalität: │ = 1, ○ = 0, < = viele (n)", "DB Modellierung"),
        ("Warum Normalisierung?", "Redundanzen eliminieren, Anomalien verhindern, Datenintegrität sichern", "DB Modellierung"),
        ("Welche Anomalien gibt es?", "Einfügeanomalie, Löschanomalie, Änderungsanomalie — durch Redundanz verursacht", "DB Modellierung"),
        ("Welche Normalformen gibt es?", "1NF (atomar), 2NF (volle Abhängigkeit), 3NF (keine transitive Abhängigkeit), BCNF", "DB Modellierung"),
        ("Was ist ein ORM?", "Object-Relational-Mapper: mappt DB-Tabellen auf Objekte (Entity Framework, Hibernate)", "DB Modellierung"),
        ("Bekannte relationale DBs?", "Oracle (Oracle Corp.), MySQL (Oracle), PostgreSQL (Open Source), MS SQL Server (Microsoft), SQLite", "DB Modellierung"),
        ("Was bedeutet referentielle Integrität?", "Fremdschlüssel muss auf existierenden Primärschlüssel verweisen — keine verwaisten Referenzen", "DB Modellierung"),
        ("Wie wird m:n in Tabellen umgesetzt?", "Durch eine Zwischentabelle (Assoziationstabelle) mit zwei Fremdschlüsseln", "DB Modellierung"),
    };

    public List<(MemoryCard Question, MemoryCard Answer)> GetPairs(string category = "Alle", int maxPairs = 10)
    {
        _idCounter = 1;
        var source = category == "Alle"
            ? AllPairs
            : AllPairs.Where(p => p.Category == category).ToArray();

        var selected = source
            .OrderBy(_ => Random.Shared.Next())
            .Take(maxPairs)
            .ToList();

        var pairs = new List<(MemoryCard, MemoryCard)>();
        int pairId = 1;

        foreach (var (question, answer, cat) in selected)
        {
            int qId = _idCounter++;
            int aId = _idCounter++;

            var questionCard = new MemoryCard
            {
                Id = qId,
                MatchingId = aId,
                Content = question,
                Type = CardType.Question,
                Category = cat
            };

            var answerCard = new MemoryCard
            {
                Id = aId,
                MatchingId = qId,
                Content = answer,
                Type = CardType.Answer,
                Category = cat
            };

            pairs.Add((questionCard, answerCard));
            pairId++;
        }

        return pairs;
    }
}
