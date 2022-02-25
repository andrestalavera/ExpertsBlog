using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace ExpertsBlog.Mobile.ViewModels
{
    public class GeolocationViewModel : ViewModelBase
    {
        private Location location;
        public Location Location
        {
            get => location;
            set => SetProperty(ref location, value);
        }

        public async void CheckAndRequestPermissions()
        {
            try
            {
                var location = await Geolocation.GetLocationAsync();

                if (location != null)
                {
                    Location = location;
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
        }
    }
}