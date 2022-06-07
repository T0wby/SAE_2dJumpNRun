# SAE2dJumpNRun

Erstelle mit den Möglichkeiten einer Game-Engine eine 2D Jump’n'Run Steuerung für eine Spielfigur. Setze dabei grundlegende Fähigkeiten wie Gehen, Laufen & Springen um. Weiterhin sollen alle gameplay-relevanten Werte für die Steuerung in der Game-Engine anpassbar sein ohne den Code direkt verändern zu müssen. Dies dient dazu die Steuerung für verschiedene Szenarien flexibel einsetzen und wiederverwenden zu können.

Die Steuerung muss folgende grundlegende Elemente beinhalten:

Die Spielfigur soll sich horizontal bewegen können.
Die Spielfigur soll einen 2D-Collider und einen 2D-Rigidbody nutzen.
Die Spielfigur soll durch das Betätigen einer Taste springen können. Auch die Möglichkeit für einen Mehrfachsprung soll implementiert werden. Diese Eigenschaft soll in der Engine an- und abwählbar gemacht werden.
Diese folgenden Punkte sind optional und eignen sich dafür die Steuerung im Verlauf des Unterrichtsblocks “Spielmechaniken” zu erweitern.

Die Spielfigur kann durch das Gedrückthalten der Sprungtaste höher springen
Die Spielfigur kann einige Frames nachdem sie eine Plattform verlassen hat, noch springen und fällt nicht sofort nach unten. Dies wird im Fachjargon Coyote-Time genannt.
Wird wenige Frames bevor die Spielfigur den Boden berührt die Sprungtaste gedrückt, soll diese Eingabe gespeichert werden und einen Sprung auslösen sobald sie den Boden berührt.
Die Spielfigur kann von Wänden abspringen. Diese Eigenschaft soll in der Engine an- und abwählbar gemacht werden.
Die Spielfigur kann an Wänden hinab rutschen. Diese Eigenschaft soll in der Engine an- und abwählbar gemacht werden.
