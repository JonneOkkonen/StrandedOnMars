# Stranded peli

* V1.0.0.0 Pre-Alpha Demo  
* TTOS0700 Peliohjelmointi  
* Jyväskylän ammattikorkeakoulu (JAMK)  
* Kevät 2020  
* Julkaistu 26.4.2020  
* Tekijät: Jonne Okkonen (M2235) ja Joonas Niinimäki (M3268)  
* Copyright (C) 


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
Stranded peli on kevään 2020 aikana kehitetty peliprojekti osana TTOS0700 peliohjelmoinnin kurssia. Se on kehittänyt Jyväskylän Ammattikorkeakoulun kaksi 2. vuoden
ohjelmistotekniikan opiskelijaa Jonne Okkonen (M2235) ja Joonas Niinimäki (M3268).

## Pelin tarina

On vuosi 8 452, ihmiskunta on onnistunut kehittämään planeettojenvälisen avaruusmatkailun ja on nopeasti laajentanut ja kolonisoinut muita mailmoja. Meidän matkaaja oli osana 
kauppa-saattuetta joka joutui avaruuspiraattien hyökkäyksen kohteeksi ja kaaoksen aikana jolloin kaikki menetettiin, matkaajamme onnistui pääsemään pelastushätäkapseliin ja 
pakoon. Matkaajamme haaksirikkoutui Marssiin ja on Jätetty sinne.

## Keybinds

__W__ = Liiku eteenpäin  
__A__ = Liiku vasemmalle  
__S__ = Liiku taaksepäin  
__D__ = Liiku oikealle  
__Nuoli ylos__ = Liiku eteenpäin  
__Nuoli vasemmalle__ = Liiku vasemmalle  
__Nuoli alas__ = Liiku taaksepäin  
__Nuoli oikealle__ = Liiku oikealle
__Q__ = Toimenpide/Nosta ase näkyville ja laskle se pois näkyvistä
__Oikea hiiren klikkaus__ = Ammu pistoolilla  
__R__ = Lataa ase  
__E__ = Käytä ostoautomaattia/poistu ostoautomaatista/aseta pelastusmajakka (beacon)  
__Pidä B__ = Osta beacon  
__Tab__ = Näytä tehtäväruutu  
__Shift__ = Juokse  

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

Tässä osiossa käsitellään pelidemon suunnitelmallisia perusteluita, peliprojektin ratkaisuja ja muita suunnitteluun ja toteutukseen liittyviä asioita.  
Koska kyseessä on pelin demoversio, tarkoitus ei ole suunnitella kaikkia pelin arvoja sellaiseen muotoon, joka julkaisuversiossa olisi. Toisaalta pelattavuuden ja pelin loopin 
sisäiset arvot on pyritty asettamaan sellaisiksi, että niistä näkee jo suoraan pelimekaniikkoja sellaisenaan kuin ne julkaisuversiossa olisi.

Tästä esimerkkinä toimii se ero, että demoversiossa pistoolien lippaiden hinta on suunnitelmallisesti ja tarkoituksella jätetty pieneksi, jotta testaaja pystyy pelaamana pelin
kohtuullisen vaivattomasti läpi. Toisaalta pelaajan omat resurssit, kuten elämäpisteet, happi ja stamina, kuten myös vihollisten tekemä vahinko on laskettu vastaamaan sellaisia 
arvoja, joita ne olisi julkaisuversiossakin.

## Käytetyt teknologiat

Aloitusnäkymässä pelaajan aloitettua pelin hän katsoo hätäpelastuskapselia päin, johon on visuaaliseksi efekteiksi tehty kipinöitä ja lisätty savu-efekti näyttämään
hätäkapsulin kohtaamaa vahinkoa pakkolaskun seurauksena.

Pelaaja on käytännössä pelkästään kamera, joka seuraa pelaajan näkymää ensimmäisestä persoonasta ja pelaajaobjekti itse on renderöimätön kapsuli. Painamalla Q-näppäintä pelaaja 
voi nostaa pistoolin näkymäänsä, jolloinka pistooli renderöidään näkyviin ja pois näkyvistä nappia painamalla. Samalla kun pistooli renderöidään näkyviin, pelaajan ruudun
keskelle renderöidään crosshair-tähtäin, jonka mukaan pelaaja voi navigoida tähtäintään ampuakseen kohdetta.

Pelaajan muita UI elementtejä on elkkupisteet, joiden arvot riippuvat pelaajan ottamasta vahingosta, jatkuvasti laskeva happimittari, sekä stamina baari, joista ainoastaan
staminabaari generoi arvoaan itsestään tiettyä vauhtia sen jälkeen kun pelaaja päästää Shfit-näppäimestä irti. Muita pelaajan ja UI:n elementtien arvojen manipulaatiota
käsitellään möyhemmässä vaiheessa tätä osiota.

Peli ohjeistaa pelaajaa reaaliajassa sekä audion avulla, että tabulaattorista saatavalla näytä tehtäväruudulla näppäimellä. Pelitehtävistä peli vielä erikseen ilmoittaa ruudun 
yläkulmalla keskellä, että pelaajalle on tullut uusi tehtävä. Sen ja triggereiden avulla toimivien audio-ohjeistuksen avulla pelaajalle ohjeistetaan mitä hänen tulisi tehdä
edetäkseen pelissä eteenpäin.

Ensimmäisenä tehtävänä pelaajan on löydettävä tukikohta ja pelaajan on ammuttava sen ympäriltä kaikki hirviöt ja koodi käy läpi ja tsekkaa, että pelaaja on onnistuneesti tehnyt
tehtävät kuten pitää ennen seuraavan antamista. Moni toiminto vaatii pelaajalta nappulan pitämistä pohjaan hetken ja pelaaja saa tästä UI:hin näytetyn näppäin-kehotuksen ja
ohjeistuksen tarpeen mukaan. Esimerkiksi asetta ladatessa näytetään ruudulla teksti "reloading...", jotta pelaaja tietää mitä tehdään, ja pelin UI päivittää pelaajan lippaan 
ammusmäärän ja varastossa olevien luotien määrän reaaliaikaisesti näkymässä.

Pelaajan UI näyttää reaaliajassa pelaajalla käytettävät resurssit, joita pelaaja saa lisää tukikohdasta lisää. Happilukema päivittyy samantien kun pelaaja paineistaa
happiluukun onnistuneesti ja menee sisälle tukikohtaan. Elämäpisteitä pelaaja saa lisää menemällä kasvien luokse, painamalla toimintonäppäintä syödäkseen kasveja. Ne kasvavat
takaisin tietyn ajan kuluttua.

Mikäli pelaaja kokee vahinkoa, renderöidään pelaajan ruutuun punainen välähdys indikoimaan vahingon saantia. Tämä helpottaa tilanteen lukemista vahngon saadessa. Sen lisäksi 
peli huomauttaa pelaajalle kun tämän jokin resurssi on loppumassa stamina baarin resurssia lukuunottamma. Mikäli pelaaja kuolee, pelaajalle renderöidään näytölle punainen
kuolemis-ruutu ja pelaaja joutuu aloittamaan pelin uudestaan.

Tukikohdan sisällä on myös ostoautomaatti, josta pelaaja voi ostaa lisää pistoolin ammuksia tai pakoon tavittavan pelastusmajakan (beacon). ATM UI renderöidään pelaajalle
toisella kameralla, joka aktivoituu kun pelaaja vuorovaikuttaa ostoautomaatin kanssa ja siihen on lisätty ohjeistus myös sen käytöstä, jotta pelaaja tietää miten ostaa itselleen 
resursseja ja miten poistua ostoautomaatista. Pelastusmajakan (beacon) ostettua pelastusmajakka liimautuu ja renderöidään pelaajan UI:ssa indikoimaan, että pelaajalla on nyt se 
objekti mukana.

Metalon vihollishirviöt ovat animoitu kävelemään, hyökkäämään kahdella eri animaatiolla ja kuolemaan riippuen pelaajan ja niiden vuorovaikutuksesta. Niille on tehty kontrolleri, joka mahdollistaa myös
vaihtoehtoisessa maastokorkeudessa liikkumisen suhteellisen sulavasti ja näin pelaaja ei voi hyväksikäyttää maastoa päästäkseen vihollisten ulottumattomiin. Tästä poikkeuksena
on itse tukikohta, jonka katto on erikseen maalattu kulkemattomaksi alueeksi keneltäkään. Jokaiselle Metalonille on annettu eri arvot ja esiintymismäärät, joiden tarkoitus on 
lisätä vihollistyypin monipuolisuutta.

Pelastusmajakka (beacon) elementti voidaan ainoastaan laittaa tietyllä korkeudella, eli sen laiton UI-kehotetta ei renderöidä ennen kuin peli saa tarkistettua, että pelaaja on 
tarpeaksi korkealla, eikä pelaaja voi vuorovaikuttaa sen objektin kanssa ennen kuin tämä tarkistus palauttaa oikean arvon. Asetettuaan pelastusmajakan peli renderöi sen
peliobjektin kentälle ja se alkaa renderöimään signaalia indikoivia renkaita. Samalla pelaajan alle tulee valokeila indikoimaan pelastusaluksen saapumista. Kun pelaaja pelastuu 
pelaajan objekti alkaa nousemaan ylöspäin ja pelaajalle renderöidään voittonäkymä.

Liikkumisaluetta on rajattu näkymättömien seinien avulla niin, että pelaaja ei voi vahingossakaan liikkua pelimaailman ulkopuolelle ja tippua ulos pelikentästä.
Lisäksi rajaus on asetettu niin, että se ei kata koko kenttää, jotta se luo hieman sellaista illuusiota, ettei pelaaja kävelekkään vain pienellä maaston keinotekoisella
saarella vaan planeetan näkymä jatkuu horisonttiin. Näin luodaan isomman maailman illuusiota.

## Optimointi

## Ilmaiset resurssit

__Ilmaisten resurssien kirjasto:__

* [Metalon hirviö](https://assetstore.unity.com/packages/3d/characters/creatures/meshtint-free-polygonal-metalon-151383)
* [Mars](https://assetstore.unity.com/packages/3d/environments/landscapes/mars-landscape-49808)
* [Tukikohta](https://assetstore.unity.com/packages/3d/environments/sci-fi/sci-fi-styled-modular-pack-82913)
* [Pistooli](https://assetstore.unity.com/packages/3d/props/guns/sci-fi-gun-162872)
* [Avaruusalus](https://assetstore.unity.com/packages/3d/props/guns/sci-fi-gun-162872)
* [Avaruusaluksen savu-efekti](https://assetstore.unity.com/packages/vfx/particles/white-smoke-particle-system-20404)
* [ATM ostoautomaatti](https://assetstore.unity.com/packages/3d/environments/sci-fi/atm-95057)
* [Text-to-speech NaturalReaders-sivu](https://www.naturalreaders.com/online/)


## Itsearviointi

### Jonne Okkonen (M2235)

### Joonas Niinimaki (M3268)

## Tunnetut bugit

Tässä osioissa dokumentoidaan peliprojektissa tunnistetut bugit:

* 


