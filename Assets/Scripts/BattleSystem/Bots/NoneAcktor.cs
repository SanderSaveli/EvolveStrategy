using TileSystem;

public class NoneAcktor : GameAcktor
{
    public NoneAcktor(): base(AcktorList.None) 
    {    }

    public override void OfferToBuildNest(Region region)
    {   }
}

