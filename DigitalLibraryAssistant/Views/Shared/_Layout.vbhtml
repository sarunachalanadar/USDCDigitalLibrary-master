<!DOCTYPE html>
<html ng-app="app">
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Digital Library Assistant</title>

    <meta name="description" content="">
    <meta name="author" content="">

    <!-- Bootstrap Core CSS -->
    <link href="../Content/bootstrap.min.css" rel="stylesheet">

    <!-- Custom CSS -->
    <link href="../Content/landing-page.css" rel="stylesheet">

    <!-- Custom Fonts -->
    <link href="../fonts/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css">
    <link href="https://fonts.googleapis.com/css?family=Lato:300,400,700,300italic,400italic,700italic" rel="stylesheet" type="text/css">


    <!--Scripts-->
    <script src="/Scripts/AngularJS/angular.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/angularjs/1.6.4/angular-route.js"></script>
    <script src="/Scripts/App/App.js"></script>

    <!--Styles-->
</head>
<body ng-controller="homeController">

    <div id="wrapper">
        <header ng-include="'Templates/Header.html'"></header>        
        @RenderBody()
        <footer class="navbar-fixed-bottom" ng-include="'Templates/Footer.html'" style="padding:20px 0;"></footer>
    </div>
        <!-- jQuery -->
        <script src="../Scripts/jquery-1.10.2.js"></script>

        <!-- Bootstrap Core JavaScript -->
        <script src="../Scripts/bootstrap.min.js"></script>
</body>
</html>
