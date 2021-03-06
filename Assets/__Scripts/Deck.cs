using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour{
    [Header("Set in Inspector")]     
    public Sprite                     suitClub;
    public Sprite                     suitDiamond;
    public Sprite                     suitHeart;
    public Sprite                     suitSpade;

    public Sprite[]                   faceSprites;
    public Sprite[]                   rankSprites;

    public Sprite                     cardBack;
    public Sprite                     cardBackGold;
    public Sprite                     cardFront;
    public Sprite                     cardFrontGold;
    
    [Header("Set Dynamically")]
    public PT_XMLReader               xmlr;   
    public List<string>               cardNames;     
    public List<Card>                 cards;     
    public List<Decorator>            decorators;     
    public List<CardDefinition>       cardDefs;     
    public Transform                  deckAnchor;     
    public Dictionary<string,Sprite>  dictSuits;

    public void InitDeck(string deckXMLText){
        xmlr = new PT_XMLReader();
        xmlr.Parse(deckXMLText);

        string s = "xml[0] decorator[0]";
        s += "type="+xmlr.xml["xml"][0]["decorator"][0].att("type");         
        s += " x="+xmlr.xml["xml"][0]["decorator"][0].att("x");         
        s += " y="+xmlr.xml["xml"][0]["decorator"][0].att("y");         
        s += " scale="+xmlr.xml["xml"][0]["decorator"][0].att("scale");

        decorators = new List<Decorator>();
        PT_XMLHashList xDecos = xmlr.xml["xml"][0]["decorator"];
        Decorator deco;

        for(int i=0; i<xDecos.Count; i++){
            deco = new Decorator();
            deco.type = xDecos[i].att("type");
            deco.flip = (xDecos[i].att("flip") == "1");
            deco.scale = float.Parse(xDecos[i].att("scale"));
            deco.loc.x = float.Parse(xDecos[i].att("x"));
            deco.loc.y = float.Parse(xDecos[i].att("z"));
            decorators.Add(deco);
        }

        cardDefs = new List<CardDefinition>();
        PT_XMLHashList xCardDefs = xmlr.xml["xml"][0]["card"];
        for(int i=0; i<xCardDefs.Count; i++){
            CardDefinition cDef = new CardDefinition();
            cDef.rank = int.Parse(xCardDefs[i].att("rank"));
            PT_XMLHashList xPips = xCardDefs[i]["pip"];
            
            if(xPips != null){
                for(int j=0; j<xPips.Count; j++){
                    deco = new Decorator();
                    deco.type = "pip";
                    deco.flip = (xPips[j].att("flip") == "1");
                    deco.loc.x = float.Parse(xPips[j].att("x"));
                    deco.loc.y = float.Parse( xPips[j].att ("y") );
                    deco.loc.z = float.Parse( xPips[j].att ("z") );
                    if ( xPips[j].HasAtt("scale") ) {                         
                        deco.scale = float.Parse( xPips[j].att ("scale") );                     
                    }
                    cDef.pips.Add(deco);
                }
            }

            if(xCardDefs[i].HasAtt("face")){
                cDef.face = xCardDefs[i].att("face");
            }
            cardDefs.Add(cDef);
        }
    }
}
