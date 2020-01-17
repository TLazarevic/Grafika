# Grafika
Projektni zadatak 13.2–Kotrljanje bureta

Modelovanje statičke 3D scene (prva faza): 

Uključiti testiranje dubine i sakrivanje nevidljivih površina. Definisati projekciju u perspektivi (fov=60, near=1, a vrednostfar zadati po potrebi) i viewport-om preko celog prozora unutar Resize metode. 
Koristeći AssimpNet bibloteku i klasu AssimpScene, importovati model bureta.Ukoliko je model podeljen u nekoliko fajlova, potrebno ih je sve učitati i iscrtati. Skalirati model, ukoliko je neophodno, tako da u celosti bude vidljiv.
Modelovati sledeće objekte: 
Podlogu površine mora koristeći GL_QUADS primitivu, 
držač bureta koristeći Cube klasu, i
rupu kroz koje bure propada koristeći Disk klasu
Ispisati 3Dtekst žutom bojom u donjem desnom uglu prozora (redefinisati projekciju korišćenjem gluOrtho2D metode). Font je Arial, 14pt, bold. Tekst treba biti oblika: 
Predmet: Racunarska grafika 
Sk.god: 2019/20.
Ime: <ime_studenta>
Prezime: <prezime_studenta>
Sifra zad: <sifra_zadatka>

Predmetni projekat - faza 1 sačuvati pod nazivom: PF1S13.2. Obrisati poddirektorijume bin i obj. Zadaci se brane na vežbama, pred asistentima.
Vreme za izradu predmetnog projekta – faze 1 su dve nedelje. Predmetni projekat – faza 1 vredi 15 bodova. Način bodovanja je prikazan u tabeli.


Definisanje materijala, osvetljenja, tekstura, interakcije i kamere u 3D sceni  (druga faza):

Uključiti color tracking mehanizam i podesiti da se pozivom metode glColor definiše ambijentalna i difuzna komponenta materijala.
Definisati tačkasti svetlosni izvorbele boje i pozicionirati ga gore desno u odnosu na centar scene(na pozitivnom delu vertikalnei horizontalne ose). Svetlosni izvor treba da bude stacionaran (tj. transformacije nad modelom ne utiču na njega). Definisati normale za podlogu, držač i disk. Uključiti normalizaciju.
Za teksture podesiti wrapping da bude GL_REPEAT po obema osama. Podesiti filtere za teksture da budulinearnofiltriranje. Način stapanja teksture sa materijalom postaviti da bude GL_ADD. 
Držaču bureta pridružiti teksturu drveta. Rupi kroz koju bure propada pridružiti teksturu metala.Definisati koordinate tekstura.
Podlozi pridružiti teksturu betona (slika koja se koristi je jedan segment mora).Pritom obavezno skalirati teksture (shodno potrebi). Skalirati teksture korišćenjem Texture matrice. Definisati koordinate teksture.
Pozicionirati kameru,tako da gleda na scenu sa leve strane, odgore (ne previše izdignuta od podloge). Koristiti gluLookAt() metodu.
Pomoću ugrađenih WPF kontrola, omogućiti sledeće:
odabir faktora skaliranja bureta po visini
izbor boje reflektorskog svetlosnog izvora, i
pomeranje reflektorskog svetlosnog izvora po horizontalnoj osi
Omogućiti interakciju korisnika preko tastature: sa Qse izlazi iz aplikacije, sa tasterima 
W/Svrši se rotacija za 5 stepeni oko horizontalne ose, sa tasterima A/Dvrši se rotacija za 5 stepeni oko vertikalne ose, a sa tasterima +/- približavanje i udaljavanje od centra scene. Ograničiti rotaciju tako da se nikada ne vidi donja strana horizontalne podloge i da scena nikada ne bude prikazana naopako.
Definisati reflektorski svetlosni izvor (cut-off=30º)crveneboje iznadbureta. 
Način stapanja teksture sa materijalom za modelburetapostaviti na GL_ADD.
Kreirati animaciju kotrljanja bureta. Animacija treba da sadrži sledeće:
Na početku se bure nalazi u držaču koji se otvara preko rotacije.
U trenutku kada se otvori, bure kreće da se kotrlja niz padinu koja se nalazi na podlozi sve dok ne dođe do rupe u koju propada.
	U toku animacije, onemogućiti interakciju sa korisnikom (pomoću kontrola korisničkog interfejsa i tastera). Animacija se može izvršiti proizvoljan broj puta i pokreće se pritiskom na taster C. 


Neophodne teksture pronaći na Internetu.Predmetni projekat - faza 2 sačuvati pod nazivom: PF2S13.2. Obrisati poddirektorijume bin i obj. Zadaci se brane na vežbama, pred asistentima.
Vreme za izradu predmetnog projekta – faze 2 su četiri nedelje. Predmetni projekat – faza 2 vredi 35 bodova. Način bodovanja je prikazan u tabeli.
 

