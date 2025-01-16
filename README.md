# Fomp

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
