namespace FAwesome.ScenarioCore.Systems.WavePlacerSystemUnit.Example
{
    public class ExampleWavePlacerFiller : 
        WavePlacerFillerBase<
            ExampleWavePlacerWorld, 
            ExampleWavePlacerInstance, 
            ExampleWavePlacerData, 
            ExamplePlaceContainer>
    {
        protected override void SetAdditionalValues(ExampleWavePlacerInstance instance, ExamplePlaceContainer place)
        {
            instance.AdditionalValue = place.AdditionalValue;
        }

        protected override void GetAdditionalValues(ExampleWavePlacerInstance instance, ExamplePlaceContainer place)
        {
            place.AdditionalValue = instance.AdditionalValue;
        }
    }
}