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

        public void CheckAndRequestPermissions()
        {
            Task.Run(async () =>
            {
                Console.WriteLine("Toto");
                Permissions.LocationWhenInUse locationWhenInUse = new Permissions.LocationWhenInUse();
                //var requiredDeclarations = locationWhenInUse.RequiredDeclarations;
                //var requiredInfoPlistKeys = locationWhenInUse.RequiredInfoPlistKeys;
                //var requiredPermissions = locationWhenInUse.RequiredPermissions;
                locationWhenInUse.EnsureDeclared();


                //var isCapacityDeclared = Permissions.IsCapacityDeclared(Permissions.LocationWhenInUse);
                var shouldShowRationale = Permissions.ShouldShowRationale<Permissions.LocationWhenInUse>();
                var locationCheckStatus = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
                if (locationCheckStatus != PermissionStatus.Granted)
                {
                    var locationRequestStatuts = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                    var s = locationWhenInUse.RequestAsync();
                }

                // Geolocation

                try
                {
                    var location = await Geolocation.GetLocationAsync();

                    if (location != null)
                    {
                        // Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
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
            });
        }
    }
}