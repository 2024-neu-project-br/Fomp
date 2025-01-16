# Fomp

## Felhasználói dokumentáció

Ez a játék egy Tamagotchi-szerű működéssel bír, ahol egy rókát kell gondoznunk.

![](https://t9012260663.p.clickup-attachments.com/t9012260663/c9f1dd61-2e63-4ae0-8e95-9086b3e6f716/K%C3%A9perny%C5%91k%C3%A9p%202024-12-07%20062938.png)

## Mentési logika

A játékban 3 slot ("játékállás mentési hely") áll rendelkezésünkre, ezeket a játékon belül felül is írhatjuk. Ezen mentéseket _.fomp_ fájlkiterjesztésű fájlokba menti a játék, melyből elő is tudja hívni a játékállást, és ezt követően az eltelt időnek megfelelő eseményeket "megtörténteti".

![](https://t9012260663.p.clickup-attachments.com/t9012260663/c7ef9a12-ef52-408d-a0ea-3337eb3d59e6/k%C3%A9p.png)

## Játékmenet

A rókánknak 3 fő igényét kell kielégítenünk, ezek a következők:

- **Boldogság**

  Ezt a róka megsimogatásával tudjuk szinten tartani / feltölteni.

  Legkisebb értéke: 100

  Legnagyobb értéke: 0

- **Éhség**

  Ezt a róka megetetésével tudjuk szinten tartani / feltölteni.

  Legkisebb értéke: 0

  Legnagyobb értéke: 100

- **Energia**

  Ezt a róka elaltatásával tudjuk szinten tartani / feltölteni.

  Legkisebb értéke: 0

  Legnagyobb értéke: 100

Ha ezen igények bármelyike kielégítettségében túl alacsony vagy éppen túl magas értéket érne el, akkor a rókánk szól róla.

![](https://t9012260663.p.clickup-attachments.com/t9012260663/35599128-72db-4562-aa2f-2a80d05373f1/k%C3%A9p.png)

Illetve ha ennek ellenére ezeket a figyelmeztetéseket figyelmen kívül hagyjuk, a rókánk életerőt fog veszíteni, amely a róka elpusztulásához is vezethet.

![](https://t9012260663.p.clickup-attachments.com/t9012260663/19b3b4fd-6c51-485a-80ef-5c1ab342a47b/k%C3%A9p.png)

Ezeknek az értékeknek a frissitése 600000 miliszekundumonként (10 percenként) történik, ezzel kalkulál a játék egy mentés betöltése esetén, amikor megújítja ezeket a játék betöltési idejét is felhasználva.

A felhasználő által végrehajtott műveletek után van egy "cooldown" ("várakozási idő"), ez 5000 miliszekundum (5 másodperc), ezalatt az idő alatt az adott műveletet nem hajtható újra végre.

## Fejlesztői dokumentáció

A program három fő osztályt használ arra, hogy megjelenítse és kezelje a felhasználói felületet, ezek a következők:

- [DisplayHelper](https://github.com/2024-neu-project-br/Fomp/blob/main/Foxo/DisplayHelper.cs)

  Ez az osztály felel a fontos elemek konzolra való kiíratásáért, konkrétabban a következőkért:

  - A "FOMP" logo kiíratásáért
  - Az opciók kiíratásáért:
    - Kiválasztható menüelemek
    - Játékállás mentési helyek/slot-ok

- [MenuHelper](https://github.com/2024-neu-project-br/Fomp/blob/main/Foxo/MenuHelper.cs)

  Ez az osztály a menürendszer kezeléséért felel, ehhez a [DisplayHelper](https://github.com/2024-neu-project-br/Fomp/blob/main/Foxo/DisplayHelper.cs) osztályt nagy mértékben veszi igénybe, ezeket a menüket jelenítteti meg (felhasználó által navigálható faszerkezetben):

  - _MainMenu_
    - _NewGameSlotMenu_ (Ez a menü csak akkor jelenik meg, ha nem található egy játékmentés sem)
      - **InitGame**
    - _SlotMenu_ (Ez az általános menü, ahol kiválaszthatunk egy játékmentési helyet/slot-ot)
      - _EmptySlotMenu_ (Ez a menü akkor jelenik meg, ha a helyen/slot-on nincsen mentett játékállás)
        - **InitGame**
      - _NonEmptySlotMenu_ (Ez a menü akkor jelenik meg, ha a helyen/slot-on van mentett játékállás)
        - **InitGame**

- [InputHelper](https://github.com/2024-neu-project-br/Fomp/blob/main/Foxo/InputHelper.cs)

  Ez az osztály felel a felhasználói bemenetek kezeléséért/ellenőrzéséért, ezeket a metódusokat használja ehhez:

  - GetValidatedInput<T>(string prompt, Func<string, bool> validationFunc)

    Ez a metódus egy ún. "prompt"-ot ad vissza, ahol a felhasználó bemenetét validálja, majd visszatér T típusú bemenettel.

    **string prompt**: ezt az üzenetet jeleníti meg a felhasználói felületen mielőtt bekéri a bemenetet

    **Func<string, bool> validationFunc:** ezzel a metódussal validálja T tipusú bemenetet, majd visszatér a funkció kimenetével, hiba esetén ezelőtt azt kiíratja a konzolra

  - CreateValidationFuncForInt(int min, int max)

    Ez a metódus egy validációs funkciót generál int típushoz, amely később felhasználható a GetValidatedInput<T> metódusban.

    **int min**: a szám minimum értéke

    **int max**: a szám minimum értéke

  - CreateValidationFuncForStr(int min, int max)

    Ez a metódus egy validációs funkciót generál string típushoz, amely később felhasználható a GetValidatedInput<T> metódusban.

    **int min**: a szöveg minimum karakterszáma

    **int max**: a szöveg maximum karakterszáma

  - CreateValidationFuncForChar(char\[\] charList)

    Ez a metódus egy validációs funkciót generál char típushoz, amely később felhasználható a GetValidatedInput<T> metódusban.

    **char\[\] charList**: elfogadott karakterek tömbje

A program játék részét két osztály kezeli, ezek a következők:

- [Game](https://github.com/2024-neu-project-br/Fomp/blob/main/FoxoLib/Game.cs)

  Ez az osztály kezeli a tényleges játék elemeinek jelentős részét, a konstruktorát beleértve, ahol betölti az adott játékmentési helyen/slot-on található mentett játékállást, ha ez nem létezik, akkor a nullable string típusú fompName változót felhasználva létrehoz egy új mentést, és ezen a néven fogja elnevezni a rókát\*, majd ezt a játékállást be is tölti.

  Ezen kívül még használ néhány metódust, ezek a következők:

  - SaveState

    Ez a metódus elmenti a játékállást az adott mentési helyre/slot-ra.

  - OnTimeElapsed

    Ez a metódus akkor fut le, ha eltelt az UpdateFrequency változóban miliszekundumokban megadott időintervallum, ilyenkor frissíti a róka tulajdonságainak értékét, újrarajzolja/rendereli a rókát, majd elmenti ezt a játékállást a SaveState metódus segítségével.

  \*minden játékmenethez tartoznia kell pontosan egy rókának.

- [Fomp](https://github.com/2024-neu-project-br/Fomp/blob/main/FoxoLib/Fomp.cs)

  Ez az osztály felel a róka állapotának megjelenítéséért a tulajdonságainak értékéért, illetve a program bezárt állapota alatt megtörtént események "megtörténttetéséért", ezeket több metódussal teszi, ezek a következők:

  - Update

    Ez a metódus akkor van meghívva, ha eltelt a játék UpdateFrequency változójában miliszekundumokban megadott időintervallum, illetve akkor, ha a játékos végrehajt egy műveletet, amely meg tudja változtatni a róka egy adott tulajdonságának értékét.

    Ha a róka alszik, akkor a tulajdonságainak értéke kevésbé fog romlani, mintha ébren lenne, de ha elaltatjuk a rókát, az automatikusan felébred, ha az energiaszintje eléri a 100-at.

    Ha a róka egyik tulajdonságának értéke 40 álá csökken vagy 40 felé emelkedik, akkor a rókánk szólni fog az adott tulajdonság romló értékéről, ezt a Render metódus használatával meg is tudjuk jeleníteni.

    Ha a róka egyik tulajdonságának értéke eléri a legrosszabb lehetséges értéket (0 vagy 100), akkor a róka veszít egy életet és az adott tulajdonság értéke visszaáll a legjobb lehetséges értékre.

    Ha a róka elpusztult, akkor a metódus meghívásának semmilyen hatása nem lesz.

  - Render

    Ez a metódus felel a róka állapotának megjelenítéséért. Először felső állapotkijelző sávot írja újra, majd áttér a róka arckifejezésének újraírására, legutolsósorban pedig az alsó mini útmutatót írja ki a konzolra, mindehhez használ különböző kalkulációkat, illetve mozgatja a kurzort minden újraírásnál, így nagyban megelőzve a képernyő villódzását.

    A róka arckifejezésének kiírása két részre lett felbontva:

    - moodExpression (például: /ᐠ˵- ᴗ -˵ マ)
    - moodString (például: ᶻ 𝗓 𐰁)

    Ezt a két string típusú változót összefűzi a program, és a példák alapján kiadott érték a következő lesz: /ᐠ˵- ᴗ -˵ マ - ᶻ 𝗓 𐰁

    Ugyanezen a módon jelzi a rókánk, ha egyik tulajdonságának értéke jelentősen romlott.

  - Feed\*

    Ez a metódus megnézi, hogy a róka alszik-e, illetve, hogy letelt-e a cooldown/timeout amelyet a művelet előző végrehajtásakor állítottunk be, ha ezek a feltételek megfelelőek (nem alszik a róka és letelt a cooldown/timeout), akkor a róka éhség szintje csökken 5-el, ha az 5 alatt van, akkor visszaáll 0 értékre.

  - ToggleSleep\*

    Helyzettől függően elaltatja vagy felébreszti a rókát.

  - Pet\*

    Ez a metódus megnézi, hogy a róka alszik-e, illetve, hogy letelt-e a cooldown/timeout amelyet a művelet előző végrehajtásakor állítottunk be, ha ezek a feltételek megfelelőek (nem alszik a róka és letelt a cooldown/timeout), akkor a róka boldogság szintje növekszik 5-el, ha az 95 felett van, akkor visszaáll 100 értékre.

  A \*-al jelölt metódusok lefutásukkal meghívják a Render metódust is.

Ha a program a játékmentés betöltése közben hibába ütközik, akkor [SaveFileException](https://github.com/2024-neu-project-br/Fomp/blob/main/FoxoLib/SaveFileException.cs)\-t dob, amelynek szövegét kiíratja a konzolra, így a felhasználó is értesül róla.
