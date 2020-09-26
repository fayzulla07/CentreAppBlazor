
var map = undefined;
var dotNetHelper;
var marker;
function InitializeMyGeo(DotNetHelper) {
    dotNetHelper = DotNetHelper;
      // create the map
    var latlng222 = L.latLng(42.3166130218313, 69.6888198831584);
    map = L.map('map').setView(latlng222, 16);

      // get some tiles
      L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
          attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
      }).addTo(map);

  }
function SetLatLong(name, lat, long, drag) {
    var latlng222 = L.latLng(lat, long);
     marker = L.marker(latlng222, { draggable: drag });

 
        marker.on('dragend', function (event) {
            var marker = event.target;
            var position = marker.getLatLng();
            let data = { latitude: position.lat, longitude: position.lng }
            let mygeoloc = JSON.stringify(data);
            dotNetHelper.invokeMethodAsync('MarkerChangedJs', mygeoloc);
        });
  
   
    marker.title = name;
    marker.alt = name;
    marker.bindPopup(name);
    marker.addTo(map);
    FlyToLocation(lat, long);
  
}
function FlyToLocation(lat, long) {
    var latlng222 = L.latLng(lat, long);
    map.flyTo(latlng222, 17);
}
function MarkerRemove() {
    map.removeLayer(marker);
}
function getLocation(DotNetHelper) {
    dotNetHelper = DotNetHelper;
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (location)
        {
            let data = { latitude: location.coords.latitude, longitude: location.coords.longitude }
            let mygeoloc = JSON.stringify(data);
            dotNetHelper.invokeMethodAsync('GetGeoJs', mygeoloc);
        });
    } else {
        console.log('location is not detected');
    }
}

// Sidebar
function RunSidebar(dotnetHelper) {
    var appmenu = document.querySelectorAll(".app-menu__item");

    for (var i = 0; i < appmenu.length; i++) {
        if (!appmenu[i].classList.contains('fayzulla')) {
            appmenu[i].addEventListener("click", function () {
                var w = window.innerWidth
                    || document.documentElement.clientWidth
                    || document.body.clientWidth;
                if (w < 768) {
                    dotnetHelper.invokeMethodAsync('NavClick');
                }

            });
        }
           
    }
    
} 

function saveasFile(filename, byteBase64) {
    var link = document.createElement('a');
    link.download = filename;
    link.href = 'data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64,' + byteBase64;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}
  