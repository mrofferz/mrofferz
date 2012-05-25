<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test2.aspx.cs" Inherits="Test2" %>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="content-type" content="text/html; charset=UTF-8" />
    <title>Google Maps v3 - getBounds is undefined</title>
</head>
<body>

    <script src="http://maps.google.com/maps/api/js?sensor=false" type="text/javascript"></script>

    <script type="text/javascript">
        function InitializeMap(x_coordination, y_coordination, map_zoom) {
            var map = new google.maps.Map(document.getElementById("map"), {
                zoom: map_zoom,
                center: new google.maps.LatLng(x_coordination, y_coordination),
                mapTypeId: google.maps.MapTypeId.ROADMAP,
                mapTypeControl: false,
                navigationControl: false,
                scaleControl: false,
                disableDoubleClickZoom: false,
                scrollwheel: true,
                draggable: false,
                streetViewControl: true,
                draggableCursor: 'move'
            });

            var marker = new google.maps.Marker({ position: new google.maps.LatLng(30.066492, 31.320364), map: map });
        }
    </script>

    <div>
        <input id="Button1" type="button" value="button" onclick="javascript:InitializeMap(31.066492,32.320364,10);" />
    </div>
    <div id="map" style="width: 400px; height: 400px;">
    </div>
</body>
</html>
