<!DOCTYPE html>
<html ng-app="app">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Digital Library Assistant</title>

    <!--Scripts-->
    <script src="/Scripts/AngularJS/angular.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/angularjs/1.6.4/angular-route.js"></script>
    <script src="/Scripts/App/App.js"></script>
    <!--Styles-->
    <!--Controllers-->
    <!--Services-->
</head>
<body ng-controller="mainController">
    <nav>
        <h1>Header Coming Soon</h1>
    </nav>
    <div class="container body-content">
        @RenderBody()
    </div>
    <footer>
        <p>&copy; @DateTime.Now.Year - My ASP.NET Application</p>
    </footer>
</body>
</html>
