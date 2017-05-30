var app = angular.module("app", ["ngRoute"]);
app.config(function ($routeProvider, $locationProvider) {
    var templatesDir = "/Pages/";
    $locationProvider.html5Mode({
        enabled: true,
        requireBase: false
    });
    $routeProvider
    .when("/", {
        templateUrl: templatesDir + "Home.html",
        controller: "mainController"
    })
    .when("/home", {
        templateUrl: templatesDir + "Home.html",
        controller: "mainController"
    })
    .when("/test", {
        templateUrl: templatesDir + "Test.html",
        controller: "mainController"
    })
    .otherwise({
        redirectTo: "/"
    });
});

app.controller('mainController', function ($scope) {
    $scope.message = "This message is from the controller";
});