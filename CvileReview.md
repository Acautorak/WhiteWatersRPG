# Cvile Review

* Ne implementiraj stvari koje se jos ne koriste, i odmah brisi stvari koje se vise ne koriste. Npr polja u Consumable.
Neces predvideti sta ce ti trebati u buducnosti, niko to ne ume, implementiraj samo ono sto ti u tom trenutku treba.
Ne cuvaj kod koji se ne koristi, brisi ga odmah, i ne komentarisi kod nikad nego ga uvek brisi.
Nekorisceni kod cini da se teze cita i odrzava projekat, a nema nikakvih benefita jer ces uvek na kraju uraditi drugacije.
* DontDestroyScene je lose ime, sustinski to je LoadingScene, i skroz je ok da postoji kao prva scena u tvojoj igri.
Slicno tako FirstManager je mnogo cudno ime za klasu, a to je SceneLoader i treba tako da se zove.
I kad si vec kod toga, ima smisla da koristis SceneLoader uz tvoj SceneIndex uvek, umesto Unity-evog ucitavanja scena.
* Ne reimplementiraj singleton patern svaki put, kad-tad ces pogresiti ili hteti da refaktorises iz korena.
Umesto toga ubaci jedan genericki singleton i koristi njega uvek. Ubacicu ti ja na drugu granu, sa primerima.
* Svako polje u monobehaviour treba da bude ili [SerializedField] private, ili public ali da nije serijalizovano.
Jer ako je samo obicno public, onda se ono potencijalno koristi i u public APIju i za serijalizaciju, a to brka njegovu svrhu i dodaje kompleksnost.
* Projekat ima previse MonoBehaviour-a, svaka glupa klasica ti je MonoBehaviour. 
Njima treba vise babysittinga i treba da budu last resort. Ako moze obicna klasa, ako ne onda ScriptableObject, ako ne onda tek MonoBehaviour.
Npr BaseAction i sve akcije bas nema potreba, niti je prirodno da budu monobehaviours.
Sa njima si implementirao prakticno strategy pattern, tako da tako i uradi: umesto BaseAction napravi IAction interfejs,
sve akcije ga nasledjuju i NISU monobehaviours, a onda sav zajednicki kod za sve akcije se nalazi u npr ActionExecutor koji moze da bude MonoBehaviour ako zelis.
Drugi primer -- kad imas vise svojih monobehaviours na istom game objektu uvek -- to je znak da treba da se spoje u jednu skriptu.
* Koristi log levels ispravno -- uvek LogError samo za prave greske, uvek Log samo za dodatan info i debug stvari.
* GetComponent koristi sto redje, skoro nikad. Umesto toga biraj SerializedField.
Ako ikad koristis GetComponent, radis to SAMO na skripti koja je na istom gameobject-u kao ta komponenta, i samo uz [RequireComponent] atribut.
* static event EventHandler izbaci iz price. To sada koristis umesto da imas "global event bus". Dacu ti jedan, zove se Notifier :)
* Generalno EventHandler i EventArgs ti ne treba nigde. Umesto EventHandler koristi Action ili Func ili pravi svoje delegates.
* Scripts/GameManagers folder nije koristan, bolje svaki manager ubaci u folder "feature-a" kome pripada.
* Sta god ne mora da bude public, prebaci u private -- uvek u svakom tipu programiranja, u svakom jeziku.
* Imas apsolutno previse singleton-a, bukvalno skoro koliko Viola's Quest vec.
Gledaj da imas mozda jedan-dva singleton-a u celoj igri, a da sve sisteme i feature-e uvezes u hijerarhije gde prirodno znaju jedni za druge koliko treba.
* UnifiedActionManager je frankenstajn. U njoj imas mnogo znakova da to treba da bude jedan ceo sistem, podeljen na desetak manjih pure c# klasa.
Za pocetak ono kad imas sekcije za polja, znaci da je klasa vec u metastazi.
Headeri su takodje obicno znak da ti treba vise klasa u kompoziciji, npr u klasi UnitStats.
* Uvek pred svaki AddListener treba uraditi RemoveAllListeners. Dacu ti extension metodu za SetListener koja radi to automatski.
* Kad god se na bilo sta subscribujes, moras se unsubscribe-ovati kad ti vise ne treba, npr u UnitWorldUI.
