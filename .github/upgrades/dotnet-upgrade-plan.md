# Plán upgradu na .NET 9.0

## Kroky provedení

Následující kroky proveďte postupně, jeden po druhém, v uvedeném pořadí.

1. Ověřte, že je na počítači nainstalována sada .NET 9.0 SDK, která je pro tento upgrade vyžadována, a pokud ne, pomozte s její instalací.
2. Ujistěte se, že verze sady SDK zadaná v souborech global.json je kompatibilní s upgradem na .NET 9.0.
3. Převeďte projekt Pokladna.csproj na projekt ve stylu sady SDK.
4. Upgradujte projekt Pokladna.csproj.

## Nastavení

Tato část obsahuje nastavení a data používaná v krocích provádění.

### Souhrnné úpravy balíčků NuGet napříč všemi projekty

Balíčky NuGet použité ve všech vybraných projektech nebo jejich závislostech, které vyžadují aktualizaci verze v projektech, jež na ně odkazují.

| Název balíčku                   | Aktuální verze | Nová verze | Popis                                                 |
|:--------------------------------|:--------------:|:----------:|:------------------------------------------------------|
| Microsoft.Extensions.Logging    | 10.0.9         | 9.0.17     | Doporučeno pro .NET 9.0                               |
| System.Drawing.Common           | 10.0.9         | 9.0.17     | Doporučeno pro .NET 9.0                               |
| System.Memory                   | 4.6.3          |            | Funkce balíčku je součástí nové cílové architektury   |
| System.Text.Json                | 10.0.9         | 9.0.17     | Doporučeno pro .NET 9.0                               |
| System.Threading.Tasks.Extensions | 4.6.3          |            | Funkce balíčku je součástí nové cílové architektury   |

### Podrobnosti o upgradu projektu
Tato část obsahuje podrobnosti o upgradu každého projektu a úpravách, které je třeba v projektu provést.

#### Úpravy projektu Pokladna.csproj

Změny vlastností projektu:
  - Cílová architektura by se měla změnit z `net48` na `net9.0-windows`

Změny balíčků NuGet:
  - Microsoft.Extensions.Logging by měl být aktualizován z `10.0.9` na `9.0.17` (*doporučeno pro .NET 9.0*)
  - System.Drawing.Common by měl být aktualizován z `10.0.9` na `9.0.17` (*doporučeno pro .NET 9.0*)
  - System.Text.Json by měl být aktualizován z `10.0.9` na `9.0.17` (*doporučeno pro .NET 9.0*)
  - System.Memory by měl být odebrán (*součást architektury*)
  - System.Threading.Tasks.Extensions by měl být odebrán (*součást architektury*)

Ostatní změny:
  - Projekt musí být převeden na styl sady SDK.
