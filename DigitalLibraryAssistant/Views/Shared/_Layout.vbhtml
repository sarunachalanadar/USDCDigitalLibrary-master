﻿<!DOCTYPE html>
<html ng-app="app">
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">
    <title>Digital Library Assistant</title>

    <!--Scripts-->
    <script src="/Scripts/AngularJS/angular.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/angularjs/1.6.4/angular-route.js"></script>
    <script src="/Scripts/App/App.js"></script>

    <!--Styles-->
    <link rel="stylesheet" type="text/css" href="~/Content/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="~/Content/landing-page.css" />
    <link href="https://fonts.googleapis.com/css?family=Lato:300,400,700,300italic,400italic,700italic" rel="stylesheet" type="text/css">

    <!--Fonts-->
    <link href="~/fonts/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css">

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->

</head>
<body ng-controller="homeController">
    @RenderBody()
</body>
</html>
