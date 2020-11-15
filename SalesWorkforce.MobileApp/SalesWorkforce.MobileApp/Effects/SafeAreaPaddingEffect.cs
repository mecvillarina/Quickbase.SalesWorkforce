using SalesWorkforce.MobileApp.Common.Constants;
using Xamarin.Forms;

namespace SalesWorkforce.MobileApp.Effects
{
    public class SafeAreaPaddingEffect : RoutingEffect
    {
        public SafeAreaPaddingEffect() : base($"{AppConstants.EffectsNamespace}.{nameof(SafeAreaPaddingEffect)}")
        {

        }
    }
}
