var app = angular.module("app", ["ngRoute"]);
app.config(function ($routeProvider, $locationProvider) {
    var templatesDir = "/Templates/";
    //$locationProvider.html5Mode({
    //    enabled: true,
    //    requireBase: false
    //});
    $routeProvider
    .when("/", {
        templateUrl: templatesDir + "Index.html",
        controller: "homeController"
    })
    .when("/home", {
        templateUrl: templatesDir + "Index.html",
        controller: "homeController"
    })
    .when("/test", {
        templateUrl: templatesDir + "Test.html",
        controller: "testController"
        })
    .when("/dashboard", {
        templateUrl: templatesDir + "Dashboard.html",
        controller: "homeController"
    })
    .otherwise({
        redirectTo: "/"
    });
});

app.controller('homeController', function ($scope, apiService,$location) {
    $scope.message = "This message is from the Home controller";
    $scope.isAuthenticated = false;

    $scope.buttonClick = function () {
        apiService.GetRequest("test", "2");
    };
    
    $scope.login = function () {
        $scope.isAuthenticated = true;
        $location.path('/dashboard');
    };
    $scope.isActive = function (viewLocation) {
        return viewLocation === $location.path();
    };

    $scope.$on('$locationChangeSuccess', function (/* EDIT: remove params for jshint */) {
        if ($scope.isAuthenticated == true) {
            $location.path('/dashboard');
        }
    });

});

app.controller('testController', function ($scope) {
    $scope.message = "This message is from the Test controller";
});

app.factory("apiService", function ($http) {
    var url = "/api/";

    return {
        GetRequest: function (path, parameters) {
            $http({
                method: 'GET',
                url: url + path + "/" + parameters
            })
            .then(function successCallback(response) {
                alert("Success");
            },
            function errorCallback(response) {
                alert("Failure");
            });
        }
    }
});

app.controller('dashboardController', function ($scope) {
    $scope.message = "This message is from the Test controller";
    $scope.isAuthenticated = false;
    $scope.FirstName = "John";
    $scope.LastName = "Smith";
});