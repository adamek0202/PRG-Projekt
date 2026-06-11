# Zpráva o upgradu na .NET 9

## Úpravy cílové architektury projektu

| Název projektu | Stará cílová architektura | Nová cílová architektura | Commity |
|:---|:---:|:---:|:---|
| Pokladna.csproj | net48 | net9.0-windows | 8549fa0b |

## Balíčky NuGet

| Název balíčku | Stará verze | Nová verze | ID commitu |
|:---|:---:|:---:|:---|
| Microsoft.Extensions.Logging |  | 9.0.17 | 0c586c9b |
| System.Drawing.Common |  | 9.0.17 | 0c586c9b |
| System.Text.Json |  | 9.0.17 | 0c586c9b |
| System.Memory |  |  | 0c586c9b |
| System.Threading.Tasks.Extensions |  |  | 0c586c9b |

## Všechny commity

| ID commitu | Popis |
|:---|:---|
| 4f8e1656 | Commit upgrade plan |
| bd40fceb | Uložit konečné změny pro krok Převeďte projekt Pokladna.csproj na projekt ve stylu sady SDK. |
| 8549fa0b | Uložit konečné změny pro krok Upgradujte projekt Pokladna.csproj. |
| 0c586c9b | Refactor: Clean up Pokladna.csproj dependencies |
| 1f6e30cc | refactor: Upgrade project to .NET 9 |

## Další kroky

- Zkontrolujte a otestujte funkčnost aplikace, abyste se ujistili, že po upgradu vše funguje podle očekávání.
- Zvažte refaktorování kódu, abyste využili nové funkce a vylepšení dostupné v .NET 9.
