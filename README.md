# KassaSystemet

Programmera ett kassasystem – (som man har i kassan i en matbutik)
Produkter i kassasystemet lagras i en fil. Följande data ska lagras på Produkt:
        
        *produktid (snabbkommando i kassan, ex ”300” för bananer nedan)
        *produktnamn
        *pris
        *pris typ – är det per kilo eller per styck

Exempel på två kommandon i kassan:
        
        <produktid> <antal> ex 300 1, betyder lägg till en av produktid

        PAY = vi ”fejkar” att det betalas och kvittot sparas ned (se nedan) och vi kommer tillbaka till menyn

# Krav för godkänt

        *Funktionalitet enilgt ovan (OBS: Du MÅSTE följa specifikationen – tex inmatningen av produkt och antal MÅSTE ske enligt ”300 2” på en rad med mellanslag emellan)

        *Kvitton sparas ned vid PAY till en fil RECEIPT_yyyyMMdd.txt (dagens datum). OBS! Det blir alltså FLERA kvitton i samma fil. Fundera ut och implementera någon slags särskiljare så man kan skilja olika kvitton åt

# Krav för VG

        *Adminverktyg där man ska kunna ändra namn och pris för produkter och dessutom lägga till nya produkter
        *Kvitton ska ha ett LÖPNUMMER och detta måste plussas med ett hela tiden (även om ni stänger ner programmet ska sedan nästa kvitto få senaste kvittonumret + 1)
        *Det ska finnas möjlighet att administrera KAMPANJPRISER. Det som menas är att tex från 2023-03-18 till 2023-03-19 kostar mjölken 10 kr. OBS: En produkt kan ha MÅNGA kampanjer Naturligtvis ska detta pris gälla och visas på kvitton vid dessa tillfällen
        *Lägga till/ta bort kampanjer. 
