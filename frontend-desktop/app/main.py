"""Frontend desktop scaffold per Cambio Stato Preventivi."""

from __future__ import annotations

from dataclasses import dataclass
from typing import List

import requests
from PySide6.QtCore import Qt
from PySide6.QtWidgets import (
    QApplication,
    QComboBox,
    QHBoxLayout,
    QLabel,
    QMainWindow,
    QMessageBox,
    QPushButton,
    QTableWidget,
    QTableWidgetItem,
    QVBoxLayout,
    QWidget,
)


@dataclass
class PreventivoRow:
    id: str
    codice: str
    cliente: str
    stato_nome: str


class ApiClient:
    """Client API con fallback mock per sviluppo locale."""

    def __init__(self, base_url: str = "http://localhost:5000") -> None:
        self.base_url = base_url.rstrip("/")

    def get_preventivi(self) -> List[PreventivoRow]:
        try:
            response = requests.get(f"{self.base_url}/api/preventivi", timeout=2)
            response.raise_for_status()
            payload = response.json()
            return [
                PreventivoRow(
                    id=item["id"],
                    codice=item["codice"],
                    cliente=item["cliente"],
                    stato_nome=item["statoNome"],
                )
                for item in payload
            ]
        except Exception:
            return [
                PreventivoRow("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa", "PRV-2026-0001", "Acme S.p.A.", "Bozza"),
                PreventivoRow("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb", "PRV-2026-0002", "Globex S.r.l.", "In revisione"),
            ]


class MainWindow(QMainWindow):
    def __init__(self, client: ApiClient) -> None:
        super().__init__()
        self.client = client
        self.rows: List[PreventivoRow] = []

        self.setWindowTitle("Cambio Stato Preventivi")
        self.resize(900, 500)

        central = QWidget(self)
        self.setCentralWidget(central)
        root_layout = QVBoxLayout(central)

        header_layout = QHBoxLayout()
        header_layout.addWidget(QLabel("Filtro stato:"))

        self.state_filter = QComboBox()
        self.state_filter.addItem("Tutti")
        self.state_filter.currentTextChanged.connect(self.refresh_table)
        header_layout.addWidget(self.state_filter)
        header_layout.addStretch(1)

        self.open_button = QPushButton("Apri dettaglio")
        self.open_button.clicked.connect(self.open_detail)
        header_layout.addWidget(self.open_button)

        root_layout.addLayout(header_layout)

        self.table = QTableWidget(0, 3)
        self.table.setHorizontalHeaderLabels(["Codice", "Cliente", "Stato"])
        self.table.horizontalHeader().setStretchLastSection(True)
        self.table.setSelectionBehavior(QTableWidget.SelectionBehavior.SelectRows)
        self.table.setSelectionMode(QTableWidget.SelectionMode.SingleSelection)
        root_layout.addWidget(self.table)

        self.load_data()

    def load_data(self) -> None:
        self.rows = self.client.get_preventivi()
        stati = sorted({row.stato_nome for row in self.rows})
        for stato in stati:
            if self.state_filter.findText(stato) == -1:
                self.state_filter.addItem(stato)
        self.refresh_table()

    def refresh_table(self) -> None:
        filter_value = self.state_filter.currentText()
        visible_rows = self.rows if filter_value == "Tutti" else [r for r in self.rows if r.stato_nome == filter_value]

        self.table.setRowCount(len(visible_rows))
        for index, row in enumerate(visible_rows):
            self.table.setItem(index, 0, QTableWidgetItem(row.codice))
            self.table.setItem(index, 1, QTableWidgetItem(row.cliente))
            state_item = QTableWidgetItem(row.stato_nome)
            state_item.setTextAlignment(Qt.AlignmentFlag.AlignCenter)
            self.table.setItem(index, 2, state_item)
            self.table.item(index, 0).setData(Qt.ItemDataRole.UserRole, row.id)

    def open_detail(self) -> None:
        row = self.table.currentRow()
        if row < 0:
            QMessageBox.information(self, "Dettaglio", "Seleziona un preventivo dalla tabella.")
            return

        preventivo_id = self.table.item(row, 0).data(Qt.ItemDataRole.UserRole)
        QMessageBox.information(self, "Dettaglio", f"Apertura dettaglio preventivo: {preventivo_id}")


def main() -> None:
    app = QApplication([])
    window = MainWindow(ApiClient())
    window.show()
    app.exec()


if __name__ == "__main__":
    main()
