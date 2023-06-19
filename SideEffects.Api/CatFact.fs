namespace SideEffects.Api

type CatFact = {
    Text: string
}

module CatFact =
    
    type CatFacts = CatFact list
    
    let fromJson = JsonSerializer.deserialize<CatFacts>
    