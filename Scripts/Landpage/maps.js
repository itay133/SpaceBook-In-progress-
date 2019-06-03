(function ($) {
    "use strict";
    var markerIcon = {
        anchor: new google.maps.Point(22, 16),
        url: 'images/marker.png',
    }
    function mainMap() {
        function locationData(locationURL, locationCategory, locationImg, locationTitle, locationAddress, locationPhone, locationStarRating, locationRevievsCounter) {
            return ('<div class="map-popup-wrap"><div class="map-popup"><div class="infoBox-close"><i class="fa fa-times"></i></div><div class="map-popup-category">' + locationCategory + '</div><a href="' + locationURL + '" class="listing-img-content fl-wrap"><img src="' + locationImg + '" alt=""></a> <div class="listing-content fl-wrap"><div class="card-popup-raining map-card-rainting" data-staRrating="' + locationStarRating + '"><span class="map-popup-reviews-count">( ' + locationRevievsCounter + ' reviews )</span></div><div class="listing-title fl-wrap"><h4><a href=' + locationURL + '>' + locationTitle + '</a></h4><span class="map-popup-location-info"><i class="fa fa-map-marker"></i>' + locationAddress + '</span><span class="map-popup-location-phone"><i class="fa fa-phone"></i>' + locationPhone + '</span></div></div></div></div>')
        }
        var locations = [
            [locationData('listing-single2.html', 'Hotels', 'images/all/1.jpg', 'Luxary Hotel-Spa', "1327 Intervale Ave, Bronx, NY ", "+38099231212", "5", "27"), 40.72956781, -73.99726866, 1, markerIcon],
            [locationData('listing-single.html', 'Food and Drink', 'images/all/1.jpg', 'Luxary Restaurant', "W 85th St, New York, NY ", "+38099231212", "4", "5"), 40.76221766, -73.96511769, 2, markerIcon],
            [locationData('listing-single.html', 'Gym - Fitness', 'images/all/1.jpg', 'Gym In CityCenter', "40 Journal Square Plaza, Jersey City, NJ", "+38099231212", "4", "5"), 40.88496706, -73.88191222, 3, markerIcon],
            [locationData('listing-single.html', 'Shop - Store', 'images/all/1.jpg', 'Shop In City Mol', "75 Prince St, New York, NY ", "+38099231212", "4", "127"), 40.72228267, -73.99246214, 4, markerIcon],
            [locationData('listing-single.html', 'Food and Drink', 'images/all/1.jpg', 'Luxary Restaurant', "34-42 Montgomery St, New York, NY", "+38099231212", "5", "43"), 40.94982541, -73.84357452, 5, markerIcon],
            [locationData('listing-single.html', 'Gym - Fitness', 'images/all/1.jpg', 'Gym In CityCenter', "70 Bright St, Jersey City, NJ", "+38099231212", "4", "7"), 40.90261483, -74.15737152, 6, markerIcon],
            [locationData('listing-single.html', 'Shop - Store', 'images/all/1.jpg', 'Shop In City Mol', "123 School St. Lynchburg, NY ", "+38099231212", "3", "4"), 40.79145927, -74.08252716, 7, markerIcon],
            [locationData('listing-single2.html', 'Hotels', 'images/all/1.jpg', 'Fancy Hotel', "Mt Carmel Pl, New York, NY", "+38099231212", "5", "3"), 40.58423508, -73.96099091, 8, markerIcon],
            [locationData('listing-single2.html', 'Hotels', 'images/all/1.jpg', 'Luxary Hotel-Spa', "1-30 Hunters Point Ave, Long Island City, NY", "+38099231212", "5", "12"), 40.58110616, -73.97678375, 9, markerIcon],
            [locationData('listing-single3.html', 'Conference and Events', 'images/all/1.jpg', 'Web Design Event ', "726-1728 2nd Ave, New York, NY", "+38099231212", "5", "17"), 40.73112881, -74.07897948, 10, markerIcon],
            [locationData('listing-single3.html', 'Conference and Events', 'images/all/1.jpg', 'Apartment Design Event ', "9443 Fairview Ave, North Bergen, NJ", "+38099231212", "4", "11"), 40.67386831, -74.10438536, 11, markerIcon],
        ];
        var mapZoomAttr = $('#map-main').attr('data-map-zoom');
        var mapScrollAttr = $('#map-main').attr('data-map-scroll');
        if (typeof mapZoomAttr !== typeof undefined && mapZoomAttr !== false) {
            var zoomLevel = parseInt(mapZoomAttr);
        }
        else {
            var zoomLevel = 10;
        }
        if (typeof mapScrollAttr !== typeof undefined && mapScrollAttr !== false) {
            var scrollEnabled = parseInt(mapScrollAttr);
        }
        else {
            var scrollEnabled = false;
        }
        $('.nextmap-nav').on("click", function (e) {
            e.preventDefault();
            map.setZoom(14);
            var index = currentInfobox;
            if (index + 1 < allMarkers.length) {
                google.maps.event.trigger(allMarkers[index + 1], 'click');
            }
            else {
                google.maps.event.trigger(allMarkers[0], 'click');
            }
        });
        $('.prevmap-nav').on("click", function (e) {

            e.preventDefault();
            map.setZoom(14);
            if (typeof (currentInfobox) == "undefined") {
                google.maps.event.trigger(allMarkers[allMarkers.length - 1], 'click');
            }
            else {
                var index = currentInfobox;
                if (index - 1 < 0) {
                    google.maps.event.trigger(allMarkers[allMarkers.length - 1], 'click');
                }
                else {
                    google.maps.event.trigger(allMarkers[index - 1], 'click');
                }
            }
        });
        var map = new google.maps.Map(document.getElementById('map-main'), {
            zoom: zoomLevel,
            scrollwheel: scrollEnabled,
            center: new google.maps.LatLng(40.7, -73.87),
            mapTypeId: google.maps.MapTypeId.ROADMAP,
            zoomControl: false,
            mapTypeControl: false,
            scaleControl: false,
            panControl: false,
            navigationControl: false,
            streetViewControl: false,
            animation: google.maps.Animation.BOUNCE,
            gestureHandling: 'cooperative',
            styles: [{
                    "featureType": "administrative",
                    "elementType": "labels.text.fill",
                    "stylers": [{
                        "color": "#444444"
                    }]
                }
            ]
        });
        var boxText = document.createElement("div");
        boxText.className = 'map-box'
        var currentInfobox;
        var boxOptions = {
            content: boxText,
            disableAutoPan: true,
            alignBottom: true,
            maxWidth: 300,
            pixelOffset: new google.maps.Size(-140, -45),
            zIndex: null,
            boxStyle: {
                width: "260px"
            },
            closeBoxMargin: "0",
            closeBoxURL: "",
            infoBoxClearance: new google.maps.Size(1, 1),
            isHidden: false,
            pane: "floatPane",
            enableEventPropagation: false,
        };
        var markerCluster, marker, i;
        var allMarkers = [];
        var clusterStyles = [{
            url: '',
            height: 40,
            width: 40
        }];
        var zoomControlDiv = document.createElement('div');
        var zoomControl = new ZoomControl(zoomControlDiv, map);
        function ZoomControl(controlDiv, map) {
            zoomControlDiv.index = 1;
            map.controls[google.maps.ControlPosition.RIGHT_CENTER].push(zoomControlDiv);
            controlDiv.style.padding = '5px';
            var controlWrapper = document.createElement('div');
            controlDiv.appendChild(controlWrapper);
            var zoomInButton = document.createElement('div');
            zoomInButton.className = "mapzoom-in";
            controlWrapper.appendChild(zoomInButton);
            var zoomOutButton = document.createElement('div');
            zoomOutButton.className = "mapzoom-out";
            controlWrapper.appendChild(zoomOutButton);
            google.maps.event.addDomListener(zoomInButton, 'click', function () {
                map.setZoom(map.getZoom() + 1);
            });
            google.maps.event.addDomListener(zoomOutButton, 'click', function () {
                map.setZoom(map.getZoom() - 1);
            });
        }
        for (i = 0; i < locations.length; i++) {
            marker = new google.maps.Marker({
                animation: google.maps.Animation.DROP,
                position: new google.maps.LatLng(locations[i][1],
                    locations[i][2]),
                icon: locations[i][4],
                id: i
            });
            allMarkers.push(marker);
            var ib = new InfoBox();
            google.maps.event.addListener(ib, 'domready', function () {
                cardRaining();
            });
            google.maps.event.addListener(marker, 'click', (function (marker, i) {
                return function () {
                    ib.setOptions(boxOptions);
                    boxText.innerHTML = locations[i][0];
                    ib.open(map, marker);
                    currentInfobox = marker.id;
                    var latLng = new google.maps.LatLng(locations[i][1], locations[i][2]);
                    map.panTo(latLng);
                    map.panBy(0, -180);
                    google.maps.event.addListener(ib, 'domready', function () {
                        $('.infoBox-close').click(function (e) {
                            e.preventDefault();
                            ib.close();
                        });
                    });
                }
            })(marker, i));
        }
        var options = {
            imagePath: 'images/',
            styles: clusterStyles,
            minClusterSize: 2
        };
        markerCluster = new MarkerClusterer(map, allMarkers, options);
        google.maps.event.addDomListener(window, "resize", function () {
            var center = map.getCenter();
            google.maps.event.trigger(map, "resize");
            map.setCenter(center);
        });
    }
    var map = document.getElementById('map-main');
    if (typeof (map) != 'undefined' && map != null) {
        google.maps.event.addDomListener(window, 'load', mainMap);
    }
    function singleMap() {
        var myLatLng = {
            lng: $('#singleMap').data('longitude'),
            lat: $('#singleMap').data('latitude'),
        };
        var single_map = new google.maps.Map(document.getElementById('singleMap'), {
            zoom: 14,
            center: myLatLng,
            scrollwheel: false,
            zoomControl: false,
            mapTypeControl: false,
            scaleControl: false,
            panControl: false,
            navigationControl: false,
            streetViewControl: false,
            styles: [{
                "featureType": "landscape",
                "elementType": "all",
                "stylers": [{
                    "color": "#f2f2f2"
                }]
            }]
        });
        var marker = new google.maps.Marker({
            position: myLatLng,
            map: single_map,
            icon: markerIcon,
            title: 'Our Location'
        });
        var zoomControlDiv = document.createElement('div');
        var zoomControl = new ZoomControl(zoomControlDiv, single_map);
        function ZoomControl(controlDiv, single_map) {
            zoomControlDiv.index = 1;
            single_map.controls[google.maps.ControlPosition.RIGHT_CENTER].push(zoomControlDiv);
            controlDiv.style.padding = '5px';
            var controlWrapper = document.createElement('div');
            controlDiv.appendChild(controlWrapper);
            var zoomInButton = document.createElement('div');
            zoomInButton.className = "mapzoom-in";
            controlWrapper.appendChild(zoomInButton);
            var zoomOutButton = document.createElement('div');
            zoomOutButton.className = "mapzoom-out";
            controlWrapper.appendChild(zoomOutButton);
            google.maps.event.addDomListener(zoomInButton, 'click', function () {
                single_map.setZoom(single_map.getZoom() + 1);
            });
            google.maps.event.addDomListener(zoomOutButton, 'click', function () {
                single_map.setZoom(single_map.getZoom() - 1);
            });
        }
    }
    var single_map = document.getElementById('singleMap');
    if (typeof (single_map) != 'undefined' && single_map != null) {
        google.maps.event.addDomListener(window, 'load', singleMap);
    }
})(this.jQuery);