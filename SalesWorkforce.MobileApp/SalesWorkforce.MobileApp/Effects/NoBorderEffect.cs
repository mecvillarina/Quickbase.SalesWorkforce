using SalesWorkforce.MobileApp.Common.Constants;
using Xamarin.Forms;

namespace SalesWorkforce.MobileApp.Effects
{
    public class NoBorderEffect : RoutingEffect
    {
        public NoBorderEffect() : base($"{AppConstants.EffectsNamespace}.{nameof(NoBorderEffect)}")
        {

        }
    }
}
