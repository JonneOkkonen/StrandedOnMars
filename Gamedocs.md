## Sisällysluettelo 

1. [Johdanto](#johdanto)
1. [Pelin Tarina](#pelin-tarina)
1. [Keybinds](#keybinds)
1. [Gameloop](#gameloop)
1. [Peli vs. demo](#peli-vs-demo)
1. [Käytetyt teknologiat](#käytetyt-teknologiat)
1. [Optimointi](#optimointi) 
1. [Ilmaiset resurssit](#käytetyt-teknologiat)
1. [Itsearviointi](#itsearviointi)
1. [Tunnetut bugit](#tunnetut-bugit)


## Johdanto

Stranded peli on sekä FPS, että survival shooter kategorian peli, jossa pelaajan tavoitteena on päästä pelaustautua vieraalta planeetalta, johon hän on tehnyt hätärikon.
Vieraalla planeetalla on mysteerinen tyhjä rakennus ja jokapuolella on outoja tuntemattomia hirviöitä. Pelaajamme on keksittävä tapa päästä planeetalta pois ehjin nahoin.

## Pelin tarina

On vuosi 8 452, ihmiskunta on onnistunut kehittämään planeettojenvälisen avaruusmatkailun ja on nopeasti laajentanut ja kolonisoinut muita mailmoja. Meidän matkaaja oli osana 
kauppa-saattuetta joka joutui avaruuspiraattien hyökkäyksen kohteeksi ja kaaoksen aikana jolloin kaikki menetettiin, matkaajamme onnistui pääsemään pelastushätäkapseliin ja 
pakoon. Matkaajamme haaksirikkoutui Marssiin ja on Jätetty sinne.

## Keybinds

W = Liiku eteenpäin  
A = Liiku vasemmalle  
S = Liiku taaksepäin  
D = Liiku oikealle  
Nuoli ylos = Liiku eteenpäin  
Nuoli vasemmalle = Liiku vasemmalle
Nuoli alas = Liiku taaksepäin
Nuoli oikealle = Liiku oikealle
Q = Toimenpide  
R = Lataa ase  
E = Käytä ostoautomaattia/poistu ostoautomaatista/aseta pelastusmajakka (beacon)  
Pidä B = Osta beacon  
Tab = Näytä tehtäväruutu  
Shift = Juokse  

## Gameloop

Pelaajalla on rajalliset resurssit käytössään ja hänen tulee miettiä tarkkaan tekemisiään. Pelaajalle on annettu 100 elämäpointtia (Hp = Health point),
happea 300 sekunniksi (s = seconds), staminabaari jossa on rajallimen määrä staminaa jouksemiseksi, sekä pistooli jossa on rajallinen määrä ammuksia.
Näistä ainoastaan stamina latautuu itsestään tietyllä nopeudella, muut resurssit on pelaajan itse haettava tukikohdasta eri toimintoja suorittamalla.

Sitä ennen on pelaajan kuitenkin ensin löydettävä tukikohta ja pistoolinsa avulla hänen on ensin puhdistettava tukikohdan ympärillä olevat hirviöt. Sen jälkeen hänen on
vielä löydettävä rikki olevan tukikohdan vialla oleva komponentti ja käydä korjaamassa tämä ennen kuin häneltä loppuu happi ja ammukset. Tämän jälkeen pelaaja saa tukikohdan
käyttöönsä.

Tämän jälkeen pelaaja voi käyttää tukikohtaa niin paljon kuin hän haluaa ja se toimii eräänlaisena ankkurina ja tukimajakkana, jonka kautta pelaaja voi operoida ympäristössään.
Tukikohdasta pelaaja saa aina happipullonsa mittarin täyteen tukikohtaan astuessaan sisään ja paineistamalla oven ja astumalla sisään tutkikohtaan. Tukikohdassa ikkunan äärellä 
on kasveja, jotka generoituvat ajan kanssa uudelleen ja niitä syömällä pelaaja saa lisää elämäpisteitä jos hän on menettänyt niitä. Tulokulmasta vasemmalla oven vieressä on
ostoautomaatti, jota kautta pelaaja voi ostaa pistoolinsa luoteja ja pelastumiseen vaaditun pelustusmerkkimajakan.

Pelastusmerkkimajakka voidaan ainoastaan kutsua kun pelaaja pääsee tarpeaksi korkealle vuorille ja asettaa majakan rinteelle.
Tämä aloittaa loppuskenaarion, jossa kaikki viholliset agroutuu höykkäämään pelaajaa päin ja pelaajan on selvittävä kunnes pelastusryhmä saapuu ja pelastaa. Selvitessään
loppuskenaarion pelaaja voittaa pelin.

## Peli vs. demo

## Käytetyt teknologiat

Peli ohjeistaa pelaajaa reaaliajassa sekä audion avulla, että tabulaattorista saatavalla näytä tehtäväruudulla näppäimellä.

## Optimointi

## Ilmaiset resurssit

__Ilmaisten resurssien kirjasto:__



## Itsearviointi

### Jonne Okkonen (M2235)

### Joonas Niinimaki (M3268)

## Tunnetut bugit